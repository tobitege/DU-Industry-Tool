using System;
using System.Windows.Forms;
using DU_Helpers;
using Krypton.Toolkit;

namespace DU_Industry_Tool.Interfaces
{
    public interface IContentDocument
    {
        bool IsProductionList { get; set; }
        void HideAll();

        EventHandler RecalcProductionListClick { get; set; }
        EventHandler ItemClick { get; set; }
        EventHandler IndustryClick { get; set; }
        LinkClickedEventHandler LinkClick { get; set; }
        FontsizeChangedEventHandler FontChanged { get; set; }

        void SetCalcResult(CalculatorClass calc);
        decimal Quantity { get; set; }

        void CheckPaletteChanges(KryptonCustomPaletteBase palette);
        ThemeChangePublisher themeChangePublisher { get; set; }
    }
}
