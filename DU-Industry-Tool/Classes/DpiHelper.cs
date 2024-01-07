using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

public class ControlInfo
{
    public string Name { get; set; }
    public Rectangle Position { get; set; }
    public Size Size { get; set; }
    public Padding Margin { get; set; }
}

public static class DpiHelper
{
    private static Dictionary<Form, Dictionary<string, ControlInfo>> originalSizesAndPositions = new Dictionary<Form, Dictionary<string, ControlInfo>>();

    public static void StoreOriginalSizesAndPositions(Form form)
    {
        if (!originalSizesAndPositions.ContainsKey(form))
        {
            originalSizesAndPositions[form] = new Dictionary<string, ControlInfo>();
        }
        else
        {
            originalSizesAndPositions[form].Clear();
        }

        // Store the original sizes, positions, and margins of controls on the form
        foreach (Control control in form.Controls)
        {
            originalSizesAndPositions[form][control.Name] = new ControlInfo
            {
                Name = control.Name,
                Position = control.Bounds,
                Size = control.Size,
                Margin = control.Margin
            };
        }
    }

    public static void UpdateControlSizesAndPositions(Form form, DpiChangedEventArgs e)
    {
        float scale = e.DeviceDpiNew / (float)e.DeviceDpiOld;

        // Check if the form is in the dictionary
        if (!originalSizesAndPositions.ContainsKey(form))
        {
            return;
        }

        // Update the sizes, positions, and margins of controls on the form
        foreach (Control control in form.Controls)
        {
            // Check if the control is in the dictionary
            if (originalSizesAndPositions[form].ContainsKey(control.Name))
            {
                var controlInfo = originalSizesAndPositions[form][control.Name];

                var newBounds = new Rectangle(
                    (int)(controlInfo.Position.X * scale),
                    (int)(controlInfo.Position.Y * scale),
                    (int)(controlInfo.Position.Width * scale),
                    (int)(controlInfo.Position.Height * scale));

                control.Bounds = newBounds;
                control.Size = new Size((int)(controlInfo.Size.Width * scale), (int)(controlInfo.Size.Height * scale));
                control.Margin = new Padding(
                    (int)(controlInfo.Margin.Left * scale),
                    (int)(controlInfo.Margin.Top * scale),
                    (int)(controlInfo.Margin.Right * scale),
                    (int)(controlInfo.Margin.Bottom * scale));
            }
        }
    }

    public static void UpdateControlSizesAndPositions(Form form, string controlName, DpiChangedEventArgs e)
    {
        // Check if the form is in the dictionary
        if (!originalSizesAndPositions.ContainsKey(form) || e?.DeviceDpiOld < 75)
        {
            return;
        }

        float scale = e.DeviceDpiNew / (float)e.DeviceDpiOld;

        // Check if the control with the specified name is in the dictionary
        if (originalSizesAndPositions[form].ContainsKey(controlName))
        {
            var controlInfo = originalSizesAndPositions[form][controlName];
            var control = form.Controls[controlName];

            var newBounds = new Rectangle(
                (int)(controlInfo.Position.X * scale),
                (int)(controlInfo.Position.Y * scale),
                (int)(controlInfo.Position.Width * scale),
                (int)(controlInfo.Position.Height * scale));

            control.Bounds = newBounds;
            control.Size = new Size((int)(controlInfo.Size.Width * scale), (int)(controlInfo.Size.Height * scale));
            control.Margin = new Padding(
                (int)(controlInfo.Margin.Left * scale),
                (int)(controlInfo.Margin.Top * scale),
                (int)(controlInfo.Margin.Right * scale),
                (int)(controlInfo.Margin.Bottom * scale));
        }
    }

    public static void ClearAllData()
    {
        originalSizesAndPositions.Clear();
    }

    public static void RemoveFormByName(string formName)
    {
        var formToRemove = originalSizesAndPositions.Keys.FirstOrDefault(form => form.Name == formName);
        if (formToRemove != null)
        {
            originalSizesAndPositions.Remove(formToRemove);
        }
    }

    public static string SerializeDictionary()
    {
        return JsonConvert.SerializeObject(originalSizesAndPositions, Formatting.Indented);
    }

    public static bool SaveDictionaryToFile(string filePath, bool overwrite = true)
    {
        try
        {
            if (!overwrite && File.Exists(filePath))
            {
                Console.WriteLine($"File already exists at '{filePath}'. Not overwriting.");
                return false;
            }

            string json = SerializeDictionary();
            File.WriteAllText(filePath, json);
            return true;
        }
        catch (Exception ex)
        {
            // Handle the exception, e.g., log it or display an error message.
            Console.WriteLine($"Error saving dictionary to file: {ex.Message}");
            return false;
        }
    }

    public static bool LoadDictionaryFromFile(string filePath)
    {
        try
        {
            if (!File.Exists(filePath)) return false;
            string json = File.ReadAllText(filePath);
            originalSizesAndPositions = JsonConvert.DeserializeObject<Dictionary<Form, Dictionary<string, ControlInfo>>>(json);
            return true;

        }
        catch (Exception ex)
        {
            // Handle the exception, e.g., log it or display an error message.
            Console.WriteLine($"Error loading dictionary from file: {ex.Message}");
            return false;
        }
    }
}
