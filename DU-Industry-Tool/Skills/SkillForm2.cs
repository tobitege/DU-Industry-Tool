using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DU_Helpers;
using Krypton.Toolkit;
#pragma warning disable CS0169 // Field is never used

namespace DU_Industry_Tool.Skills
{
    public sealed partial class SkillForm2: KryptonForm
    {
        private readonly Color HighlightColor = Color.Aquamarine;
        private bool _applicableTalentsExist;
        private bool _changed;
        private string _lastSelected;
        private List<Talent> tmpT;

        public SkillForm2()
        {
            InitializeComponent();
            
            treeView.ExpandAll();
            flowPanel.AutoScroll = true;
            TimerLoad.Enabled = true;
            BtnSetAll.Enabled = false;
            CmbLevel.SelectedIndex = 0;
            LblSection.Text = "Loading...";
            tmpT = Talents.Values.Clone();
        }

        private void SkillForm2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_changed || e.CloseReason == CloseReason.WindowsShutDown) return;
            var res = KryptonMessageBox.Show("One or more talent values were changed!\r\n\r\n" +
                "Save the full talents list now to file?\r\n\r\n"+
                "No reverts all changes. Cancel keeps this dialogue open.", "Talents changed!",
                KryptonMessageBoxButtons.YesNoCancel, KryptonMessageBoxIcon.Exclamation);
            switch (res)
            {
                case DialogResult.Yes:
                    SaveTalents();
                    break;
                case DialogResult.No:
                    break;
                default:
                    e.Cancel = true;
                    break;
            }
        }

        private void SkillForm2_FormClosed(object sender, FormClosedEventArgs e)
        {
            _lastSelected = LblSection.Text;
            SettingsMgr.UpdateSettings(SettingsEnum.LastTalentsSection, _lastSelected);
            SettingsMgr.SaveSettings();
        }

        private void TimerLoad_Tick(object sender, EventArgs e)
        {
            TimerLoad.Enabled = false;
            _applicableTalentsExist = Calculator.ApplicableTalents?.Any() == true;

            _lastSelected = SettingsMgr.GetStr(SettingsEnum.LastTalentsSection);
            if (string.IsNullOrEmpty(_lastSelected))
            {
                _lastSelected = "Pures";
            }
            var node = FindNodeByText(treeView.Nodes, _lastSelected);
            if (node == null)
            {
                _lastSelected = "";
                return;
            }
            treeView.SelectedNode = node;
            node.EnsureVisible();
        }

        private void NumericUpDown_Validated(object sender, EventArgs e)
        {
            if (!(sender is KryptonNumericUpDown numericUpDown)) return;
            var key = numericUpDown.Tag as string;
            if (string.IsNullOrEmpty(key)) return;
            var talent = tmpT.FirstOrDefault(t => t.Key == key);
            if (talent == null || talent.Value == (int)numericUpDown.Value) return;
            _changed = true;
            BtnSave.Enabled = true;
            talent.Value = (int)numericUpDown.Value;
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!(e.Node.Tag is string section)) return;
            LblSection.Text = section;
            FillTree(section);
        }

        private void TextboxOnEnter(object sender, EventArgs e)
        {
            if (!(sender is KryptonNumericUpDown comp) || comp.Text.Length == 0) return;
            comp.Select(0, 1);
        }

        private void TextboxOnKeyPress(object sender, KeyPressEventArgs e)
        {
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
            SaveTalents();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnSetAll_Click(object sender, EventArgs e)
        {
            var val = CmbLevel.SelectedIndex;
            if (val < 0 || tmpT.All(x => x.Section != LblSection.Text)) return;
            if (KryptonMessageBox.Show($"Set all visible talents to value {val}?", "Batch update talents?",
                KryptonMessageBoxButtons.YesNo, KryptonMessageBoxIcon.Exclamation) != DialogResult.Yes)
            {
                return;
            }
            _changed = true;
            foreach(var entry in Talents.Where(x => x.Section == LblSection.Text))
            {
                tmpT.First(x => x.Key == entry.Key).Value = val;
            }
            FillTree(LblSection.Text);
        }

        private void BtnReload_Click(object sender, EventArgs e)
        {
            if (tmpT.All(x => x.Section != LblSection.Text)) return;
            if (KryptonMessageBox.Show("Reloading this section will revert all changes!"+
                    "\r\nContinune with reload?", "Reload talents?",
                    KryptonMessageBoxButtons.YesNo, KryptonMessageBoxIcon.Exclamation) != DialogResult.Yes)
            {
                return;
            }
            foreach (var entry in Talents.Where(x => x.Section == LblSection.Text))
            {
                tmpT.First(x => x.Key == entry.Key).Value = entry.Value;
            }
            FillTree(LblSection.Text);
        }

        private void FillTree(string section)
        {
            var sixRows = new[] { "Products", "Scraps" };
            var minRowCount = sixRows.Contains(section) ? 6 : 5;

            BtnSave.Enabled = false;
            flowPanel.Parent.SuspendLayout();
            flowPanel.Hide();
            try
            {
                flowPanel.SuspendLayout();
                flowPanel.Controls.Clear();

                var groupedTalents = tmpT
                    .Where(t => t.Section == section)
                    .OrderBy(t => t.Tier).ThenBy(t => t.Name)
                    .GroupBy(t => t.Group);

                var fnt = new Font(treeView.StateCommon.Node.Content.ShortText.Font?.Name ?? "Segoe UI",
                                   treeView.StateCommon.Node.Content.ShortText.Font?.Size ?? 10.8f);

                foreach (var group in groupedTalents)
                {
                    var groupBox = new KryptonGroupBox
                    {
                        MinimumSize = new Size(400, 50),
                        Text = group.Key.Replace("&", "&&"), // + " " + group.Tier,
                        Margin = new Padding(4),
                    };
                    groupBox.SuspendLayout();
                    groupBox.StateCommon.Content.ShortText.Font = (Font)fnt.Clone();
                    flowPanel.Controls.Add(groupBox);

                    var tableLayoutPanel = new KryptonTableLayoutPanel
                    {
                        ColumnCount = 2,
                        Dock = DockStyle.Fill,
                        Padding = new Padding(2),
                        Margin = new Padding(0)
                    };
                    tableLayoutPanel.SuspendLayout();

                    groupBox.Panel.Controls.Add(tableLayoutPanel);
                    groupBox.CaptionStyle = LabelStyle.BoldControl;
                    groupBox.CaptionOverlap = 100;

                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70));

                    // Loop over all entries in the group and
                    // put specific entries to the bottom
                    var lblCnt = 0;
                    foreach (var talent in group.OrderBy(t => t.Entry == "Efficiency" ||
                                                              t.Entry == "Productivity" ||
                                                              t.Entry == "Refinery" ? 1 : 0))
                    {
                        lblCnt++;
                        var lbl = new KryptonLabel
                        {
                            Text = talent.Entry,
                            Dock = DockStyle.Fill,
                        };
                        lbl.StateNormal.ShortText.Font = (Font)fnt.Clone();
                        if (lblCnt > 4)
                        {
                            lbl.StateNormal.ShortText.TextV = PaletteRelativeAlign.Near;
                        }
                        tableLayoutPanel.Controls.Add(lbl, 0, tableLayoutPanel.RowCount);
                        var numericUpDown = new KryptonNumericUpDown
                        {
                            Value = talent.Value,
                            Tag = talent.Key,
                            Dock = DockStyle.Fill,
                            Maximum = 5,
                            Minimum = 0,
                            StateNormal = { Back = { Color1 = Color.Bisque }, Content = { Color1 = Color.Black }}
                        };
                        numericUpDown.StateNormal.Content.Font = (Font)fnt.Clone();
                        numericUpDown.Validated += NumericUpDown_Validated;
                        numericUpDown.Enter += TextboxOnEnter;
                        numericUpDown.KeyPress += TextboxOnKeyPress;
                        tableLayoutPanel.Controls.Add(numericUpDown, 1, tableLayoutPanel.RowCount);

                        if (_applicableTalentsExist && Calculator.ApplicableTalents.Contains(talent.Name))
                        {
                            numericUpDown.StateCommon.Content.Color1 = Color.Black;
                            numericUpDown.StateCommon.Back.Color1 = HighlightColor;
                        }

                        tableLayoutPanel.RowCount++;
                    }
                    while (tableLayoutPanel.RowCount < minRowCount)
                    {
                        tableLayoutPanel.RowCount++;
                    }
                    if (section != "Schematics")
                    {
                        groupBox.MinimumSize = new Size(300, 40 + 34 * Math.Max(minRowCount, group.Count()));
                    }

                    tableLayoutPanel.ResumeLayout();
                    groupBox.ResumeLayout();
                }

                if (flowPanel.Controls.Count == 0) return;
                var groupBoxWidth = flowPanel.Controls[0].Width +
                                    flowPanel.Controls[0].Margin.Left * 2;
                Width = (groupBoxWidth * 2) + 40 +
                         splitContainer.SplitterDistance +
                         (splitContainer.SplitterWidth +
                         splitContainer.Panel2.Padding.Left +
                         flowPanel.Padding.Left +
                         Padding.Right) * 4;
            }
            finally
            {
                LblSection.Text = section;
                flowPanel.ResumeLayout();
                flowPanel.Show();
                flowPanel.Parent.ResumeLayout();
                BtnSetAll.Enabled = flowPanel.Controls.Count > 0;
                BtnSave.Enabled = _changed && BtnSetAll.Enabled;
            }
        }

        private void SaveTalents()
        {
            _changed = false;
            BtnSave.Enabled = false;
            Talents.Values = tmpT.Clone();
            TalentsManager.SaveTalentValues();
        }

        private TreeNode FindNodeByText(TreeNodeCollection nodes, string searchtext)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Text == searchtext) return node;
                var foundNode = FindNodeByText(node.Nodes, searchtext);
                if (foundNode != null) return foundNode;
            }
            return null;
        }
    }
}
