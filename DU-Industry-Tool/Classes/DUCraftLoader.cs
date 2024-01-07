using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using Krypton.Toolkit;

[Serializable]
public class CraftData
{
    public string Version { get; set; }
    public string Language { get; set; }
    public Dictionary<string, int> SkillValues { get; set; } = new Dictionary<string, int>();
    public Dictionary<string, int> Prices { get; set; } = new Dictionary<string, int>();

    public CraftData()
    {
        // Default constructor
    }
}

public class DuCraftDataHandler
{
    private CraftData craftData = new CraftData();

    // Public constants for skill keys
    public const string OutputProductivityBasic = "Schematics.Output Productivity.Basic";
    public const string OutputProductivityAdvanced = "Schematics.Output Productivity.Advanced";
    public const string ResearchTimeEfficiencyBasic = "Schematics.Research Time Efficiency.Basic";
    public const string ResearchTimeEfficiencyAdvanced = "Schematics.Research Time Efficiency.Advanced";
    public const string CostOptimizationBasic = "Schematics.Cost Optimization.Basic";
    public const string CostOptimizationAdvanced = "Schematics.Cost Optimization.Advanced";

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
            return LoadFromJson(jsonData);
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error loading from file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
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
            return LoadFromJson(jsonData);
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error loading from clipboard: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }

    private bool LoadFromJson(string jsonData)
    {
        try
        {
            craftData = JsonConvert.DeserializeObject<CraftData>(jsonData);

            // Mapping specific keys from the data to the SkillValues dictionary
            TryGetSkillValue(OutputProductivityBasic);
            TryGetSkillValue(OutputProductivityAdvanced);
            TryGetSkillValue(ResearchTimeEfficiencyBasic);
            TryGetSkillValue(ResearchTimeEfficiencyAdvanced);
            TryGetSkillValue(CostOptimizationBasic);
            TryGetSkillValue(CostOptimizationAdvanced);

            return true;
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error loading from JSON: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }

    private void TryGetSkillValue(string key)
    {
        if (craftData.SkillValues.TryGetValue(key, out int value))
        {
            craftData.SkillValues[key] = value;
        }
        else
        {
            craftData.SkillValues[key] = 0;
        }
    }

    public int GetSkillValue(string key)
    {
        return craftData.SkillValues.TryGetValue(key, out int value) ? value : 0;
    }

    public CraftData GetCraftData()
    {
        return craftData;
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
