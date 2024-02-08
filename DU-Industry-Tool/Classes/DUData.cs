using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DU_Industry_Tool.Skills;
using Krypton.Toolkit;
using Newtonsoft.Json;

namespace DU_Industry_Tool
{
    public class FontsizeChangedEventArgs : EventArgs
    {
        public float Fontsize { get; set; }
    }

    public delegate void FontsizeChangedEventHandler(object sender, FontsizeChangedEventArgs e);

    public delegate void ProductionListHandler(object sender);

    public enum SummationType
    {
        ORES,
        PURES,
        PRODUCTS,
        PARTS,
        INGREDIENTS
    }

    /// <summary>
    /// Global static container for almost all DU/program related data.
    /// </summary>
    public static class DUData
    {
        public static IndustryMgr IndyMgrInstance { get; set; }

        #region Global Data
        private static SortedDictionary<string, string> ItemTypeNames { get; set; }
        private static readonly string _recipesFile = "RecipesGroups.json";
        private static readonly string _schematicsFile = "schematicValues.json";

        public static Color SecondaryBackColor;
        public static Color SecondaryForeColor;

        public const string IndyProductsTabTitle = "Industry Products";
        public static readonly List<string> IndustryTypesList = new List<string> { "",
            "Chemical Industry M", "Metalwork Industry M", "3D Printer M", "Glass Furnace M", "Electronics Industry M" };
        public static readonly List<string> TierNames = new List<string> { "", "Basic", "Uncommon", "Advanced", "Rare", "Exotic" };
        public static readonly List<string> SizeList = new List<string> { "XS", "S", "M", "L", "XL" };
        public static readonly List<string> SectionNames = new List<string> { "Ores", "Pures", "Products", "Parts", "Schematics", "Industry", "Ingredients" };

        //public static int[] ConstructSupportPriceList => new [] { 150, 375, 1000, 3000, 0 }; // Construct Support
        //public static int[] CoreUnitsPriceList => new [] { 250, 5000, 62500, 725000, 0 }; // Core Units

        public static List<Ore> Ores { get; private set; }

        public static SortedDictionary<string, SchematicRecipe> Recipes { get; set; }
        public static List<string> RecipeNames { get; } = new List<string>();

        public static Dictionary<string, Group> Groups { get; private set; }
        public static SortedDictionary<string, Schematic> Schematics { get; private set; } = new SortedDictionary<string, Schematic>();
        //public static List<Talent> Talents { get; set; } = new List<Talent>();
        public static List<string> Groupnames { get; private set; } = new List<string>(370);
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
            result = SchematicRecipe.GetByKey(recipeKey);
            return result != null;
        }

        // returns a clone!
        public static bool GetRecipeCloneByName(string recipeName, out SchematicRecipe result)
        {
            result = SchematicRecipe.GetByName(recipeName);
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
            var json = File.ReadAllText(_recipesFile);
            Recipes = JsonConvert.DeserializeObject<SortedDictionary<string, SchematicRecipe>>(json);
            ItemTypeNames = new SortedDictionary<string, string>();
            foreach (var entry in Recipes)
            {
                ItemTypeNames.Add(entry.Value.Name, entry.Key);
            }
        }

        ///<summary>
        ///Apply cost-reduction and output-raising talents for schematics crafting.
        ///For time being (pun intended) we ignore time-reduction talents.
        ///Schematics file is to be kept "vanilla" with 0 talents,
        ///so recalculate prices and batch outputs once on startup.
        ///</summary>
        private static void ApplySchematicCraftingTalents()
        {
            var costFactor = 1m - SchemaTalentsCache.CostOptimizationBasic * 0.05m - SchemaTalentsCache.CostOptimizationAdvanced * 0.03m;
            var prodFactor = 1m + SchemaTalentsCache.OutputProductivityBasic * 0.03m + SchemaTalentsCache.OutputProductivityAdvanced * 0.02m;
            var timeFactor = 1m - SchemaTalentsCache.ResearchTimeEfficiencyBasic * 0.03m - SchemaTalentsCache.ResearchTimeEfficiencyAdvanced * 0.02m;
            foreach (var item in Schematics)
            {
                item.Value.BatchSize = (int)Math.Round(item.Value.BatchSize * prodFactor, 2, MidpointRounding.AwayFromZero);
                item.Value.BatchCost = Math.Round(item.Value.Cost * costFactor, 2, MidpointRounding.AwayFromZero);
                item.Value.Cost = Math.Round(item.Value.BatchCost / item.Value.BatchSize, 2, MidpointRounding.AwayFromZero);
                item.Value.BatchTime = (int)Math.Round(item.Value.BatchTime * timeFactor, 2, MidpointRounding.AwayFromZero);
            }
        }
        
        public static void LoadSchematics()
        {
            // Schematics and prices
            var loaded = false;
            if (File.Exists(_schematicsFile))
            {
                try
                {
                    Schematics = JsonConvert.DeserializeObject<SortedDictionary<string, Schematic>>(
                                             File.ReadAllText(_schematicsFile));
                    loaded = true;
                    ApplySchematicCraftingTalents();
                }
                catch (Exception) { }
            }
            if (!loaded)
            {
                MessageBox.Show("Required file '"+ _schematicsFile + "' not found or invalid!\r\nPlease restore from GitHub repo!");
            }
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

        public static string GetItemTypeFromName(string itemName)
        {
            var res = ItemTypeNames.FirstOrDefault(x =>
                x.Key.Equals(itemName, StringComparison.InvariantCultureIgnoreCase));
            return res.Value ?? itemName;
        }

        public static string GetTierName(int tier)
        {
            switch (tier)
            {
                case 1: return "Basic";
                case 2: return "Uncommon";
                case 3: return "Advanced";
                case 4: return "Rare";
                case 5: return "Exotic";
                default: throw new ArgumentException("Invalid tier", nameof(tier));
            }
        }

        public static decimal GetOrePriceByName(string oreName)
        {
            if (string.IsNullOrEmpty(oreName)) return 0;
            return Ores?.FirstOrDefault(x => x.Name == oreName)?.Value ?? 0;
        }

        public static bool SaveOreValues()
        {
            try
            {
                File.WriteAllText("oreValues.json", JsonConvert.SerializeObject(Ores));
                return true;
            }
            catch (Exception e)
            {
                KryptonMessageBox.Show("Failed to write ore values file!\r\n" + e.Message,
                    "Error", KryptonMessageBoxButtons.OK, false);
            }
            return false;
        }

        public static bool SaveRecipes()
        {
            try
            {
                File.WriteAllText(_recipesFile, JsonConvert.SerializeObject(Recipes));
                return true;
            }
            catch (Exception)
            {
                KryptonMessageBox.Show("Failed to write recipes file!", "Error",
                    KryptonMessageBoxButtons.OK, false);
            }
            return false;
        }

        private static void SaveSchematicValues()
        {
            try
            {
                File.WriteAllText(_schematicsFile, JsonConvert.SerializeObject(Schematics));
            }
            catch (Exception e)
            {
                KryptonMessageBox.Show("Failed to write schematics file!\r\n"+e.Message,
                    "Error", KryptonMessageBoxButtons.OK, false);
            }
        }

        public static string GetElementSize(string elemName, bool noLowerTier = false)
        {
            if (string.IsNullOrEmpty(elemName)) return "";
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

        public static string GetIndustryType(string elemName)
        {
            if (string.IsNullOrEmpty(elemName)) return "";
            if (elemName.Contains("Assembly")) return "Assembly";
            var s = elemName.Split(' ');
            return s.Length < 2 ? "" : IndustryTypesList.FirstOrDefault(x => x.StartsWith(s[1]));
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
