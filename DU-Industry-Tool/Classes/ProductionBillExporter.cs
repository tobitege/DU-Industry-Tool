using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DU_Industry_Tool.Skills;
using Newtonsoft.Json;

namespace DU_Industry_Tool
{
    /// <summary>
    /// Exports current calculator results into myDUDreamTool production-bill contract v1.0.
    /// </summary>
    public static class ProductionBillExporter
    {
        private const string ContractVersion = "1.0";

        public static void Export(CalculatorClass calc, string outputPath, string talentProfileName = "DU-Industry-Tool Active Talents")
        {
            if (calc == null) throw new ArgumentNullException(nameof(calc));
            if (string.IsNullOrWhiteSpace(outputPath)) throw new ArgumentNullException(nameof(outputPath));

            var demands = BuildDemands(calc);
            if (demands.Count == 0)
            {
                throw new InvalidOperationException("No demand entries could be generated from the current calculation.");
            }

            var target = BuildTarget(calc, demands);
            var payload = new ProductionBillContract
            {
                Version = ContractVersion,
                CreatedAtUtc = DateTime.UtcNow,
                Target = target,
                TalentsProfile = BuildTalentProfile(talentProfileName),
                Demands = demands,
                Schematics = BuildSchematics(calc)
            };

            var json = JsonConvert.SerializeObject(payload, Formatting.Indented,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            File.WriteAllText(outputPath, json);
        }

        private static ProductionTargetContract BuildTarget(
            CalculatorClass calc,
            IReadOnlyList<ProductionDemandContract> demands)
        {
            var primaryProduct = calc.Recipe?.Products?.FirstOrDefault(x => !x.IsByproduct && x.Quantity > 0);
            if (primaryProduct != null)
            {
                var productRecipe = FindRecipeByName(primaryProduct.Name);
                return new ProductionTargetContract
                {
                    ItemNqId = productRecipe?.NqId ?? 0,
                    ItemName = primaryProduct.Name,
                    QuantityPerDay = Convert.ToDouble(primaryProduct.Quantity)
                };
            }

            var firstDemand = demands[0];
            return new ProductionTargetContract
            {
                ItemNqId = firstDemand.ItemNqId,
                ItemName = firstDemand.ItemName,
                QuantityPerDay = firstDemand.RatePerDay
            };
        }

        private static List<ProductionDemandContract> BuildDemands(CalculatorClass calc)
        {
            var result = new Dictionary<string, ProductionDemandContract>(StringComparer.InvariantCultureIgnoreCase);

            AddProductDemands(calc, result);
            AddSectionDemands(calc, SummationType.ORES, "Ores", result);
            AddSectionDemands(calc, SummationType.PURES, "Pures", result);
            AddSectionDemands(calc, SummationType.PRODUCTS, "Products", result);
            AddSectionDemands(calc, SummationType.PARTS, "Parts", result);

            return result.Values
                .OrderBy(x => x.Section ?? string.Empty, StringComparer.InvariantCultureIgnoreCase)
                .ThenBy(x => x.Tier ?? int.MaxValue)
                .ThenBy(x => x.ItemName, StringComparer.InvariantCultureIgnoreCase)
                .ToList();
        }

        private static void AddProductDemands(CalculatorClass calc, IDictionary<string, ProductionDemandContract> demands)
        {
            if (calc.Recipe?.Products?.Any() != true) return;

            foreach (var product in calc.Recipe.Products.Where(x => !x.IsByproduct && x.Quantity > 0))
            {
                var recipe = FindRecipeByName(product.Name);
                var tier = recipe?.Level > 0 ? (int?)recipe.Level : product.Level;
                AddOrMergeDemand(demands, new ProductionDemandContract
                {
                    ItemNqId = recipe?.NqId ?? product.Id,
                    ItemName = product.Name,
                    RatePerDay = Convert.ToDouble(product.Quantity),
                    RatePerSecond = Convert.ToDouble(product.Quantity / 86400m),
                    Section = DUData.ProductionListTitle,
                    Tier = tier,
                    Origin = "industry-tool"
                });
            }
        }

        private static void AddSectionDemands(
            CalculatorClass calc,
            SummationType sumType,
            string section,
            IDictionary<string, ProductionDemandContract> demands)
        {
            var entries = calc.Get(sumType);
            if (entries?.Any() != true) return;

            foreach (var entry in entries)
            {
                var realName = GetRealName(entry.Key, out var keyTier);
                if (string.IsNullOrWhiteSpace(realName) || entry.Value.Qty <= 0) continue;

                var recipe = FindRecipeByName(realName);
                var tier = keyTier > 0 ? (int?)keyTier : (recipe?.Level > 0 ? (int?)recipe.Level : null);
                var qtyPerDay = entry.Value.Qty;

                AddOrMergeDemand(demands, new ProductionDemandContract
                {
                    ItemNqId = recipe?.NqId ?? 0,
                    ItemName = realName,
                    RatePerDay = Convert.ToDouble(qtyPerDay),
                    RatePerSecond = Convert.ToDouble(qtyPerDay / 86400m),
                    Section = section,
                    Tier = tier,
                    Origin = "industry-tool"
                });
            }
        }

        private static void AddOrMergeDemand(
            IDictionary<string, ProductionDemandContract> demands,
            ProductionDemandContract candidate)
        {
            var key = candidate.ItemNqId > 0
                ? "nq:" + candidate.ItemNqId
                : "name:" + candidate.ItemName;

            if (!demands.TryGetValue(key, out var existing))
            {
                demands[key] = candidate;
                return;
            }

            existing.RatePerDay += candidate.RatePerDay;
            existing.RatePerSecond = existing.RatePerDay / 86400d;
            if (string.IsNullOrEmpty(existing.Section) && !string.IsNullOrEmpty(candidate.Section))
            {
                existing.Section = candidate.Section;
            }
            if (!existing.Tier.HasValue && candidate.Tier.HasValue)
            {
                existing.Tier = candidate.Tier;
            }
        }

        private static List<SchematicDemandContract> BuildSchematics(CalculatorClass calc)
        {
            var result = new List<SchematicDemandContract>();
            if (calc.SumSchemClass?.Any() != true) return result;

            foreach (var schema in calc.SumSchemClass)
            {
                var requiredQty = Math.Round(schema.Value.Item1, 6);
                if (requiredQty <= 0) continue;

                decimal copies;
                if (!Calculator.CalcSchematic(schema.Key, requiredQty, out _, out _, out copies))
                {
                    copies = 0m;
                }

                result.Add(new SchematicDemandContract
                {
                    SchemaKey = schema.Key,
                    RequiredQty = Convert.ToDouble(requiredQty),
                    CopyJobs = copies > 0 ? (double?)Convert.ToDouble(copies) : null
                });
            }
            return result;
        }

        private static TalentProfileContract BuildTalentProfile(string profileName)
        {
            var values = Talents.Values
                .Where(t => t != null && !string.IsNullOrWhiteSpace(t.Name) && t.Value > 0)
                .OrderBy(t => t.Name, StringComparer.InvariantCultureIgnoreCase)
                .Select(t => new TalentValueContract
                {
                    Name = t.Name,
                    Level = t.Value
                })
                .ToList();

            return new TalentProfileContract
            {
                Name = string.IsNullOrWhiteSpace(profileName) ? "DU-Industry-Tool Talents" : profileName,
                Values = values
            };
        }

        private static SchematicRecipe FindRecipeByName(string itemName)
        {
            if (string.IsNullOrWhiteSpace(itemName)) return null;
            return DUData.Recipes?.Values.FirstOrDefault(x =>
                x != null &&
                x.Name != null &&
                x.Name.Equals(itemName, StringComparison.InvariantCultureIgnoreCase));
        }

        private static string GetRealName(string key, out int tier)
        {
            tier = 0;
            if (string.IsNullOrWhiteSpace(key)) return string.Empty;

            var normalized = key.Trim();
            if (normalized.Length < 4 || normalized[0] != 'T' || normalized[2] != ' ' || !char.IsDigit(normalized[1]))
            {
                return normalized;
            }

            tier = normalized[1] - '0';
            return normalized.Substring(3).Trim();
        }
    }

    internal sealed class ProductionBillContract
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("createdAtUtc")]
        public DateTime CreatedAtUtc { get; set; }

        [JsonProperty("target")]
        public ProductionTargetContract Target { get; set; }

        [JsonProperty("talentsProfile")]
        public TalentProfileContract TalentsProfile { get; set; }

        [JsonProperty("demands")]
        public IReadOnlyList<ProductionDemandContract> Demands { get; set; }

        [JsonProperty("schematics")]
        public IReadOnlyList<SchematicDemandContract> Schematics { get; set; }
    }

    internal sealed class ProductionTargetContract
    {
        [JsonProperty("itemNqId")]
        public ulong ItemNqId { get; set; }

        [JsonProperty("itemName")]
        public string ItemName { get; set; }

        [JsonProperty("quantityPerDay")]
        public double QuantityPerDay { get; set; }
    }

    internal sealed class ProductionDemandContract
    {
        [JsonProperty("itemNqId")]
        public ulong ItemNqId { get; set; }

        [JsonProperty("itemName")]
        public string ItemName { get; set; }

        [JsonProperty("ratePerDay")]
        public double RatePerDay { get; set; }

        [JsonProperty("ratePerSecond")]
        public double RatePerSecond { get; set; }

        [JsonProperty("section")]
        public string Section { get; set; }

        [JsonProperty("tier")]
        public int? Tier { get; set; }

        [JsonProperty("origin")]
        public string Origin { get; set; }
    }

    internal sealed class SchematicDemandContract
    {
        [JsonProperty("schemaKey")]
        public string SchemaKey { get; set; }

        [JsonProperty("requiredQty")]
        public double RequiredQty { get; set; }

        [JsonProperty("copyJobs")]
        public double? CopyJobs { get; set; }
    }

    internal sealed class TalentProfileContract
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("values")]
        public IReadOnlyList<TalentValueContract> Values { get; set; } = Array.Empty<TalentValueContract>();
    }

    internal sealed class TalentValueContract
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }
    }
}
