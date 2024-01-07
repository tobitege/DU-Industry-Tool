using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace DU_Industry_Tool
{
    public partial class LabelRow : UserControl
    {
        public event EventHandler<int> ValueSelected;

        private int minValue = 0;
        private int maxValue = 5;
        private int selectedValue = 0;

        public LabelRow()
        {
            InitializeComponent();
            InitializeLabelRow();
            UpdateColors();
        }

        private void InitializeLabelRow()
        {
            using (var g = this.CreateGraphics())
            {
                float dpiX = g.DpiX;
                float dpiY = g.DpiY;
                int labelSize = 24;// (int)(24 * (dpiX / 96)); // 20 is the desired size at 96 DPI
                for (int i = minValue; i <= maxValue; i++)
                {
                    var lbl = new KryptonLabel();
                    var fontFamily = new FontFamily("Segoe UI");
                    var font = new Font(fontFamily, 10, FontStyle.Regular, GraphicsUnit.Point);
                    lbl.StateCommon.ShortText.Font = font;
                    lbl.AutoSize = false;
                    lbl.Margin = new System.Windows.Forms.Padding(0);
                    lbl.Name = "lbl" + i;
                    lbl.Text = i.ToString();
                    lbl.Tag = i;
                    lbl.Size = new Size(labelSize, labelSize);
                    lbl.Click += LabelClick;
                    panel.Controls.Add(lbl);
                }
            }
        }

        private void UpdateColors()
        {
            // Highlight the selected button .LightBlue
            foreach (KryptonLabel lbl in panel.Controls)
            {
                lbl.BackColor = (lbl.Tag.ToString() == selectedValue.ToString()) ? System.Drawing.Color.Coral : System.Drawing.SystemColors.Control;
                lbl.StateCommon.ShortText.Color1 = (lbl.Tag.ToString() == selectedValue.ToString()) ? System.Drawing.Color.Coral : System.Drawing.SystemColors.Control;
            }
        }

        private void LabelClick(object sender, EventArgs e)
        {
            if (!(sender is KryptonLabel clicked)) return;
            selectedValue = (int)clicked.Tag;
            UpdateColors();

            // Trigger the event to notify the parent form of the selected value
            ValueSelected?.Invoke(this, selectedValue);
        }

        public int GetValue()
        {
            return selectedValue;
        }

        public void SetValue(int value)
        {
            if (value < minValue || value > maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be within the specified range.");
            }
            selectedValue = value;
            UpdateColors();
            ValueSelected?.Invoke(this, selectedValue);
        }

        public void SetMin(int min)
        {
            if (min < 0 || min >= maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(min), "Minimum value must be less than maximum value.");
            }
            minValue = min;
            UpdateLabelRow();
        }

        public void SetMax(int max)
        {
            if (max <= minValue || max > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(max), "Maximum value must be greater than minimum value.");
            }
            maxValue = max;
            UpdateLabelRow();
        }

        private void UpdateLabelRow()
        {
            panel.Controls.Clear();
            InitializeLabelRow();
            SetValue(selectedValue);
        }
    }
}
