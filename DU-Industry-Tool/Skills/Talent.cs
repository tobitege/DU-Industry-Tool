using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DU_Industry_Tool.Skills
{
    [Serializable]
    public class TalentValue
    {
        public string Key { get; set; }
        public int Value { get; set; }
    }

    [Serializable]
    public class Talent
    {
        public string Name { get; set; }
        public decimal Multiplier { get; set; }
        public int Addition { get; set; }
        public List<string> ApplicableRecipes { get; set; } = new List<string>();
        public bool InputTalent { get; set; }
        public bool EfficiencyTalent { get; set; }
        public int Value { get; set; } // 2024-01-21 moved to talentValues! remains for legacy support
        public string Key { get; set; }
        public int Tier { get; set; }
        [JsonIgnore]
        public string Section { get; set; }
        [JsonIgnore]
        public string Group { get; set; }
        [JsonIgnore]
        public string Entry { get; set; }
        /// <summary>
        /// Returns a shallow clone of "talent" without ApplicableRecipes
        /// </summary>
        /// <param name="talent">Source talent object</param>
        /// <returns>Copy of talent</returns>
        public static Talent Clone(Talent talent)
        {
            return new Talent
            {
                Name = talent.Name,
                Multiplier = talent.Multiplier,
                Addition = talent.Addition,
                ApplicableRecipes = new List<string>(),
                InputTalent = talent.InputTalent,
                EfficiencyTalent = talent.EfficiencyTalent,
                Tier = talent.Tier,
                Section = talent.Section,
                Group = talent.Group,
                Entry = talent.Entry,
                Value = talent.Value,
            };
        }
    }
}