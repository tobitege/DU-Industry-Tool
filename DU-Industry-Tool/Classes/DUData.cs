using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DocumentFormat.OpenXml;
using Krypton.Toolkit;
using Newtonsoft.Json;

namespace DU_Industry_Tool
{
    public delegate void ProductionListHandler(object sender);

    public enum SummationType
    {
        ORES,
        PURES,
        PRODUCTS,
        PARTS,
        INGREDIENTS
    }

    public class DUDataBindings
    {
        public const string NO_FILE = "unnamed";
        public BindingList<ProductionItem> ProductionBindingList { get; set; }
        public BindingList<string> RecipeNamesBindingList { get; set; }

        public bool ListLoaded { get; private set; }
        public string Filepath { get; set; }
        public string LastErrorMsg { get; set; }

        public event ProductionListHandler ProductionListChanged;

        public bool HasData => ProductionBindingList?.Any() == true;
        public int Count => ProductionBindingList?.Count ?? 0;

        private void CheckInstance()
        {
            ProductionBindingList = ProductionBindingList ?? new BindingList<ProductionItem>();
        }

        public void Add(string itemName, decimal qty)
        {
            CheckInstance();
            if (string.IsNullOrEmpty(itemName) || ProductionBindingList.Any(x => x.Name == itemName)) return;
            var item = new ProductionItem
            {
                Name = itemName,
                Quantity = Math.Max(1, qty)
            };
            ProductionBindingList.Add(item);
            Notify();
        }

        public void Remove(string itemName)
        {
            CheckInstance();
            var item = ProductionBindingList.FirstOrDefault(x => x.Name == itemName);
            if (item == null) return;
            ProductionBindingList.Remove(item);
            Notify();
        }

        public void Clear()
        {
            ProductionBindingList = new BindingList<ProductionItem>();
            Filepath = "";
            ListLoaded = false;
            LastErrorMsg = "";
            Notify();
        }

        public bool Load(string filename)
        {
            if (!File.Exists(filename)) return false;
            try
            {
                var tmp = JsonConvert.DeserializeObject<List<ProductionItem>>(File.ReadAllText(filename));
                if (tmp == null) return false;

                ProductionBindingList = new BindingList<ProductionItem>();
                foreach (var entry in tmp)
                {
                    if (!string.IsNullOrEmpty(entry?.Name))
                    {
                        ProductionBindingList.Add(entry);
                    }
                }
                ListLoaded = true;
                Filepath = filename;
                Notify();
                return true;
            }
            catch (Exception ex)
            {
                LastErrorMsg = ex.Message;
                throw;
            }
        }

        public bool Save(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return false;
            try
            {
                File.WriteAllText(filename, JsonConvert.SerializeObject(ProductionBindingList));
                Filepath = filename;
                ListLoaded = true;
                Notify();
                return true;
            }
            catch (Exception ex)
            {
                LastErrorMsg = ex.Message;
                throw;
            }
        }

        private void Notify()
        {
            ProductionListChanged?.Invoke(ProductionBindingList);
        }

        public string GetFilename()
        {
            if (!ListLoaded || string.IsNullOrEmpty(Filepath)) return NO_FILE;
            return Path.GetFileName(Filepath);
        }

        public bool PrepareProductListRecipe()
        {
            if (ProductionBindingList.Count < 1) return false;
            var cmp = new SchematicRecipe
            {
                Key = DUData.CompoundName,
                Name = DUData.ProductionListTitle
            };
            var cnt = 0;
            foreach (var prodItem in ProductionBindingList)
            {
                if (prodItem.Quantity < 1) continue;

                if (!Calculator.CreateCloneByName(prodItem.Name, out var calc))
                    continue;
                if (calc.Recipe.Ingredients?.Any() != true || calc.Recipe.Products?.Any() != true)
                    continue;

                calc.GetTalents();

                // Add items to the overall products list
                var batchCount = 1m;
                foreach (var prod in calc.Recipe.Products)
                {
                    /* Example: Production List item: 5000 L Pure Silver
                     * Talents: lvl 3 for both refining (-15% in) and productivity (+9% out)
                     * Results (rounded):
                     *   Input                Output
                     *   6029 L Acanthite     5000 L Pure Silver
                     *                        1667 L Pure Sulfur
                     */
                    if (prod.Name.Equals(prodItem.Name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        prod.Quantity = prodItem.Quantity;
                        prod.Level = calc.Tier;
                        prod.SchemaType = calc.SchematicType;
                        prod.Mass += prodItem.Quantity * (calc.Recipe.UnitMass ?? 0);
                        prod.Volume += prodItem.Quantity * (calc.Recipe.UnitVolume ?? 0);
                        // TODO check why BatchOutput is occasionally null here
                        var batchOutput = calc.IsBatchmode ? (decimal)(calc.BatchOutput ?? 0) : prod.Quantity;
                        if (calc.CalcSchematicFromQty(prod.SchemaType, prod.Quantity, batchOutput,
                                out batchCount , out var minCost, out var _, out var _))
                        {
                            prod.SchemaQty = batchCount;
                            prod.SchemaAmt = minCost;
                        }
                        if (calc.BatchTime == null)
                        {
                            calc.BatchTime = calc.Recipe.Time * (calc.EfficencyFactor ?? 1);
                        }
                        cmp.Time += (decimal)calc.BatchTime * batchCount;
                        cmp.Products.Add(prod);
                        continue;
                    }

                    // Add Byproducts
                    if (!DUData.GetRecipeCloneByKey(prod.Type, out var rec2))
                        continue;
                    if (rec2.IsPlasma) continue;
                    prod.IsByproduct = true;
                    prod.Name += DUData.ByproductMarker;
                    prod.Quantity *= calc.OutputMultiplier;
                    prod.Quantity *= batchCount;
                    cmp.Products.Add(prod);
                }

                // this returns ingredients complete with batch sizes and times according to talents
                var ingredientsList = Calculator.GetIngredientRecipes(calc.Key, prodItem.Quantity, calc.IsOre);

                // Sum up top-level ingredients
                foreach (var ing in ingredientsList)
                {
                    // check if ingredient already exists (add/update)
                    var tmp = cmp.Ingredients.FirstOrDefault(x => x.Name.Equals(ing.Name, StringComparison.InvariantCultureIgnoreCase));
                    if (tmp != null)
                    {
                        tmp.Quantity += ing.Quantity;
                    }
                    else
                    {
                        var newIng = new ProductDetail(ing);
                        cmp.Ingredients.Add(newIng);
                    }
                }

                cnt++;
            }
            if (cnt == 0) return false;
            DUData.CompoundRecipe = cmp;

            // Add compound recipe to main recipe list, check first to remove an existing one
            DUData.Recipes.Remove(DUData.CompoundName);
            DUData.Recipes[DUData.CompoundName] = cmp;
            return true;
        }
    }

    /// <summary>
    /// Global static container for almost all DU/program related data.
    /// </summary>
    public static class DUData
    {
        #region Global Data
        public const string IndyProductsTabTitle = "Industry Products";
        public static readonly List<string> SizeList = new List<string> { "XS", "S", "M", "L", "XL" };
        public static readonly List<string> TierNames = new List<string> { "", "Basic", "Uncommon", "Advanced", "Rare", "Exotic" };
        public static readonly List<string> SectionNames = new List<string> { "Ores", "Pures", "Products", "Parts", "Schematics", "Industry", "Ingredients" };

        public static int[] ConstructSupportPriceList => new [] { 150, 375, 1000, 3000, 0 }; // Construct Support
        public static int[] CoreUnitsPriceList => new [] { 250, 5000, 62500, 725000, 0 }; // Core Units

        public static List<Ore> Ores { get; private set; }

        public static SortedDictionary<string, SchematicRecipe> Recipes { get; set; }
        public static List<string> RecipeNames { get; } = new List<string>();

        public static Dictionary<string, Group> Groups { get; private set; }
        public static SortedDictionary<string, Schematic> Schematics { get; private set; } = new SortedDictionary<string, Schematic>();
        public static int[] SchemCraftingTalents { get; set; } = new[] { 0, 0, 0, 0 };
        public static List<Talent> Talents { get; private set; } = new List<Talent>();
        public static List<string> Groupnames { get; private set; } = new List<string>(370);
        public static SortedDictionary<string, string> ItemTypeNames { get; set; }
        #endregion

        #region Production List
        ///<summary>
        ///<br>True, if CompoundRecipe is to be used as calculation target,</br>
        ///<br>which can contain any amount of items and is created with the</br>
        ///<br>help of the Production List dialogue/ribbon buttons.</br>
        ///</summary>
        public static bool FullSchematicQuantities { get; set; }
        public static bool ProductionListMode { get; set; }
        public static SchematicRecipe CompoundRecipe { get; set; }
        public static readonly string CompoundName = "COMPOUNDLIST";
        public static readonly string ProductionListTitle = "Production List";
        #endregion

        //public static readonly string IndustryTitle = "Industry";
        public static readonly string SubpartSectionTitle = "Subpart";
        public static readonly string SchematicsTitle = "Schematics";
        public static readonly string PlasmaStart = "Relic Plasma";
        public static readonly string ByproductMarker = " (B)";

        ///<summary>
        ///<br>Returns true if result was created as a clone of a recipe identified by key "recipeKey".</br>
        ///<param name="recipeKey">Unique Key of the recipe in Recipes.</param>
        ///<param name="result">Out variable containing the clone created from an existing recipe, otherwise null.</param>
        ///</summary>
        public static bool GetRecipeCloneByKey(string recipeKey, out SchematicRecipe result)
        {
            result = null;
            var tmp = Recipes.FirstOrDefault(x => x.Key.Equals(recipeKey, StringComparison.InvariantCultureIgnoreCase));
            if (tmp.Value != null)
            {
                result = (SchematicRecipe)tmp.Value.Clone();
                result.Key = tmp.Key;
                result.ParentGroupName = tmp.Value.ParentGroupName;
            }
            return result != null;
        }

        // returns a clone!
        public static bool GetRecipeCloneByName(string recipeName, out SchematicRecipe result)
        {
            result = null;
            var tmp = Recipes.FirstOrDefault(x => x.Value.Name.Equals(recipeName, StringComparison.InvariantCultureIgnoreCase));
            if (tmp.Value != null)
            {
                result = (SchematicRecipe)tmp.Value.Clone();
                result.Key = tmp.Key;
            }
            return result != null;
        }

        //public static bool GetRecipeName(string key, out string result)
        //{
        //    result = null;
        //    if (GetRecipeCloneByKey(key, out var tmp)) result = tmp.Name;
        //    return result != null;
        //}

        public static void LoadRecipes()
        {
            var json = File.ReadAllText(@"RecipesGroups.json");
            Recipes = JsonConvert.DeserializeObject<SortedDictionary<string, SchematicRecipe>>(json);
            ItemTypeNames = new SortedDictionary<string, string>();
            foreach (var entry in Recipes)
            {
                ItemTypeNames.Add(entry.Value.Name, entry.Key);
            }
        }

        public static string GetItemTypeFromName(string itemName)
        {
            var res = ItemTypeNames.FirstOrDefault(x =>
                x.Key.Equals(itemName, StringComparison.InvariantCultureIgnoreCase));
            return res.Value ?? itemName;
        }

        ///<summary>
        ///Apply cost-reduction and output-raising talents for schematics crafting.
        ///For time being (pun intended) we ignore time-reduction talents.
        ///Schematics file is to be kept "vanilla" with 0 talents,
        ///so recalculate prices and batch outputs once on startup.
        ///</summary>
        private static void ApplySchematicCraftingTalents()
        {
            // TODO
            var costFactor = 1m - DUData.SchemCraftingTalents[0] * 0.05m - DUData.SchemCraftingTalents[1] * 0.03m;
            var prodFactor = 1m + DUData.SchemCraftingTalents[2] * 0.03m + DUData.SchemCraftingTalents[3] * 0.02m;
            foreach (var item in Schematics)
            {
                item.Value.BatchSize = (int)Math.Round(item.Value.BatchSize * prodFactor, MidpointRounding.AwayFromZero);
                item.Value.BatchCost = Math.Round(item.Value.Cost * costFactor, MidpointRounding.AwayFromZero);
                item.Value.Cost = Math.Round(item.Value.BatchCost / item.Value.BatchSize, 2);
            }
        }
        
        public static void LoadSchematics()
        {
            // Schematics and prices
            // mostly by formula as of patch 0.31.6
            var loaded = false;
            if (File.Exists("schematicValues.json"))
            {
                try
                {
                    Schematics = JsonConvert.DeserializeObject<SortedDictionary<string, Schematic>>(
                                            File.ReadAllText("schematicValues.json"));
                    loaded = true;
                    ApplySchematicCraftingTalents();
                }
                catch (Exception) { }
            }
            if (!loaded)
            {
                MessageBox.Show("Required file 'schematicValues.json' not found or invalid!\r\nPlease restore from GitHub repo!");
            }
            
            // Generate (prefixed with Tx with x = tier):
            // U  = Pures, e.g. T2U
            // P  = Products, e.g. T3P
            // HU = Honeycomb Pures, e.g. T2HC
            // HP = Honeycomb Products, e.g. T3HP
            // SC = Scraps, e.g. T2SC
            // TxEy = Tier x, Element size y, e.g. T2XL tier 2, XL
            /*
            var groups = new[] { "U", "P", "HU", "HP", "SC" };
            var prices = new [] { 187.5M, 180, 125, 50, 30 }; // only first price for each group
            var scraps = new [] { 30, 75, 187.48M, 468.72M };
            var factors = new [] { 3.125M, 3.3333333M, 3.75M, 5 };
            var batchTimes = new [] { 1500, 1800, 625, 250, 150 };

            for (byte i = 0; i <= 4; i++)
            {
                var g = groups[i];
                var isHc = g.StartsWith("H");
                var isSc = g == "SC";
                var title = isSc ? "Scrap" : (g.EndsWith("U") ? "Pure" : "Product");
                title += isHc ? " Honeycomb" : (g == "SC" ? "" : " Material");
                var price = g == "SC" ? scraps[i - 1] : prices[i];
                var bTime = batchTimes[i];
                for (byte tier = 1; tier <= 5; tier++)
                {
                    var bs = (isHc || isSc) ? 25 : (60 - tier * 10);
                    if ((g == "SC" || g.EndsWith("U")) && tier < 2)
                        continue; // no schematics for T1 pures/scraps/hc-pures
                    var schem = new Schematic
                    {
                        Name = $"Tier {tier} {title} Schematic",
                        Key = $"T{tier}{g}",
                        Level = tier,
                        Cost = Math.Round(price, 2),
                        BatchSize = bs,
                        BatchTime = bTime
                    };
                    Schematics.Add(schem.Key, schem);
                    if (tier < 5)
                    {
                        bTime = (int)Math.Round(bTime * 2.5, 2);
                        price *= (g.StartsWith("H") ? 2.5M : factors[tier - 1]);
                    }
                }
            }

            // Element schematics
            prices = new [] { 375, 937.5M, 2343.7M, 5859.3M, 14648.4M };
            factors = new [] { 4.000085M, 3.3333333M, 3, 4 };
            var batchSizes = new [] { 10, 5, 3, 2, 1 };
            batchTimes = new [] { 750, 1875, 4687, 11700, 29280 }; // seconds for XS per tier 1-5
            var batchTimeFactors = new List<decimal[]>
            {
                new decimal[] { 2, 2, 2, 2 }, // factor of time between T1 sizes (XS->S->M...)
                new [] { 2.5M, 2.4997333M, 2.496266M, 2.502564M }, // factor of time between T2 sizes (XS->S->M...)
                new [] { 1.997013M, 1.997863M, 1.996805M, 2 }, // factor of time between T3 sizes (XS->S->M...)
                new [] { 2.005128M, 1.997442M, 2.001280M, 1.996161M }, // factor of time between T4 sizes (XS->S->M...)
                new [] { 2.002049M, 1.998976M, 1.996927M, 2 } // factor of time between T5 sizes (XS->S->M...)
            };
            for (byte tier = 1; tier <= 5; tier++)
            {
                var timeFactors = batchTimeFactors[tier - 1];
                var price = prices[tier - 1];
                var btime = batchTimes[tier - 1];
                for (byte size = 0; size < SizeList.Count; size++)
                {
                    var schem = new Schematic
                    {
                        Name = $"Tier {tier} {SizeList[size]} Element Schematic",
                        Key = $"T{tier}E{SizeList[size]}",
                        Level = tier,
                        Cost = Math.Round(price, 2),
                        BatchSize = batchSizes[size],
                        BatchTime = btime
                    };
                    Schematics.Add(schem.Key, schem);
                    if (size >= 4) continue;
                    btime = (int)Math.Round(btime * timeFactors[size], 2);
                    price = Math.Round(price * factors[size], 2);
                }
            }

            // Add Construct Support and Core Units schematics
            var csTime = 750;
            var batchSizesCs = new [] { 25, 20, 15, 10 };
            var batchSizesCu = new [] { 10, 5, 2, 1 };
            var batchTimesCu = new [] { 750, 5000, 25020, 145020 };
            for (var idx = 0; idx <= 3; idx++) // XS - L
            {
                var size = SizeList[idx];
                // Construct Support
                var key = "CS-" + size;
                Schematics.Add(key, new Schematic()
                {
                    Name = $"Construct Support {size} Schematic",
                    Key = key,
                    Cost = ConstructSupportPriceList[idx],
                    BatchSize = batchSizesCs[idx],
                    BatchTime = csTime
                });
                csTime *= 2;
                // Core Units
                key = "CU-" + size;
                Schematics.Add(key, new Schematic()
                {
                    Name = $"Core Unit {size} Schematic",
                    Key = key,
                    Cost = CoreUnitsPriceList[idx],
                    BatchSize = batchSizesCu[idx],
                    BatchTime = batchTimesCu[idx]
                });
            }

            // Ammo schematics, tiers T2-T4, sizes XS-L
            prices = new decimal[] { 600, 1500, 3750 }; // starting prices per tier' XS
            batchTimes = new [] { 3000, 7500, 18780 }; // starting duration per tier' XS
            for (byte i = 2; i <= 4; i++)
            {
                var time = batchTimes[i - 2];
                var price = prices[i - 2];
                foreach (var size in SizeList)
                {
                    if (size == "XL") continue;
                    var key = $"T{i}A{size}";
                    Schematics.Add(key, new Schematic()
                    {
                        Key = key,
                        Name = $"Tier {i} Ammo {size} Schematic",
                        Level = i,
                        Cost = price,
                        BatchSize = 25,
                        BatchTime = time
                    });
                    time *= 2;
                    price *= 2;
                }
            }

            // Fuel schematics
            var schema = new Schematic
            {
                Name = "Atmospheric Fuel Schematic",
                Key = "AtmoFuel",
                Level = 1,
                Cost = 60,
                BatchSize = 50,
                BatchTime = 600
            };
            Schematics.Add(schema.Key, schema);
            schema = new Schematic
            {
                Name = "Space Fuels Schematic",
                Key = "SpaceFuels",
                Level = 0,
                Cost = 150,
                BatchSize = 50,
                BatchTime = 1500
            };
            Schematics.Add(schema.Key, schema);
            schema = new Schematic
            {
                Name = "Rocket Fuels Schematic",
                Key = "RocketFuels",
                Level = 0,
                Cost = 375,
                BatchSize = 50,
                BatchTime = 3750
            };
            Schematics.Add(schema.Key, schema);
            // Standalone schematics
            schema = new Schematic
            {
                Name = "Bonsai Schematic",
                Key = "Bonsai",
                Level = 0,
                Cost = 10000000,
                BatchSize = 1,
                BatchTime = 1
            };
            Schematics.Add(schema.Key, schema);
            schema = new Schematic
            {
                Name = "Territory Unit Schematic",
                Key = "TerritoryUnit",
                Level = 0,
                Cost = 20000,
                BatchSize = 5,
                BatchTime = 19980
            };
            Schematics.Add(schema.Key, schema);
            schema = new Schematic
            {
                Name = "Warp Cell Schematic",
                Key = "WarpCell",
                Level = 3,
                Cost = 5000,
                BatchSize = 10,
                BatchTime = 10020
            };
            Schematics.Add(schema.Key, schema);
            schema = new Schematic
            {
                Name = "Warp Beacon Schematic",
                Key = "WarpBeacon",
                Level = 0,
                Cost = 5000000,
                BatchSize = 1,
                BatchTime = 1000800
            };
            Schematics.Add(schema.Key, schema);
            ApplySchematicCraftingTalents();
            SaveSchematicValues();
            */
        }

        public static void LoadOres()
        {
            var changed = false;
            if (File.Exists("oreValues.json"))
            {
                Ores = JsonConvert.DeserializeObject<List<Ore>>(File.ReadAllText("oreValues.json"));
            }
            else
            {
                changed = true;
                Ores = new List<Ore>();
                foreach (var recipe in Recipes.Where(r => r.Value.ParentGroupName == "Ore"))
                {
                    Ores.Add(new Ore()
                    {
                        Key = recipe.Key, Name = recipe.Value.Name, Value = 25 * recipe.Value.Level, Level = recipe.Value.Level
                    }); // BS some values
                }
            }

            // Add plasmas (if missing) to the ore list so a cost can be assigned
            var plasmas = new List<string>
            {
                "Relic Plasma Unus l",
                "Relic Plasma Duo l",
                "Relic Plasma Tres l",
                "Relic Plasma Quattuor l",
                "Relic Plasma Quinque l",
                "Relic Plasma Sex l",
                "Relic Plasma Septem l",
                "Relic Plasma Octo l",
                "Relic Plasma Novem l",
                "Relic Plasma Decem l"
            };
            for (var i = 1; i <= 10; i++)
            {
                var plasmaKey = $"Plasma{i}";
                if (Ores.Exists(x => x.Key == plasmaKey))
                    continue;
                Ores.Add(new Ore()
                {
                    Key = plasmaKey,
                    Name = plasmas[i - 1],
                    Value = 10000000,
                    Level = 0
                });
                changed = true;
            }

            if (changed)
            {
                SaveOreValues();
            }
        }

        public static void LoadGroups()
        {
            var json = File.ReadAllText(@"Groups.json");
            Groups = JsonConvert.DeserializeObject<Dictionary<string, Group>>(json);
            if (Recipes?.Any() == true)
            {
                foreach (var recipe in Recipes.Values) // Set parent names
                {
                    recipe.ParentGroupName = GetParentGroupName(recipe.GroupId);
                    if (!Groupnames.Contains(recipe.ParentGroupName))
                    {
                        Groupnames.Add(recipe.ParentGroupName);
                    }
                }
            }
            Groupnames.Sort();
        }

        public static void LoadTalents()
        {
            // Check if talents file already exists to load values from
            if (File.Exists("talentSettings.json"))
            {
                Talents = JsonConvert.DeserializeObject<List<Talent>>(File.ReadAllText("talentSettings.json"));
                // make sure new talents are available
                AddPureEfficiencyTalents();
                return;
            }

            // Generate Talents:
            // below is to re-generate the talents file programmatically if it was missing
            var multiplierTalentGroups = new[] { "Pure", "Scraps", "Product" }; // Setup the names of the item-specific multiplier talents

            var genericScrapTalents =
                new
                    List<Talent>() // Setup the scrap talents so we can add each scrap to their applicableRecipe later
                    {
                        new Talent() { Name = "Basic Scrap Refinery", Addition = -1, InputTalent = true },
                        new Talent() { Name = "Uncommon Scrap Refinery", Addition = -1, InputTalent = true },
                        new Talent() { Name = "Advanced Scrap Refinery", Addition = -1, InputTalent = true },
                        new Talent() { Name = "Rare Scrap Refinery", Addition = -1, InputTalent = true }
                    };

            foreach (var kvp in Recipes.Where(r => r.Value?.ParentGroupName != null &&
                                                   multiplierTalentGroups.Any(t => t == r.Value.ParentGroupName)))
            {
                // Iterate over every recipe that is part of one of the multiplierTalentGroups, "Pure", "Scraps", or "Product"
                var recipe = kvp.Value;
                var talent = new Talent() { Name = recipe.Name + " Productivity", Addition = 0, Multiplier = 0.03M }; // They all have 3% multiplier
                talent.ApplicableRecipes.Add(kvp.Key); // Each of these only applies to its one specific element
                Talents.Add(talent);
                switch (recipe.ParentGroupName)
                {
                    case "Pure":
                    case "Product":
                    {
                        // Pures and products have an input reduction of 0.03 multiplier as well as the output multiplier
                        var nameString = recipe.Name;
                        if (recipe.ParentGroupName == "Pure")
                        {
                            nameString += " Ore";
                            //if (nameString.StartsWith("Pure ")) nameString = nameString.Substring(5);
                        }
                        talent = new Talent() { Name = nameString + " Refining", Addition = 0, Multiplier = -0.03M, InputTalent = true };
                        talent.ApplicableRecipes.Add(kvp.Key);
                        Talents.Add(talent);

                        break;
                    }
                    case "Scraps":
                    {
                        // And scraps get a flat -1L general, and -2 specific
                        talent = new Talent() { Name = recipe.Name + " Scrap Refinery", Addition = -2, InputTalent = true };
                        talent.ApplicableRecipes.Add(kvp.Key);
                        Talents.Add(talent);
                        if (recipe.Level < 5) // Exotics don't have one
                            genericScrapTalents[recipe.Level - 1].ApplicableRecipes.Add(kvp.Key);

                        break;
                    }
                }
            }

            Talents.AddRange(genericScrapTalents);

            // Fuel talents
            // Generic gets -2% per level to inputs
            var genericRefineryTalent = new Talent()
                { Name = "Fuel Refinery", Addition = 0, Multiplier = -0.02M, InputTalent = true };

            foreach (var groupList in Recipes
                         .Where(r => r.Value != null && r.Value.ParentGroupName == "Fuels")
                         .GroupBy(r => r.Value.GroupId))
            {
                var groupName = Groups.Values.FirstOrDefault(g => g.Id == groupList.Key)?.Name;
                var outTalent = new Talent()
                    { Name = groupName + " Productivity", Addition = 0, Multiplier = 0.05M };
                var inTalent = new Talent()
                    { Name = groupName + " Refinery", Addition = 0, Multiplier = -0.03M, InputTalent = true };
                // Specific gets -3% per level to inputs

                // Store that everything in this group is applicable to all of these Recipes (mostly catches the kergon varieties)
                foreach (var recipe in groupList)
                {
                    outTalent.ApplicableRecipes.Add(recipe.Key);
                    inTalent.ApplicableRecipes.Add(recipe.Key);
                    genericRefineryTalent.ApplicableRecipes.Add(recipe.Key);
                }

                Talents.Add(outTalent);
                Talents.Add(inTalent);
            }

            Talents.Add(genericRefineryTalent);

            AddPureEfficiencyTalents();

            // Intermediary talents
            Talents.Add(new Talent()
            {
                Name = "Basic Intermediary Part Productivity", Addition = 1, Multiplier = 0,
                ApplicableRecipes = Recipes
                    .Where(r => r.Value != null && r.Value.ParentGroupName == "Intermediary parts" &&
                                r.Value.Level == 1).Select(r => r.Key)
                    .ToList()
            });
            Talents.Add(new Talent()
            {
                Name = "Uncommon Intermediary Part Productivity", Addition = 1, Multiplier = 0,
                ApplicableRecipes = Recipes
                    .Where(r => r.Value != null && r.Value.ParentGroupName == "Intermediary parts" &&
                                r.Value.Level == 2).Select(r => r.Key)
                    .ToList()
            });
            Talents.Add(new Talent()
            {
                Name = "Advanced Intermediary Part Productivity", Addition = 1, Multiplier = 0,
                ApplicableRecipes = Recipes
                    .Where(r => r.Value != null && r.Value.ParentGroupName == "Intermediary parts" &&
                                r.Value.Level == 3).Select(r => r.Key)
                    .ToList()
            });

            // Ammo Talents... they have uncommon, advanced of each of xs, s, m, l
            var typeList = new[] { "Uncommon", "Advanced" };
            foreach (var type in typeList)
            {
                foreach (var size in SizeList)
                {
                    if (size == "XL") continue;
                    Talents.Add(new Talent()
                    {
                        Name = type + " Ammo " + size + " Productivity", Addition = 1, Multiplier = 0,
                        ApplicableRecipes = Recipes
                            .Where(r => r.Value != null && r.Value.ParentGroupName.Contains("Ammo") &&
                                        r.Value.Level == (type == "Uncommon" ? 2 : 3) &&
                                        r.Value.Name.ToLower().EndsWith(" ammo " + size.ToLower())).Select(r => r.Key)
                            .ToList()
                    });
                }
            }
            Talents.Sort(TalentComparer);
            SaveTalents();
        }

        private static void AddPureEfficiencyTalents()
        {
            // Bas/Unc/Adv/Exo pure refinery efficiency
            var typeList = new[] { "", "Basic", "Uncommon", "Advanced", "Rare", "Exotic" };
            for (var idx = 1; idx <= 5; idx++)
            {
                if (Talents.Any(x => x.Name == typeList[idx] + " pure refinery efficiency"))
                    continue;
                Talents.Add(new Talent()
                {
                    Addition = 0,
                    Name = typeList[idx] + " pure refinery efficiency",
                    EfficiencyTalent = true,
                    Multiplier = -0.05M, // -5% time per level
                    ApplicableRecipes = Recipes.Where(
                        r => r.Value != null && r.Value.ParentGroupName == "Pure" && r.Value.Level == idx &&
                             r.Key.EndsWith("Pure")).Select(r => r.Key).ToList()
                });
            }
        }

        private static int TalentComparer(Talent x, Talent y)
        {
            return string.Compare(x.Name, y.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        public static void SaveOreValues()
        {
            try
            {
                File.WriteAllText("oreValues.json", JsonConvert.SerializeObject(Ores));
            }
            catch (Exception)
            {
                KryptonMessageBox.Show("Failed to write ore values file!", "Error",
                    MessageBoxButtons.OK, KryptonMessageBoxIcon.ERROR);
            }
        }

        public static void SaveTalents()
        {
            try
            {
                File.WriteAllText("talentSettings.json", JsonConvert.SerializeObject(Talents));
            }
            catch (Exception)
            {
                KryptonMessageBox.Show("Failed to write talents file!", "Error",
                    MessageBoxButtons.OK, KryptonMessageBoxIcon.ERROR);
            }
        }

        public static void SaveRecipes()
        {
            try
            {
                File.WriteAllText("RecipesGroups.json", JsonConvert.SerializeObject(Recipes));
            }
            catch (Exception)
            {
                KryptonMessageBox.Show("Failed to write recipes file!", "Error",
                    MessageBoxButtons.OK, KryptonMessageBoxIcon.ERROR);
            }
        }

        private static void SaveSchematicValues()
        {
            try
            {
                File.WriteAllText("schematicValues.json", JsonConvert.SerializeObject(Schematics));
            }
            catch (Exception)
            {
                KryptonMessageBox.Show("Failed to write schematics file!", "Error",
                    MessageBoxButtons.OK, KryptonMessageBoxIcon.ERROR);
            }
        }

        public static string GetElementSize(string elemName, bool noLowerTier = false)
        {
            for (var idx = 0; idx < SizeList.Count; idx++)
            {
                if (elemName.EndsWith(" " + SizeList[idx], StringComparison.InvariantCultureIgnoreCase))
                {
                    // most XL cannot be produced on an L assembler so "idx < 4" condition
                    return SizeList[(idx > 0 && idx < 4 && !noLowerTier) ? idx - 1 : idx];
                }
            }
            return "";
        }

        private static string FindParent(Guid groupId)
        {
            if (groupId == Guid.Empty) return "";
            var grp = DUData.Groups.FirstOrDefault(x => x.Value.Id == groupId);
            if (grp.Value == null || string.IsNullOrEmpty(grp.Key)) return "";
            if (grp.Key == "ConsumableDisplay" || grp.Key == "Material" || grp.Key == "Element")
            {
                return grp.Value.Name;
            }
            return grp.Value.ParentId == Guid.Empty ? grp.Value.Name : FindParent(grp.Value.ParentId);
        }

        public static string GetTopLevelGroup(string groupName)
        {
            if (string.IsNullOrEmpty(groupName)) return "";
            var grp = DUData.Groups.FirstOrDefault(x => x.Value.Name == groupName);
            if (string.IsNullOrEmpty(grp.Key)) return "";
            if (grp.Value.ParentId == Guid.Empty || grp.Value.Name == "Product")
            {
                // e.g. "Parts"
                return grp.Value.Name;
            }
            var tmp = DUData.FindParent(grp.Value.ParentId);
            return tmp;
        }

        // This was going to be recursive but those are way too generic. We just want one parent up.
        private static string GetParentGroupName(Guid id)
        {
            var group = Groups.Values.FirstOrDefault(g => g.Id == id);
            if (group == null) return null;
            return group.ParentId != Guid.Empty
                ? Groups.Values.FirstOrDefault(g => g.Id == group.ParentId)?.Name ?? "xxx"
                : group.Name;
        }

        public static bool IsIgnorableTitle(string s)
        {
            return string.IsNullOrEmpty(s) ||
                   s.StartsWith(IndyProductsTabTitle) ||
                   s.StartsWith(ProductionListTitle) ||
                   SectionNames.Contains(s);
        }

    }
}
