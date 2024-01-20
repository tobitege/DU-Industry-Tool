using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace DU_Helpers
{
    public enum SettingsEnum
    {
        ProdListDirectory,
        LaunchProdList,
        LastProductionList,
        LastLeftPos,
        LastTopPos,
        LastHeight,
        LastWidth,
        RestoreWindow,
        RecentProdLists,
        ThemeId,
        FullSchematicQuantities,
        SchemCraftCost1,
        SchemCraftCost2,
        SchemCraftOutput1,
        SchemCraftOutput2,
        SchemApplyMargin,
        SchemGrossMargin,
        SchemApplyRounding,
        SchemRoundDigits,
        ProdListApplyMargin,
        ProdListGrossMargin,
        ProdListApplyRounding,
        ProdListRoundDigits,
    };

    internal static class SettingsMgr
    {
        private static string DefaultAppFolder { get ; set ; } = "DU-Industry-Tool";
        private static string DefaultFilename { get ; set ; } = "settings";
        private static readonly string[] SettingsNames = Enum.GetNames(typeof(SettingsEnum));
        private static string GetSettingsPath()
        {
            var p = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), DefaultAppFolder) + '\\';
            return p;
        }
        private static string SettingsFullPath => Path.Combine(GetSettingsPath(), $"{DefaultFilename}.json");

        public static readonly SortedDictionary<string, string> Settings = new SortedDictionary<string, string>();

        static SettingsMgr()
        {
            if (!Directory.Exists(GetSettingsPath()))
            {
                try
                {
                    Directory.CreateDirectory(GetSettingsPath());
                }
                catch (Exception e)
                {
                }
            }

            LoadSettings();
        }

        public static bool GetBool(SettingsEnum key)
        {
            if (!Settings.TryGetValue(SettingsNames[(int)key], out string value)) return false;
            if (value is string s) return s == "True";
            return false;
        }

        public static decimal GetDecimal(SettingsEnum key)
        {
            if (!Settings.TryGetValue(SettingsNames[(int)key], out string value)) return 0m;
            if (value is string s && decimal.TryParse(s, out var dec)) return dec;
            return 0m;
        }

        public static int GetInt(SettingsEnum key)
        {
            if (!Settings.TryGetValue(SettingsNames[(int)key], out string value)) return 0;
            if (value is string s && int.TryParse(s, out var iVal)) return iVal;
            return 0;
        }

        public static string GetStr(SettingsEnum key)
        {
            if (Settings.TryGetValue(SettingsNames[(int)key], out string value)) return value;
            return "";
        }

        public static bool SaveSettings()
        {
            try
            {
                var json = JsonConvert.SerializeObject(Settings);
                File.WriteAllText(SettingsFullPath, json);
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

        public static bool LoadSettings()
        {
            try
            {
                var loadedInfo = JsonConvert.DeserializeObject<SortedDictionary<string, string>>(File.ReadAllText(SettingsFullPath));
                if (loadedInfo?.Count == 0)
                {
                    return false;
                }

                foreach (var setting in SettingsNames)
                {
                    if (loadedInfo.TryGetValue(setting, out var val))
                    {
                        Settings[setting] = val;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.GetType().Name} - {ex.Message}");
                return false;
            }
        }

        public static void UpdateSettings(SettingsEnum key, bool value)
        {
            UpdateSettings(SettingsNames[(int)key], value ? "True" : "False");
        }

        public static void UpdateSettings(SettingsEnum key, int value)
        {
            UpdateSettings(SettingsNames[(int)key], value.ToString());
        }

        public static void UpdateSettings(SettingsEnum key, decimal value)
        {
            UpdateSettings(SettingsNames[(int)key], $"{value:0.000}");
        }

        public static void UpdateSettings(SettingsEnum key, string value)
        {
            UpdateSettings(SettingsNames[(int)key], value);
        }
        
        private static void UpdateSettings(string settingKey, string value)
        {
            Settings[settingKey] = value;
            OnSettingChanged(new SettingChangedEventArgs(settingKey, value));
        }

        public static event EventHandler<SettingChangedEventArgs> SettingChanged;

        private static void OnSettingChanged(SettingChangedEventArgs e)
        {
            SettingChanged?.Invoke(null, e);
        }
    }

    public class SettingChangedEventArgs : EventArgs
    {
        public string SettingKey { get; }
        public object NewValue { get; }

        public SettingChangedEventArgs(string settingKey, object newValue)
        {
            SettingKey = settingKey;
            NewValue = newValue;
        }
    }
}
