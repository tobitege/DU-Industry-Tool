using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
// ReSharper disable MemberCanBeProtected.Global

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
        SchemApplyMargin,
        SchemGrossMargin,
        SchemApplyRounding,
        SchemRoundDigits,
        ProdListApplyMargin,
        ProdListGrossMargin,
        ProdListApplyRounding,
        ProdListRoundDigits,
        LastTalentsSection,
        UseCustomTheme,
        LastCustomTheme,
        ResultsFontSize,
    }
    
    public class FileSettingsBase
    {
        public string DefaultAppFolder { get; set; } = "DU-Industry-Tool";

        public string DefaultFilename { get; set; } = "settings";

        public string GetSettingsPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), DefaultAppFolder) + '\\';
        }

        public string SettingsFullPath => Path.Combine(GetSettingsPath(), $"{DefaultFilename}.json");
        
        public bool CheckPath()
        {
            if (Directory.Exists(GetSettingsPath())) return true;
            try
            {
                Directory.CreateDirectory(GetSettingsPath());
                return true;
            }
            catch (Exception) { }
            return false;
        }
    }

    public class StringSettingsBase : FileSettingsBase
    {
        protected bool SaveSettings(SortedDictionary<string, string> settings)
        {
            try
            {
                var json = JsonConvert.SerializeObject(settings);
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

        protected bool LoadSettings(SortedDictionary<string, string> settings)
        {
            try
            {
                var loadedInfo = JsonConvert.DeserializeObject<SortedDictionary<string, string>>(File.ReadAllText(SettingsFullPath));
                if (loadedInfo == null || loadedInfo.Count == 0)
                {
                    return false;
                }
                foreach (var s in loadedInfo.Where(s => settings.ContainsKey(s.Key)))
                {
                    settings[s.Key] = s.Value;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.GetType().Name} - {ex.Message}");
                return false;
            }
        }
    }
    
    public class SettingsMgr : StringSettingsBase
    {
        private static SettingsMgr _instance;
        public static SettingsMgr Instance => _instance ?? (_instance = new SettingsMgr());

        private static readonly string[] SettingsNames = Enum.GetNames(typeof(SettingsEnum));

        public static readonly SortedDictionary<string, string> Settings = new SortedDictionary<string, string>();

        private SettingsMgr()
        {
            foreach (var setting in SettingsNames)
            {
                Settings[setting] = "";
            }
            CheckPath();
        }

        public static bool GetBool(SettingsEnum key)
        {
            if (!Settings.TryGetValue(SettingsNames[(int)key], out string value)) return false;
            if (value is string s) return s.ToLower() == "true";
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
            return Settings.TryGetValue(SettingsNames[(int)key], out string value) ? value : "";
        }

        public static bool LoadSettings()
        {
            return Instance.LoadSettings(Settings);
        }

        public static bool SaveSettings()
        {
            return Instance.SaveSettings(Settings);
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
