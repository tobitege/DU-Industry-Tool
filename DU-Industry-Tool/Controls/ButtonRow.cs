using System;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace DU_Industry_Tool
{
    public partial class ButtonRow : UserControl
    {
        public event EventHandler<int> ValueSelected;

        private int minValue = 0;
        private int maxValue = 5;
        private int selectedValue = 0;

        public ButtonRow()
        {
            InitializeComponent();
            InitializeButtonRow();
        }
        private void InitializeButtonRow()
        {
            for (int i = minValue; i <= maxValue; i++)
            {
                var btn = new KryptonButton();
                btn.Margin = new System.Windows.Forms.Padding(2);
                btn.Name = "btn"+i;
                btn.Text = i.ToString();
                btn.Tag = i;
                btn.Width = 24;
                btn.Click += Button_Click;
                panel.Controls.Add(btn);
            }
        }

        private void UpdateColors()
        {
            // Highlight the selected button .LightBlue
            foreach (KryptonButton btn in panel.Controls)
            {
                btn.StateCommon.Back.Color1 = (btn.Tag.ToString() == selectedValue.ToString()) ? System.Drawing.Color.Coral : System.Drawing.SystemColors.Control;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (!(sender is KryptonButton clickedButton)) return;
            selectedValue = (int)clickedButton.Tag;
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
            UpdateButtonRow();
        }

        public void SetMax(int max)
        {
            if (max <= minValue || max > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(max), "Maximum value must be greater than minimum value.");
            }
            maxValue = max;
            UpdateButtonRow();
        }

        private void UpdateButtonRow()
        {
            panel.Controls.Clear();
            InitializeButtonRow();
            SetValue(selectedValue);
        }
    }
}
