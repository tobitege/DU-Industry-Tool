using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DU_Helpers;

namespace DU_Industry_Tool
{
    public struct CalcEntry
    {
        public decimal Qty { get; set; }
        public decimal Amt { get; set; }
        public decimal? QtySchemata { get; set; }
        public decimal? AmtSchemata { get; set; }
        public string SchematicType { get; set; }
    }

    public static class CalcOptions
    {
        public static decimal MarginPct = 0m;
        public static bool ApplyMargin = false;
        public static bool ApplyRnd = false;
        public static int RndDigits = 0;
    }

    public static class Calculator
    {
        private static string _currentRecipe;

        public static SortedDictionary<Guid, CalculatorClass> All { get; private set; } = new SortedDictionary<Guid, CalculatorClass>();

        // Used products quantity for cost calculations
        public static decimal ProductQuantity { get; set; }

        // Keep list of applied talents from last calculation, so these
        // can be used by e.g. the skills form for highlighting
        public static List<string> ApplicableTalents { get; private set; }

        public static void ResetRecipeName()
        {
            _currentRecipe = "";
        }

        public static void Initialize()
        {
            ProductQuantity = 1;
            if (ApplicableTalents == null)
                ApplicableTalents = new List<string>();
            else
                ApplicableTalents.Clear();
            if (All == null)
                All = new SortedDictionary<Guid, CalculatorClass>();
            else
                All.Clear();
        }

        public static CalculatorClass Get(string key, Guid parentId, bool useAllStorage = true)
        {
            if (useAllStorage)
            {
                var entry = All.FirstOrDefault(x => x.Value.Parent == parentId && x.Value.Key == key);
                if (entry.Value != null)
                {
                    return entry.Value;
                }
            }

            if (!CreateByKey(key, out var calc)) return null;
            calc.SetParent(parentId);
            if (useAllStorage)
            {
                All.Add(calc.Id, calc);
            }
            return calc;
        }

        private static bool CreateCloneByName(string recipeName, out CalculatorClass result)
        {
            result = null;
            if (string.IsNullOrEmpty(recipeName) || !DUData.GetRecipeCloneByName(recipeName, out var rec))
                return false;
            result = new CalculatorClass(rec);
            return true;
        }

        public static bool CreateByKey(string key, out CalculatorClass result)
        {
            result = null;
            if (string.IsNullOrEmpty(key) || !DUData.GetRecipeCloneByKey(key, out var rec))
                return false;
            result = new CalculatorClass(rec);
            return true;
        }

        //public static bool GetFromStoreById(Guid id, out CalculatorClass calc)
        //{
        //    calc = All.FirstOrDefault(x => x.Key == id).Value;
        //    return calc != null;
        //}

        //public static bool GetFromStoreByName(Guid parentId, string name, out IEnumerable<CalculatorClass> calc)
        //{
        //    calc = All.Where(x => x.Key == parentId && x.Value?.Name == name).Select(x => x.Value);
        //    return calc?.Any() == true;
        //}

        //public static bool GetFromStoreWithSums(Guid id, out CalculatorClass calc)
        //{
        //    calc = All.FirstOrDefault(x => x.Key == id && x.Value?.Sums?.Any() == true).Value;
        //    return calc != null;
        //}

        public static bool GetFromStoreWithNodes(Guid id, out CalculatorClass calc)
        {
            calc = All.FirstOrDefault(x => x.Key == id && x.Value?.Nodes?.Any() == true).Value;
            return calc != null;
        }

        public static bool GetFromStoreWithProducts(Guid id, out CalculatorClass calc)
        {
            calc = All.FirstOrDefault(x => x.Key == id && x.Value?.Recipe?.Products?.Any() == true).Value;
            return calc != null;
        }

        public static bool GetFromStoreWithSchemas(Guid id, out CalculatorClass calc)
        {
            calc = All.FirstOrDefault(x => x.Key == id && x.Value?.SumSchemClass?.Any() == true).Value;
            return calc != null;
        }

        /// <summary>
        /// Collect() is the final summation run on either the single calculation item
        /// or a production list (and its elements)
        /// </summary>
        /// <param name="calc"></param>
        private static void Collect(CalculatorClass calc)
        {
            // Summarize all categories over all ingredients
            foreach (var entry in All)
            {
                if (entry.Value.Key != calc.Key)
                {
                    for (var sumType = 0; sumType < (int)SummationType.INGREDIENTS; sumType++)
                    {
                        var sum = entry.Value.Get((SummationType)sumType);
                        if (sum?.Keys.Any() != true) continue;
                        foreach (var s in sum)
                        {
                            calc.Add((SummationType)sumType, s.Key, s.Value);
                        }
                    }
                }
                // Build leafs for current node
                entry.Value.Nodes = new SortedDictionary<Guid, CalculatorClass>();
                foreach (var node in All.Where(x => x.Value.Parent == entry.Key).OrderBy(y => y.Value.Name))
                {
                    // Fill recipe if missing
                    if (!node.Value.RecipeExists)
                    {
                        node.Value.Recipe = DUData.Recipes[node.Value.Key];
                    }
                    entry.Value.Nodes.Add(node.Key, node.Value);
                }
            }

            calc.Recipe = DUData.Recipes[calc.Key].Clone();

            var prodMode = DUData.ProductionListMode && calc.Key == DUData.CompoundName &&
                           calc.RecipeExists && calc.Recipe?.Products?.Any() == true;
            if (prodMode)
            {
                foreach (var prod in calc.Recipe.Products)
                {
                    if (prod.IsByproduct || !CreateByKey(prod.Type, out var pCalc))
                        continue;
                    if (pCalc.IsPlasma || string.IsNullOrEmpty(pCalc.SchematicType))
                        continue;
                    pCalc.GetTalents();
                    // TODO determine correct schematics needed (pures, products)!
                    if (pCalc.IsBatchmode)
                    {
                        if (pCalc.BatchOutput != null)
                        {
                            if (pCalc.CalcSchematicFromQty(pCalc.SchematicType, prod.Quantity, (decimal)pCalc.BatchOutput,
                                    out var cnt, out var minCost1, out var _, out var _))
                            {
                                calc.AddSchema(pCalc.SchematicType, cnt, minCost1);
                                calc.AddSchematicCost(minCost1);
                            }
                        }
                    }
                    else
                    if (CalcSchematic(pCalc.SchematicType, prod.Quantity, out var minCost2, out _, out _))
                    {
                        calc.AddSchema(pCalc.SchematicType, prod.Quantity, minCost2);
                        calc.AddSchematicCost(minCost2);
                    }
                }
            }
            else
            {
                calc.ResetSchematicCost();
            }
            // calculate T2+ ore schematic costs
            CollectSchematics(calc, SummationType.PURES);
            CollectSchematics(calc, SummationType.PRODUCTS);

            calc.OreCost = calc.TotalOreCost();
            calc.Collected = true;

            var marginPct = CalcOptions.MarginPct;
            var applyMargin = CalcOptions.ApplyMargin && marginPct > 0.00m;
            var applyRnd = CalcOptions.ApplyRnd;
            var rndDigi = CalcOptions.RndDigits;

            if (prodMode)
            {
                // Recalculate retail price and margin, reducing potential rounding errors
                for (int i = 0; i < calc.Recipe.Products.Count; i++)
                {
                    var prodItem = calc.Recipe.Products[i];
                    CalcRetail(prodItem, applyMargin, marginPct, applyRnd, rndDigi);
                    calc.Recipe.Products[i] = prodItem;
                }

                calc.IsProdItem = true;
                calc.OreCost = calc.Recipe.Products.Sum(x => Math.Round(x.Cost, 2, MidpointRounding.AwayFromZero));
                calc.SchematicsCost = calc.Recipe.Products.Sum(x => Math.Round(x.SchemaAmt, 2, MidpointRounding.AwayFromZero));

                calc.Margin = calc.Recipe.Products.Sum(x => Math.Round(x.Margin, 2, MidpointRounding.AwayFromZero));
                calc.Retail = calc.OreCost + calc.SchematicsCost + calc.Margin;
                return;
            }

            // Process main item's schematic (if exists and not in prod list mode)
            if (!string.IsNullOrEmpty(calc.SchematicType))
            {
                var batches = ProductQuantity;
                if (calc.IsBatchmode && calc.BatchOutput > 0)
                {
                    batches = Math.Round(ProductQuantity / (decimal)calc.BatchOutput, 2);
                    if (DUData.FullSchematicQuantities)
                    {
                        batches = Math.Ceiling(batches);
                    }
                }
                if (CalcSchematic(calc.SchematicType, batches, out var minCost, out _, out _))
                {
                    calc.AddSchema(calc.SchematicType, batches, minCost);
                    calc.AddSchematicCost(minCost);
                }
            }

            // Recalculate retail sum and margin, fixing potential rounding errors
            var item = new ProductDetail
            {
                Cost = calc.OreCost,
                SchemaAmt = calc.SchematicsCost,
                Quantity = calc.Quantity,
                Margin = 0m,
                Retail = 0m
            };
            CalcRetail(item, applyMargin, marginPct, applyRnd, rndDigi);
            calc.Margin = item.Margin;
            calc.Retail = item.Retail;
        }

        /// <summary>
        /// minCost  = cost for exactly "qtySchematic" amount of schematics
        /// copyCost = cost for all required copy processes (each containing x schematics per DU)
        ///            to cover at least the given number of "qtySchematic" schematics
        /// </summary>
        public static bool CalcSchematic(string schematicId, decimal qtySchematic,
                                         out decimal minCost, out decimal copyCost,
                                         out decimal qtyCopies)
        {
            minCost = 0M;
            copyCost = 0M;
            qtyCopies = 0;
            if (string.IsNullOrEmpty(schematicId)) return false;
            var schemata = DUData.Schematics.FirstOrDefault(x => x.Key.Equals(schematicId)).Value;
            if (schemata?.Key == null)
            {
                return false;
            }

            minCost = Math.Round(schemata.Cost * qtySchematic, 2); // cost is a breakdown to x1 schematic
            // number of copy jobs that need to be started to cover all needed schematics,
            // (which depends on the batch size of a copy, e.g. 10 per copy process):
            qtyCopies = qtySchematic / Math.Max(1, Math.Round((decimal)schemata.BatchSize, 2));
            
            if (DUData.FullSchematicQuantities)
            {
                qtyCopies = Math.Ceiling(qtyCopies);
            }
            
            // copyCost is the single schematic cost multiplied by batch size and the number of copies:
            copyCost = Math.Round(schemata.Cost * schemata.BatchSize * qtyCopies, 2);
            return true;
        }

        private static void CollectSchematics(CalculatorClass calc, SummationType sumType)
        {
            if (!calc.Sums.ContainsKey(sumType)) return;
            foreach (var entry in calc.Sums[sumType].ToList()) // List important!
            {
                var val = entry.Value;
                if (entry.Key.StartsWith(DUData.PlasmaStart)) continue;

                var schemType = sumType == SummationType.PURES ? "U" : "P";
                val.SchematicType = entry.Key.Substring(0,2) + schemType; // e.g. T2U = Pure
                if (val.SchematicType == "T1U") continue;

                var schem = DUData.Schematics.FirstOrDefault(x => x.Key == val.SchematicType);
                if (schem.Key == null) continue;

                var key = entry.Key.Substring(3);
                if (!CreateCloneByName(key, out var tmp))
                {
                    continue;
                }
                tmp.Quantity = val.Qty;
                tmp.GetTalents();
                if (tmp.BatchOutput == null) continue;// happens (on purpose)
                tmp.Quantity = Math.Round(tmp.Quantity / (decimal)tmp.BatchOutput, 2);
                if (DUData.FullSchematicQuantities)
                {
                    tmp.Quantity = (int)Math.Ceiling(tmp.Quantity);
                }
                val.QtySchemata = tmp.Quantity;

                if (!CalcSchematic(val.SchematicType, tmp.Quantity,
                        out var minCost, out _, out var copies))
                {
                    continue;
                }
                val.AmtSchemata = minCost;
                calc.AddSchema(val.SchematicType, val.QtySchemata ?? 0, minCost);
                calc.AddSchematicCost(minCost);
                calc.Sums[sumType][entry.Key] = val;
            }
        }

        /// <summary>
        /// This method calculates the cost for all ingredients for a given item, taking into account available talents
        /// (not all efficiency talents may exist in the core configuration yet!).
        /// </summary>
        /// <remarks>
        /// <para>
        /// Key is most important to identify the item. Amount is the quantity of the item.
        /// Level can be ignored as it is used for some debug output only.
        /// A parent should be provided so the global calculator store connects parent and children items.
        /// </para>
        /// <para>
        /// It only processes ingredients and does not take into account any byproducts.
        /// Special materials like Catalyst, Pure Oxygen and Pure Hydrogen are not applied with cost.
        /// </para>
        /// </remarks>
        public static decimal CalculateRecipe(string key, decimal amount = 0, string level = "",
            int depth = 0, Guid? parent = null, bool silent = true)
        {
            // Hints:
            // - cost is *only* being being accumulated for ores, other types only accumulate by quantity!
            // - Catalysts are NOT calculated due to their positive return ratio, i.e.
            //   they usually return in a higher amount than they're used for (with talents)
            // - Plasma as ingredient is assumed to be 1 L for all recipes.
            if (!DUData.GetRecipeCloneByKey(key, out var recipe))
            {
                var err = $"*** Recipe not found: '{key}' !!!";
                Debug.WriteLine(err);
                MessageBox.Show(err);
                return 0;
            }

            if (depth > 0 && key.StartsWith("Catalyst")) return 0;

            if (depth == 0)
            {
                _currentRecipe = key;
                if (!DUData.ProductionListMode)
                {
                    amount = Math.Max(1, ProductQuantity);
                }
                ApplicableTalents = new List<string>();
            }
            else
            if (depth > 10 || key == _currentRecipe) // faulty recipe?!
            {
                return 0;
            }

            var product = recipe.Products?.FirstOrDefault();

            var calc = Get(key, parent ?? Guid.Empty);
            calc.Parent = parent ?? Guid.Empty;
            calc.OreCost = 0;
            calc.Quantity = amount;
            calc.Margin = 0;
            calc.Retail = 0;
            calc.SchematicsCost = 0m;
            calc.SchematicType = recipe.SchemaType;
            calc.SetParent(parent ?? Guid.Empty); // empty for depth 0
            calc.Recipe = recipe;
            calc.CopyRecipeValuesFrom(recipe);
            calc.GetTalents();

            // product is null for ores!
            var productQty = product?.Quantity ?? amount;

            // Ammo override: ammunition has special batch size of 40 like "magazine size".
            // We take the requested amount as # of ammo rounds to be calculated
            // and determine the minimum of batches to be produced
            if (depth == 0 && calc.IsAmmo)
            {
                productQty = Math.Round(amount / (decimal)(calc.BatchOutput ?? 40), 2);
                amount = productQty;
            }
            var curLevel = level;
            level += "     ";
            Debug.WriteLineIf(!silent, $"{curLevel}> {recipe.Name} ({amount:N2})");

            // Special case for ore and plasma
            if (depth == 0 && (recipe.IsOre || recipe.IsPlasma))
            {
                decimal cost = 0;
                var ore = DUData.Ores.FirstOrDefault(o => o.Key == recipe.Key);
                if (ore != null)
                {
                    cost = Math.Round(amount * ore.Value, 2);
                }
                calc.OreCost = cost;
                return cost;
            }

            foreach (var ingredient in recipe.Ingredients)
            {
                if (string.IsNullOrEmpty(ingredient.Type) || !DUData.GetRecipeCloneByKey(ingredient.Type, out var myRecipe))
                {
                    var err = $"{curLevel}     MISSING: {recipe.Key}->{ingredient.Name} !!!";
                    Debug.WriteLine(err);
                    MessageBox.Show(err);
                    continue;
                }

                decimal qty;
                decimal cost;
                var ingName = ingredient.Name;
                var ingKey = ingName;
                if (!myRecipe.IsPlasma)
                {
                    // prefix ingredient key with tier
                    ingKey = "T" + (myRecipe.Level < 2 ? "1" : myRecipe.Level.ToString()) + " " + ingKey;
                }

                var factor = 1m;
                if (myRecipe.IsOre || myRecipe.IsPure || myRecipe.IsProduct)
                {
                    factor = (productQty * calc.OutputMultiplier + calc.OutputAdder) /
                             (ingredient.Quantity * calc.InputMultiplier + calc.InputAdder);
                }
                else
                {
                    factor = (ingredient.Quantity * calc.InputMultiplier + calc.InputAdder) /
                             (productQty * calc.OutputMultiplier + calc.OutputAdder);
                }

                if (myRecipe.IsOre || myRecipe.IsPlasma)
                {
                    var calc2 = Get(ingredient.Type, calc.Id);
                    calc2.SchematicType = recipe.SchemaType;
                    calc2.SetParent(calc.Id);

                    if (DUData.ProductionListMode && depth == 0)
                        qty = ingredient.Quantity;
                    else
                        // assumption: Plasma qty always 1
                        qty = myRecipe.IsPlasma ? 1 : (amount / factor);
                    qty = Math.Round(qty, 2);
                    cost = Math.Round(qty * DUData.Ores.First(o => o.Key == ingredient.Type).Value, 2);
                    // Only for ores we accumulate the cost within "calc"!
                    calc2.OreCost = cost;
                    calc2.Quantity = qty;
                    calc.Add(SummationType.ORES, ingKey, qty, cost);
                    Debug.WriteLineIf(!silent && qty > 0, $"{curLevel}     ({ingredient.Name}: {qty:N2} = {cost:N2}q)");
                    continue;
                }

                qty = amount;
                if (myRecipe.IsPart || myRecipe.IsProduct || myRecipe.IsPure)
                {
                    if (depth == 0)
                    {
                        qty = Math.Round(ingredient.Quantity * amount, 2);
                    }
                    else
                    if (myRecipe.IsPart)
                    {
                        qty *= ingredient.Quantity;
                        ingName = "   " + ingKey;
                    }
                    else
                    {
                        qty /= factor;
                        ingName = (myRecipe.IsProduct ? "  " : " ") + ingKey;
                    }
                    qty = Math.Round(qty, 2);
                    calc.Add(SummationType.INGREDIENTS, ingName, qty, 0);
                }
                // Keep these 3 as ingredients, but now don't add to ores/costs
                if (myRecipe.IsProduct && (ingKey.Contains("Catalyst") || ingKey.Contains("Hydrogen") || ingKey.Contains("Oxygen")))
                {
                    continue;
                }
                cost = CalculateRecipe(ingredient.Type, qty, level, depth + 1, parent: calc.Id, silent: silent);
                Debug.WriteLineIf(!silent && cost > 0, $"{curLevel}     = {cost:N2}q");

                if (myRecipe.IsPart)
                {
                    calc.Add(SummationType.PARTS, ingKey, qty, cost);
                    continue;
                }
                if (myRecipe.IsProduct)
                {
                    calc.Add(SummationType.PRODUCTS, ingKey, qty, cost);
                    continue;
                }
                if (myRecipe.IsPure)
                {
                    calc.Add(SummationType.PURES, ingKey, qty, cost);
                }
                else
                {
                    Debug.WriteLineIf(!silent, myRecipe.Name);
                }
            }

            // once the top-level recipe is done, do a collection run
            if (depth == 0)
            {
                Collect(calc);
            }
            return calc.OreCost;
        }

        public static List<string> GetTalentsForKey(string key, out decimal inputMultiplier, out decimal inputAdder,
            out decimal outputMultiplier, out decimal outputAdder, out decimal efficiencyFactor)
        {
            inputMultiplier = 1;
            inputAdder = 0;
            outputMultiplier = 1;
            outputAdder = 0;
            efficiencyFactor = 1;

            foreach (var talent in DUData.Talents.Where(t => t.ApplicableRecipes.Contains(key)))
            {
                if (ApplicableTalents == null) ApplicableTalents = new List<string>();
                if (!ApplicableTalents.Contains(talent.Name))
                {
                    ApplicableTalents.Add(talent.Name);
                }
                if (talent.EfficiencyTalent)
                {
                    efficiencyFactor = talent.GetEfficiencyFactor();
                    continue;
                }

                if (talent.InputTalent)
                {
                    // Add each talent's multipler and adder so that we get values like 1.15 or 0.85, for pos/neg multipliers
                    inputMultiplier += talent.Multiplier * talent.Value;
                    inputAdder += talent.Addition * talent.Value;
                }
                else
                {
                    outputMultiplier += talent.Multiplier * talent.Value;
                    outputAdder += talent.Addition * talent.Value;
                }
            }

            ApplicableTalents?.Sort();
            return ApplicableTalents;
        }

        //public static decimal GetBaseCost(string key)
        //{
        //    // Just like the other one, but ignore DUData.Talents.
        //    if (DUData.Recipes?.Keys.Contains(key) != true)
        //        return 0;
        //    var recipe = DUData.Recipes[key];

        //    // Skip catalysts entirely tho.
        //    if (DUData.Groups.Values.FirstOrDefault(g => g.Id == recipe.GroupId)?.Name == "Catalyst")
        //        return 0;

        //    decimal totalCost = 0;
        //    foreach (var ingredient in recipe.Ingredients)
        //    {
        //        if (!DUData.Recipes.Keys.Contains(ingredient.Type))
        //        {
        //            Console.WriteLine($@"Schematic {ingredient.Type} not found!");
        //            continue;
        //        }

        //        if (DUData.Recipes.Keys.Contains(ingredient.Type) && DUData.Recipes[ingredient.Type].ParentGroupName == "Ore")
        //        {
        //            totalCost += (ingredient.Quantity *
        //                          DUData.Ores.First(o => o.Key == ingredient.Type).Value) /
        //                         (recipe.Products.First().Quantity);
        //        }
        //        else
        //        {
        //            totalCost += GetBaseCost(ingredient.Type) * ingredient.Quantity /
        //                         (recipe.Products.First().Quantity);
        //        }
        //    }
        //    return totalCost;
        //}

        /// <summary>
        /// This method returns a list of all ingredients for a given item, taking into account available talents.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Key is most important to identify the item.
        /// </para>
        /// <para>
        /// It only processes ingredients and does not take into account any byproducts.
        /// Special materials like Catalyst, Pure Oxygen and Pure Hydrogen are not applied with cost.
        /// </para>
        /// </remarks>
        public static List<CalculatorClass> GetIngredientRecipes(string key, decimal quantity = 1, bool reverse = false)
        {
            var results = new List<CalculatorClass>();
            if (key.StartsWith("Catalyst") || !CreateByKey(key, out var calc))
            {
                return results;
            }
            if (calc.Recipe.Ingredients?.Any() != true || calc.Recipe.Products?.Any() != true)
            {
                return results;
            }

            calc.GetTalents();

            foreach (var ingredient in calc.Recipe.Ingredients)
            {
                if (!CreateByKey(ingredient.Type, out var entry))
                {
                    continue;
                }

                // check that group exists
                var group = DUData.Groups.Values.FirstOrDefault(g => g.Id != Guid.Empty && g.Id == entry.Recipe.GroupId);
                if (string.IsNullOrEmpty(group?.Name) || group.Name.StartsWith("Catalyst"))
                {
                    continue;
                }

                entry.GetTalents();
                entry.SetParent(calc.Id);

                var factor = 1m;
                var productQty = calc.Recipe.Products.First().Quantity;
                if (reverse)
                {
                    // For reverse calculation for an ore, use the Pure's data
                    if (calc.IsPure && entry.IsOre)
                    {
                        if (string.IsNullOrEmpty(entry.Recipe.Industry))
                        {
                            entry.Recipe.Industry = calc.Recipe.Industry;
                        }
                        entry.CopyBaseValuesFrom(calc);
                    }
                    factor = (productQty * calc.OutputMultiplier + calc.OutputAdder) /
                             (ingredient.Quantity * calc.InputMultiplier + calc.InputAdder);
                    entry.Quantity = Math.Round(quantity * factor, 2);
                }
                else
                {
                    factor = (ingredient.Quantity * calc.InputMultiplier + calc.InputAdder) /
                             (productQty * calc.OutputMultiplier + calc.OutputAdder);
                    entry.Quantity = Math.Round(quantity * factor, 2);
                }

                results.Add(entry);
            }
            return results;
        }

        public static void CalcRetail(ProductDetail item,
                                      bool applyMargin = false, decimal marginPct = 0m,
                                      bool applyRounding = false, int roundingDigits = 1)
        {
            item.Margin = 0;
            item.Retail = 0;
            if (item.Quantity == 0) return;
            item.Cost = Math.Round(item.Cost / item.Quantity, 2) * item.Quantity;
            item.SchemaAmt = Math.Round(item.SchemaAmt / item.Quantity, 2) * item.Quantity;
            var totalCostWithoutMargin = item.Cost + item.SchemaAmt;
            item.Retail = totalCostWithoutMargin;
            if (applyMargin)
            {
                item.Retail = Math.Round(totalCostWithoutMargin * (1 + marginPct / 100), 2, MidpointRounding.AwayFromZero);
            }
            if (applyRounding)
            {
                item.Retail = Utils.RoundUp(item.Retail, -roundingDigits);
            }
            item.Margin = item.Retail - totalCostWithoutMargin;
        }

    }

    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class CalculatorClass : RecipeBase, INotifyPropertyChanged
    {
        private SchematicRecipe _recipe;

        public Dictionary<SummationType, SortedDictionary<string, CalcEntry>> Sums { get; }
        public SortedDictionary<string, Tuple<decimal, decimal>> SumSchemClass { get; private set; }

        public Guid Id { get; private set; }
        public Guid Parent { get; set; }
        public SortedDictionary<Guid, CalculatorClass> Nodes;

        public string Key { get; }
        public byte Tier { get; set; }

        public decimal Volume { get; set; }
        public decimal Mass { get; set; }

        public bool IsProdItem { get; set; }
        public bool Collected { get; set; }

        public string SchematicType { get; set; }

        public bool RecipeExists => Recipe != null;

        // costs
        public decimal OreCost { get; set; }
        public decimal SchematicsCost { get; set; }
        public decimal Margin { get; set; } // Margin = Retail - OreCost + SchematicCost (in Quanta)
        public decimal Retail { get; set; } // Retail = (OreCost + SchematicCost) * (1 + margin/100) (in Quanta)

        public SchematicRecipe Recipe
        {
            get => _recipe;
            set
            {
                _recipe = value;
                if (_recipe == null)
                {
                    ParentGroupName = ""; // trigger update of "IsXXX" props
                    Tier = 0;
                    SchematicType = null;
                    return;
                }
                ParentGroupName = _recipe.ParentGroupName;
                Tier = _recipe.Level;
                SchematicType = Recipe.SchemaType;
            }
        }

        private CalculatorClass()
        {
            Id = Guid.NewGuid();
            Sums = new Dictionary<SummationType, SortedDictionary<string, CalcEntry>>();
            SchematicsCost = 0;
            SumSchemClass = new SortedDictionary<string, Tuple<decimal, decimal>>();
        }

        //public CalculatorClass(string key) : this()
        //{
        //    Key = key;
        //    Recipe = DUData.Recipes.FirstOrDefault(x => x.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)).Value;
        //    Name = Recipe?.Name;
        //    ParentGroupName = Recipe?.ParentGroupName;
        //    SchematicType = Recipe?.SchemaType;
        //    SchematicsCost = Recipe?.SchemaPrice ?? 0;
        //}

        public CalculatorClass(SchematicRecipe recipe) : this()
        {
            Recipe = recipe;
            Key = Recipe?.Key;
            Name = Recipe?.Name;
            ParentGroupName = Recipe?.ParentGroupName;
            SchematicType = Recipe?.SchemaType;
            SchematicsCost = Recipe?.SchemaPrice ?? 0;
        }

        public void CopyBaseValuesFrom(CalculatorClass entry)
        {
            SchematicType = entry.SchematicType;

            EfficencyFactor = entry.EfficencyFactor;
            InputMultiplier = entry.InputMultiplier;
            InputAdder = entry.InputAdder;
            OutputMultiplier = entry.OutputMultiplier;
            OutputAdder = entry.OutputAdder;

            BatchOutput = entry.BatchOutput;
            BatchInput = entry.BatchInput;
            BatchTime = entry.BatchTime;
        }

        public void CopyRecipeValuesFrom(ProductNameClass entry)
        {
            IsOre = entry.IsOre;
            IsAmmo = entry.IsAmmo;
            IsPlasma = entry.IsPlasma;
            IsPart = entry.IsPart;
            IsPure = entry.IsPure;
            IsProduct = entry.IsProduct;
        }

        public void SetParent(Guid parent)
        {
            Parent = parent;
        }

        public void AddSchematicCost(decimal cost)
        {
            SchematicsCost += cost;
        }

        public void ResetSchematicCost()
        {
            SchematicsCost = 0;
        }

        public void AddSchema(string schemaKey, decimal qty, decimal amount)
        {
            if (string.IsNullOrEmpty(schemaKey) || qty < 0.001m) return;
            if (SumSchemClass == null)
            {
                SumSchemClass = new SortedDictionary<string, Tuple<decimal, decimal>>();
            }

            if (SumSchemClass.ContainsKey(schemaKey))
            {
                var item1 = SumSchemClass[schemaKey].Item1 + qty;
                var item2 = SumSchemClass[schemaKey].Item2 + amount;
                SumSchemClass.Remove(schemaKey);
                SumSchemClass.Add(schemaKey, new Tuple<decimal, decimal>(item1, item2));
            }
            else
                SumSchemClass.Add(schemaKey, new Tuple<decimal, decimal>(qty, amount));
        }

        public void Add(SummationType sumType, string key, CalcEntry value)
        {
            Add(sumType, key, value.Qty, value.Amt, value.SchematicType, value.QtySchemata, value.AmtSchemata);
        }

        public void Add(SummationType sumType, string key, decimal quantity, decimal amount,
                        string schematicType=null, decimal? schemaQty=null, decimal? schemaAmt=null)
        {
            if (!Sums.ContainsKey(sumType))
            {
                Sums.Add(sumType, new SortedDictionary<string, CalcEntry>());
            }

            var tmp = new CalcEntry();
            if (Sums[sumType].ContainsKey(key))
            {
                tmp = Sums[sumType][key];
                tmp.Qty += Math.Round(quantity, 2);
                tmp.Amt += Math.Round(amount, 2);
                if (schemaQty > 0) tmp.QtySchemata += schemaQty;
                if (schemaAmt > 0) tmp.AmtSchemata += schemaAmt;
                if (schematicType != null) tmp.SchematicType = schematicType;
                Sums[sumType][key] = tmp;
            }
            else
            {
                tmp.Qty = Math.Round(quantity, 2);
                tmp.Amt = Math.Round(amount, 2);
                tmp.QtySchemata = schemaQty;
                tmp.AmtSchemata = schemaAmt;
                tmp.SchematicType = schematicType;
                Sums[sumType].Add(key, tmp);
            }
        }

        public SortedDictionary<string, CalcEntry> Get(SummationType sumType)
        {
            if (!Sums.ContainsKey(sumType))
            {
                Sums.Add(sumType, new SortedDictionary<string, CalcEntry>());
            }
            return Sums[sumType];
        }

        public decimal TotalOreCost()
        {
            if (Sums != null && Sums.TryGetValue(SummationType.ORES, out var ores))
            {
                return ores.Sum(x => Math.Round(x.Value.Amt, 2));
            }
            return 0;
        }

        public List<string> GetTalents()
        {
            return GetTalents(Recipe); // in RecipeBase
        }

        // not used for pures, but elements
        public bool CalcSchematicFromQty(string schematicType, decimal qty, decimal batchOutput,
                        out decimal batches, out decimal minCost, out decimal copyCost, out decimal qtyCopies)
        {
            minCost = 0M;
            copyCost = 0M;
            qtyCopies = 0;
            batches = 0;
            if (string.IsNullOrEmpty(schematicType)) // e.g. for T1 ore
            {
                return false;
            }

            if (batchOutput > 0)
            {
                qty /= batchOutput;
            }
            batches = DUData.FullSchematicQuantities ? (int)Math.Ceiling(qty) : Math.Round(qty, 3);
            return Calculator.CalcSchematic(schematicType, batches, out minCost, out copyCost, out qtyCopies);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    // TODO use databinding in results output
    public class BindableCalculatorClass : INotifyPropertyChanged
    {
        private CalculatorClass _calculator;
        public CalculatorClass Calculator
        {
            get => _calculator;
            set
            {
                if (_calculator != value)
                {
                    if (_calculator != null)
                    {
                        _calculator.PropertyChanged -= Calculator_PropertyChanged;
                    }
                    _calculator = value;
                    if (_calculator != null)
                    {
                        _calculator.PropertyChanged += Calculator_PropertyChanged;
                    }
                    OnPropertyChanged();
                }
            }
        }
        private void Calculator_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Forward the PropertyChanged event
            OnPropertyChanged(nameof(Calculator));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}