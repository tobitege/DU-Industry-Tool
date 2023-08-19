using System;
using System.Collections.Generic;

namespace DU_Industry_Tool
{
    /// <summary>
    /// Collection of static utility functions and variables
    /// </summary>
    public static class Utils
    {
        public static SortedDictionary<string, DuLuaItem> LuaSchematics; // List of schematics from du-lua.dev
        public static SortedDictionary<string, DuLuaItem> LuaItems; // List of items from du-lua.dev
        public static SortedDictionary<string, DuLuaRecipe> LuaRecipes; // List of recipes from du-lua.dev

        public static string TrimLast(this string mystring, string text)
        {
            if (string.IsNullOrEmpty(mystring) || string.IsNullOrEmpty(text))
                return mystring;

            var bpPos = mystring.LastIndexOf(text, StringComparison.InvariantCulture);
            if (bpPos >= 0)
            {
                mystring = mystring.Substring(0, bpPos);
            }
            return mystring;
        }

        // https://stackoverflow.com/a/909583
        public static string GetVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            var ver = fvi.FileVersion;
            if (ver == null) return "";
            for (var idx = 0; idx < 2; idx++)
            {
                var dotPos = ver.LastIndexOf(".0");
                if (dotPos >= 0)
                {
                    ver = ver.Substring(0, dotPos);
                }
            }
            return ver;
        }

        /// <summary>
        /// Format a file size into a more intelligible value
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string FormatFileSize(long size)
        {
            var limits = new int[] { 1024 * 1024 * 1024, 1024 * 1024, 1024 };
            var units = new string[] { "GB", "MB", "KB" };

            for (var i = 0; i < limits.Length; i++)
            {
                if (size >= limits[i])
                    return string.Format("{0:#,##0.##} " + units[i], ((double)size / limits[i]));
            }

            return $"{size} bytes";
        }

        /// <summary>
        /// Format a time (in seconds) into a readable value
        /// </summary>
        /// <param name="durationInSeconds">Duration value in seconds</param>
        /// <returns></returns>
        public static string GetReadableTime(decimal durationInSeconds)
        {
            var sp = TimeSpan.FromSeconds((double)durationInSeconds);
            var result = (sp.Days > 0 ? $"{sp.Days}d : " : "") +
                         (sp.Hours > 0 || sp.Minutes > 0 || sp.Seconds > 0 ? $"{sp.Hours}h " : "") +
                         (sp.Minutes > 0 || sp.Seconds > 0 ? $": {sp.Minutes}m " : "") +
                         (sp.Seconds > 0 ? $" : {sp.Seconds}s" : "");
            return result;
        }

        // https://stackoverflow.com/a/70683169
        public static bool IsEqual(this double value1, double value2, int precision = 2)
        {
            var dif = Math.Abs(Math.Round(value1, precision) - Math.Round(value2, precision));
            while (precision > 0)
            {
                dif *= 10;
                precision--;
            }
            return dif < 1;
        }

        public static bool IsEqual(this decimal value1, decimal value2, int precision = 2)
        {
            var dif = Math.Abs(Math.Round(value1, precision) - Math.Round(value2, precision));
            while (precision > 0)
            {
                dif *= 10;
                precision--;
            }
            return dif < 1;
        }

        public static bool IsEven(this int value)
        {
            Math.DivRem(value, 2, out var rem);
            return rem == 0;
        }

        public static bool IsOdd(this int value)
        {
            return !IsEven(value);
        }

        // https://stackoverflow.com/a/2691042
        public static int MathMod(int a, int b)
        {
            return (Math.Abs(a * b) + a) % b;
        }

        public static string[] FunHints = new[]
        {
            "Loading data...",
            "Grab a beer, brb...",
            "Patience, youngling!",
            "Git'in it done...",
            "Out for lunch...",
            "Checking mails...",
            "Hold my beer...",
            "Grabbing coffee...",
            "Out shopping...",
            "Mowing the lawn...",
            "Come back later!",
        };
    }
}
