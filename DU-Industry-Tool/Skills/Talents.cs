using System;
using System.Collections.Generic;
using System.Linq;
using DU_Helpers;

namespace DU_Industry_Tool.Skills
{
    /// <summary>
    /// Static encapsulation of full talents list.
    /// </summary>
    public static class Talents
    {
        private static List<Talent> talents = new List<Talent>();
        
        public static List<Talent> Values { get => talents; set => talents = value; }

        public static int Count => talents?.Count ?? 0;

        public static void Clear()
        {
            talents.Clear();
        }
        public static void Add(Talent talent)
        {
            talents.Add(talent);
        }
        public static void AddRange(IEnumerable<Talent> talentsToAdd)
        {
            talents.AddRange(talentsToAdd);
        }
        public static void Delete(Talent talent)
        {
            talents.Remove(talent);
        }
        public static IEnumerable<Talent> Where(Func<Talent, bool> predicate)
        {
            return talents.Where(predicate);
        }
        public static bool Any(Func<Talent, bool> predicate)
        {
            return talents.Any(predicate);
        }
        public static Talent FirstOrDefault(Func<Talent, bool> predicate)
        {
            return talents.FirstOrDefault(predicate);
        }
        public static bool All(Func<Talent, bool> predicate)
        {
            return talents.All(predicate);
        }
        public static void SetValue(string key, int skill)
        {
            var entry = talents.FirstOrDefault(x => x.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
            if (entry != null)
            {
                entry.Value = Utils.ClampInt(skill, 0, 5);
            }
        }
        public static Talent GetByIdx(int idx)
        {
            return talents.Count() > idx ? talents[idx] : null;
        }
        public static Talent GetByKey(string key)
        {
            return talents.FirstOrDefault(t => t.Key == key);
        }
        public static Talent GetByName(string name)
        {
            return talents.FirstOrDefault(t => t.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}