using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using DU_Helpers;

namespace DU_Industry_Tool.Skills
{
    public static class TalentsExtentions
    {
        public static bool SetValue(this IEnumerable<Talent> list, string key, int val)
        {
            var entry = list.FirstOrDefault(x => x.Key == key);
            if (entry == null) return false;
            entry.Value = Utils.ClampInt(val, 0, 5);
            return true;
        }

        public static int ValueForKey(this IEnumerable<Talent> list, string key)
        {
            return list.FirstOrDefault(x => x.Key == key)?.Value ?? 0;
        }

        public static List<Talent> Clone(this List<Talent> list)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, list);
                ms.Position = 0;
                return (List<Talent>)formatter.Deserialize(ms);
            }
        }
    }
}