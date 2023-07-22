using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.Serialization;
using Newtonsoft.Json;
// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global

namespace DU_Industry_Tool
{
    public class ProductNameClass
    {
        public string Name { get; set; }

        [JsonIgnore]
        private string _parentGroupName;
        public string ParentGroupName
        {
            get => _parentGroupName;
            set
            {
                if (_parentGroupName == value) return;
                _parentGroupName = value;
                if (string.IsNullOrEmpty(_parentGroupName))
                {
                    IsOre = false;
                    IsPart = false;
                    IsPure = false;
                    IsProduct = false;
                    IsFuel = false;
                    return;
                }
                IsOre = _parentGroupName.Equals("Ore");
                IsPart = _parentGroupName.EndsWith(" parts");
                IsPlasma = Name?.StartsWith("Relic Plasma") == true &&
                           _parentGroupName.Equals("Consumables");
                IsPure = _parentGroupName.Equals("Pure");
                IsProduct = _parentGroupName.Equals("Product") || _parentGroupName.Equals("Refined Materials");
                IsFuel = _parentGroupName.Equals("Fuels");
            }
        }

        [JsonIgnore]
        public bool IsOre { get; protected set; }
        [JsonIgnore]
        public bool IsPart { get; protected set; }
        [JsonIgnore]
        public bool IsPlasma { get; protected set; }
        [JsonIgnore]
        public bool IsPure { get; protected set; }
        [JsonIgnore]
        public bool IsProduct { get; protected set; }
        [JsonIgnore]
        public bool IsFuel { get; protected set; }
        [JsonIgnore]
        public bool IsBatchmode => IsOre || IsPure || IsProduct || IsFuel;
    }

    public class SchematicRecipe : ProductNameClass
    {
        public byte Level { get; set; }
        [DataMember(Name="id")]
        public ulong Id { get; set; }
        public List<ProductDetail> Products { get; } = new List<ProductDetail>();
        public decimal Time { get; set; }
        public List<ProductDetail> Ingredients { get; } = new List<ProductDetail>();
        public Guid GroupId { get; set; }
        [JsonIgnore]
        public string Key { get; set; }
        [JsonIgnore]
        public TreeNode Node { get; set; }
        public ulong NqId { get; set; } // Item Id, not recipe Id
        public string SchemaType { get; set; }
        public decimal SchemaPrice { get; set; }
        public decimal? UnitMass { get; set; }
        public decimal? UnitVolume { get; set; }
        public bool Nanocraftable { get; set; }
        public string Size { get; set; }
        public string Industry { get; set; }

        public object Clone()
        {
            // setup
            var json = JsonConvert.SerializeObject(this);
            // get
            return JsonConvert.DeserializeObject<SchematicRecipe>(json);
        }

        public SchematicRecipe(){}

        public SchematicRecipe(SchematicRecipe entry)
        {
            if (entry == null) return;
            Key = entry.Key;
            Level = entry.Level;
            Name = entry.Name;
            SchemaType = entry.SchemaType;
        }

        public SchematicRecipe(ProductDetail entry)
        {
            if (entry == null) return;
            Key = entry.Type;
            Name = entry.Name;
            SchemaType = entry.SchemaType;
            Level = entry.Level;
        }

        private string ConvertName(string itemName)
        {
            if (itemName == "Uncommon Casing l") return "casing_2_l";
            if (itemName == "Uncommon Screen l") return "screen_2_l";
            if (itemName == "Uncommon Screen m") return "screen_2_m";
            if (itemName == "Uncommon Screen s") return "screen_2_s";
            return DUData.GetItemTypeFromName(itemName);
        }

        public SchematicRecipe(DuLuaRecipe entry)
        {
            Key = "INVALID";
            if (entry?.Products?.Any() != true) return;
            Level = entry.Tier;
            Time = entry.Time;
            Nanocraftable = entry.Nanocraftable;
            Name = entry.Products[0].DisplayNameWithSize;
            Key = ConvertName(Name);
            NqId = entry.Products[0].Id;
            if (entry.Producers?.Any() == true) // v1.4
            {
                Industry = entry.Producers[0].DisplayNameWithSize;
            }
            foreach (var entryProduct in entry.Ingredients)
            {
                var newProd = new ProductDetail
                {
                    Id = entryProduct.Id,
                    Quantity = entryProduct.Quantity,
                    Name = entryProduct.DisplayNameWithSize,
                    Type = ConvertName(entryProduct.DisplayNameWithSize)
                };
                Ingredients.Add(newProd);
            }
            foreach (var entryProduct in entry.Products)
            {
                var newProd = new ProductDetail
                {
                    Id = entryProduct.Id,
                    Quantity = entryProduct.Quantity,
                    Name = entryProduct.DisplayNameWithSize,
                    Type = ConvertName(entryProduct.DisplayNameWithSize)
                };
                Products.Add(newProd);
            }
        }

        public static SchematicRecipe GetByKey(string recipeKey)
        {
            if (string.IsNullOrEmpty(recipeKey)) return null;
            var rec = DUData.Recipes.FirstOrDefault(x =>
                x.Key.Equals(recipeKey, StringComparison.CurrentCultureIgnoreCase));
            if (rec.Key == null) return null;
            var result = rec.Value;
            result.Key = rec.Key;
            return result;
        }

        public static SchematicRecipe GetByName(string recipeName)
        {
            if (string.IsNullOrEmpty(recipeName)) return null;
            var rec = DUData.Recipes.FirstOrDefault(x =>
                x.Value.Name.Equals(recipeName, StringComparison.CurrentCultureIgnoreCase));
            if (rec.Key == null) return null;
            var result = rec.Value;
            result.Key = rec.Key;
            return result;
        }
    }

    public class RecipeBase : ProductNameClass
    {
        public decimal Quantity { get; set; }
        [JsonIgnore]
        public decimal? BatchInput { get; set; }
        [JsonIgnore]
        public decimal? BatchOutput { get; set; }
        [JsonIgnore]
        public decimal? EfficencyFactor { get; set; }
        [JsonIgnore]
        public decimal? BatchTime { get; set; }
        [JsonIgnore]
        public decimal InputMultiplier { get; set; }
        [JsonIgnore]
        public decimal InputAdder { get; set; }
        [JsonIgnore]
        public decimal OutputMultiplier { get; set; }
        [JsonIgnore]
        public decimal OutputAdder { get; set; }

        protected void ResetTalents()
        {
            InputMultiplier = 1;
            InputAdder = 0;
            OutputMultiplier = 1;
            OutputAdder = 0;
            EfficencyFactor = 1;

            BatchTime = null;
            BatchInput = null;
            BatchOutput = null;
        }

        protected List<string> GetTalents(SchematicRecipe recipe)
        {
            ResetTalents();
            if (string.IsNullOrEmpty(recipe?.Key)) return null;

            var result = Calculator.GetTalentsForKey(recipe.Key,
                out var inputMultiplier,  out var inputAdder,
                out var outputMultiplier, out var outputAdder,
                out var efficiencyFactor);

            InputMultiplier = inputMultiplier;
            InputAdder = inputAdder;
            OutputMultiplier = outputMultiplier;
            OutputAdder = outputAdder;
            EfficencyFactor = efficiencyFactor;

            BatchTime = recipe.Time;
            if (!recipe.IsBatchmode || recipe.IsOre) return result;

            BatchInput = (recipe.IsProduct ? 100 : 65) * inputMultiplier + inputAdder;
            BatchOutput = (recipe.IsProduct ? 75 : 45) * outputMultiplier + outputAdder;

            if (recipe.Time > 0)
            {
                BatchTime = recipe.Time * (EfficencyFactor ?? 1);
            }
            return result;
        }
    }

    public class ProductDetail
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        [JsonIgnore]
        public ulong Id { get; set; }

        public string Type { get; set; }
        [JsonIgnore]
        public byte Level { get; set; }
        [JsonIgnore]
        public string SchemaType { get; set; }
        [JsonIgnore]
        public int SchemaQty { get; set; }
        [JsonIgnore]
        public decimal SchemaAmt { get; set; }
        [JsonIgnore]
        public bool IsByproduct { get; set; }

        public ProductDetail() {}

        public ProductDetail(SchematicRecipe entry)
        {
            Type = entry.Key;
            Name = entry.Name;
            Level = entry.Level;
            SchemaType = entry.SchemaType;
            SchemaAmt = entry.SchemaPrice;
            //ParentGroupName = entry.ParentGroupName;
        }

        public ProductDetail(ProductDetail entry)
        {
            Type = entry.Type;
            Quantity = entry.Quantity;
            Name = entry.Name;
            SchemaType = entry.SchemaType;
            SchemaAmt = entry.SchemaAmt;
            Level = entry.Level;
        }

        public ProductDetail(CalculatorClass entry)
        {
            Type = entry.Key;
            Quantity = entry.Quantity;
            Name = entry.Name;
            SchemaType = entry.SchematicType;
            SchemaAmt = entry.SchematicsCost; // TODO ??
            Level = entry.Tier;
        }
    }

    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ParentId { get; set; }
    }

    public class Talent
    {
        public string Name { get; set; }
        public decimal Multiplier { get; set; }
        public int Addition { get; set; }
        public List<string> ApplicableRecipes { get; set; } = new List<string>();
        public int Value { get; set; } // Contains the effective talent level that we're using.
        public bool InputTalent { get; set; } = false;
        public bool EfficiencyTalent { get; set; } = false;

        public decimal GetEfficiencyFactor()
        {
            if (!EfficiencyTalent) return 1;
            return 1 + (Value * Multiplier);
        }
    }

    // For saving settings, a simple lookup
    public class Ore
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int Level { get; set; }
    }

    public class Schematic
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }  // cost per 1 copy
        public int Level { get; set; }
        public int BatchSize { get; set; } // copies per batch
        public int BatchTime { get; set; } // seconds
    }

    // external/3rd party json structures:

    public class DuLuaSchematic
    {
        [DataMember(Name="id")]
        public string Id { get; set; }
        [DataMember(Name = "displayNameWithSize")]
        public string DisplayNameWithSize { get; set; }
        [DataMember(Name = "locDisplayNameWithSize")]
        public string LocDisplayNameWithSize { get; set; }
        [DataMember(Name = "locDisplayNameWithSizeDE")]
        public string LocDisplayNameWithSizeDE { get; set; }
    }

    public class DuLuaItem
    {
        [DataMember(Name="id")]
        public string Id { get; set; }
        [DataMember(Name="tier")]
        public byte Tier { get; set; }
        [DataMember(Name="displayNameWithSize")]
        public string DisplayNameWithSize { get; set; }
        [DataMember(Name = "locDisplayNameWithSize")]
        public string LocDisplayNameWithSize { get; set; }
        [DataMember(Name = "locDisplayNameWithSizeDE")]
        public string LocDisplayNameWithSizeDE { get; set; }
        [DataMember(Name="unitMass")]
        public decimal UnitMass { get; set; }
        [DataMember(Name="unitVolume")]
        public decimal UnitVolume { get; set; }
        [DataMember(Name="size")]
        public string Size { get; set; }
        [DataMember(Name="iconPath")]
        public string IconPath { get; set; }
        [DataMember(Name="description")]
        public string Description { get; set; }
        [DataMember(Name="schematics")]
        public List<DuLuaSchematic> Schematics { get; set; }
        [DataMember(Name = "products")]
        public List<DuLuaSubItem> Products { get; set; }
        [DataMember(Name = "producers")]
        public List<DuLuaProducer> Producers { get; set; }
        [DataMember(Name = "classId")]
        public string ClassId { get; set; }
        [DataMember(Name = "displayClassId")]
        public string DisplayClassId { get; set; }
    }

    public class DuLuaRecipe
    {
        [DataMember(Name="id")]
        public string Id { get; set; }
        [DataMember(Name="tier")]
        public byte Tier { get; set; }
        [DataMember(Name="time")]
        public int Time { get; set; }
        [DataMember(Name="nanocraftable")]
        public bool Nanocraftable { get; set; }
        [DataMember(Name="ingredients")]
        public List<DuLuaSubItem> Ingredients { get; set; }
        [DataMember(Name="products")]
        public List<DuLuaSubItem> Products { get; set; }
        [DataMember(Name= "producers")]
        public List<DuLuaProducer> Producers { get; set; }
    }

    public class DuLuaProducer
    {
        [DataMember(Name="id")]
        public ulong Id { get; set; }
        [DataMember(Name="displayNameWithSize")]
        public string DisplayNameWithSize { get; set; }
        [DataMember(Name= "locDisplayNameWithSize")]
        public string LocDisplayNameWithSize { get; set; }
        [DataMember(Name= "locDisplayNameWithSizeDE")]
        public string LocDisplayNameWithSizeDE { get; set; }
    }

    public class DuLuaSubItem
    {
        [DataMember(Name="quantity")]
        public decimal Quantity { get; set; }
        [DataMember(Name="id")]
        public ulong Id { get; set; }
        [DataMember(Name="displayNameWithSize")]
        public string DisplayNameWithSize { get; set; }
        [DataMember(Name = "locDisplayNameWithSize")]
        public string LocDisplayNameWithSize { get; set; }
        [DataMember(Name = "locDisplayNameWithSizeDE")]
        public string LocDisplayNameWithSizeDE { get; set; }
    }

    public class FactGenRecipe
    {
        [DataMember(Name="tier")]
        public int Tier { get; set; }
        [DataMember(Name="type")]
        public string ItemType { get; set; }
        [DataMember(Name="volume")]
        public decimal Volume { get; set; }
        [DataMember(Name="outputQuantity")]
        public decimal OutputQty { get; set; }
        [DataMember(Name="time")]
        public int Time { get; set; }
        [JsonIgnore]
        [DataMember(Name="byproducts")]
        public List<Tuple<string,decimal>> Byproducts { get; set; }
        [DataMember(Name="industry")]
        public string Industry { get; set; }
        [JsonIgnore]
        [DataMember(Name="input")]
        public List<Tuple<string,decimal>> Inputs { get; set; }
    }

    public class ProductionItem
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
    }
}
