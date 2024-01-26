using System;
using System.Drawing;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DU_Helpers;
using Krypton.Toolkit;
using Timer = System.Windows.Forms.Timer;

namespace DU_Industry_Tool
{
    public sealed partial class SkillForm : KryptonForm
    {
        private readonly Color HighlightColor = Color.Aquamarine;
        private bool _applicableTalentsExist = false;
        private int colNameWidth;
        private int colValueWidth;
        private int padding = 15;

        public SkillForm()
        {
            InitializeComponent();
            TimerLoad.Enabled = true;
            colValueWidth = 60;
            colNameWidth = ClientSize.Width - (4 * padding) - colValueWidth;
        }

        private void TimerLoad_Tick(object sender, EventArgs e)
        {
            TimerLoad.Enabled = false;
            _applicableTalentsExist = Calculator.ApplicableTalents?.Any() == true;
            FillData();
        }

        private void AddSection(string title)
        {
            var panel = new FlowLayoutPanel
            {
                WrapContents = false,
                AutoSize = true,
                Margin = new Padding(0),
                BackColor = Color.Azure
            };
            var label = new Label
            {
                Text = title,
                AutoSize = false,
                BackColor = Color.Azure,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                ForeColor = Color.Navy,
                Location = new Point(4, 12),
                Size = new Size(650, 32),
                Margin = new Padding(4, 4, 4, 0),
                TabStop = false
            };
            panel.Controls.Add(label);
            FlowPanel.Controls.Add(panel);
            FlowPanel.ScrollControlIntoView(panel);
        }

        private void AddRow(Talent talent)
        {
            var panel = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowOnly,
                Margin = new Padding(0),
                WrapContents = false,
            };

            if (_applicableTalentsExist && Calculator.ApplicableTalents.Contains(talent.Name))
            {
                panel.BackColor = HighlightColor;
            }

            var label = new Label
            {
                Text = talent.Name,
                AutoSize = false,
                Location = new Point(2, 12),
                Size = new Size(colNameWidth, 30),
                Margin = new Padding(4, 4, 4, 0),
                TabStop = false
            };
            panel.Controls.Add(label);

            var textbox = new KryptonNumericUpDown()
            {
                Text = talent.Value.ToString(),
                Location = new Point(colNameWidth + padding, 0),
                Size = new Size(colValueWidth, 32),
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


        private void FillData()
        {
            FlowPanel.Controls.Clear();
            SuspendLayout();

            FlowPanel.BringToFront();
            FlowPanel.SuspendLayout();

            if (this.SearchTextBox.Text != "")
            {
                foreach (var talent in DUData.Talents.Where(t => t.Name.ToLower().Contains(this.SearchTextBox.Text.ToLower())).OrderBy(t => t.Name))
                {
                    AddRow(talent);
                }
            }
            else
            {
                AddSection("--- CRAFTING ---");
                foreach (var talent in DUData.Talents.Where(
                             t => !t.Name.Contains("Ammo") &&
                                  !t.Name.Contains("Scrap") &&
                                  !t.Name.Contains("Fuel")).OrderBy(t => t.Name))
                {
                    AddRow(talent);
                }

                AddSection("--- AMMO ---");
                foreach (var talent in DUData.Talents.Where(t => t.Name.Contains("Ammo")).OrderBy(t => t.Name))
                {
                    AddRow(talent);
                }

                AddSection("--- FUEL ---");
                foreach (var talent in DUData.Talents.Where(t => t.Name.Contains("Fuel")).OrderBy(t => t.Name))
                {
                    AddRow(talent);
                }

                AddSection("--- SCRAP ---");
                foreach (var talent in DUData.Talents.Where(t => t.Name.Contains("Scrap")).OrderBy(t => t.Name))
                {
                    AddRow(talent);
                }
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
            if (!(sender is KryptonNumericUpDown comp) || comp.Text.Length == 0) return;
            comp.Select(0, 1);
        }

        private void TextboxOnKeyPress(object sender, KeyPressEventArgs e)
        {
            // Prevent more than 1 digit, replace existing text
            if (!(sender is KryptonNumericUpDown comp) || comp.Text.Length == 0) return;
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
            comp.Text = "0";
            comp.Select(0, 1);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            foreach (var panel in FlowPanel.Controls.OfType<FlowLayoutPanel>())
            {
                if (panel.Controls.Count != 2) continue;
                if (!(panel.Controls[0] is Label lbl)) continue;
                var talentName = lbl.Text;
                if (!(panel.Controls[1] is KryptonNumericUpDown upDown)) continue;
                var talent = DUData.Talents.FirstOrDefault(t => t.Name == talentName);
                if (talent != null)
                {
                    talent.Value = (int)upDown.Value;
                }
            }
            DUData.SaveTalents();
            Close();
        }
        
        private void timerSearch_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("timer started");
            FillData();
            timerSearch.Stop();
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!timerSearch.Enabled) timerSearch.Start();
        }
    }
}
