using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DU_Helpers;
using Krypton.Toolkit;
using Newtonsoft.Json;

namespace DU_Industry_Tool.Skills
{
    /// <summary>
    /// Cache specific for schematic talents to avoid repeated searches in full list
    /// </summary>
    public static class SchemaTalentsCache
    {
        private static int _costOptimizationBasic;
        private static int _costOptimizationAdvanced;
        private static int _outputProductivityBasic;
        private static int _outputProductivityAdvanced;
        private static int _researchTimeEfficiencyBasic;
        private static int _researchTimeEfficiencyAdvanced;
        
        public static int CostOptimizationBasic
        {
            get => _costOptimizationBasic; 
            set {
                _costOptimizationBasic = Utils.ClampInt(value, 0, 5); 
                SetValue("Schematics.Cost Optimization.Basic", CostOptimizationBasic); 
            }
        }
        public static int CostOptimizationAdvanced
        {
            get => _costOptimizationAdvanced;
            set
            {
                _costOptimizationAdvanced = Utils.ClampInt(value, 0, 5);
                SetValue("Schematics.Cost Optimization.Advanced", CostOptimizationAdvanced);
            }
        }
        public static int OutputProductivityBasic
        {
            get => _outputProductivityBasic; 
            set
            {
                _outputProductivityBasic = Utils.ClampInt(value, 0, 5);
                SetValue("Schematics.Output Productivity.Basic", OutputProductivityBasic);
            }
        }
        public static int OutputProductivityAdvanced
        {
            get => _outputProductivityAdvanced;
            set
            {
                _outputProductivityAdvanced = Utils.ClampInt(value, 0, 5);
                SetValue("Schematics.Output Productivity.Advanced", OutputProductivityAdvanced);
            }
        }
        public static int ResearchTimeEfficiencyBasic
        {
            get => _researchTimeEfficiencyBasic; 
            set
            {
                _researchTimeEfficiencyBasic = Utils.ClampInt(value, 0, 5);
                SetValue("Schematics.Research Time Efficiency.Basic", ResearchTimeEfficiencyBasic);
            }
        }
        public static int ResearchTimeEfficiencyAdvanced
        {
            get => _researchTimeEfficiencyAdvanced; 
            set
            {
                _researchTimeEfficiencyAdvanced = Utils.ClampInt(value, 0, 5);
                SetValue("Schematics.Research Time Efficiency.Advanced", ResearchTimeEfficiencyAdvanced);
            }
        }

        private static void SetValue(string key, int value)
        {
            try
            {
                TalentValues.Values[key] = Utils.ClampInt(value,0,5);
            }
            catch (Exception)
            {
            }
        }

        private static int TryGet(string key)
        {
            try
            {
                return TalentValues.Values[key];
            }
            catch (Exception)
            {
            }
            return 0;
        }

        public static void PullFromTalentValues()
        {
            CostOptimizationBasic = TryGet("Schematics.Cost Optimization.Basic");
            CostOptimizationAdvanced = TryGet("Schematics.Cost Optimization.Advanced");
            OutputProductivityBasic = TryGet("Schematics.Output Productivity.Basic");
            OutputProductivityAdvanced = TryGet("Schematics.Output Productivity.Advanced");
            ResearchTimeEfficiencyBasic = TryGet("Schematics.Research Time Efficiency.Basic");
            ResearchTimeEfficiencyAdvanced = TryGet("Schematics.Research Time Efficiency.Advanced");
        }

        public static void PushAllToTalentValues()
        {
            SetValue("Schematics.Cost Optimization.Basic", CostOptimizationBasic);
            SetValue("Schematics.Cost Optimization.Advanced", CostOptimizationAdvanced);
            SetValue("Schematics.Output Productivity.Basic", OutputProductivityBasic);
            SetValue("Schematics.Output Productivity.Advanced", OutputProductivityAdvanced);
            SetValue("Schematics.Research Time Efficiency.Basic", ResearchTimeEfficiencyBasic);
            SetValue("Schematics.Research Time Efficiency.Advanced", ResearchTimeEfficiencyAdvanced);
        }
    }

    public class TalentValues : FileSettingsBase
    {
        private static TalentValues _instance;
        private static TalentValues Instance => _instance ?? (_instance = new TalentValues());

        public static SortedDictionary<string, int> Values { get; private set; } = new SortedDictionary<string, int>();

        private TalentValues(string fname = "talentValues")
        {
            SchemaTalentsCache.PushAllToTalentValues(); // to initialize
            DefaultFilename = fname;
            PathValid = CheckPath();
            if (!PathValid)
            {
                KryptonMessageBox.Show("ERROR: cannot create required folder in %APPDATA% for this application!",
                    "Error", KryptonMessageBoxButtons.OK, false);
            }
        }

        private static bool PathValid { get;  set; }
        
        public static bool HasData => Values.Count > 0;
        
        public static bool FileExists()
        {
            return File.Exists(Instance.SettingsFullPath);
        }

        public static bool SaveValues()
        {
            if (Values?.Count == 0) return false;
            try
            {
                var valuesJson = JsonConvert.SerializeObject(Values);
                File.WriteAllText(Instance.SettingsFullPath, valuesJson);
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Error: UnauthorizedAccessException - {ex.Message}");
                return false;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: IOException - {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.GetType().Name} - {ex.Message}");
                return false;
            }
        }

        public static bool LoadValues()
        {
            try
            {
                if (!File.Exists(Instance.SettingsFullPath)) return false;
                Values = JsonConvert.DeserializeObject<SortedDictionary<string, int>>(File.ReadAllText(Instance.SettingsFullPath));
                SchemaTalentsCache.PullFromTalentValues();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.GetType().Name} - {ex.Message}");
            }
            return false;
        }
    }

    public class TalentsManager
    {
        private static bool v1loaded = false;
        private static bool v2loaded = false;

        private static readonly string _talentsFile = "talentSettings.json";

        /// <summary>
        /// Called once per application-startup. Loads both talents and -values.
        /// If applicable, it upgrades legacy v1 to v2 file formats as of v2024.1.11.
        /// </summary>
        /// <param name="version"></param>
        public static void LoadTalentsAndValues(int version = 1)
        {
#if DEBUGx
            // DEV TOOLS ONLY!
            GenerateTalentsV2();
            SaveTalents(2);
#endif

            // Check, if a legacy talents file exists AND
            // whether a needed talent values file does not yet exist:
            // perform "upgrade" by creating talent values file (in appdata)
            // based on the values loaded from legacy talents file
            if (TalentsFileExists() && !TalentValues.FileExists())
            {
                // first, load legacy talents file, which includes
                // user-configured talent values
                if (LoadTalents(1, true))
                {
                    v1loaded = true;
                    // try to save talent values now into new default file
                    SaveTalentValues();
                }
            }

            LoadTalents(2);
            LoadTalentValues();
        }

        private static bool TalentsFileExists(int version = 1)
        {
            var fname = _talentsFile;
            if (version > 1)
            {
                fname = Path.GetFileNameWithoutExtension(fname) + $" v{version}.json";
            }
            return File.Exists(fname);
        }

        private static bool LoadTalents(int version = 1, bool silent = false)
        {
            Talents.Clear();

            var fname = _talentsFile;
            if (version > 1)
            {
                fname = Path.GetFileNameWithoutExtension(fname) + $" v{version}.json";
            }
            // Check if talents file already exists to load values from
            bool loaded = false;
            if (File.Exists(fname))
            {
                try
                {
                    Talents.Values = JsonConvert.DeserializeObject<List<Talent>>(File.ReadAllText(fname));
                    // make sure new-ish talents are available (legacy code)
                    //if (version < 2)
                    //{
                    //    UpdateEfficiencyTalents(true, silent);
                    //}
                    loaded = true;
                }
                catch (Exception) { }
            }
            if (loaded) return true;

            // At this stage the file did not exist, was unreadable or had invalid data (exception).
            if (!silent)
            {
                KryptonMessageBox.Show("Required file '" + fname + "' not found or invalid!\r\n" +
                                       "Please restore from GitHub repo!", "Error",
                    KryptonMessageBoxButtons.OK, false);
            }
            return false;
        }

        private static void LoadTalentValues()
        {
            // upgrade: we got legacy-keyed talent values, need to apply
            // them to talents and save the talentValues file again
            if (v1loaded && !v2loaded && TalentValues.HasData)
            {
                foreach (var tv in TalentValues.Values)
                {
                    var entry = Talents.GetByName(tv.Key);
                    if (entry == null) continue;
                    entry.Value = tv.Value;
                }
                v1loaded = false;
                SaveTalentValues();
            }

            // If values files wasn't loaded, re-create it from current Talents,
            // which would happen at very first use of this feature after 2024-01-21
            if (!TalentValues.LoadValues())
            {
                SaveTalentValues();
            }

            // Apply values to main Talents storage
            for (int i = 0; i < Talents.Count; i++)
            {
                // convert Key to structural parts
                var entry = Talents.GetByIdx(i);
                if (string.IsNullOrEmpty(entry.Key)) continue;
                var structure = entry.Key.Split('.');
                if (structure.Length == 3)
                {
                    entry.Section = structure[0];
                    entry.Group = structure[1];
                    entry.Entry = structure[2];
                }
                entry.Value = TalentValues.Values.TryGetValue(entry.Key, out var val) ? Utils.ClampInt(val, 0, 5) : 0;
            }
        }

        private static void SaveTalents(int version = 1)
        {
            var fname = _talentsFile;
            try
            {
                if (version > 1)
                {
                    fname = Path.GetFileNameWithoutExtension(fname) + $" v{version}.json";
                }
                File.WriteAllText(fname, JsonConvert.SerializeObject(Talents.Values));
            }
            catch (Exception)
            {
                KryptonMessageBox.Show("Failed to write talents to file:\r\n" + fname, "Error",
                    KryptonMessageBoxButtons.OK, false);
            }
        }

        public static bool SaveTalentValues()
        {
            CopyValuesFromTalents();
            return TalentValues.SaveValues();
        }

        private static void CopyValuesFromTalents()
        {
            TalentValues.Values.Clear();
            foreach (var talent in Talents.Values)
            {
                if (v1loaded && talent.Key == null)
                {
                    var v2name = Xlatv1Name(talent.Name);
                    v2name = v2name.Replace("Scrap Scrap", "Scrap");
                    TalentValues.Values[v2name] = Utils.ClampInt(talent.Value, 0, 5);
                    continue;
                }
                if (talent.Key == null) return;
                TalentValues.Values[talent.Key] = Utils.ClampInt(talent.Value, 0, 5);
                
            }
            if (v1loaded) return;
            SchemaTalentsCache.PullFromTalentValues();
        }
        
        private static string Xlatv1Name(string name)
        {
            // "xxx Ore Refining" -> "Pure xxx Refining"
            var ix = name.IndexOf(" ore refining", StringComparison.InvariantCultureIgnoreCase);
            if (ix > 1)
            {
                name = "Pure " + name.Substring(0, ix) + " Refining";
            }
            name = name.Replace("Scrap Scrap", "Scrap");
            return name;
        }

        private static void UpdateEfficiencyTalents(bool save = true, bool silent = false)
        {
            var newTalents = new List<string>();
            // Industry efficiency + handling
            for (var idx = 1; idx <= 5; idx++)
            {
                foreach (var subTitle in new[] { "", " Handling" })
                {
                    var title = DUData.IndustryTypesList[idx] + " Industry Efficiency" + subTitle;
                    if (Talents.Any(x => x.Name == title)) continue;
                    newTalents.Add(title);
                    var tal = new Talent() {
                        Name = title,
                        EfficiencyTalent = true,
                        Multiplier = -0.02m, // -2% time per level
                        Tier = idx,
                        Section = "Industry",
                        Group = "Industry Efficiency" + subTitle,
                        Entry = DUData.IndustryTypesList[idx]
                    };
                    tal.Key = $"{tal.Section}.{tal.Group}.{tal.Entry}";
                    Talents.Add(tal);
                }
            }
            // Assembly efficiency + handling
            for (var idx = 0; idx < 5; idx++)
            {
                foreach (var subTitle in new[] { "", " Handling" })
                {
                    var title = $"Assembly {DUData.SizeList[idx]} Efficiency" + subTitle;
                    if (Talents.Any(x => x.Name == title)) continue;
                    newTalents.Add(title);
                    var tal = new Talent() {
                        Name = title,
                        EfficiencyTalent = true,
                        Multiplier = -0.02m, // -2% time per level
                        Tier = idx+1,
                        Section = "Industry",
                        Group = "Assembly Efficiency" + subTitle,
                        Entry = $"Assembly Line {DUData.SizeList[idx]}"
                    };
                    tal.Key = $"{tal.Section}.{tal.Group}.{tal.Entry}";
                    Talents.Add(tal);
                }
            }
            // Product+Pure Refining efficiency
            for (var idx = 1; idx <= 5; idx++)
            {
                foreach (var subTitle in new[] { " pure", " product" })
                {
                    var isPure = subTitle == " pure";
                    var title = DUData.TierNames[idx] + subTitle + " refinery efficiency";
                    if (Talents.Any(x => x.Name == title)) continue;
                    newTalents.Add(title);
                    var tal = new Talent()
                    {
                        Name = title,
                        EfficiencyTalent = true,
                        Multiplier = -0.05m, // -5% time per level
                        ApplicableRecipes = DUData.Recipes.Where(
                            r => r.Value != null && r.Value.Level == idx &&
                                 r.Value.ParentGroupName.ToLower() == subTitle.Trim())
                            .Select(r => r.Key).ToList(),
                        Tier = idx,
                        Section = isPure ? "Pures" : "Products",
                        Group = DUData.GetTierName(idx) + (isPure ? " Ore" : " Product") + " Refining",
                        Entry = "Efficiency"
                    };
                    tal.Key = $"{tal.Section}.{tal.Group}.{tal.Entry}";
                    Talents.Add(tal);
                }
            }

            if (newTalents.Count == 0) return;
            if (!silent)
            {
                KryptonMessageBox.Show("Important! The below talents have been added:\r\n\r\n" +
                    string.Join("\r\n", newTalents) +
                    "\r\nAfter this message the talents will be written to their file.", "Talents updated",
                    KryptonMessageBoxButtons.OK, false);
            }
            
            if (save)
            {
                TalentsManager.SaveTalents();
            }
        }

        private static void GenerateTalentsV2()
        {
            // Generate Talents:
            // below is to re-generate the talents file programmatically if it was missing
            // Setup the names of the item-specific multiplier talents
            var multiplierTalentGroups = new[] { "Pure", "Product", "Scraps" };

            // Setup the scrap talents so we can add each scrap to their applicableRecipe later
            var genScrapRefiningTalents = new List<Talent>()
                {
                    new Talent() { Tier = 1, Entry = "Refinery", Group = "Basic Scrap Refining", Section = "Scraps", Name = "Basic Scrap Refinery", Addition = -1, InputTalent = true },
                    new Talent() { Tier = 2, Entry = "Refinery", Group = "Uncommon Scrap Refining", Section = "Scraps", Name = "Uncommon Scrap Refinery", Addition = -1, InputTalent = true },
                    new Talent() { Tier = 3, Entry = "Refinery", Group = "Advanced Scrap Refining", Section = "Scraps", Name = "Advanced Scrap Refinery", Addition = -1, InputTalent = true },
                    new Talent() { Tier = 4, Entry = "Refinery", Group = "Rare Scrap Refining", Section = "Scraps", Name = "Rare Scrap Refinery", Addition = -1, InputTalent = true },
                    new Talent() { Tier = 5, Entry = "Refinery", Group = "Exotic Scrap Refining", Section = "Scraps", Name = "Exotic Scrap Refinery", Addition = -1, InputTalent = true }
                };
            var genScrapEfficiencyTalents = new List<Talent>()
                {
                    new Talent() { Tier = 1, Entry = "Efficiency", Group = "Basic Scrap Refining", Section = "Scraps", Name = "Basic Scrap Efficiency", EfficiencyTalent = true, Multiplier = -0.1m },
                    new Talent() { Tier = 2, Entry = "Efficiency", Group = "Uncommon Scrap Refining", Section = "Scraps", Name = "Uncommon Scrap Efficiency", EfficiencyTalent = true, Multiplier = -0.1m },
                    new Talent() { Tier = 3, Entry = "Efficiency", Group = "Advanced Scrap Refining", Section = "Scraps", Name = "Advanced Scrap Efficiency", EfficiencyTalent = true, Multiplier = -0.1m },
                    new Talent() { Tier = 4, Entry = "Efficiency", Group = "Rare Scrap Refining", Section = "Scraps", Name = "Rare Scrap Efficiency", EfficiencyTalent = true, Multiplier = -0.1m },
                    new Talent() { Tier = 5, Entry = "Efficiency", Group = "Exotic Scrap Refining", Section = "Scraps", Name = "Exotic Scrap Efficiency", EfficiencyTalent = true, Multiplier = -0.1m },
                };

            var sProd = " Productivity";
            var sRef = " Refining";
            var sSection = "";
            foreach (var kvp in DUData.Recipes.Where(r => r.Value?.ParentGroupName != null &&
                                                   multiplierTalentGroups.Any(t => t == r.Value.ParentGroupName)))
            {
                // Iterate over every recipe that is part of one of the multiplierTalentGroups, "Pure", "Scraps", or "Product"
                // They all have 3% multiplier for productivity
                var recipe = kvp.Value;

                // fill with du-craft.online compatible naming
                var group = GenerateGroupname(recipe.Level, recipe.ParentGroupName);
                sSection = recipe.ParentGroupName.TrimLastStr(" materials").TrimEnd('s') + "s";
                var recNameShort = recipe.Name.TrimLastStr(" Product").TrimLastStr(" product");
                var talent = new Talent() {
                    Name = recipe.Name + sProd,
                    Multiplier = 0.03m,
                    Tier = recipe.Level,
                    Section = sSection,
                    Group = group.TrimEnd('s') + sProd,
                    Entry = recNameShort
                };
                talent.ApplicableRecipes.Add(kvp.Key); // Each of these only applies to its one specific element
                Talents.Add(talent);
                group = GenerateGroupname(recipe.Level, recipe.ParentGroupName, true);
                switch (recipe.ParentGroupName)
                {
                    case "Pure":
                    case "Product":
                        {
                            // Pures and products have an input reduction of 0.03 multiplier as well as the output multiplier
                            talent = new Talent() {
                                Name = recipe.Name + sRef,
                                Multiplier = -0.03m,
                                InputTalent = true,
                                Tier = recipe.Level,
                                Section = sSection,
                                Group = group + sRef,
                                Entry = recNameShort
                            };
                            talent.ApplicableRecipes.Add(kvp.Key);
                            Talents.Add(talent);
                            break;
                        }
                    case "Scraps":
                        {
                            // And scraps get a flat -1L general, and -2 specific
                            talent = new Talent() {
                                Name = recipe.Name + " Refining",
                                Addition = -2,
                                InputTalent = true,
                                Tier = recipe.Level,
                                Section = sSection,
                                Group = group.TrimEnd('s') + " Refining",
                                Entry = recipe.Name
                            };
                            talent.ApplicableRecipes.Add(kvp.Key);
                            Talents.Add(talent);
                            genScrapRefiningTalents[recipe.Level - 1].ApplicableRecipes.Add(kvp.Key);
                            genScrapEfficiencyTalents[recipe.Level - 1].ApplicableRecipes.Add(kvp.Key);
                            break;
                        }
                }
            }

            Talents.AddRange(genScrapRefiningTalents);
            Talents.AddRange(genScrapEfficiencyTalents);

            var tierPuresHC = new Dictionary<int, List<string>>
            {
                { 1, new List<string> { "Aluminium", "Carbon", "Iron", "Silicon" } },
                { 2, new List<string> { "Calcium", "Chromium", "Copper", "Sodium" } },
                { 3, new List<string> { "Lithium", "Nickel", "Silver", "Sulfur" } },
                { 4, new List<string> { "Cobalt", "Fluorine", "Gold", "Scandium" } },
                { 5, new List<string> { "Manganese", "Niobium", "Titanium", "Vanadium" } },
            };
            var tierProductsHC = new Dictionary<int, List<string>>
            {
                { 1, new List<string> { "Polycarbonate Plastic", "Silumin", "Steel" } },
                { 2, new List<string> { "Duralumin", "Stainless Steel" } },
                { 3, new List<string> { "Al-Li", "Inconel" } },
                { 4, new List<string> { "Sc-Al", "Maraging Steel" } },
                { 5, new List<string> { "Grade 5 Titanium", "Mangalloy" } }
            };

            // Honeycomb talents for recipes
            for (var idx = 1; idx <= 5; idx++)
            {
                // add efficiency talents per tier for each Pure and Product HC
                // -10% time multiplier
                var tierName = DUData.GetTierName(idx);
                var hc = "Honeycomb";
                var hcProd = $"{hc} Productivity";
                var hcRef = $"{hc} Refining";
                var hcMat = $"{hc} Materials";
                foreach (var hcSection in new[] { "Product", "Pure" })
                {
                    sSection = $"{hcSection} {hc} Efficiency";
                    Talents.Add(new Talent()
                    {
                        Name = $"{tierName} {sSection}",
                        EfficiencyTalent = true,
                        Multiplier = -0.1m,
                        ApplicableRecipes = DUData.Recipes.Where(r => r.Value != null &&
                            r.Value.ParentGroupName == $"{hcSection} {hcMat}" &&
                            r.Value.Level == idx).Select(r => r.Key).ToList(),
                        Tier = idx,
                        Section = $"{hcSection} {hc}s",
                        Group = $"{tierName} {hcSection} {hcRef}",
                        Entry = "Efficiency"
                    });

                    // "Productivity" and "Refining" talents for products and pures
                    var hcSource = hcSection == "Product" ? tierProductsHC[idx] : tierPuresHC[idx];
                    foreach (var material in hcSource)
                    {
                        sSection = $"{hcSection} {hc}s";
                        var targets = DUData.Recipes.Where(r => r.Value != null &&
                                r.Value.ParentGroupName == $"{hcSection} {hcMat}" &&
                                r.Value.Ingredients.Any(x => x.Name.ToLower().Contains(material.ToLower())) &&
                                r.Value.Level == idx).Select(r => r.Key).ToList();
                        var inTalent = new Talent() {
                            Name = $"{material} {hcRef}",
                            Multiplier = -0.03m,
                            InputTalent = true, // Specific gets -3% per level to inputs
                            Tier = idx,
                            Section = sSection,
                            Group = $"{tierName} {hcSection} {hcRef}",
                            Entry = $"{material} {hc}",
                            ApplicableRecipes = targets
                        };
                        var outTalent = new Talent() {
                            Name = $"{material} {hcProd}",
                            Multiplier = 0.05m,
                            Tier = idx,
                            Section = sSection,
                            Group = $"{tierName} {hcSection} {hcProd}",
                            Entry = $"{material} {hc}",
                            ApplicableRecipes = targets
                        };
                        Talents.Add(outTalent);
                        Talents.Add(inTalent);
                    }
                }
            }

            // Building Product Honeycombs
            var tierBuildingHC = new List<string> { "Brick", "Carbon Fiber", "Concrete", "Marble", "Wood" };
            sSection = "Product Honeycomb";
            foreach (var material in tierBuildingHC)
            {
                var matKey = material.Replace(" ", "")+"Product";
                var matHc = $"{material} Honeycomb";
                var targets = DUData.Recipes.Where(r => r.Value != null &&
                        r.Value.ParentGroupName == $"{sSection} Materials" &&
                        r.Value.Ingredients.Any(x => x.Type == matKey)).Select(r => r.Key).ToList();
                var inTalent = new Talent()
                {
                    Name = $"{matHc} Refining",
                    Multiplier = -0.03m,
                    InputTalent = true, // Specific gets -3% per level to inputs
                    ApplicableRecipes = targets,
                    Section = sSection+"s",
                    Group = $"Building {sSection} Refining",
                    Entry = matHc
                };
                var outTalent = new Talent()
                {
                    Name = $"{matHc} Productivity",
                    Multiplier = 0.05m,
                    ApplicableRecipes = targets,
                    Section = sSection+"s",
                    Group = $"Building {sSection} Productivity",
                    Entry = matHc
                };
                Talents.Add(outTalent);
                Talents.Add(inTalent);
            }

            // Fuel talents
            // Generic gets -2% per level to inputs
            var genFuelRefineryTalent = new Talent()
            {
                Entry = "Refinery",
                Group = "Fuel Refining",
                Section = "Fuels",
                Name = "Fuel Refinery",
                Multiplier = -0.02m,
                InputTalent = true
            };
            var genFuelRefineryTalentEff = new Talent()
            {
                Entry = "Efficiency",
                Group = "Fuel Refining",
                Section = "Fuels",
                Name = "Fuel Efficiency",
                Multiplier = -0.1m,
                EfficiencyTalent = true
            };
            sSection = "Fuels";
            foreach (var groupList in DUData.Recipes
                         .Where(r => r.Value != null && r.Value.ParentGroupName == "Fuels")
                         .GroupBy(r => r.Value.GroupId))
            {
                var groupType = groupList.FirstOrDefault().Value.SchemaType;
                switch (groupType)
                {
                    case "AtmoFuel":
                        groupType = "Atmospheric Fuel";
                        break;
                    case "SpaceFuels":
                        groupType = "Space Fuel";
                        break;
                    case "RocketFuels":
                        groupType = "Rocket Fuel";
                        break;
                }
                var outTalent = new Talent()
                {
                    Name = $"{groupType} {sProd}",
                    Multiplier = 0.05m,
                    Section = sSection,
                    Group = "Fuel Productivity",
                    Entry = groupType
                };
                var inTalent = new Talent()
                {
                    Name = $"{groupType} {sRef}",
                    Multiplier = -0.03m,
                    InputTalent = true, // Specific gets -3% per level to inputs
                    Section = sSection,
                    Group = "Fuel Refining",
                    Entry = groupType
                };

                // Store that everything in this group is applicable to all of these Recipes (mostly catches the kergon varieties)
                foreach (var recipe in groupList)
                {
                    outTalent.ApplicableRecipes.Add(recipe.Key);
                    inTalent.ApplicableRecipes.Add(recipe.Key);
                    genFuelRefineryTalent.ApplicableRecipes.Add(recipe.Key);
                }
                Talents.Add(outTalent);
                Talents.Add(inTalent);
            }

            Talents.Add(genFuelRefineryTalent);
            Talents.Add(genFuelRefineryTalentEff);

            UpdateEfficiencyTalents(false, true);

            // Intermediary Part Productivity talents (Bas to Adv)
            sSection = "Parts";
            sProd = "Intermediary Part Productivity";
            for (var idx = 1; idx <= 3; idx++)
            {
                var tierName = DUData.GetTierName(idx);
                Talents.Add(new Talent()
                {
                    Name = $"{tierName} {sProd}",
                    Addition = 1,
                    ApplicableRecipes = DUData.Recipes
                        .Where(r => r.Value != null && r.Value.ParentGroupName == "Intermediary parts" &&
                                    r.Value.Level == idx).Select(r => r.Key).ToList(),
                    Tier = idx,
                    Section = sSection,
                    Group = sProd,
                    Entry = tierName,
                });
            }

            // Elements Efficiency talents (all -2% time per level)
            var elemEffs = new[] { "Combat", "Furniture & Appliances", "Industry & Infrastructure", "Piloting", "Systems" };
            foreach (var elem in elemEffs)
            {
                sSection = elem + " Element Efficiency";
                for (var idx = 1; idx <= 5; idx++)
                {
                    var tierName = DUData.GetTierName(idx);
                    Talents.Add(new Talent()
                    {
                        Name = tierName + " " + sSection,
                        EfficiencyTalent = true,
                        Multiplier = -0.02m,
                        ApplicableRecipes = DUData.Recipes
                            .Where(r => r.Value != null && r.Value.ParentGroupName == sSection &&
                                        r.Value.Level == idx).Select(r => r.Key).ToList(),
                        Tier = idx,
                        Section = "Elements",
                        Group = sSection,
                        Entry = tierName
                    });
                }
            }

            // Parts Manufacturer talents (all -10% time per level)
            var partsManufs = new Dictionary<string, int[]>
            {
                { "Complex",        new [] { 1, 5 } },
                { "Exceptional",    new [] { 3, 5 } },
                { "Functional",     new [] { 1, 5 } },
                { "Intermediary",   new [] { 1, 3 } },
                { "Structural",     new [] { 1, 5 } }
            };
            foreach (var manuf in partsManufs)
            {
                sSection = manuf.Key + " Part Manufacturer";
                for (var idx = manuf.Value[0]; idx <= manuf.Value[1]; idx++)
                {
                    var tierName = DUData.GetTierName(idx);
                    Talents.Add(new Talent()
                    {
                        Name = tierName + " " + sSection,
                        EfficiencyTalent = true,
                        Multiplier = -0.1m,
                        ApplicableRecipes = DUData.Recipes
                            .Where(r => r.Value != null && r.Value.ParentGroupName == manuf.Key + " parts" &&
                                        r.Value.Level == idx).Select(r => r.Key).ToList(),
                        Tier = idx,
                        Section = "Parts",
                        Group = sSection,
                        Entry = tierName
                    });
                }
            }

            // Ammo Talents (only uncommon and advanced, all sizes except XL)
            sSection = "Ammunition";
            sProd = " Ammo Productivity";
            for (var idx = 2; idx <= 3; idx++)
            {
                var tierName = DUData.GetTierName(idx);
                Talents.Add(new Talent()
                {
                    Name = tierName + " Ammo Efficiency",
                    EfficiencyTalent = true,
                    Multiplier = -0.1m,
                    ApplicableRecipes = DUData.Recipes
                        .Where(r => r.Value != null && r.Value.ParentGroupName.Contains("Ammo") &&
                                    r.Value.Level == idx &&
                                    r.Value.Name.ToLower().Contains(" ammo ")).Select(r => r.Key).ToList(),
                    Tier = idx,
                    Section = sSection,
                    Group = tierName + sProd,
                    Entry = "Efficiency"
                });
                for (var sizeIdx = 0; sizeIdx <= 3; sizeIdx++) // no XL!
                {
                    var size = DUData.SizeList[sizeIdx];
                    Talents.Add(new Talent()
                    {
                        Name = tierName + " Ammo " + size + " Productivity",
                        Addition = 1,
                        ApplicableRecipes = DUData.Recipes
                            .Where(r => r.Value != null && r.Value.ParentGroupName.Contains("Ammo") &&
                                        r.Value.Level == idx &&
                                        r.Value.Name.ToLower().EndsWith(" ammo " + size.ToLower())).Select(r => r.Key).ToList(),
                        Tier = idx,
                        Section = sSection,
                        Group = tierName + sProd,
                        Entry = size
                    });
                }
            }

            // Schematics talents
            var schematics = new[] { "Cost Optimization.Advanced", "Cost Optimization.Basic",
                                     "Output Productivity.Advanced", "Output Productivity.Basic",
                                     "Research Slot Upgrades.Advanced", "Research Slot Upgrades.Basic",
                                     "Research Time Efficiency.Advanced", "Research Time Efficiency.Basic" };
            foreach (var schem in schematics)
            {
                var details = schem.Split('.');
                var t = new Talent()
                {
                    Name = (details[1] == "Basic" ? "" : "Advanced ") + "Schematic " + details[0],
                    Section = "Schematics",
                    Group = details[0],
                    Entry = details[1]
                };
                if (schem.StartsWith("Cost"))
                {
                    t.Multiplier = t.Entry == "Basic" ? -0.05m : -0.03m;
                }
                else
                if (schem.StartsWith("Output"))
                {
                    t.Multiplier = t.Entry == "Basic" ? 0.03m : 0.02m;
                }
                else
                if (schem.Contains("Slot"))
                {
                    t.Addition = 1;
                }
                else
                if (schem.Contains("Time"))
                {
                    t.EfficiencyTalent = true;
                    t.Multiplier = t.Entry == "Basic" ? -0.03m : -0.02m;
                }
                Talents.Add(t);
            }

            UpdateKeys();
#if DEBUG
            var tFaults = Talents.Where(x => string.IsNullOrEmpty(x.Section) ||
                                               string.IsNullOrEmpty(x.Group) ||
                                               string.IsNullOrEmpty(x.Entry)).Select(x => x).ToList();
            if (tFaults.Any())
            {
                KryptonMessageBox.Show("DEV INFO: talents with missing details:\r\n\r\n" +
                    string.Join("\r\n", tFaults.Select(x => x.Key)), "Talents CHECK",
                    KryptonMessageBoxButtons.OK, false);
            }
            var tNames = Talents.Values.Select(x => x.Section + "." + x.Group + "." + x.Entry).Distinct().ToList();
            File.WriteAllText("talentNames.json", JsonConvert.SerializeObject(tNames));
#endif
        }
        
        private static void UpdateKeys()
        {
            // Set the Key for all talents
            for (var idx = 0; idx < Talents.Count; idx++) // do not use foreach!
            {
                Talents.Values[idx].Key = $"{Talents.Values[idx].Section}.{Talents.Values[idx].Group}.{Talents.Values[idx].Entry}";
            }
            Talents.Values = Talents.Values.OrderBy(t => t.Section)
                             .ThenBy(t => t.Group)
                             .ThenBy(t => t.Entry)
                             .ToList();
        }

        private static string GenerateGroupname(int tier, string parentGroupName, bool isOre = false)
        {
            var tierName = DUData.GetTierName(tier);
            if (parentGroupName == "Pure" && isOre)
            {
                parentGroupName = "Ore";
            }
            return $"{tierName} {parentGroupName}";
        }
    }
}