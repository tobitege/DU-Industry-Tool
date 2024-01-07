using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace DU_Industry_Tool
{
    public sealed partial class SkillForm : KryptonForm
    {
        private readonly Color HighlightColor = Color.Aquamarine;

        public SkillForm()
        {
            InitializeComponent();
            TimerLoad.Enabled = true;
        }

        private void TimerLoad_Tick(object sender, EventArgs e)
        {
            TimerLoad.Enabled = false;
            FillData();
        }

        public void FillData()
        {
            SuspendLayout();

            FlowPanel.BringToFront();
            FlowPanel.SuspendLayout();
            var applicableTalentsExist = Calculator.ApplicableTalents?.Any() == true;
            foreach (var talent in DUData.Talents.OrderBy(t => t.Name))
            {
                var panel = new FlowLayoutPanel
                {
                    WrapContents = false,
                    AutoSize = true,
                    Margin = new Padding(0)
                };
                if (applicableTalentsExist && Calculator.ApplicableTalents.Contains(talent.Name))
                {
                    panel.BackColor = HighlightColor;
                }
                var label = new Label
                {
                    Text = talent.Name,
                    AutoSize = false,
                    Location = new Point(4, 12),
                    Size = new Size(380, 30),
                    Margin = new Padding(4, 4, 4, 0),
                    TabStop = false
                };
                panel.Controls.Add(label);

                var textbox = new KryptonNumericUpDown()
                {
                    Text = talent.Value.ToString(),
                    Location = new Point(390, 0),
                    Size = new Size(30, 32),
                    Minimum = 0,
                    Maximum = 5,
                    InterceptArrowKeys = true,
                    UpDownAlign = LeftRightAlignment.Right
                };
                textbox.KeyPress += TextboxOnKeyPress;
                textbox.Enter += TextboxOnEnter;
                panel.Controls.Add(textbox);
                FlowPanel.Controls.Add(panel);
                FlowPanel.ScrollControlIntoView(panel);
            }
            FlowPanel.ResumeLayout();
            LblHint.Visible = false;
            Controls.Add(FlowPanel);
            ResumeLayout();
            AutoScroll = true;
            BtnSave.Enabled = true;
        }

        private void TextboxOnEnter(object sender, EventArgs e)
        {
            // Select value when entering editor
            if (sender is KryptonNumericUpDown comp && comp.Text.Length > 0)
                comp.Select(0, 1);
        }

        private void TextboxOnKeyPress(object sender, KeyPressEventArgs e)
        {
            // Prevent more than 1 digit, replace existing text
            if (sender is KryptonNumericUpDown comp && comp.Text.Length > 0)
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                    return;
                }
                comp.Text = "";
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            foreach (var panel in FlowPanel.Controls.OfType<FlowLayoutPanel>())
            {
                if (panel.Controls.Count != 2) continue;
                var talentName = (panel.Controls[0] as Label).Text;
                var talentValue = (panel.Controls[1] as KryptonNumericUpDown).Value;
                DUData.Talents.First(t => t.Name == talentName).Value = (int)talentValue;
            }
            DUData.SaveTalents();
            Close();
        }
    }
}
