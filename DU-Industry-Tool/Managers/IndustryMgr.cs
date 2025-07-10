using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DU_Industry_Tool.Skills;
using Krypton.Toolkit;

namespace DU_Industry_Tool
{
    public class IndustryMgr
    {
#if DEBUGx
        private static SortedDictionary<string, DuLuaItem> LuaSchematics; // List of schematics from du-lua.dev
        private static SortedDictionary<string, DuLuaItem> LuaItems; // List of items from du-lua.dev
        private static SortedDictionary<string, DuLuaRecipe> LuaRecipes; // List of recipes from du-lua.dev
#endif
        private readonly bool SkipProcessing = true;

        public DUDataBindings Databindings { get; } = new DUDataBindings();

        // Constructor
        public IndustryMgr()
        {
            if (!File.Exists(@"RecipesGroups.json") || !File.Exists("Groups.json"))
            {
                KryptonMessageBox.Show("Files 'RecipesGroups.json' and/or 'Groups.json' are missing!"+
                    "\r\nPlease re-download the release archive!", "Error",
                    KryptonMessageBoxButtons.OK, false);
                Application.Exit();
                return;
            }

            // On initialization, read all our data from files
            DUData.LoadRecipes();
            DUData.LoadOres();
            DUData.LoadGroups();
            TalentsManager.LoadTalentsAndValues();
            DUData.LoadSchematics();

            var dmp = false;
            var dmpRcp = false;
#region du lua dev helper
#if DEBUGx
            // Load @jericho's awesome list of ID's to be able to cross-check our DUData.Recipes
            // Item dump from https://du-lua.dev/#/items

            if (dmp && File.Exists("items_api_dump.json"))
            {
                try
                {
                    var tmpItems = JsonConvert.DeserializeObject<List<DuLuaItem>>(File.ReadAllText("items_api_dump.json"));
                    LuaItems = new SortedDictionary<string, DuLuaItem>();
                    LuaSchematics = new SortedDictionary<string, DuLuaItem>();
                    foreach (var item in tmpItems)
                    {
                        // 2023-07-22: filter out ~416 entries
                        // filter out schematics:
                        if (item.ClassId == item.DisplayClassId && item.DisplayNameWithSize.EndsWith(" Schematic Copy"))
                        {
                            Debug.WriteLine("Skip: " + item.DisplayNameWithSize);
                            LuaSchematics.Add(item.DisplayNameWithSize, item);
                            continue;
                        }

                        // this catches pretty much all categories and non-usable entries:
                        if (item.UnitMass == 0 || item.UnitVolume == 0)
                        {
                            Debug.WriteLine("Skip: " + item.DisplayNameWithSize);
                            continue;
                        }
                        if (item.ClassId == item.DisplayClassId && item.UnitMass == 0 && item.UnitVolume == 0)  // category
                        {
                            Debug.WriteLine("Skip: " + item.DisplayNameWithSize);
                            continue;
                        }
                        if (item.ClassId == "1727255352" || item.DisplayClassId == "1727255352") continue; // soils
                        if (item.ClassId == "3294944260" || item.DisplayClassId == "3294944260") continue; // build tools
                        if (item.ClassId == "1236945684" || item.DisplayClassId == "1236945684") continue; // explore tools
                        if (item.ClassId == "2332967944" || item.DisplayClassId == "2332967944") continue; // schematics
                        if (item.ClassId == "1315100172" || item.DisplayClassId == "1315100172") continue; // honeycomb materials category (has mass+volume, doh!)
                        if (item.ClassId == "809433891" || item.DisplayClassId == "809433891") continue; // minable materials categories
                        if (LuaItems.ContainsKey(item.DisplayNameWithSize))
                        {
                            Debug.WriteLine("DUPLICATE: " + item.DisplayNameWithSize);
                            continue;
                        }
                        LuaItems.Add(item.DisplayNameWithSize, item);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                dmp = false;
            }

            if (dmpRcp && File.Exists("recipes_api_dump.json"))
            {
                try
                {
                    var tmpItems = JsonConvert.DeserializeObject<List<DuLuaRecipe>>(File.ReadAllText("recipes_api_dump.json"));
                    LuaRecipes = new SortedDictionary<string, DuLuaRecipe>();
                    foreach (var item in tmpItems.Where(x => x.Products?.Count > 0 && !string.IsNullOrEmpty(x.Products[0].DisplayNameWithSize)))
                    {
                        var prdName = item.Products[0].DisplayNameWithSize;
                        if (LuaRecipes.ContainsKey(prdName))
                        {
                            Debug.WriteLine("DUPLICATE: " + prdName);
                            continue;
                        }
                        LuaRecipes.Add(prdName, item);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                dmpRcp = false;
            }
#endif
#endregion

            // Below ONLY needed by developer for recipe updates!
            // DO NOT CHANGE #if CONDITION!
#if DEBUGx
            // Populate Keys, fix products and assign schematics
            foreach (var kvp in DUData.Recipes)
            {
                kvp.Value.Key = kvp.Key;
                var itemId = kvp.Value.NqId.ToString();

                // Fix names and some other details
                if (kvp.Value.Name.Equals("Territory Unit", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Value.Name.Equals("Territory Scanner", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Value.Name.Equals("Sanctuary Territory Unit", StringComparison.InvariantCultureIgnoreCase))
                {
                    kvp.Value.Name += " xl";
                }

                if (kvp.Value.Name.Equals("Anti-Gravity Pulsor", StringComparison.InvariantCultureIgnoreCase))
                {
                    kvp.Value.Name += " m";
                }

                if (kvp.Value.Name.Equals("Emergency controller", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Value.Name.Equals("Programming board", StringComparison.InvariantCultureIgnoreCase))
                {
                    kvp.Value.Name += " xs";
                }

                var product = kvp.Value.Products?.SingleOrDefault(p => p.Type == kvp.Key);
                if (product == null && kvp.Value.Products.Count > 0)
                {
                    // Key was wrong, happens for many honeycombs
                    product = kvp.Value.Products?.First();
                }

                if (product != null)
                {
                    if (!string.IsNullOrEmpty(kvp.Value?.Name) && product.Name != kvp.Value.Name)
                    {
                        product.Name = kvp.Value.Name;
                    }

                    if (!string.IsNullOrEmpty(product.Type) && product.Type != kvp.Key)
                    {
                        product.Type = kvp.Key;
                    }
                }

                // Check against Items from du-lua.dev (optional)
                if (dmp)
                {
                    var dmpItem = Enumerable.FirstOrDefault<KeyValuePair<string, DuLuaItem>>(LuaItems, x => x.Value.Id == itemId);
                    if (dmpItem.Value != null)
                    {
                        // Transfer updateable values and only protocol to debug console
                        if (!string.IsNullOrEmpty(dmpItem.Value.DisplayNameWithSize) &&
                            kvp.Value.Name != dmpItem.Value.DisplayNameWithSize &&
                            !dmpItem.Value.DisplayNameWithSize.EndsWith("hydraulics") &&
                            !dmpItem.Value.DisplayNameWithSize.Equals("Silver Pure", StringComparison.InvariantCultureIgnoreCase) &&
                            !dmpItem.Value.DisplayNameWithSize.Equals("Nickel Pure", StringComparison.InvariantCultureIgnoreCase))
                        {
                            //Debug.WriteLine($"{kvp.Value.Name} Name to {dmpItem.Value.DisplayNameWithSize}");
                            kvp.Value.Name = dmpItem.Value.DisplayNameWithSize;
                        }

                        //if (kvp.Value.Level != dmpItem.Value.Tier)
                        //{
                        //    Debug.WriteLine($"{kvp.Value.Name} Level {kvp.Value.Level} to {dmpItem.Value.Tier}");
                        //}
                        kvp.Value.Level = dmpItem.Value.Tier;

                        if (!string.IsNullOrEmpty(dmpItem.Value.Size) && kvp.Value.Size != dmpItem.Value.Size)
                        {
                            //Debug.WriteLine($"{kvp.Value.Name} Size {kvp.Value.Size} to {dmpItem.Value.Size}");
                            kvp.Value.Size = dmpItem.Value.Size;
                        }

                        //if (kvp.Value.UnitMass != 0 && kvp.Value.UnitMass != dmpItem.Value.UnitMass)
                        //{
                        //    Debug.WriteLine($"{kvp.Value.Name} UnitMass {kvp.Value.UnitMass:N2} to {dmpItem.Value.UnitMass:N2}");
                        //}
                        kvp.Value.UnitMass = dmpItem.Value.UnitMass;

                        //if (kvp.Value.UnitVolume != 0 && kvp.Value.UnitVolume != dmpItem.Value.UnitVolume)
                        //{
                        //    Debug.WriteLine($"{kvp.Value.Name} UnitVolume {kvp.Value.UnitVolume:N2} to {dmpItem.Value.UnitVolume:N2}");
                        //}
                        kvp.Value.UnitVolume = dmpItem.Value.UnitVolume;
                    }
                    else
                    {
                        Debug.WriteLine("NqId NOT FOUND: " + itemId + " Key: " + kvp.Key);
                        itemId = kvp.Value.Id.ToString();
                        if (dmp && !LuaItems.ContainsKey(itemId))
                        {
                            Debug.WriteLine("Id NOT FOUND: " + itemId + " Key: " + kvp.Key);
                        }
                    }

                    // Special honeycomb check for wrong ID's
                    if (kvp.Key.StartsWith("hc"))
                    {
                        var luaItem = Enumerable.FirstOrDefault<KeyValuePair<string, DuLuaItem>>(LuaItems, x => x.Value.Description.StartsWith("Honeycomb") &&
                                        !x.Value.DisplayNameWithSize.EndsWith("Schematic Copy") &&
                                        x.Value.DisplayNameWithSize.Equals(kvp.Value.Name,
                                        StringComparison.InvariantCultureIgnoreCase));
                        if (!string.IsNullOrEmpty(luaItem.Key) && luaItem.Key != kvp.Value.NqId.ToString())
                        {
                            if (ulong.TryParse(luaItem.Key, out var uTmp))
                            {
                                kvp.Value.NqId = uTmp;
                                kvp.Value.Id = uTmp;
                            }
                        }
                    }
                }

                // Check against DUData.Recipes from du-lua.dev (optional)
                // and transfer usable values
                if (dmpRcp)
                {
                    var dmpItem = Enumerable.FirstOrDefault<KeyValuePair<string, DuLuaRecipe>>(LuaRecipes, x =>
                        x.Value.Products?.Count > 0 &&
                        x.Value.Products[0].Id == kvp.Value.NqId);
                    if (dmpItem.Value == null)
                    {
                        if (!kvp.Value.IsOre && !kvp.Value.IsPlasma)
                        {
                            Debug.WriteLine($"ERROR {kvp.Value.Name} recipe not found by Id in du-lua recipe file!");
                        }
                    }
                    else
                    {
                        if (kvp.Value.Id == 0 && ulong.TryParse(dmpItem.Value.Id, out var tmpId))
                        {
                            Debug.WriteLine($"{kvp.Value.Name} Id was 0, set to {dmpItem.Value.Id}");
                            kvp.Value.Id = tmpId;
                        }
                        // Transfer updateable values and only protocol to debug console
                        if (kvp.Value.Level != dmpItem.Value.Tier)
                        {
                            Debug.WriteLine($"{kvp.Value.Name} Level {kvp.Value.Level} to {dmpItem.Value.Tier}");
                        }
                        kvp.Value.Level = dmpItem.Value.Tier;

                        if ((int)kvp.Value.Time != dmpItem.Value.Time)
                        {
                            Debug.WriteLine($"{kvp.Value.Name} Time {kvp.Value.Time} to {dmpItem.Value.Time}");
                        }
                        kvp.Value.Time = dmpItem.Value.Time;

                        var tmp = dmpItem.Value.Producers?.FirstOrDefault()?.DisplayNameWithSize;
                        if (!string.IsNullOrEmpty(tmp) && kvp.Value.Industry != tmp)
                        {
                            Debug.WriteLine($"{kvp.Value.Name} Industry {kvp.Value.Industry} to {tmp}");
                            kvp.Value.Industry = tmp;
                        }

                        if (kvp.Value.Nanocraftable != dmpItem.Value.Nanocraftable)
                        {
                            Debug.WriteLine($"{kvp.Value.Name} Nanocraftable {kvp.Value.Nanocraftable} to {dmpItem.Value.Nanocraftable}");
                        }
                        kvp.Value.Nanocraftable = dmpItem.Value.Nanocraftable;

                        // Check whether ingredients have changed
                        var cnt1 = kvp.Value.Ingredients?.Count ?? 0;
                        var cnt2 = dmpItem.Value.Ingredients?.Count ?? 0;
                        if (cnt1 != cnt2)
                        {
                            Debug.WriteLine($"{kvp.Value.Name}: ingredients differ ({cnt1} vs {cnt2})!");
                        }
                        else
                        if (cnt1 > 0 && cnt2 > 0)
                        {
                            foreach (var ing in kvp.Value.Ingredients)
                            {
                                var dmpIng = dmpItem.Value.Ingredients.FirstOrDefault(x =>
                                    x.DisplayNameWithSize.Equals(ing.Name,
                                        StringComparison.InvariantCultureIgnoreCase));
                                // ingredient exists?
                                if (dmpIng == null)
                                {
                                    Debug.WriteLine($"{kvp.Value.Name}.{ing.Name}: ingredient not listed!");
                                }
                                else
                                // ingredient quantity changed?
                                if (dmpIng.Quantity != ing.Quantity)
                                {
                                    Debug.WriteLine($"{kvp.Value.Name}.{ing.Name}: ingredient has wrong quantity (old: {ing.Quantity}, new: {dmpIng.Quantity})!");
                                }
                            }
                        }
                        else
                        {
                            Debug.WriteLine($"{kvp.Value.Name}: ingredients missing!");
                        }
                    }
                }

                if ((kvp.Key.StartsWith("Catalyst") ||
                     kvp.Value.ParentGroupName == "Ore" ||
                     (kvp.Value.ParentGroupName == "Pure" && kvp.Value.Level <= 1))
                    && (kvp.Value.SchemaPrice != 0m || !string.IsNullOrEmpty(kvp.Value.SchemaType)))
                {
                    kvp.Value.SchemaPrice = 0;
                    kvp.Value.SchemaType = null;
                    continue;
                }

                if (string.IsNullOrEmpty(kvp.Value.ParentGroupName) || kvp.Value.GroupId == Guid.Empty)
                {
                    kvp.Value.ParentGroupName = "INVALID";
                    var err = kvp.Value.Name + ": ParentGroupName/GroupId missing!";
                    Console.WriteLine(err);
                    continue;
                }

                // LOTS of code below to cleanup old data, which might be removed again eventually --tobitege
                /*
                // Preset "ParentGroupName" and "GroupId" for containers
                if (kvp.Key.StartsWith("chemicalcont", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Key.StartsWith("combustionchamber", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Key.StartsWith("electricengine", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Key.StartsWith("firingsystem", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Key.StartsWith("gazcylinder", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Key.StartsWith("laserchamber", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Key.StartsWith("light_", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Key.StartsWith("magneticrail", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Key.StartsWith("motherboard", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Key.StartsWith("orescanner", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Key.StartsWith("powerconvertor", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Key.StartsWith("roboticarm", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Key.StartsWith("silo_", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Key.StartsWith("controlsystem", StringComparison.InvariantCultureIgnoreCase))
                {
                    kvp.Value.ParentGroupName = "Functional Parts";
                    kvp.Value.GroupId = new Guid("08d8a31f-5127-4f25-8138-779a7f0e5c8d");
                }
                else if (kvp.Key.StartsWith("ammocontainer", StringComparison.InvariantCultureIgnoreCase))
                {
                    kvp.Value.ParentGroupName = "Containers";
                    kvp.Value.GroupId = new Guid("08d8a31f-4fcc-40fe-8e92-9fd90394d5c2");
                }
                else if (kvp.Key.StartsWith("missioncontainer", StringComparison.InvariantCultureIgnoreCase))
                {
                    kvp.Value.ParentGroupName = "Containers";
                    kvp.Value.GroupId = new Guid("08d8a31f-4fcc-40fe-8e92-9fd90394d5c3");
                }
                else if (kvp.Key.StartsWith("antimattercapsule", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Key.StartsWith("igniter", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Key.StartsWith("processor", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Key.StartsWith("warhead_", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Key.StartsWith("singularitycontainer", StringComparison.InvariantCultureIgnoreCase)
                    )
                {
                    kvp.Value.ParentGroupName = "Complex parts";
                    kvp.Value.GroupId = new Guid("08d8a31f-5116-4472-84d8-1cf2de11b3a3");
                }
                else if (kvp.Key.StartsWith("antimattercore", StringComparison.InvariantCultureIgnoreCase))
                {
                    kvp.Value.ParentGroupName = "Exceptional parts";
                    kvp.Value.GroupId = new Guid("08d8a31f-511c-4062-8bcb-fd56f993d7ab");
                }
                else if (kvp.Key.IndexOf("container", StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    kvp.Value.ParentGroupName = "Containers";
                    kvp.Value.GroupId = new Guid("08d8a31f-4ff1-4b54-8484-9d05d3885b52");
                }
                else if (
                    kvp.Value.Name.IndexOf("hover engine ", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                    kvp.Value.Name.IndexOf("vertical booster ", StringComparison.InvariantCultureIgnoreCase) >= 0
                    )
                {
                    kvp.Value.ParentGroupName = "Ground Engines";
                    kvp.Value.GroupId = new Guid("08d8a31f-5034-42d4-84e8-d23f90cc84af");
                    var elSize = GetElementSize(kvp.Value.Name);
                    if (elSize != "")
                    {
                        kvp.Value.SchemaType = "CS-" + elSize;
                        kvp.Value.SchemaPrice = Schematics.FirstOrDefault(x =>
                                x.Key.Equals(kvp.Value.SchemaType)).Value.Cost;
                    }
                }
                else
                if (kvp.Key.StartsWith("industry", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (kvp.Value.Level < 1) kvp.Value.Level = 1;
                    kvp.Value.ParentGroupName = "Industry";
                    var grp = Groups.FirstOrDefault(x => x.Key == "Industry" + kvp.Value.Level);
                    if (grp.Value?.Id != null)
                    {
                        kvp.Value.GroupId = grp.Value.Id;
                    }
                }
                */

                // Determine the required industry to produce this item
                //if (string.IsNullOrEmpty(kvp.Value.Industry))
                //{
                //    DetermineIndustryFor(kvp.Value);
                //}

                //if (!string.IsNullOrEmpty(kvp.Value.SchemaType))
                //{
                //    var r = DUData.Schematics.FirstOrDefault(x => x.Key == kvp.Value.SchemaType);
                //    if (r.Value != null && kvp.Value.SchemaPrice != r.Value.Cost)
                //    {
                //        Console.WriteLine(kvp.Value.Name + $" schema cost fix {kvp.Value.SchemaPrice} to {r.Value.Cost}");
                //        kvp.Value.SchemaPrice = r.Value.Cost;
                //    }
                //}

                // Skip further processing on some conditions...
                if (SkipProcessing ||
                    (!string.IsNullOrEmpty(kvp.Value.SchemaType) && kvp.Value.SchemaPrice > 0) ||
                    kvp.Value.ParentGroupName.EndsWith("parts", StringComparison.InvariantCultureIgnoreCase) ||
                    kvp.Value.Level < 1 ||
                    kvp.Value.Name.StartsWith("catalyst", StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                var idx = "";
                var isElement = false;
                var parentName = kvp.Value.ParentGroupName;

                if (kvp.Key.IndexOf("deep space asteroid", StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    var r = DUData.Schematics.First(x => x.Key == "T1EXL");
                    kvp.Value.SchemaType = r.Key;
                    kvp.Value.SchemaPrice = r.Value.Cost;
                    continue;
                }

                if (kvp.Key.Equals("WarpCellStandard", StringComparison.InvariantCultureIgnoreCase))
                {
                    var r = DUData.Schematics.First(x => x.Key == "WarpCell");
                    kvp.Value.SchemaType = r.Key;
                    kvp.Value.SchemaPrice = r.Value.Cost;
                    continue;
                }

                if (kvp.Key.Equals("WarpBeacon", StringComparison.InvariantCultureIgnoreCase))
                {
                    var r = DUData.Schematics.First(x => x.Key == "WarpBeacon");
                    kvp.Value.SchemaType = r.Key;
                    kvp.Value.SchemaPrice = r.Value.Cost;
                    continue;
                }

                if (kvp.Key.Equals("Bonsai", StringComparison.InvariantCultureIgnoreCase))
                {
                    var r = DUData.Schematics.First(x => x.Key == "Bonsai");
                    kvp.Value.Level = 5;
                    kvp.Value.SchemaType = r.Key;
                    kvp.Value.SchemaPrice = r.Value.Cost;
                    continue;
                }

                // Core Units
                if (true == parentName?.Equals("core units", StringComparison.InvariantCultureIgnoreCase))
                {
                    var size = DUData.GetElementSize(kvp.Value.Name);
                    idx = "CU-" + size;
                    kvp.Value.SchemaType = idx;
                    kvp.Value.SchemaPrice = DUData.ConstructSupportPriceList[DUData.SizeList.IndexOf(size)];
                    continue;
                }

                // Ammo
                if (kvp.Value.Level > 1 && parentName != null &&
                    parentName.IndexOf(" ammo", StringComparison.InvariantCultureIgnoreCase) > 0)
                {
                    idx = "A" + DUData.GetElementSize(kvp.Value.Name);
                }

                if (idx == "")
                {
                    var skipGroups = new List<string>
                    {
                        "Displays",
                        "Decorative Element",
                        " parts",
                        "Logic Operators"
                    };
                    if (parentName != null && skipGroups.Any(x => parentName.EndsWith(x, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        continue;
                    }

                    if (idx == "" && DUData.GetTopLevelGroup(kvp.Value.ParentGroupName) == "Elements")
                    {
                        isElement = true;
                    }

                    var elemGroups = new List<string>
                    {
                        "Atmospheric Engines",
                        "Cannons",
                        "Containers",
                        "Control Units",
                        "Electronics",
                        "Engines",
                        "Ground Engines",
                        "High-Tech Transportation",
                        "Industry",
                        "Laser Ammo M",
                        "Lasers",
                        "Mining Units",
                        "Missile Pods",
                        "Radar",
                        "Railguns",
                        "Relic Plasmas",
                        "Screens",
                        "Sensors",
                        "Shield Generators",
                        "Space Engines",
                        "Space Radars",
                        "Support Tech",
                        "Triggers",
                    };

                    // Construct Support
                    var csGroups = new List<string>
                    {
                        "Adjustor",
                        "Aileron",
                        "Atmospheric Brakes",
                        "Compact Aileron",
                        "Space Brakes",
                        "Stabilizer",
                        "Wing",
                    };
                    if (csGroups.Any(x => kvp.Value.Name.StartsWith(x, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        var size = DUData.GetElementSize(kvp.Value.Name);
                        idx = "CS-" + size;
                        kvp.Value.SchemaType = idx;
                        kvp.Value.SchemaPrice = DUData.CoreUnitsPriceList[DUData.SizeList.IndexOf(size)];
                        continue;
                    }

                    if (isElement ||
                        elemGroups.Any(x => x.Equals(parentName, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        idx = "E" + DUData.GetElementSize(kvp.Value.Name);
                    }
                    else if (parentName.Equals("Product", StringComparison.InvariantCultureIgnoreCase))
                    {
                        idx = "P";
                    }
                    else if ((parentName.Equals("Ore", StringComparison.InvariantCultureIgnoreCase) ||
                              parentName.Equals("Pure", StringComparison.InvariantCultureIgnoreCase)))
                    {
                        if (kvp.Value.Level <= 1)
                        {
                            kvp.Value.SchemaType = null;
                            kvp.Value.SchemaPrice = 0;
                            continue; // no T1
                        }

                        idx = "U";
                    }
                    else if (parentName.Equals("Scraps", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (kvp.Value.Level <= 1)
                        {
                            kvp.Value.SchemaType = null;
                            kvp.Value.SchemaPrice = 0;
                            continue; // no T1
                        }

                        idx = "SC";
                    }
                    else if (parentName.StartsWith("Product Honeycomb", StringComparison.InvariantCultureIgnoreCase))
                    {
                        idx = "HP";
                    }
                    else if (parentName.StartsWith("Pure Honeycomb", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (kvp.Value.Level <= 1)
                        {
                            kvp.Value.SchemaType = null;
                            kvp.Value.SchemaPrice = 0;
                            continue; // no T1
                        }

                        idx = "HU";
                    }
                    else if (parentName == "Fuels")
                    {
                        if (kvp.Key.StartsWith("Kergon", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var schemaClass = DUData.Schematics.FirstOrDefault(x => x.Key == "SpaceFuels");
                            kvp.Value.SchemaType = schemaClass.Key;
                            kvp.Value.SchemaPrice = schemaClass.Value.Cost;
                        }
                        else if (kvp.Key.Equals("Nitron", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var schemaClass = DUData.Schematics.FirstOrDefault(x => x.Key == "AtmoFuel");
                            kvp.Value.SchemaType = schemaClass.Key;
                            kvp.Value.SchemaPrice = schemaClass.Value.Cost;
                        }
                        else if (kvp.Key.Equals("Xeron", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var schemaClass = DUData.Schematics.FirstOrDefault(x => x.Key == "RocketFuels");
                            kvp.Value.SchemaType = schemaClass.Key;
                            kvp.Value.SchemaPrice = schemaClass.Value.Cost;
                        }
                        continue;
                    }
                }

                // Apply schematic price if T1+ product or T2+ pure
                if (idx != ""
                    && kvp.Value.ParentGroupName != "Decorative Element"
                    && !kvp.Value.ParentGroupName.EndsWith(" Parts", StringComparison.InvariantCultureIgnoreCase))
                {
                    var schemaClass = DUData.Schematics.FirstOrDefault(x => x.Key == $"T{kvp.Value.Level}{idx}");
                    if (schemaClass.Value == null) continue;
                    kvp.Value.SchemaType = schemaClass.Key;
                    kvp.Value.SchemaPrice = schemaClass.Value.Cost;
                    Console.WriteLine($@"{kvp.Value.Name} = {kvp.Value.SchemaPrice} ({parentName})");
                }
                else if (kvp.Value.ParentGroupName == "Refined Materials" ||
                         (kvp.Value.Level >= 1 &&
                          kvp.Value.Name.EndsWith("product", StringComparison.InvariantCultureIgnoreCase)))
                {
                    var schemaClass = DUData.Schematics.FirstOrDefault(x => x.Key == $"T{kvp.Value.Level}P");
                    if (schemaClass.Value == null) continue;
                    kvp.Value.SchemaType = schemaClass.Key;
                    kvp.Value.SchemaPrice = schemaClass.Value.Cost;
                    Console.WriteLine($@"{kvp.Value.Name} = {kvp.Value.SchemaPrice} ({parentName})");
                }
            }

            if (dmp)
            {
                /*
                // Ammo types output
                List<string> ammoCategories = new List<string> {
                    "326757369",  "3336558558", "399761377",  "512435856",  // Tier 2
                    "2413250793", "1705420479", "3125069948", "2913149958", // Tier 3
                    "2293088862", "3636126848", "3847207511", "2557110259", // Tier 4
                };
                foreach (var aCat in ammoCategories)
                {
                    Debug.WriteLine(Utils.LuaItems[aCat].DisplayNameWithSize);
                    var catEntries = Utils.LuaItems.Where(x => x.Value.Schematics.Count > 0 &&
                                                          x.Value.Schematics[0].Id == aCat).Select(x => x.Key).ToList();
                    var entries = DUData.Recipes.Where(x => catEntries.Contains(x.Value.NqId.ToString())).OrderBy(x => x.Key);
                    foreach (var kvp in entries)
                    {
                        Debug.WriteLine(kvp.Key);
                        //Debug.WriteLine(kvp.Value.Name);
                    }
                    Debug.WriteLine("");
                }
                */
                var missingIds = new StringBuilder();
                var idLine = "";
                var idCount = 0;
                foreach (var kvp in LuaItems)
                {
                    // Exclusion checks
                    if (kvp.Value.DisplayNameWithSize.EndsWith("Schematic Copy")) continue; // schematics
                    if (kvp.Value.DisplayNameWithSize.StartsWith("Art deco")) continue; // reward
                    if (kvp.Value.DisplayNameWithSize.StartsWith("Model of")) continue; // reward
                    if (kvp.Value.DisplayNameWithSize.StartsWith("Sanctuary Territory Unit")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("The Andromeda")) continue; // reward
                    if (kvp.Value.DisplayNameWithSize.Equals("The Circinus")) continue; // reward
                    if (kvp.Value.DisplayNameWithSize.Equals("The Milky Way")) continue; // reward
                    if (kvp.Value.DisplayNameWithSize.Equals("The Triangulum")) continue; // reward
                    if (kvp.Value.DisplayNameWithSize.Equals("The Whirlpool")) continue; // reward
                    if (kvp.Value.DisplayNameWithSize.Equals("Thoramine")) continue; // not in game
                    if (kvp.Value.DisplayNameWithSize.Equals("Scanner result")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Package")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Information")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Market Pod")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Gravity changer")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Alarm light")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Speaker")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Weapon")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Battery")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Smoke")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Scanner")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Deployable light orb")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Deploy construct")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Alarm speaker",
                            StringComparison.InvariantCultureIgnoreCase)) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Construct key")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Territory key")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Cryogenic pod m")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Conveyor belt")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Fake Core Unit")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Teleportation Node")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Deploy ground Element")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Mission Reserved Space")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Directional detector")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.Equals("Electricity generator")) continue; // not in game
                    if (kvp.Value.DisplayNameWithSize.EndsWith(" tool", StringComparison.InvariantCultureIgnoreCase))
                        continue; // internal
                    if (kvp.Value.DisplayNameWithSize.EndsWith(" Parts")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.EndsWith(" 256m")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.EndsWith(" 512m")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.EndsWith(" 1024m")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.EndsWith(" 2048m")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.EndsWith(" 4096m")) continue; // internal
                    if (kvp.Value.DisplayNameWithSize.StartsWith("Admin ")) continue; // not in game
                    if (kvp.Value.DisplayNameWithSize.StartsWith("Alien Space Core")) continue; // not in game
                    if (kvp.Value.DisplayNameWithSize.StartsWith("Basic Light")) continue; // not in game
                    if (kvp.Value.DisplayNameWithSize.StartsWith("Artifact ")) continue; // not in game
                    if (kvp.Value.DisplayNameWithSize.StartsWith("Repulsor")) continue; // not in game
                    if (kvp.Value.DisplayNameWithSize.StartsWith("Deprecated")) continue; // not in game
                    if (kvp.Value.DisplayNameWithSize.StartsWith("Decorative Territory Unit")) continue; // not in game
                    if (kvp.Value.DisplayNameWithSize.StartsWith("Small depracated decorative gun"))
                        continue; // not in game
                    if (kvp.Value.DisplayNameWithSize.EndsWith("Teleportation Node")) continue; // not in game
                    if (kvp.Value.Description.StartsWith("This is a non-valuable")) continue; // world material
                    if (kvp.Value.Description.StartsWith("Deprecated")) continue; // Deprecated
                    if (kvp.Value.DisplayNameWithSize.StartsWith("Firework")) continue; // Fireworks
                    if (kvp.Value.DisplayNameWithSize.StartsWith("Obsidian")) continue; // Reward
                    if (kvp.Value.DisplayNameWithSize.StartsWith("Art-deco")) continue; // not in game
                    if (kvp.Value.Description.StartsWith("Admin")) continue; // internal stuff
                    if (kvp.Value.DisplayNameWithSize.Contains("Hologram")) continue; // reward
                    if (kvp.Value.DisplayNameWithSize.EndsWith("Mission Package")) continue; // missions item
                    if (kvp.Value.DisplayNameWithSize.Contains("deprecated")) continue; // Deprecated
                    if (kvp.Value.DisplayNameWithSize.Contains("Throne")) continue; // Reward
                    if (kvp.Value.DisplayNameWithSize.Contains("Blueprint")) continue; // internal

                    var itemId = kvp.Value.Id;
                    var item = DUData.Recipes.Values.FirstOrDefault(x => x.NqId.ToString() == itemId);
                    if (item != null)
                        continue;
                    var rec = DUData.Recipes.FirstOrDefault(x =>
                        x.Value.Name.Equals(kvp.Value.DisplayNameWithSize,
                            StringComparison.InvariantCultureIgnoreCase));
                    if (rec.Key != null && rec.Value?.NqId.ToString() != kvp.Value.Id)
                    {
                        if (ulong.TryParse(kvp.Value.Id, out var uTmp))
                        {
                            rec.Value.NqId = uTmp;
                            var luaRcp = Enumerable.FirstOrDefault<KeyValuePair<string, DuLuaRecipe>>(LuaRecipes, x =>
                                x.Value.Products != null &&
                                x.Value.Products[0].Id == uTmp);
                            if (luaRcp.Value != null && ulong.TryParse(luaRcp.Value.Id, out uTmp))
                            {
                                rec.Value.Id = uTmp;
                            }
                        }
                        continue;
                    }

                    idLine += itemId + ", ";
                    idCount++;
                    if (idCount > 5)
                    {
                        idCount = 0;
                        missingIds.AppendLine(idLine);
                        idLine = "";
                    }

                    Debug.WriteLine("NqId NOT IN DUData.Recipes: " + itemId + " Key: " + kvp.Value.DisplayNameWithSize);
                }

                Debug.WriteLine(missingIds.ToString());
            }

            if (dmpRcp)
            {
                var funcPartsId = DUData.Groups["FunctionalPart"].Id;
                var missingRec = Enumerable.Where<KeyValuePair<string, DuLuaRecipe>>(LuaRecipes, x => x.Value.Products?.Count > 0 && 
                                 DUData.Recipes.Values.All(y => y.NqId != x.Value.Products[0].Id))
                    .OrderBy(x => x.Value.Products[0].DisplayNameWithSize);
                foreach (var missing in missingRec.ToList())
                {
                    var prodname = missing.Value.Products[0].DisplayNameWithSize;

                    // Skip items that are not selectable ingame by players
                    if ((prodname.StartsWith("Advanced Screen", StringComparison.InvariantCulture) &&
                         !prodname.EndsWith(" xs", StringComparison.InvariantCultureIgnoreCase)) ||
                        prodname.StartsWith("Admin", StringComparison.InvariantCulture) ||
                        prodname.StartsWith("Alarm", StringComparison.InvariantCulture) ||
                        prodname.StartsWith("Basic Antenna s", StringComparison.InvariantCulture) ||
                        prodname.StartsWith("Deployable light orb", StringComparison.InvariantCulture) ||
                        prodname.StartsWith("Market Core", StringComparison.InvariantCulture) ||
                        prodname.StartsWith("Medium Dispenser m", StringComparison.InvariantCulture) ||
                        prodname.StartsWith("Light Detector", StringComparison.InvariantCulture) ||
                        prodname.StartsWith("Lithium Cobaltate", StringComparison.InvariantCulture) ||
                        prodname.StartsWith("Lithium niobate", StringComparison.InvariantCulture) ||
                        (prodname.IndexOf(" Optical Sensor ", StringComparison.InvariantCulture) > -1) ||
                        prodname.StartsWith("Deprecated ", StringComparison.InvariantCulture) ||
                        (prodname.IndexOf(" deprecated ", StringComparison.InvariantCulture) > -1) ||
                        prodname.EndsWith(" Parts", StringComparison.CurrentCultureIgnoreCase) ||
                        prodname.StartsWith("Sulfur Acid", StringComparison.InvariantCulture) ||
                        prodname.Equals("Radar s", StringComparison.CurrentCultureIgnoreCase) ||
                        prodname.Equals("Radar m", StringComparison.CurrentCultureIgnoreCase) ||
                        prodname.Equals("Radar l", StringComparison.CurrentCultureIgnoreCase) ||
                        false)
                    {
                        continue;
                    }

                    if (!DUData.Recipes.ContainsKey(prodname))
                    {
                        var newRecipe = new SchematicRecipe(missing.Value);
                        if (newRecipe.Key != "INVALID")
                        {
                            newRecipe.NqId = missing.Value.Products[0].Id;
                            if (ulong.TryParse(missing.Value.Id, out var tmp))
                            {
                                newRecipe.Id = tmp;
                            }
                            else
                            {
                                // no valid Id? skip it!
                                continue;
                            }

                            // Cross-check with Items-Dump - if exists - to get extra attributes
                            var isPart = false;
                            if (dmp)
                            {
                                var dmpItem = Enumerable.FirstOrDefault<KeyValuePair<string, DuLuaItem>>(LuaItems, x => x.Value.Id == newRecipe.NqId.ToString());
                                if (dmpItem.Value != null)
                                {
                                    newRecipe.UnitMass = dmpItem.Value.UnitMass;
                                    newRecipe.UnitVolume = dmpItem.Value.UnitVolume;
                                    newRecipe.Size = dmpItem.Value.Size;
                                    newRecipe.Level = dmpItem.Value.Tier;
                                    isPart = (dmpItem.Value.Description.IndexOf(" parts are ", StringComparison.InvariantCultureIgnoreCase) != -1);
                                    if (isPart)
                                    {
                                        var firstWord = dmpItem.Value.Description.Substring(0, dmpItem.Value.Description.IndexOf(" ", StringComparison.InvariantCulture));
                                        newRecipe.ParentGroupName = firstWord + " Parts";
                                        var grpId = DUData.Groups.First(x => x.Value.Name.Equals(newRecipe.ParentGroupName, StringComparison.InvariantCultureIgnoreCase)).Value.Id;
                                        newRecipe.GroupId = grpId;
                                    }
                                    else
                                    if (dmpItem.Value.Schematics?.Count < 1)
                                    {
                                        // ??
                                    }
                                    else
                                    {
                                        var schem = dmpItem.Value.Schematics[0].DisplayNameWithSize;
                                        schem = schem.Replace(" Schematic Copy", "");
                                        newRecipe.SchemaType = $"T{newRecipe.Level}";
                                        // TODO
                                        if (schem.IndexOf("Product Honeycomb", StringComparison.CurrentCultureIgnoreCase) != -1)
                                        {
                                            newRecipe.SchemaType += "HP";
                                            newRecipe.SchemaPrice = 50;
                                        }
                                        else
                                        if (schem.IndexOf("Pure Honeycomb", StringComparison.CurrentCultureIgnoreCase) != -1)
                                        {
                                            newRecipe.SchemaType += "HU";
                                            newRecipe.SchemaPrice = 50;
                                        }
                                    }
                                }
                            }

                            var isIngred = Enumerable.Any<KeyValuePair<string, DuLuaRecipe>>(LuaRecipes, x => 
                                x.Value.Ingredients != null &&
                                Enumerable.Any<DuLuaSubItem>(x.Value.Ingredients, y => y.Id == newRecipe.NqId)) == true;
                            // IF it is a part, skip it if it is not an ingredient anywhere (not ingame parts!)
                            if (isPart && !isIngred)
                            {
                                Debug.WriteLine("***** Skipped part '" + prodname + "' -> not an ingredient.");
                                continue;
                            }

                            if (isIngred)
                            {
                                Debug.WriteLine("***** Ingredient: " + prodname);
                            }
                            if (
                                 (newRecipe.Name.IndexOf(" Antenna ", StringComparison.InvariantCulture) > -1) ||
                                 (newRecipe.Name.IndexOf(" Control System ", StringComparison.InvariantCulture) > -1) ||
                                 (newRecipe.Name.IndexOf(" Core System ", StringComparison.InvariantCulture) > -1) ||
                                 (newRecipe.Name.IndexOf(" Light ", StringComparison.InvariantCulture) > -1) ||
                                 (newRecipe.Name.IndexOf(" Mechanical Sensor ", StringComparison.InvariantCulture) > -1) ||
                                 (newRecipe.Name.IndexOf(" Motherboard ", StringComparison.InvariantCulture) > -1) ||
                                 (newRecipe.Name.IndexOf(" Ore Scanner ", StringComparison.InvariantCulture) > -1) ||
                                 (newRecipe.Name.IndexOf(" Power Trans", StringComparison.InvariantCulture) > -1) ||
                                 newRecipe.Name.StartsWith("Uncommon Screen", StringComparison.InvariantCulture) ||
                                 false
                                )
                            {
                                newRecipe.GroupId = funcPartsId;
                                newRecipe.ParentGroupName = "Functional Parts";
                                //newRecipe.Industry = "Electronics Industry M";
                            }
                            else
                            if ( 
                                 (newRecipe.Name.IndexOf(" Firing System ", StringComparison.InvariantCulture) > -1) ||
                                 false
                                )
                            {
                                newRecipe.GroupId = funcPartsId;
                                newRecipe.ParentGroupName = "Functional Parts";
                                //newRecipe.Industry = "Metalwork Industry M";
                            }
                            else
                            if (newRecipe.Name.IndexOf(" Casing ", StringComparison.InvariantCulture) > -1)
                            {
                                //newRecipe.Industry = "Basic 3D Printer M";
                            }
                            else
                            if (
                                newRecipe.Name.Equals("Empty Shelf s", StringComparison.InvariantCulture) ||
                                newRecipe.Name.Equals("Half-Full Shelf s", StringComparison.InvariantCulture) ||
                                false)
                            {
                                newRecipe.GroupId = new Guid("08d8a31f-4f99-4d0e-8eef-88178c97ce38");
                                newRecipe.ParentGroupName = "Decorative Element";
                                //newRecipe.Industry = "Basic Assembly Line xs";
                            }
                            else
                            if (newRecipe.Name.IndexOf("Modern ", StringComparison.InvariantCultureIgnoreCase) != -1)
                            {
                                var isTransp = (newRecipe.Key.IndexOf("Modern Transp",
                                    StringComparison.InvariantCultureIgnoreCase) != -1);
                                var keyname = "modern" + (isTransp ? "transparent" : "") + "screen_2_";
                                //newRecipe.Industry = "Basic Assembly Line";
                                if (newRecipe.Key.EndsWith(" xxxl", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    newRecipe.Industry += " XL";
                                    newRecipe.SchemaType = "T2EXL";
                                    newRecipe.SchemaPrice = 150000;
                                    newRecipe.Key = keyname + "xl3";
                                }
                                else
                                if (newRecipe.Key.EndsWith(" xxl", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    newRecipe.Industry += " L";
                                    newRecipe.SchemaType = "T2EXL";
                                    newRecipe.SchemaPrice = 150000;
                                    newRecipe.Key = keyname + "xl2";
                                }
                                else
                                if (newRecipe.Key.EndsWith(" xl", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    newRecipe.Industry += " M";
                                    newRecipe.SchemaType = "T2EXL";
                                    newRecipe.SchemaPrice = 150000;
                                    newRecipe.Key = keyname + "xl";
                                }
                                else
                                if (newRecipe.Key.EndsWith(" l", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    newRecipe.Industry += " S";
                                    newRecipe.SchemaType = "T2EL";
                                    newRecipe.SchemaPrice = 37500;
                                    newRecipe.Key = keyname + "l";
                                }
                                else
                                if (newRecipe.Key.EndsWith(" m", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    newRecipe.Industry += " XS";
                                    newRecipe.SchemaType = "T2EM";
                                    newRecipe.SchemaPrice = 12500;
                                    newRecipe.Key = keyname + "m";
                                }
                                else
                                if (newRecipe.Key.EndsWith(" s", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    newRecipe.Industry += " XS";
                                    newRecipe.SchemaType = "T2ES";
                                    newRecipe.SchemaPrice = 3750;
                                    newRecipe.Key = keyname + "s";
                                }
                                else
                                {
                                    newRecipe.Industry += " XS";
                                    newRecipe.SchemaType = "T2EXS";
                                    newRecipe.SchemaPrice = 937.5m;
                                    newRecipe.Key = keyname + "xs";
                                }

                                newRecipe.Products[0].Type = newRecipe.Key;
                                newRecipe.GroupId = new Guid("08d8a31f-4fa9-43d5-838b-5faf295bf7b2");
                                newRecipe.ParentGroupName = "Screens";
                            }
                            else
                            if ( 
                                 (newRecipe.Name.IndexOf(" Chemical Container ", StringComparison.InvariantCulture) > -1) ||
                                 (newRecipe.Name.IndexOf(" Combustion Chamber ", StringComparison.InvariantCulture) > -1) ||
                                 (newRecipe.Name.IndexOf(" Electric Engine ", StringComparison.InvariantCulture) > -1) ||
                                 (newRecipe.Name.IndexOf(" Gas Cylinder ", StringComparison.InvariantCulture) > -1) ||
                                 (newRecipe.Name.IndexOf(" Magnetic Rail ", StringComparison.InvariantCulture) > -1) ||
                                 (newRecipe.Name.IndexOf(" Missile Silo ", StringComparison.InvariantCulture) > -1) ||
                                 (newRecipe.Name.IndexOf(" Robotic Arm ", StringComparison.InvariantCulture) > -1) ||
                                 false
                                 )
                            {
                                newRecipe.GroupId = new Guid("08d8a31f-5127-4f25-8138-779a7f0e5c8d");
                                newRecipe.ParentGroupName = "Functional parts";
                                //newRecipe.Industry = "Metalworks";
                            }
                            else
                            if ( 
                                 (newRecipe.Name.IndexOf(" pattern ", StringComparison.InvariantCulture) > -1) ||
                                 newRecipe.Name.EndsWith(" plastic", StringComparison.InvariantCulture) ||
                                 newRecipe.Name.EndsWith("(cold)", StringComparison.InvariantCulture) ||
                                 false
                                 )
                            {
                                newRecipe.SchemaType = "T1HP";
                                newRecipe.SchemaPrice = 50;
                                newRecipe.GroupId = new Guid("08d8a31f-508e-41db-8c89-308e391f5508");
                                newRecipe.ParentGroupName = "Product Honeycomb Materials";
                                //newRecipe.Industry = "Basic Honeycomb Refinery M";
                            }
                            else
                            if ( 
                                 newRecipe.Name.StartsWith("Luminescent", StringComparison.InvariantCulture) ||
                                 false
                                 )
                            {
                                newRecipe.SchemaType = "T1HP";
                                newRecipe.SchemaPrice = 50;
                                newRecipe.GroupId = new Guid("08d8a31f-5086-41b0-8388-5a5244cc6060");
                                newRecipe.ParentGroupName = "Product Honeycomb Materials";
                                //newRecipe.Industry = "Basic Glass Furnace M";
                            }

                            if (string.IsNullOrEmpty(newRecipe.ParentGroupName))
                            {
                                Debug.WriteLine(newRecipe.Name + " -> empty ParentGroupName!");
                            }

                            if (string.IsNullOrEmpty(newRecipe.Industry))
                            {
                                Debug.WriteLine(newRecipe.Name + " -> unknown industry!");
                            }
                            else
                            {
                                Debug.WriteLine(newRecipe.Name + " -> industry = " + newRecipe.Industry);
                            }
                            //DUData.Recipes.Add(newRecipe.Key, newRecipe);
                            Debug.WriteLine("ADDED " + prodname);
                        }
                        else
                        {
                            Debug.WriteLine("FAILED to add: " + missing.Value.Products[0].Id.ToString() + " " +
                                            missing.Value.Products[0].DisplayNameWithSize);
                        }
                    }
                    else
                    {
                        Debug.WriteLine(missing.Value.Products[0].Id.ToString().PadRight(30) + " " +
                                        missing.Value.Products[0].DisplayNameWithSize);
                    }
                }

                // Check for missing Plasma in DUData.Recipes' ingredients
                var plasmaIds = new List<ulong>
                {
                    1769135512, 1831558336, 1831557945, 1831558342,
                    1831558338, 1831558341, 1831558343, 1831558340,
                    1831558339, 1831558337
                };
                var plas = DUData.Recipes.Values.Where(x => x.Ingredients.Count > 0 &&
                                                            x.Ingredients.Any(y => plasmaIds.Contains(y.Id)))
                                 .OrderBy(x => x.Products[0].Name).ToList();
                foreach (var pl in plas)
                {
                    var ingName = pl.Ingredients.First(x => x.Name.StartsWith("Relic Plasma")).Name;
                    var rec = DUData.Recipes.FirstOrDefault(x => x.Value.NqId == pl.Products[0].Id);
                    if (rec.Key == null)
                    {
                        Debug.WriteLine(pl.Products[0].Name + " " + pl.Products[0].Id + " NOT FOUND!");
                        continue;
                    }
                    if (rec.Value.Ingredients.All(x => x.Name != ingName))
                    {
                        Debug.WriteLine(rec.Key + " " + rec.Value.Name + " is missing " + ingName);
                    }
                }
            }

            // 1st check:
            // Is this a "part", but not an ingredient in any recipe?
            // Could also be just an API category. Only uncomment when needed!
            //var removalEntries = (from kvp in DUData.Recipes.Values.Where(x =>
            //    x.ParentGroupName.EndsWith(" parts", StringComparison.InvariantCultureIgnoreCase))
            //                      let found = DUData.Recipes.Values.Any(x => x.Ingredients?.Any(
            //                          y => y.Name.Equals(kvp.Name, StringComparison.InvariantCultureIgnoreCase)) == true)
            //                      where !found
            //                      select kvp.Key).ToList();
            //foreach (var removalEntry in removalEntries)
            //{
            //    //DUData.Recipes.Remove(removalEntry);
            //    Debug.WriteLine("***"+removalEntry);
            //}
            // 2nd check: any ingredient has circular reference to Key?
            //var removalEntries = DUData.Recipes.Where(x => x.Value.Ingredients?.Any(y => y.Type == x.Key) == true).ToList();

            // ONLY uncomment if "RecipeGroups.json" should be overwritten everytime!
            //DUData.SaveRecipes();
#else
            // Copy key over, required!
            foreach (var kvp in DUData.Recipes)
            {
                kvp.Value.Key = kvp.Key;
            }
#endif

            DUData.RecipeNames.AddRange(DUData.Recipes.Select(x => x.Value.Name).OrderBy(y => y).ToList());
        }

        //private void DetermineIndustryFor(SchematicRecipe recipe)
        //{
        //    if (recipe.ParentGroupName == "Ore" ||
        //        recipe.Name.StartsWith("relic plasma", StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        recipe.Industry = "";
        //        return;
        //    }

        //    // elements can be produced by same tier as well as 1 lower tier assemblies:
        //    var indPrefix = recipe.Level > 1 ? DUData.TierNames[recipe.Level-1] : DUData.TierNames[recipe.Level];
        //    var indSuffix = "";

        //    var isPart    = recipe.ParentGroupName.EndsWith("Parts", StringComparison.InvariantCultureIgnoreCase);
        //    var isPure    = recipe.ParentGroupName.Equals("Pure");
        //    var isProduct = recipe.ParentGroupName.Equals("Product");
        //    var isScrap   = recipe.ParentGroupName.Equals("Scraps");
        //    var isHC      = recipe.ParentGroupName == "Product Honeycomb Materials" ||
        //                    recipe.ParentGroupName == "Pure Honeycomb Materials";

        //    var isElect   = isPart && (
        //                        (recipe.Name.IndexOf(" antenna ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        recipe.Name.EndsWith(" antimatter core", StringComparison.InvariantCultureIgnoreCase) ||
        //                        recipe.Name.EndsWith(" anti-gravity core", StringComparison.InvariantCultureIgnoreCase) ||
        //                        (recipe.Name.IndexOf(" button ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        recipe.Name.EndsWith(" component", StringComparison.InvariantCultureIgnoreCase) ||
        //                        recipe.Name.EndsWith(" connector", StringComparison.InvariantCultureIgnoreCase) ||
        //                        (recipe.Name.IndexOf(" control system ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        (recipe.Name.IndexOf(" core system ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        (recipe.Name.IndexOf(" electronics", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        (recipe.Name.IndexOf(" mechanical sensor ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        (recipe.Name.IndexOf(" motherboard ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        (recipe.Name.IndexOf(" ore scanner ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        recipe.Name.EndsWith(" power system", StringComparison.InvariantCultureIgnoreCase) ||
        //                        (recipe.Name.IndexOf(" power transformer ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        recipe.Name.EndsWith(" processor", StringComparison.InvariantCultureIgnoreCase) ||
        //                        recipe.Name.EndsWith(" quantum alignment unit", StringComparison.InvariantCultureIgnoreCase) ||
        //                        recipe.Name.EndsWith(" quantum barrier", StringComparison.InvariantCultureIgnoreCase) ||
        //                        (recipe.Name.IndexOf("uncommon light", StringComparison.InvariantCultureIgnoreCase) >= 0));

        //    var isMetal   = isPart && !isElect && (
        //                        recipe.Name.EndsWith(" burner", StringComparison.InvariantCultureIgnoreCase) ||
        //                        (recipe.Name.IndexOf(" chemical container ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        (recipe.Name.IndexOf(" combustion chamber ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        (recipe.Name.IndexOf(" container ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        (recipe.Name.IndexOf(" electric engine ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        (recipe.Name.IndexOf(" firing system ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        (recipe.Name.IndexOf(" frame ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        (recipe.Name.IndexOf(" gas cylinder ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        recipe.Name.EndsWith(" hydraulics", StringComparison.InvariantCultureIgnoreCase) ||
        //                        (recipe.Name.IndexOf(" ionic chamber ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        recipe.Name.EndsWith(" magnet", StringComparison.InvariantCultureIgnoreCase) ||
        //                        (recipe.Name.IndexOf(" magnetic rail ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        (recipe.Name.IndexOf(" missile silo ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        (recipe.Name.IndexOf(" mobile panel ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        recipe.Name.EndsWith(" pipe", StringComparison.InvariantCultureIgnoreCase) ||
        //                        (recipe.Name.IndexOf(" power transformer ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        (recipe.Name.IndexOf(" robotic arm ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        (recipe.Name.IndexOf(" screen ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        recipe.Name.EndsWith(" screw", StringComparison.InvariantCultureIgnoreCase) ||
        //                        recipe.Name.EndsWith(" singularity container", StringComparison.InvariantCultureIgnoreCase) ||
        //                        recipe.Name.EndsWith(" solid warhead", StringComparison.InvariantCultureIgnoreCase));

        //    var is3D      = !isElect && !isMetal && (
        //                        recipe.Name.Equals("carbon fiber product", StringComparison.InvariantCultureIgnoreCase) ||
        //                        (recipe.Name.IndexOf(" casing ", StringComparison.InvariantCultureIgnoreCase) > 0) ||
        //                        recipe.Name.EndsWith(" fixation", StringComparison.InvariantCultureIgnoreCase) ||
        //                        recipe.Name.EndsWith(" injector", StringComparison.InvariantCultureIgnoreCase) ||
        //                        recipe.Name.EndsWith(" quantum core", StringComparison.InvariantCultureIgnoreCase) ||
        //                        (recipe.Name.IndexOf(" screen ", StringComparison.InvariantCultureIgnoreCase) > 0));

        //    var isGlass   = !isElect && !isMetal && !is3D && (
        //                        recipe.Key.StartsWith("led_", StringComparison.InvariantCultureIgnoreCase) ||
        //                        recipe.Key.StartsWith("WarpCell") ||
        //                        recipe.Name.EndsWith(" antimatter capsule", StringComparison.InvariantCultureIgnoreCase) ||
        //                        (recipe.Name.IndexOf(" laser chamber", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //                        recipe.Name.EndsWith(" optics", StringComparison.InvariantCultureIgnoreCase) ||
        //                        (recipe.Name.IndexOf("glass", StringComparison.InvariantCultureIgnoreCase) >= 0));

        //    var isChem    = !isElect && !isMetal && !is3D && !isGlass && (
        //                        recipe.Name.StartsWith("Biological") ||
        //                        recipe.Name.StartsWith("Catalyst") ||
        //                        recipe.Name.EndsWith(" explosive module", StringComparison.InvariantCultureIgnoreCase) ||
        //                        recipe.Name.StartsWith("Fluoropolymer") ||
        //                        recipe.ParentGroupName.Equals("Fuels") ||
        //                        recipe.ParentGroupName.EndsWith("plastic product", StringComparison.InvariantCultureIgnoreCase));

        //    var isAssy = !isElect && !isMetal && !is3D && !isChem && !isGlass &&
        //                 !isPart && !isPure && !isProduct && !isScrap && !isHC;

        //    string getIndyName(string industry) => $"{indPrefix} {industry} {indSuffix}".Trim();

        //    if (recipe.Key == "OxygenPure" || recipe.Key == "HydrogenPure" || isScrap)
        //    {
        //        recipe.Industry = getIndyName("Basic Recycler M"); return;
        //    }
        //    if (isGlass)
        //    {
        //        recipe.Industry = getIndyName("Glass Furnace M"); return;
        //    }
        //    if (isChem)
        //    {
        //        recipe.Industry = getIndyName("Chemical Industry M"); return;
        //    }
        //    if (is3D)
        //    {
        //        recipe.Industry = getIndyName("3D Printer M"); return;
        //    }
        //    if (isElect)
        //    {
        //        recipe.Industry = getIndyName("Electronics Industry M"); return;
        //    }
        //    if (isMetal)
        //    {
        //        recipe.Industry = getIndyName("Metalwork Industry M"); return;
        //    }
        //    if (isPure || recipe.ParentGroupName == "Refined Materials")
        //    {
        //        recipe.Industry = getIndyName("Refiner M"); return;
        //    }
        //    if (isProduct)
        //    {
        //        recipe.Industry = getIndyName("Smelter M"); return;
        //    }
        //    if (isHC)
        //    {
        //        recipe.Industry = getIndyName("Honeycomb Refinery M"); return;
        //    }
        //    if (isAssy)
        //    {
        //        // special cases, there may be more :(
        //        var noLowerTier =
        //            (recipe.Name.IndexOf("assembly line s", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //            (recipe.Name.IndexOf("3d printer", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //            (recipe.Name.IndexOf(" furnace ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //            (recipe.Name.IndexOf(" comb refinery ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //            (recipe.Name.IndexOf(" industry ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //            (recipe.Name.IndexOf(" refiner ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //            (recipe.Name.IndexOf(" recycler ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //            (recipe.Name.IndexOf(" smelter ", StringComparison.InvariantCultureIgnoreCase) >= 0) ||
        //            recipe.Name.StartsWith("ammo", StringComparison.InvariantCultureIgnoreCase)  ||
        //            recipe.Name.Equals("antennalarge", StringComparison.InvariantCultureIgnoreCase)  ||
        //            recipe.Name.Equals("antennamedium", StringComparison.InvariantCultureIgnoreCase)  ||
        //            recipe.Name.Equals("AntennaSmall", StringComparison.InvariantCultureIgnoreCase)  ||
        //            recipe.ParentGroupName.Equals("Furniture & Appliances", StringComparison.InvariantCultureIgnoreCase)  ||
        //            false;
        //        indSuffix = GetElementSize(recipe.Name, noLowerTier);
        //        // and more special cases:
        //        if (recipe.Name.Equals("Territory Unit XL", StringComparison.InvariantCultureIgnoreCase))
        //        {
        //            indSuffix = "M";
        //        }
        //        else
        //        if (recipe.Name.Equals("vertical light l", StringComparison.InvariantCultureIgnoreCase))
        //        {
        //            indSuffix = "S";
        //        }
        //        else
        //        if (recipe.Name.Equals("vertical light m", StringComparison.InvariantCultureIgnoreCase) ||
        //            recipe.Name.Equals("long light m", StringComparison.InvariantCultureIgnoreCase) ||
        //            recipe.Name.Equals("long light l", StringComparison.InvariantCultureIgnoreCase) ||
        //            recipe.Name.StartsWith("square light", StringComparison.InvariantCultureIgnoreCase) ||
        //            false)
        //        {
        //            indSuffix = "XS";
        //        }
        //        else
        //        switch (recipe.Key)
        //        {
        //            case "AdjunctTipSmall":
        //            case "AdjunctTipMedium": indSuffix = "XS"; break;
        //            case "AdjunctTipLarge": indSuffix = "S"; break;
        //            case "AileronLarge2": indSuffix = "M"; break;
        //            case "AileronMedium2": indSuffix = "S"; break;
        //            case "AileronShortLarge2": indSuffix = "M"; break;
        //            case "AileronShortMedium2": indSuffix = "S"; break;
        //            case "antenna_5_xl": indSuffix = "M"; break;
        //        }
        //        recipe.Industry = getIndyName("Assembly Line");
        //        return;
        //    }
        //    Debug.WriteLine(recipe.Name.PadRight(40)+" -> NO INDY!");
        //}
    }
}