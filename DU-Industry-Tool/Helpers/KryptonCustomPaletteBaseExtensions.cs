using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using Krypton.Toolkit;

namespace Krypton.Toolkit.Extensions
{
    public static class KryptonCustomPaletteBaseExtensions
    {
        /*
        Example code:
        var palette = new KryptonCustomPaletteBase();
        palette.ProcessColors((name, color) =>
        {
            // Modify the color here as needed (replace!)
            color = Color.FromArgb(color.R, color.G, color.B);
            return color;
        });
        */

        public static void ProcessColors(this Krypton.Toolkit.KryptonCustomPaletteBase palette, Func<string, Color, Color> colorAction)
        {
            // Remember the current culture setting
            var culture = Thread.CurrentThread.CurrentCulture;

            try
            {
                // Prevent lots of redraw events until all loading completes
                palette.SuspendUpdates();

                // Use the invariant culture for persistence
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

                IterateElementsWithColor(palette, false, colorAction);
            }
            finally
            {
                // Put back the old culture before existing routine
                Thread.CurrentThread.CurrentCulture = culture;
                // Must match the SuspendUpdates even if exception occurs
                palette.ResumeUpdates();
            }
        }

        /// <summary>
        /// Iterate all properties and fire colorAction for props of type Color.
        /// The func can return a changed value that is being written back to the palette.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ignoreDefaults"></param>
        /// <param name="colorAction"></param>
        private static void IterateElementsWithColor(object obj, bool ignoreDefaults, Func<string, Color, Color> colorAction)
        {
            // Cannot export from nothing
            if (obj == null) return;
            // Grab the type information for the object instance
            var t = obj.GetType();
            // We are only interested in looking at the properties
            foreach (var prop in t.GetProperties())
            {
                // Search each of the attributes applied to the property
                foreach (var attrib in prop.GetCustomAttributes(false))
                {
                    // Is it marked with the special krypton persist marker?
                    if (!(attrib is KryptonPersistAttribute persist)) continue;
                    // Should we navigate down inside the property?
                    if (persist.Navigate)
                    {
                        // If we can read the property value
                        if (!prop.CanRead) continue;
                        // Grab the property object
                        var childObj = prop.GetValue(obj, null);
                        // Test if the object contains only default values?
                        if (childObj != null && ignoreDefaults)
                        {
                            var propertyIsDefault = TypeDescriptor.GetProperties(childObj)["IsDefault"];
                            // All compound objects are expected to have an 'IsDefault' returning a boolean
                            if (propertyIsDefault != null && propertyIsDefault.PropertyType == typeof(bool))
                            {
                                // If the property is default then ignore it
                                if ((bool)propertyIsDefault.GetValue(childObj))
                                {
                                    continue;
                                }
                            }
                        }
                        // If we have an object to process
                        if (childObj == null) continue;
                        // Recurse into the object instance
                        IterateElementsWithColor(childObj, ignoreDefaults, colorAction);
                        continue;
                    }

                    // Grab the actual property value
                    var childObj2 = prop.GetValue(obj, null);
                    // Should we test if the property value is the default?
                    if (ignoreDefaults)
                    {
                        var defaultAttribs = prop.GetCustomAttributes(typeof(DefaultValueAttribute), false);
                        // Does this property have a default value attribute?
                        if (defaultAttribs.Length == 1)
                        {
                            // If the property is default then ignore it
                            if (((DefaultValueAttribute)defaultAttribs[0]).Value.Equals(childObj2))
                            {
                                continue;
                            }
                        }
                    }
                    // Continue if child is null or property not a Color value
                    if (prop.PropertyType != typeof(Color)) continue;
                    
                    // Fire delegate with property name and Color value
                    var newColor = colorAction(prop.Name, childObj2 == null ? Color.Empty : (Color)childObj2);
                    // Set the new color value back to the property
                    prop.SetValue(obj, newColor);
                }
            }
        }
    }

}