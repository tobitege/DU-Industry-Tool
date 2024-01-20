using System;
using System.Windows.Forms;

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
        void SetCalcResult(CalculatorClass calc);
        decimal Quantity { get; set; }
        int NumLabelWidth { get; set; }
    }
}
