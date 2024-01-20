using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DocumentFormat.OpenXml.VariantTypes;
using Krypton.Toolkit;

namespace DU_Helpers
{
    /// <summary>
    /// Collection of static utility functions and variables
    /// </summary>
    public static class Utils
    {
        public static string TrimLastStr(this string mystring, string text)
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

        public static char LetterByIndex(int value) // A == 1, B == 2 etc.
        {
            if (value < 1 || value > 26)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 1 and 26.");
            }
            return (char)(value + 64);
        }

        public static float ScalingFactor = 1;

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

        private static readonly Dictionary<char, int> BaseValues = new Dictionary<char, int>
        {
            { 'B', 1024 }, { 'M', 1000 }, { 'L', 1000 }
        };
        private static readonly Dictionary<char, string> BaseUnits = new Dictionary<char, string>
        {
            { 'B', "bytes" },
            { 'M', "kg" },    // mass
            { 'L', "liters" } // volume in Liters
        };
        private static readonly Dictionary<char, List<string>> Units = new Dictionary<char, List<string>>
        {
            { 'B', new List<string> { "GB", "MB", "KB" } },
            { 'M', new List<string> { "MT", "KT", "T"  } },
            { 'L', new List<string> { "GL", "ML", "KL" } }
        };

        /// <summary>
        /// Returns a metric-driven, readable value for a decimal value with unit types
        /// like T/KT for multiples of kg or KL/ML for multiples of liters.
        /// </summary>
        /// <param name="size">Value of unit as decimal</param>
        /// <param name="unit">B(ytes), M(ass) or V(olume)</param>
        /// <param name="prefix">Optional output to prefix the result</param>
        /// <returns>String with formatted value incl. unit</returns>
        public static string ReadableDecimal(decimal? size, char unit = 'M', string prefix = "")
        {
            if (size == null) return "";
            if (!BaseValues.TryGetValue(unit, out var basis) || !Units.TryGetValue(unit, out var u))
            {
                return $"{size:F}";
            }
            if (size < basis)
            {
                return $"{prefix}{size:F} {BaseUnits[unit]}";
            }

            var limit = basis * basis * basis;
            var arr = u.ToArray();
            for (var i = 0; i < arr.Length; i++)
            {
                if (size > limit)
                {
                    return $"{prefix}{size / limit:#,##0.##} {u[i]}";
                }
                limit /= basis;
            }
            return $"{prefix}{size:F}";
        }

        /// <summary>
        /// Format a time (in seconds) into a readable value
        /// </summary>
        /// <param name="durationInSeconds">Duration value in seconds</param>
        /// <returns></returns>
        public static string GetReadableTime(decimal durationInSeconds)
        {
            var sp = TimeSpan.FromSeconds((double)durationInSeconds);
            var sb = new List<string>(5);
            if (sp.Days    > 0) sb.Add($"{sp.Days}d");
            if (sp.Hours   > 0 || sp.Minutes > 0 || sp.Seconds > 0) sb.Add($"{sp.Hours}h");
            if (sp.Minutes > 0 || sp.Seconds > 0) sb.Add($"{sp.Minutes}m");
            if (sp.Seconds > 0) sb.Add($"{sp.Seconds}s");
            var result = string.Join(" : ", sb);

            //var result = (sp.Days > 0 ? $"{sp.Days}d : " : "") +
            //         (sp.Days > 0 || sp.Hours > 0 || sp.Minutes > 0 || sp.Seconds > 0 ? $": {sp.Hours}h " : "") +
            //         (sp.Days > 0 || sp.Hours > 0 || sp.Minutes > 0 || sp.Seconds > 0 ? $": {sp.Minutes}m " : "") +
            //         (sp.Days > 0 || sp.Hours > 0 || sp.Minutes > 0 || sp.Seconds > 0 ? $": {sp.Seconds}s" : "");
            return result;
        }

        // https://stackoverflow.com/a/70683169
        public static bool IsEqualDouble(this double value1, double value2, int precision = 2)
        {
            var dif = Math.Abs(Math.Round(value1, precision) - Math.Round(value2, precision));
            while (precision > 0)
            {
                dif *= 10;
                precision--;
            }
            return dif < 1;
        }

        public static bool IsEqualDec(this decimal value1, decimal value2, int precision = 2)
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

        // from https://stackoverflow.com/a/2691042
        public static int MathMod(int a, int b)
        {
            return (Math.Abs(a * b) + a) % b;
        }

        public static int ClampInt(int val, int iMin, int iMax)
        {
            return Math.Max(Math.Min(val, iMax), iMin);
        }

        public static decimal ClampDec(decimal val, decimal iMin, decimal iMax)
        {
            return Math.Max(Math.Min(val, iMax), iMin);
        }

        public static readonly string[] FunHints = new[]
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

        public static string PromptOpen(string title, SettingsEnum savePathTo = SettingsEnum.ProdListDirectory)
        {
            var lastDir = SettingsMgr.GetStr(savePathTo);
            if (string.IsNullOrEmpty(lastDir) || !Directory.Exists(lastDir))
            {
                lastDir = Application.StartupPath;
            }
            SettingsMgr.UpdateSettings(savePathTo, lastDir);

            var dlg = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".json",
                FileName = "",
                Filter = @"JSON|*.json|All files|*.*",
                FilterIndex = 1,
                Title = title,
                InitialDirectory = lastDir,
                ShowHelp = false,
                SupportMultiDottedExtensions = true,
            };
            if (dlg.ShowDialog() != DialogResult.OK) return null;
            if (!File.Exists(dlg.FileName))
            {
                KryptonMessageBox.Show(@"File does not exist!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            SettingsMgr.UpdateSettings(savePathTo, Path.GetDirectoryName(dlg.FileName));
            return dlg.FileName;
        }

        public static string PromptSave(string title, string fnameDefault, bool isExcel = false,
                                        SettingsEnum savePathTo = SettingsEnum.ProdListDirectory)
        {
            var lastDir = SettingsMgr.GetStr(savePathTo);
            if (string.IsNullOrEmpty(lastDir) || !Directory.Exists(lastDir))
            {
                lastDir = Application.StartupPath;
            }
            SettingsMgr.UpdateSettings(savePathTo, lastDir);

            var dlg = new SaveFileDialog
            {
                AddExtension = true,
                CheckPathExists = true,
                FileName = fnameDefault,
                DefaultExt = isExcel ? ".xlsx" : ".json",
                Filter = isExcel ? "XLSX|*.xlsx" : "JSON|*.json|All files|*.*",
                FilterIndex = 1,
                Title = title,
                InitialDirectory = lastDir,
                ShowHelp = false,
                SupportMultiDottedExtensions = true,
                CheckFileExists = false,
                OverwritePrompt = false
            };
            if (dlg.ShowDialog() != DialogResult.OK) return null;
            if (File.Exists(dlg.FileName) &&
                (KryptonMessageBox.Show(@"Overwrite existing file?", @"Overwrite",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes))
            {
                return null;
            }
            SettingsMgr.UpdateSettings(savePathTo, Path.GetDirectoryName(dlg.FileName));
            return dlg.FileName;
        }

        public static int CalculateDesiredWidth(Control parent, Font fnt, int padding = 4, int digits = 9, int decimals = 2)
        {
            using (var graphics = parent.CreateGraphics())
            {
                // Create a string that represents a number with specified digits and decimals
                string testString = new string('8', digits+decimals+2); // +2 == thousand separators
                // Measure the width of the string
                var stringSize = graphics.MeasureString(testString, fnt);
                // Convert the width from pixels to the current DPI and add padding
                int desiredWidth = (int)((stringSize.Width + 2 * padding) * ScalingFactor);
                return desiredWidth;
            }
        }

        /// <summary>
        /// This method rounds up a given number to the nearest power of ten specified
        /// by the decimal places, with negative decimal values rounding up to
        /// the nearest 10, 100, 1000, and so on.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static decimal RoundUp(decimal number, int decimals)
        {
            decimal multiplier = (decimal)Math.Pow(10, decimals);
            return Math.Ceiling(number * multiplier) / multiplier;

            /*
            Extra check to only round up to the next power of ten of the number,
            but that is not how Excel does it, only for educational purposes:

            // if the result is higher then the power of ten more as the
            // original number, round to the next power of ten for number
            if (decimals < 0 && Math.Log10(result) > Math.Log10(number))
            {
                return Math.Pow(10, Math.Floor(Math.Log10(number)) + 1);
            }
            return result;
            */
        }

        /// <summary>
        /// cost and schemaamt are the cost totals for quantity items.
        /// These will first be rounded so that their per-item cost is precise for 2 decimals.
        /// From there the retail and margin will be calculated.
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="schemaAmt"></param>
        /// <param name="quantity"></param>
        /// <param name="margin">Out: margin (if applicable)</param>
        /// <param name="applyMargin">optional, default false</param>
        /// <param name="marginPct">optional, default 0</param>
        /// <param name="applyRounding">optional, default false</param>
        /// <param name="roundingDigits">optional, default 1</param>
        /// <returns>Retail incl. margin</returns>
        public static decimal CalcRetail(ref decimal cost, ref decimal schemaAmt, int quantity,
                                         out decimal margin,
                                         bool applyMargin   = false, decimal marginPct = 0m, 
                                         bool applyRounding = false, int roundingDigits = 1)
        {
            margin = 0;
            if (quantity == 0) return 0;
            cost = Math.Round(cost / quantity, 2) * quantity;
            schemaAmt = Math.Round(schemaAmt / quantity, 2) * quantity;
            var totalCostWithoutMargin = cost + schemaAmt;
            var retail = totalCostWithoutMargin;
            if (applyMargin)
            {
                retail = Math.Round(totalCostWithoutMargin * (1 + marginPct / 100), 2, MidpointRounding.AwayFromZero);
            }
            if (applyRounding)
            {
                retail = RoundUp(retail, -roundingDigits);
            }
            margin = retail - totalCostWithoutMargin;
            return retail;
        }
    }
}
