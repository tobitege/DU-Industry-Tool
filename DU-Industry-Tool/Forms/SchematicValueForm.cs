using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;
using Krypton.Toolkit;
using Newtonsoft.Json;

namespace DU_Industry_Tool
{
    public partial class SchematicValueForm : KryptonForm
    {
        public SchematicValueForm()
        {
            InitializeComponent();
            PopulateCraftingTalents();
            PopulateGrid();
            BtnLoad.Click += BtnLoad_Click;
            BtnSave.Click += BtnSave_Click;
            BtnClear.Click += BtnClear_Click;
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            var lastDir = Properties.Settings.Default.ProdListDirectory;
            if (string.IsNullOrEmpty(lastDir) || !Directory.Exists(lastDir))
            {
                lastDir = Application.StartupPath;
            }
            Properties.Settings.Default.Save();

            var dlg = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".json",
                FileName = "",
                Filter = @"JSON|*.json|All files|*.*",
                FilterIndex = 1,
                Title = @"Load Schematic quantities",
                InitialDirectory = lastDir,
                ShowHelp = false,
                SupportMultiDottedExtensions = true,
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;
            if (!File.Exists(dlg.FileName))
            {
                KryptonMessageBox.Show(@"File does not exist!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Properties.Settings.Default.ProdListDirectory = Path.GetDirectoryName(dlg.FileName);
            Properties.Settings.Default.Save();

            try
            {
                var loadedInfo = JsonConvert.DeserializeObject<List<SchematicInfo>>(File.ReadAllText(dlg.FileName));
                if (loadedInfo?.Count == 0)
                {
                    KryptonMessageBox.Show(@"No data was recognized!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var total = 0m;
                foreach (DataGridViewRow row in schematicsGrid.Rows)
                {
                    var key = row.Cells[KEY.Index].Value.ToString();
                    var data = loadedInfo.FirstOrDefault(x => x.Key == key);
                    if (data?.Qty > 0)
                    {
                        var qty = (decimal)data.Qty;
                        row.Cells[Qty.Index].Value = null;
                        row.Cells[Sum.Index].Value = null;
                        if (decimal.TryParse(row.Cells[Quanta.Index].Value.ToString(), out decimal quanta))
                        {
                            var itemCost = quanta * qty;
                            row.Cells[Qty.Index].Value = qty;
                            row.Cells[Sum.Index].Value = itemCost;
                            DUData.Schematics[key].Qty = qty;
                            DUData.Schematics[key].Total = itemCost;
                            total += itemCost;
                        }
                    }
                }
                totalSumLabel.Text = $"Total: {total:#,##0.00}";

            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(@"Could not load file, maybe wrong one?" + Environment.NewLine + ex.Message,
                    @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var savedQty = DUData.Schematics
                .Where(kv => kv.Value.Qty > 0 && kv.Value.Total > 0)
                .Select(kv => new SchematicInfo { Key = kv.Value.Key, Qty = kv.Value.Qty.Value, Total = kv.Value.Total })
                .ToList();
            if (savedQty.Count == 0)
            {
                KryptonMessageBox.Show(@"No schematics with quantities found!", @"Nothing to save",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var lastDir = Properties.Settings.Default.ProdListDirectory;
            if (string.IsNullOrEmpty(lastDir) || !Directory.Exists(lastDir))
            {
                lastDir = Application.StartupPath;
            }
            Properties.Settings.Default.Save();

            var dlg = new SaveFileDialog
            {
                AddExtension = true,
                CheckPathExists = true,
                FileName = "Schematic quantities",
                DefaultExt = ".json",
                Filter = @"JSON|*.json|All files|*.*",
                FilterIndex = 1,
                Title = @"Save schematic quantities",
                InitialDirectory = lastDir,
                ShowHelp = false,
                SupportMultiDottedExtensions = true,
                CheckFileExists = false,
                OverwritePrompt = false
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            if (File.Exists(dlg.FileName) &&
                (KryptonMessageBox.Show(@"Overwrite existing file?", @"Overwrite",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes))
            {
                return;
            }
            Properties.Settings.Default.ProdListDirectory = Path.GetDirectoryName(dlg.FileName);
            Properties.Settings.Default.Save();

            try
            {
                var json = JsonConvert.SerializeObject(savedQty);
                File.WriteAllText(dlg.FileName, json);
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(@"Could not save schematic quantities! " + Environment.NewLine + ex.Message,
                    @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (KryptonMessageBox.Show(@"Really clear the list now?", @"Clear List", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            clearQuantities();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Commit talent values to global DUData store, reload schematics
        /// and repopulate the grid for cost display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void applyBtn_Click(object sender, EventArgs e)
        {
            applyBtn.Enabled = false;
            try
            {
                // save any available entries' quantities
                var savedQty = DUData.Schematics
                    .Where(kv => kv.Value.Qty > 0)
                    .Select(kv => new SchematicInfo { Key = kv.Value.Key, Qty = kv.Value.Qty.Value })
                    .ToList();

                ApplyTalentValues();
                DUData.LoadSchematics();

                // re-apply quantities and calculate the total again
                var total = 0m;
                foreach (var saved in savedQty)
                {
                    DUData.Schematics.TryGetValue(saved.Key, out var item);
                    if (item == null) continue;
                    item.Qty = saved.Qty;
                    item.Total = item.Cost * item.Qty;
                    total += (item.Total ?? 0m);
                }
                totalSumLabel.Text = $"Total: {total:#,##0.00}";
                PopulateGrid();
            }
            finally
            {
                applyBtn.Enabled = true;
            }
        }

        /// <summary>
        /// ButtonRow event handler to display value as number in corresponding label.
        /// The Tag property is used as index and name identifier.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRow1_ValueSelected(object sender, int e)
        {
            if (!(sender is ButtonRow tb)) return;
            var idx = 0;
            switch (tb.Tag)
            {
                case int tag:
                    idx = tag;
                    break;
                case string tagS:
                    {
                        if (!int.TryParse(tagS, out idx)) return;
                        break;
                    }
                default:
                    return;
            }
            if (idx < 0 || idx > 3) return;
            SetLabel(idx, tb.GetValue().ToString());
        }
        
        private void SetLabel(int idx, string val)
        {
            var el = Controls.Find("skill" + (idx + 1), true).FirstOrDefault();
            if (el is KLabel lbl)
            {
                lbl.Text = val;
            }
        }

        /// <summary>
        /// Fill grid with all schematics' cost as informational value (readonly).
        /// </summary>
        private void PopulateGrid()
        {
            schematicsGrid.SuspendLayout();
            try
            {
                schematicsGrid.Rows.Clear();
                foreach (var schema in DUData.Schematics.OrderBy(o => o.Key))
                {
                    schematicsGrid.Rows.Add(schema.Key, schema.Value.Name, schema.Value.BatchSize, schema.Value.BatchCost, schema.Value.Cost, schema.Value.Qty, schema.Value.Total);
                }
                schematicsGrid.AutoSize = true;
            }
            finally
            {
                schematicsGrid.ResumeLayout();
            }
        }

        private void PopulateCraftingTalents()
        {
            tbSkill1.SetValue(DUData.SchemCraftingTalents[0]);
            tbSkill2.SetValue(DUData.SchemCraftingTalents[1]);
            tbSkill3.SetValue(DUData.SchemCraftingTalents[2]);
            tbSkill4.SetValue(DUData.SchemCraftingTalents[3]);
        }

        private void ApplyTalentValues()
        {
            DUData.SchemCraftingTalents[0] = tbSkill1.GetValue();
            DUData.SchemCraftingTalents[1] = tbSkill2.GetValue();
            DUData.SchemCraftingTalents[2] = tbSkill3.GetValue();
            DUData.SchemCraftingTalents[3] = tbSkill4.GetValue();
        }

        private void clearQuantities()
        {
            var total = 0m;
            foreach (DataGridViewRow row in schematicsGrid.Rows)
            {
                row.Cells[Qty.Index].Value = null;
                row.Cells[Sum.Index].Value = null;
                var key = row.Cells[KEY.Index].Value.ToString();
                DUData.Schematics[key].Qty = null;
                DUData.Schematics[key].Total = null;
            }
            totalSumLabel.Text = $"Total: {total:#,##0.00}";
        }

        private void calcTotalSum()
        {
            var total = 0m;
            foreach (DataGridViewRow row in schematicsGrid.Rows)
            {
                if (row.Cells[Sum.Index].Value != null)
                {
                    if (decimal.TryParse(row.Cells[Sum.Index].Value.ToString(), out decimal quanta))
                    {
                        total += quanta;
                    }
                }
            }
            totalSumLabel.Text = $"Total: {total:#,##0.00}";
        }

        private void schematicsGrid_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != Qty.Index) return;
            var cellValue = schematicsGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            var quantity = 0;
            if (cellValue != null && !int.TryParse(cellValue.ToString(), out quantity))
            {
                e.Cancel = true;
                MessageBox.Show("Invalid quantity value. Please enter a valid integer value.");
                return;
            }
            try
            {
                var key = schematicsGrid.Rows[e.RowIndex].Cells[KEY.Index].Value.ToString();
                var quantaValue = schematicsGrid.Rows[e.RowIndex].Cells[Quanta.Index].Value;
                if (quantaValue == null)
                {
                    schematicsGrid.Rows[e.RowIndex].Cells[Sum.Index].Value = null;
                    DUData.Schematics[key].Qty = null;
                    DUData.Schematics[key].Total = null;
                    return;
                }
                if (!decimal.TryParse(quantaValue.ToString(), out decimal quanta))
                {
                    return;
                }
                if (quanta == 0 || quantity == 0)
                {
                    schematicsGrid.Rows[e.RowIndex].Cells[Qty.Index].Value = null;
                    schematicsGrid.Rows[e.RowIndex].Cells[Sum.Index].Value = null;
                    DUData.Schematics[key].Qty = null;
                    DUData.Schematics[key].Total = null;
                    return;
                }
                var total = quantity * quanta;
                schematicsGrid.Rows[e.RowIndex].Cells[Sum.Index].Value = $"{total:#,##0.00}";
                DUData.Schematics[key].Qty = quantity;
                DUData.Schematics[key].Total = total;
            }
            finally
            {
                calcTotalSum();
            }
        }

        private readonly string exl_int_format = "#,##0";
        private readonly string exl_num_format = "#,##0.00";

        private void exportKBtn_Click(object sender, EventArgs e)
        {
            var fname = "Schematics Export " + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
            var dlg = new SaveFileDialog
            {
                AddExtension = true,
                CheckPathExists = true,
                DefaultExt = ".xlsx",
                FileName = fname,
                Filter = @"XLSX|*.xlsx|All files|*.*",
                FilterIndex = 1,
                Title = @"Schematics Export",
                InitialDirectory = "",
                ShowHelp = false,
                SupportMultiDottedExtensions = true,
                CheckFileExists = false,
                OverwritePrompt = false
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;
            if (File.Exists(dlg.FileName) &&
                (KryptonMessageBox.Show(@"Overwrite existing file?", @"Overwrite",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes))
            {
                return;
            }
            fname = dlg.FileName;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Schematics Values " + DateTime.Now.ToString("yyyy-MM-dd"));

                int row = 1;
                int totalIdx = 0;
                var total = 0m;

                if (expOptInclTalents.Checked)
                {
                    worksheet.Cell(row, 1).Value = "Schematic Cost Optimization (-5% q)";
                    worksheet.Cell(row, 2).Value = DUData.SchemCraftingTalents[0];

                    row++;
                    worksheet.Cell(row, 1).Value = "Adv. Schematic Cost Optim. (-3% q)";
                    worksheet.Cell(row, 2).Value = DUData.SchemCraftingTalents[1];

                    row++;
                    worksheet.Cell(row, 1).Value = "Schematic Output Productivity (+3%)";
                    worksheet.Cell(row, 2).Value = DUData.SchemCraftingTalents[2];

                    row++;
                    worksheet.Cell(row, 1).Value = "Adv. Schematic Output Prod. (+2%)";
                    worksheet.Cell(row, 2).Value = DUData.SchemCraftingTalents[3];
                    worksheet.Range("A1:A4").Style.Font.Bold = true;

                    row++;
                    row++;
                }
                var colIdx = 1;
                worksheet.Cell(row, colIdx).Value = "Name";
                if (expOptInclSchemDetails.Checked)
                {
                    colIdx++;
                    worksheet.Cell(row, colIdx).Value = "Batch Size";
                    colIdx++;
                    worksheet.Cell(row, colIdx).Value = "Batch Cost (q)";
                    colIdx++;
                    worksheet.Cell(row, colIdx).Value = "Cost per 1 (q)";
                }
                else
                {
                    colIdx++;
                    worksheet.Cell(row, colIdx).Value = "Cost per 1 (q)";
                }
                if (expOptInclQtySums.Checked)
                {
                    colIdx++;
                    worksheet.Cell(row, colIdx).Value = "Quantity";
                    colIdx++;
                    worksheet.Cell(row, colIdx).Value = "Total (q)";
                    totalIdx = colIdx;
                }
                if (expOptInclSchemDetails.Checked)
                {
                    colIdx++;
                    worksheet.Cell(row, colIdx).Value = "Time (h:m:s)";
                    colIdx++;
                    worksheet.Cell(row, colIdx).Value = "Item ID";
                }
                worksheet.Row(row).CellsUsed().Style.Font.SetBold();

                foreach (var schem in DUData.Schematics.Where(x => (!expOptOnlyWithQty.Checked || x.Value.Qty > 0)))
                {
                    row++;
                    colIdx = 1;
                    worksheet.Cell(row, colIdx).Value = schem.Value.Name;
                    if (expOptInclSchemDetails.Checked)
                    {
                        colIdx++;
                        worksheet.Cell(row, colIdx).Value = schem.Value.BatchSize;
                        colIdx++;
                        worksheet.Cell(row, colIdx).Value = schem.Value.BatchCost;
                        worksheet.Cell(row, colIdx).Style.NumberFormat.Format = exl_num_format;
                    }
                    colIdx++;
                    worksheet.Cell(row, colIdx).Value = schem.Value.Cost;
                    worksheet.Cell(row, colIdx).Style.NumberFormat.Format = exl_num_format;
                    if (expOptInclQtySums.Checked)
                    {
                        if (schem.Value.Qty != null && schem.Value.Total != null)
                        {
                            colIdx++;
                            worksheet.Cell(row, colIdx).Value = schem.Value.Qty;
                            worksheet.Cell(row, colIdx).Style.NumberFormat.Format = exl_int_format;
                            colIdx++;
                            worksheet.Cell(row, colIdx).FormulaR1C1 = "=(R[0]C[-2]*R[0]C[-1])";
                            worksheet.Cell(row, colIdx).Style.NumberFormat.Format = exl_num_format;
                            total += (schem.Value.Total ?? 0);
                        }
                        else
                        {
                            colIdx += 2;
                        }
                    }
                    if (expOptInclSchemDetails.Checked)
                    {
                        colIdx++;
                        worksheet.Cell(row, colIdx).Value = "  "+Utils.GetReadableTime(schem.Value.BatchTime);
                        colIdx++;
                        worksheet.Cell(row, colIdx).Value = schem.Value.NqId;
                    }
                }
                if (totalIdx > 0)
                {
                    row++;
                    worksheet.Cell(row, totalIdx).Value = total;
                    worksheet.Cell(row, totalIdx).Style.NumberFormat.Format = exl_num_format;
                    worksheet.Cell(row, totalIdx).Style.Font.SetBold();
                }

                worksheet.ColumnsUsed().AdjustToContents(1, 50);

                try
                {
                    workbook.SaveAs(fname);
                    KryptonMessageBox.Show("Data successfully exported to file.", "Success",
                        MessageBoxButtons.OK, KryptonMessageBoxIcon.INFORMATION);

                }
                catch (Exception ex)
                {
                    KryptonMessageBox.Show(@"Export failed!\r\nMake sure the file is not already open in another application.\r\n" + ex.Message,
                        @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void duCraftImportBtn_Click(object sender, EventArgs e)
        {
            if (KryptonMessageBox.Show("Try to load skills from clipboard?\n\nMake sure to use the Copy Config button on the du-craft.online site now!\n\nThis will OVERWRITE above talent values!", @"Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }
            var ducraft = new DuCraftDataHandler();
            if (!ducraft.LoadFromClipboard()) return;
            tbSkill1.SetValue(ducraft.GetSkillValue(DuCraftDataHandler.CostOptimizationBasic));
            tbSkill2.SetValue(ducraft.GetSkillValue(DuCraftDataHandler.CostOptimizationAdvanced));
            tbSkill3.SetValue(ducraft.GetSkillValue(DuCraftDataHandler.OutputProductivityBasic));
            tbSkill4.SetValue(ducraft.GetSkillValue(DuCraftDataHandler.OutputProductivityAdvanced));
        }
    }
}
