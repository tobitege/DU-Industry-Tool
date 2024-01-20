using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using Krypton.Toolkit;

namespace DU_Helpers
{
    [Serializable]
    public class DUCraftData
    {
        public string Version { get; set; }
        public string Language { get; set; }
        public Dictionary<string, int> SkillValues { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> Prices { get; set; } = new Dictionary<string, int>();

        public DUCraftData()
        {
        }
    }

    public class DuCraftDataHandler
    {
        private DUCraftData _duCraftData = new DUCraftData();

        // Public constants for skill keys
        public const string CostOptimizationBasic = "Schematics.Cost Optimization.Basic";
        public const string CostOptimizationAdvanced = "Schematics.Cost Optimization.Advanced";
        public const string OutputProductivityBasic = "Schematics.Output Productivity.Basic";
        public const string OutputProductivityAdvanced = "Schematics.Output Productivity.Advanced";
        // TODO
        //public const string ResearchTimeEfficiencyBasic = "Schematics.Research Time Efficiency.Basic";
        //public const string ResearchTimeEfficiencyAdvanced = "Schematics.Research Time Efficiency.Advanced";

        public bool LoadFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    KryptonMessageBox.Show($"File not found: {filePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                string jsonData = File.ReadAllText(filePath);
                LoadFromJson(jsonData);
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show($"Error loading from file:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool LoadFromClipboard()
        {
            try
            {
                if (!Clipboard.ContainsText())
                {
                    KryptonMessageBox.Show("Clipboard does not contain text data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                string jsonData = Clipboard.GetText();
                LoadFromJson(jsonData);
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show($"Clipboard data is not compatible, loading aborted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void LoadFromJson(string jsonData)
        {
            _duCraftData = JsonConvert.DeserializeObject<DUCraftData>(jsonData);

            // Mapping specific keys from the data to the SkillValues dictionary
            GetSkillValue(CostOptimizationBasic);
            GetSkillValue(CostOptimizationAdvanced);
            GetSkillValue(OutputProductivityBasic);
            GetSkillValue(OutputProductivityAdvanced);
            // TODO
            //GetSkillValue(ResearchTimeEfficiencyBasic);
            //GetSkillValue(ResearchTimeEfficiencyAdvanced);
        }

        public int GetSkillValue(string key)
        {
            return !_duCraftData.SkillValues.TryGetValue(key, out int skill) ? 0 : Utils.ClampInt(skill, 0, 5);
        }

        public DUCraftData GetCraftData()
        {
            return _duCraftData;
        }
    }

    public class DUCraftLoader : IDisposable
    {
        private DuCraftDataHandler DuCraftDataHandler;

        public DUCraftLoader()
        {
            DuCraftDataHandler = new DuCraftDataHandler();
        }

        public bool LoadJson()
        {
            var result = KryptonMessageBox.Show("Choose how to load JSON data:", "Load JSON", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (result)
            {
                case DialogResult.Yes:
                    OpenFileDialog openFileDialog = new OpenFileDialog
                    {
                        Filter = "JSON Files (*.json)|*.json",
                        Title = "Select JSON File"
                    };

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        return DuCraftDataHandler.LoadFromFile(openFileDialog.FileName);
                    }
                    else
                    {
                        KryptonMessageBox.Show("File selection canceled.", "Operation Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }

                case DialogResult.No:
                    return DuCraftDataHandler.LoadFromClipboard();

                case DialogResult.Cancel:
                default:
                    KryptonMessageBox.Show("Operation canceled.", "Operation Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
            }
        }

        public void Dispose()
        {
            // This method is called when the object is being disposed.
            // No unmanaged resources to release in this example.
        }
    }
}