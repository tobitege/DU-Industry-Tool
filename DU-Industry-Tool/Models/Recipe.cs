﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.Serialization;
using Newtonsoft.Json;
// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace DU_Industry_Tool
{
    // DEV NOTE: be aware that any changes to properties must be
    // reflected accordingly in any of the existing Clone() methods!

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
                    IsAmmo = false;
                    IsFuel = false;
                    IsHC = false;
                    IsOre = false;
                    IsPart = false;
                    IsPure = false;
                    IsProduct = false;
                    return;
                }
                IsAmmo = _parentGroupName.IndexOf(" ammo", StringComparison.InvariantCultureIgnoreCase) > 0;
                IsFuel = _parentGroupName.Equals("Fuels");
                IsHC = _parentGroupName.Contains("Honeycomb");
                IsOre = _parentGroupName.Equals("Ore");
                IsPart = _parentGroupName.EndsWith(" parts");
                IsPlasma = Name?.StartsWith("Relic Plasma") == true &&
                           _parentGroupName.Equals("Consumables");
                IsProduct = _parentGroupName.Equals("Product") || _parentGroupName.Equals("Refined Materials");
                IsPure = _parentGroupName.Equals("Pure");
            }
        }

        [JsonIgnore]
        public bool IsAmmo { get; protected set; }
        [JsonIgnore]
        public bool IsFuel { get; protected set; }
        [JsonIgnore]
        public bool IsHC { get; protected set; }
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
        public bool IsBatchmode => IsOre || IsPure || IsProduct || IsFuel || IsAmmo;
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

        public SchematicRecipe Clone()
        {
            var result = new SchematicRecipe
            {
                Key = Key,
                Name = Name,
                Level = Level,
                Id = Id,
                Time = Time,
                GroupId = GroupId,
                Node = null,
                NqId = NqId,
                SchemaType = SchemaType,
                SchemaPrice = SchemaPrice,
                UnitMass = UnitMass,
                UnitVolume = UnitVolume,
                Nanocraftable = Nanocraftable,
                Size = Size,
                Industry = Industry,
                ParentGroupName = ParentGroupName,
                IsAmmo = IsAmmo,
                IsFuel = IsFuel,
                IsOre = IsOre,
                IsPart = IsPart,
                IsPlasma = IsPlasma,
                IsProduct = IsProduct,
                IsPure = IsPure
            };
            foreach (var ingredient in Ingredients)
            {
                result.Ingredients.Add(ingredient.Clone());
            }
            foreach (var product in Products)
            {
                result.Products.Add(product.Clone());
            }
            return result;
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
            var result = rec.Value.Clone();
            result.Key = rec.Key;
            return result;
        }

        public static SchematicRecipe GetByName(string recipeName)
        {
            if (string.IsNullOrEmpty(recipeName)) return null;
            var rec = DUData.Recipes.FirstOrDefault(x =>
                x.Value.Name.Equals(recipeName, StringComparison.CurrentCultureIgnoreCase));
            if (rec.Key == null) return null;
            var result = rec.Value.Clone();
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
        public int? Batches { get; set; }
        [JsonIgnore]
        public decimal InputMultiplier { get; set; }
        [JsonIgnore]
        public decimal InputAdder { get; set; }
        [JsonIgnore]
        public decimal OutputMultiplier { get; set; }
        [JsonIgnore]
        public decimal OutputAdder { get; set; }

        private void ResetTalents()
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

            var result = Calculator.GetTalentsForKey(recipe.Key, recipe.Industry,
                out var inputMultiplier,  out var inputAdder,
                out var outputMultiplier, out var outputAdder,
                out var efficiencyFactor, out var efficiencyFactorIndy);

            InputMultiplier = inputMultiplier;
            InputAdder = inputAdder;
            OutputMultiplier = outputMultiplier;
            OutputAdder = outputAdder;
            EfficencyFactor = efficiencyFactor;

            BatchTime = recipe.Time;
            if (recipe.IsOre) return result;
            if (recipe.IsBatchmode)
            { 
                if (recipe.IsAmmo)
                {
                    BatchInput = inputMultiplier + inputAdder;
                    BatchOutput = 40 * outputMultiplier + outputAdder;
                }
                else
                if (recipe.IsFuel)
                {
                    if (recipe.SchemaType == "SpaceFuels")
                    {
                        if (recipe.Name.Contains("X5"))
                        {
                            BatchInput = 90 * inputMultiplier + inputAdder;
                        }
                        else
                        {
                            BatchInput = 40 * inputMultiplier + inputAdder;
                        }
                        BatchOutput = 100 * outputMultiplier + outputAdder;
                    }
                    else
                    {
                        BatchInput = 20 * inputMultiplier + inputAdder;
                        BatchOutput = 100 * outputMultiplier + outputAdder;
                    }
                }
                else
                {
                    BatchInput = (recipe.IsProduct ? 100 : 65) * inputMultiplier + inputAdder;
                    BatchOutput = (recipe.IsProduct ? 75 : 45) * outputMultiplier + outputAdder;
                }
            }
            if (recipe.Time < 1) return result;

            // facepalm! the recipe talent factors are summed up, but the industry
            // efficiency factors are multiplied on top
            EfficencyFactor = (EfficencyFactor ?? 1) * efficiencyFactorIndy;
            BatchTime = recipe.Time * EfficencyFactor;
            if (!recipe.IsBatchmode)
            {
                return result;
            }

            if (recipe.Level != 1) return result;

            // Apply efficiency and only for tier 1 apply the batch size formula
            var time = (decimal)BatchTime;
            Batches = (int)Math.Max(1, Math.Floor(180 / time));
            BatchTime = Math.Round(time * (int)Batches);
            if (BatchTime >= 180)
            {
                if (Batches > 1)
                {
                    Batches--;
                }
                BatchTime = time * Batches;
            }
            BatchInput *= Batches;
            BatchOutput *= Batches;
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
        public decimal SchemaQty { get; set; }
        [JsonIgnore]
        public decimal SchemaAmt { get; set; }
        [JsonIgnore]
        public bool IsByproduct { get; set; }
        [JsonIgnore]
        public decimal Mass { get; set; }
        [JsonIgnore]
        public decimal Volume { get; set; }
        [JsonIgnore]
        public decimal Cost { get; set; }
        [JsonIgnore]
        public decimal ItemPrice { get; set; }
        [JsonIgnore]
        public decimal Margin { get; set; }
        [JsonIgnore]
        public decimal Retail { get; set; }
        [JsonIgnore]
        public bool IsProdItem { get; set; }

        public ProductDetail Clone()
        {
            var result = new ProductDetail
            {
                Name = this.Name,
                Quantity = Quantity,
                Id = Id,
                Type = Type,
                Level = Level,
                SchemaType = SchemaType,
                SchemaQty = SchemaQty,
                SchemaAmt = SchemaAmt,
                IsByproduct = IsByproduct,
                Mass = Mass,
                Volume = Volume,
                Cost = Cost,
                ItemPrice = ItemPrice,
                Margin = Margin,
                Retail = Retail,
                IsProdItem = IsProdItem
            };
            return result;
        }
        
        public ProductDetail() {}

        public ProductDetail(SchematicRecipe entry)
        {
            Type = entry.Key;
            Name = entry.Name;
            Level = entry.Level;
            SchemaType = entry.SchemaType;
            SchemaAmt = entry.SchemaPrice;
            Margin = 0m;
            Retail = 0m;
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
            Margin = entry.Margin;
            Retail = entry.Retail;
        }
    }

    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ParentId { get; set; }
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
        public decimal Cost { get; set; }       // cost per 1 schematic
        public decimal BatchCost { get; set; }  // cost per 1 batch
        public int Level { get; set; }
        public int BatchSize { get; set; }      // copies per batch
        public int BatchTime { get; set; }      // crafting time in seconds per batch
        public ulong NqId { get; set; }         // NQ's item id
        [JsonIgnore]
        public decimal? Qty { get; set; }
        [JsonIgnore]
        public decimal? Total { get; set; }
    }

    public class SchematicInfo
    {
        public string Key { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Total { get; set; }
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
