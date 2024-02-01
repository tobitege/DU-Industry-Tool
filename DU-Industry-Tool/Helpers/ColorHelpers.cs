using System;
using System.Drawing;

namespace DU_Helpers
{
    public class ColorHelpers
    {
        public static Color CalculateForegroundColor(Color backgroundColor)
        {
            return CalculateLuminance(backgroundColor) < 0.5f ? Color.White : Color.Black;
        }

        public static Color DarkenColor(Color color, int percent)
        {
            if (percent < 0 || percent > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(percent),
                    "Percent must be between 0 and 100 inclusive.");
            }

            float factor = (1 - percent / 100.0f);

            int red = Math.Max(Convert.ToInt32(color.R * factor), 0);
            int green = Math.Max(Convert.ToInt32(color.G * factor), 0);
            int blue = Math.Max(Convert.ToInt32(color.B * factor), 0);

            return Color.FromArgb(255, red, green, blue);
        }

        public static Color LightenColor(Color color, int percent)
        {
            if (percent < 0 || percent > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(percent),
                    "Percent must be between 0 and 100 inclusive.");
            }

            float factor = (1 + percent / 100.0f);

            int red = Math.Min(Convert.ToInt32(color.R * factor), 255);
            int green = Math.Min(Convert.ToInt32(color.G * factor), 255);
            int blue = Math.Min(Convert.ToInt32(color.B * factor), 255);

            return Color.FromArgb(255, red, green, blue);
        }

        private static float CalculateLuminance(Color color)
        {
            // Convert RGB values to luminance values
            float r = color.R / 255f;
            float g = color.G / 255f;
            float b = color.B / 255f;

            // Calculate luminance using the formula
            float luminance = 0.2126f * r + 0.7152f * g + 0.0722f * b;

            return luminance;
        }
    }
}