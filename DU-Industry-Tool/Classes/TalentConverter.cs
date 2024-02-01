using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace DU_Industry_Tool
{
    public class TalentName
    {
        public string Category { get; set; }
        public string Group { get; set; }
        public string Specification { get; set; }
        public string Title { get; set; }
        public string Size { get; set; }
        public string Tier { get; set; }
    }

    public class TalentSetting : Talent
    {
        public string Key { get; set; }
    }

    public class TalentConverter
    {
        private Dictionary<int, List<string>> tierOres = new Dictionary<int, List<string>>
        {
            { 1, new List<string> { "Bauxite", "Coal", "Hematite", "Quarts" } },
            { 2, new List<string> { "Limestone", "Chromite", "Malachite", "Natron" } },
            { 3, new List<string> { "Petalite", "Garnierite", "Acanthite", "Pyrite" } },
            { 4, new List<string> { "Cobaltite", "Cryolite", "Gold Nuggets", "Kolbeckite" } },
            { 5, new List<string> { "Rhodonite", "Columbite", "Ilmenite", "Vanadinite" } },
        };
        
        private Dictionary<int, List<string>> tierPures = new Dictionary<int, List<string>>
        {
            { 1, new List<string> { "Aluminium", "Carbon", "Iron", "Silicon", "Glass" } },
            { 2, new List<string> { "Calcium", "Chromium", "Copper", "Sodium" } },
            { 3, new List<string> { "Lithium", "Nickel", "Silver", "Sulfur" } },
            { 4, new List<string> { "Cobalt", "Fluorine", "Gold", "Scandium" } },
            { 5, new List<string> { "Manganese", "Niobium", "Titanium", "Vanadium" } },
        };
        
        private Dictionary<int, List<string>> tierProducts = new Dictionary<int, List<string>>
        {
            { 1, new List<string> { "Al-Fe Alloy", "Polycarbonate Plastic", "Silumin", "Steel", "Glass" } },
            { 2, new List<string> { "Calcium Reinforced Copper", "Duralumin", "Stainless Steel", "Polycalcite Plastic", "Advanced Glass" } },
            { 3, new List<string> { "Cu-Ag Alloy", "Al-Li Alloy", "Inconel", "Polysulfide Plastic", "Ag-Li Reinforced Glass" } },
            { 4, new List<string> { "Red Gold", "Fluoropolymer", "Sc-Al Alloy", "Maraging Steel", "Gold-Coated Glass" } },
            { 5, new List<string> { "Ti-Nb Supraconductor", "Vanamer", "Grade 5 Titanium Alloy", "Mangalloy", "Manganese Reinforced Glass" } }
        };
        
        public TalentConverter()
        {
        }

        public void ConvertTalents()
        {
            string talentNamesJson = File.ReadAllText("talentNames.json");
            string talentSettingsJson = File.ReadAllText("talentSettings.json");
            var talentNamesStrings = JsonConvert.DeserializeObject<List<string>>(talentNamesJson);
            var talentSettings = JsonConvert.DeserializeObject<List<TalentSetting>>(talentSettingsJson);
            var talentNames = talentNamesStrings.Select(tns =>
            {
                var parts = tns.Split('.');
                return new TalentName
                {
                    Category = parts[0],
                    Group = parts[1],
                    Specification = parts[2],
                    // Assign other properties as needed...
                };
            }).ToList();
            foreach (var talentSetting in talentSettings)
            {
                for (int tier = 1; tier <= 5; tier++)
                {
                    foreach (var product in tierProducts[tier])
                    {
                        if (talentSetting.Key.Contains(product) && talentSetting.Key.Contains("Productivity"))
                        {
                            var expectedTalentName = $"Products.{GetTierName(tier)} Product Productivity.{product}";
                            var matchingTalentName = talentNames.FirstOrDefault(tn => tn.Category == expectedTalentName);
                            if (matchingTalentName != null)
                            {
                                // Found a match, do something...
                                Console.WriteLine($"Match found: {matchingTalentName.Category}");
                            }
                        }
                    }
                }
            }
        }

        private int GetTier(string tierName)
        {
            switch (tierName)
            {
                case "Basic": return 1;
                case "Uncommon": return 2;
                case "Advanced": return 3;
                case "Rare": return 4;
                case "Exotic": return 5;
                default: throw new ArgumentException("Invalid tier", nameof(tierName));
            }
        }

        private string GetTierName(int tier)
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
    }
}