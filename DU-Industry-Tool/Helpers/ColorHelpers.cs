using System;
using System.Drawing;
using ColorConverter = ColorHelper.ColorConverter;

namespace DU_Helpers
{
    /*
    1. Foreground-text choice (relative-luminance & 0.179 threshold)
    • WCAG 2.1 definition of relative luminance: https://www.w3.org/TR/WCAG21/#dfn-relative-luminance
    • WCAG 2.1 contrast-ratio formula: https://www.w3.org/TR/WCAG21/#contrast-ratio
    • Derivation of the 0.179 decision point (3 : 1 contrast crossover) explained in this
      StackOverflow answer: https://stackoverflow.com/a/596241

    The threshold comes from setting the WCAG contrast-ratio formula equal for black-on-background
    and white-on-background and solving for L:
    (L + 0.05)/(1.05) = 1.05/(L + 0.05) ⇒ L ≈ 0.179.

    2. Linearisation of sRGB components (the “<= 0.03928 ? /12.92 : pow(..,2.4)” segment)
    • W3C “Relative Luminance” note (same WCAG section as above) – it defines the exact gamma-correction steps.

    3. Simple darken / lighten by channel interpolation
    • Sass/SCSS `darken()` / `lighten()` behaviour (percent of distance toward black or white):
      https://sass-lang.com/documentation/modules/color#darken
      We mimicked the same linear interpolation that users are familiar with in design tools.

    4. HSL model & channel-swap logic
    • Wikipedia HSL/HSV article describing the model and conversion equations: https://en.wikipedia.org/wiki/HSL_and_HSV
    */
    public class ColorHelpers
    {
        public static Color CalculateForegroundColor(Color backgroundColor)
        {
            double luminance = CalculateRelativeLuminance(backgroundColor);
            return luminance > 0.179 ? Color.Black : Color.White;
        }

        public static Color DarkenColor(Color color, int percent)
        {
            if (percent < 0 || percent > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(percent),
                    "Percent must be between 0 and 100 inclusive.");
            }

            int red = color.R - (color.R * percent / 100);
            int green = color.G - (color.G * percent / 100);
            int blue = color.B - (color.B * percent / 100);

            return Color.FromArgb(255, Math.Max(red, 0), Math.Max(green, 0), Math.Max(blue, 0));
        }

        public static Color LightenColor(Color color, int percent)
        {
            if (percent < 0 || percent > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(percent),
                    "Percent must be between 0 and 100 inclusive.");
            }

            int red = color.R + ((255 - color.R) * percent / 100);
            int green = color.G + ((255 - color.G) * percent / 100);
            int blue = color.B + ((255 - color.B) * percent / 100);

            return Color.FromArgb(255, Math.Min(red, 255), Math.Min(green, 255), Math.Min(blue, 255));
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

        public static Color TranslateColorToTargetHSL(Color sourceColor, Color targetColor, double lightnessAdjustment = 0.1)
        {
            // Convert both colors to HSL
            var targetHSL = ColorConverter.RgbToHsl(new ColorHelper.RGB(targetColor.R, targetColor.G, targetColor.B));
            var sourceHSL = ColorConverter.RgbToHsl(new ColorHelper.RGB(sourceColor.R, sourceColor.G, sourceColor.B));

            // Extract lightness
            byte lightness = sourceHSL.L;

            // Apply target hue and saturation
            sourceHSL.H = targetHSL.H;
            sourceHSL.S = targetHSL.S;
            sourceHSL.L = lightness;

            // Tone down the lightness
            if (lightnessAdjustment > 0.01)
            {
                sourceHSL.L = (byte)(Math.Max(0, Math.Min(sourceHSL.L - (byte)(lightnessAdjustment * 255), 255)));
            }

            // Convert back to RGB into a Color
            var rgb = ColorConverter.HslToRgb(sourceHSL);
            return Color.FromArgb(rgb.R, rgb.G, rgb.B);
        }

        private static double CalculateRelativeLuminance(Color color)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            r = r <= 0.03928 ? r / 12.92 : Math.Pow((r + 0.055) / 1.055, 2.4);
            g = g <= 0.03928 ? g / 12.92 : Math.Pow((g + 0.055) / 1.055, 2.4);
            b = b <= 0.03928 ? b / 12.92 : Math.Pow((b + 0.055) / 1.055, 2.4);

            return 0.2126 * r + 0.7152 * g + 0.0722 * b;
        }
    }
}