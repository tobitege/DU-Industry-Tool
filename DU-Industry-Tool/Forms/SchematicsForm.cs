using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;
using DU_Helpers;
using DU_Industry_Tool.Skills;
using Krypton.Toolkit;
using Newtonsoft.Json;

namespace DU_Industry_Tool
{
    public partial class SchematicValueForm : KryptonForm
    {
        private readonly string exl_int_format = "#,##0";
        private readonly string exl_num_format = "#,##0.00";
        private bool loading = true;

        public SchematicValueForm()
        {
            InitializeComponent();
            InitializeTalentButtons();
            PopulateGrid();
            BtnLoad.Click += BtnLoad_Click;
            BtnSave.Click += BtnSave_Click;
            BtnClear.Click += BtnClear_Click;
            ApplyGrossMarginCB.Checked = SettingsMgr.GetBool(SettingsEnum.SchemApplyMargin);
            grossMarginEdit.Value = Utils.ClampDec(SettingsMgr.GetDecimal(SettingsEnum.SchemGrossMargin), 0, 1000);
            ApplyRoundingCB.Checked = SettingsMgr.GetBool(SettingsEnum.SchemApplyRounding);
            RoundToCmb.SelectedIndex = Utils.ClampInt(SettingsMgr.GetInt(SettingsEnum.SchemRoundDigits),0,RoundToCmb.Items.Count-1);
            expCustSheet.Click += expCustSheet_Click;
            loading = false;
        }

        private void SchematicValueForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ApplyTalentValues();
            TalentValues.SaveValues();
            SettingsMgr.UpdateSettings(SettingsEnum.SchemApplyMargin, ApplyGrossMarginCB.Checked);
            SettingsMgr.UpdateSettings(SettingsEnum.SchemGrossMargin, grossMarginEdit.Value);
            SettingsMgr.UpdateSettings(SettingsEnum.SchemApplyRounding, ApplyRoundingCB.Checked);
            SettingsMgr.UpdateSettings(SettingsEnum.SchemRoundDigits, RoundToCmb.SelectedIndex+1);
            SettingsMgr.SaveSettings();
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            var fname = Utils.PromptOpen("Load Schematic quantities");
            if (fname == null) return;

            try
            {
                var loadedInfo = JsonConvert.DeserializeObject<List<SchematicInfo>>(File.ReadAllText(fname));
                if (loadedInfo == null || loadedInfo.Count == 0)
                {
                    KryptonMessageBox.Show("No data was recognized!", "Error", KryptonMessageBoxButtons.OK, false);
                    return;
                }
                Apply(loadedInfo);
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Could not load file, maybe wrong one?" + Environment.NewLine + ex.Message,
                    "ERROR", KryptonMessageBoxButtons.OK, false);
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
                MessageBox.Show("No schematics with quantities found!", "Nothing to save",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var fname = Utils.PromptSave("Save Schematic quantities", "Schematic quantities");
            if (fname == null) return;

            try
            {
                var json = JsonConvert.SerializeObject(savedQty);
                File.WriteAllText(fname, json);
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Could not save schematic quantities! " + Environment.NewLine + ex.Message,
                    @"ERROR", KryptonMessageBoxButtons.OK, false);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Really clear the list now?", @"Clear List", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            clearQuantities();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Apply(List<SchematicInfo> useThisData = null)
        {
            applyBtn.Enabled = false;
            try
            {
                List<SchematicInfo> data;
                if (useThisData == null)
                {
                    // save any available entries' quantities
                    data = DUData.Schematics
                        .Where(kv => kv.Value.Qty > 0)
                        .Select(kv => new SchematicInfo { Key = kv.Value.Key, Qty = kv.Value.Qty.Value })
                        .ToList();
                }
                else
                {
                    data = useThisData;
                }

                ApplyTalentValues();
                DUData.LoadSchematics();

                // re-apply quantities and calculate the total again
                var total = 0m;
                foreach (var saved in data)
                {
                    DUData.Schematics.TryGetValue(saved.Key, out var schem);
                    if (schem?.Name == null) continue;
                    schem.Qty = (int)(saved.Qty ?? 0);
                    var tmp = new ProductDetail { Cost = schem.Cost * (decimal)schem.Qty, Quantity = (decimal)schem.Qty };
                    Calculator.CalcRetail(tmp, ApplyGrossMarginCB.Checked, grossMarginEdit.Value, ApplyRoundingCB.Checked, RoundToCmb.SelectedIndex + 1);
                    schem.Total = tmp.Retail;
                    total += tmp.Retail;
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
        /// Commit talent values to global DUData store, reload schematics
        /// and repopulate the grid for cost display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void applyBtn_Click(object sender, EventArgs e)
        {
            Apply();
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
            if (!(el is KLabel lbl) || lbl.Text == val) return;
            lbl.Text = val;
            if (!loading) Apply();
        }

        /// <summary>
        /// Fill grid with all schematics' costs as informational value (readonly).
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

        private void InitializeTalentButtons()
        {
            tbSkill1.SetValue(SchemaTalentsCache.CostOptimizationBasic);
            tbSkill2.SetValue(SchemaTalentsCache.CostOptimizationAdvanced);
            tbSkill3.SetValue(SchemaTalentsCache.OutputProductivityBasic);
            tbSkill4.SetValue(SchemaTalentsCache.OutputProductivityAdvanced);
            // TODO
            //tbSkill5.SetValue(SchemaTalentsCache.ResearchTimeEfficiencyBasic);
            //tbSkill6.SetValue(SchemaTalentsCache.ResearchTimeEfficiencyAdvanced);
        }

        private void ApplyTalentValues()
        {

            SchemaTalentsCache.CostOptimizationBasic = tbSkill1.GetValue();
            SchemaTalentsCache.CostOptimizationAdvanced = tbSkill2.GetValue();
            SchemaTalentsCache.OutputProductivityBasic = tbSkill3.GetValue();
            SchemaTalentsCache.OutputProductivityAdvanced = tbSkill4.GetValue();
            // TODO
            //SchemaTalentsCache.ResearchTimeEfficiencyBasic = tbSkill5.GetValue();
            //SchemaTalentsCache.ResearchTimeEfficiencyAdvanced = tbSkill6.GetValue();
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
                if (row.Cells[Sum.Index].Value == null) continue;
                if (decimal.TryParse(row.Cells[Sum.Index].Value.ToString(), out decimal quanta))
                {
                    total += quanta;
                }
            }
            totalSumLabel.Text = $"Total: {total:#,##0.00}";
        }

        private void schematicsGrid_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != Qty.Index) return;
            var cellValue = schematicsGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            var quantity = 0;
            if (cellValue != null && !int.TryParse(cellValue.ToString(), out quantity))
            {
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

                var tmp = new ProductDetail { Cost = quanta * quantity, Quantity = quantity };
                Calculator.CalcRetail(tmp, ApplyGrossMarginCB.Checked, grossMarginEdit.Value, ApplyRoundingCB.Checked, RoundToCmb.SelectedIndex + 1);

                schematicsGrid.Rows[e.RowIndex].Cells[Sum.Index].Value = $"{tmp.Retail:#,##0.00}";
                DUData.Schematics[key].Qty = quantity;
                DUData.Schematics[key].Total = tmp.Retail;
            }
            finally
            {
                calcTotalSum();
            }
        }

        private void duCraftImportBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Try to load skills from clipboard?\n\n"+ 
                    "Make sure to use the Copy Config button on the du-craft.online site now!\n\n"+
                    "This will OVERWRITE above talent values!", @"Confirm",
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

        private void applyGrossMarginCB_CheckStateChanged(object sender, EventArgs e)
        {
            Apply();
        }

        private void ApplyRoundingCB_CheckStateChanged(object sender, EventArgs e)
        {
            Apply();
        }

        private void expCustSheet_Click(object sender, EventArgs e)
        {
            
            exportKBtn_Click(sender, e);
        }

        private void exportKBtn_Click(object sender, EventArgs e)
        {
            var isCustsheet = sender == expCustSheet;

            var title = "Schematics Export " + DateTime.Now.ToString("yyyy-MM-dd");
            var fname = title + ".xlsx";
            fname = Utils.PromptSave("Export schematics", fname, true);
            if (fname == null) return;

            using (var workbook = new XLWorkbook())
            {
                var ws = workbook.Worksheets.Add(title);

                int row = 1;
                int totalIdx = 0;
                IXLRange range = null;

                if (!isCustsheet && expOptInclTalents.Checked)
                {
                    ws.Cell(row, 1).Value = "Schematic Cost Optimization (-5% q)";
                    ws.Cell(row, 2).Value = SchemaTalentsCache.CostOptimizationBasic;

                    row++;
                    ws.Cell(row, 1).Value = "Adv. Schematic Cost Optim. (-3% q)";
                    ws.Cell(row, 2).Value = SchemaTalentsCache.CostOptimizationAdvanced;

                    row++;
                    ws.Cell(row, 1).Value = "Schematic Output Productivity (+3%)";
                    ws.Cell(row, 2).Value = SchemaTalentsCache.OutputProductivityBasic;

                    row++;
                    ws.Cell(row, 1).Value = "Adv. Schematic Output Prod. (+2%)";
                    ws.Cell(row, 2).Value = SchemaTalentsCache.OutputProductivityAdvanced;

                    row++;
                    ws.Cell(row, 1).Value = "Schematic Research Time Efficiency (-3%)";
                    ws.Cell(row, 2).Value = SchemaTalentsCache.ResearchTimeEfficiencyBasic;

                    row++;
                    ws.Cell(row, 1).Value = "Adv. Schematic Research Time Efficiency (-2%)";
                    ws.Cell(row, 2).Value = SchemaTalentsCache.ResearchTimeEfficiencyAdvanced;
                    
                    ws.Range($"A1:A{row}").Style.Font.Bold = true;
                    range = ws.Range($"B1:B{row}");
                    range.Style.NumberFormat.Format = exl_int_format;

                    row += 2;
                }
                var colIdx = 1;
                ws.Cell(row, colIdx).Value = "Name";
                if (!isCustsheet && expOptInclSchemDetails.Checked)
                {
                    ws.Cell(row, ++colIdx).Value = "Batch Size";
                    ws.Cell(row, ++colIdx).Value = "Batch Cost (q)";
                    ws.Cell(row, ++colIdx).Value = "Cost per 1 (q)";
                }
                else
                {
                    ws.Cell(row, ++colIdx).Value = "Cost per 1 (q)";
                }
                if (isCustsheet || expOptInclQtySums.Checked)
                {
                    ws.Cell(row, ++colIdx).Value = "Quantity";
                    ws.Cell(row, ++colIdx).Value = "Net total (q)";
                    totalIdx = colIdx;
                }
                if (!isCustsheet && expOptInclSchemDetails.Checked)
                {
                    if (ApplyGrossMarginCB.Checked && expOptInclQtySums.Checked)
                    {
                        ws.Cell(row, ++colIdx).Value = "Margin (q)";
                        ws.Cell(row, ++colIdx).Value = "Gross (q)";
                    }
                    ws.Cell(row, ++colIdx).Value = "Time (h:m:s)";
                    ws.Cell(row, ++colIdx).Value = "Item ID";
                }
                ws.Row(row).CellsUsed().Style.Font.SetBold();

                var dataStart = row+1;
                foreach (var schem in DUData.Schematics.Where(x => 
                             (!isCustsheet && !expOptOnlyWithQty.Checked || x.Value.Qty > 0)))
                {
                    row++;
                    colIdx = 1;
                    ws.Cell(row, colIdx).Value = schem.Value.Name;
                    if (!isCustsheet)
                    {
                        if (expOptInclSchemDetails.Checked)
                        {
                            ws.Cell(row, ++colIdx).Value = schem.Value.BatchSize;
                            ws.Cell(row, ++colIdx).Value = schem.Value.BatchCost;
                            ws.Cell(row, colIdx).Style.NumberFormat.Format = exl_num_format;
                        }
                        ws.Cell(row, ++colIdx).Value = schem.Value.Cost;
                        ws.Cell(row, colIdx).Style.NumberFormat.Format = exl_num_format;
                    }
                    else
                    {
                        ws.Cell(row, ++colIdx).Value = schem.Value.Total / schem.Value.Qty;
                        ws.Cell(row, colIdx).Style.NumberFormat.Format = exl_num_format;
                    }

                    if (isCustsheet || expOptInclQtySums.Checked)
                    {
                        if (schem.Value.Qty != null && schem.Value.Total != null)
                        {
                            ws.Cell(row, ++colIdx).Value = schem.Value.Qty;
                            ws.Cell(row, colIdx).Style.NumberFormat.Format = exl_int_format;
                            ws.Cell(row, ++colIdx).FormulaR1C1 = "=(R[0]C[-2]*R[0]C[-1])";
                            ws.Cell(row, colIdx).Style.NumberFormat.Format = exl_num_format;
                        }
                        else
                        {
                            colIdx += 2;
                        }
                    }
                    if (isCustsheet || !expOptInclSchemDetails.Checked) continue;

                    if (ApplyGrossMarginCB.Checked && expOptInclQtySums.Checked)
                    {
                        var tmp = new ProductDetail { Cost = schem.Value.Cost * (decimal)(schem.Value?.Qty ?? 0), Quantity = (decimal)(schem.Value?.Qty ?? 0) };
                        Calculator.CalcRetail(tmp, ApplyGrossMarginCB.Checked, grossMarginEdit.Value, ApplyRoundingCB.Checked, RoundToCmb.SelectedIndex + 1);
                        ws.Cell(row, ++colIdx).Value = tmp.Margin;
                        ws.Cell(row, ++colIdx).Value = tmp.Retail;
                        ws.XRangeFmtDec(row, colIdx-2, row, colIdx);
                    }
                    ws.Cell(row, ++colIdx).Value = "  " + Utils.GetReadableTime(schem.Value?.BatchTime ?? 0);
                    ws.Cell(row, ++colIdx).Value = schem.Value?.NqId ?? 0;
                }
                // Footer
                if (totalIdx > 0)
                {
                    row++;
                    totalIdx--;
                    var letter = 'A';
                    ws.XSetCellSum(ref row, ref totalIdx, dataStart, ref letter);
                    ws.Cell(row, totalIdx).Style.NumberFormat.Format = exl_num_format;
                    ws.Cell(row, totalIdx).Style.Font.SetBold();
                    if (!isCustsheet && expOptInclSchemDetails.Checked && ApplyGrossMarginCB.Checked)
                    {
                        ws.XSetCellSum(ref row, ref totalIdx, dataStart, ref letter); // Margin
                        ws.XSetCellSum(ref row, ref totalIdx, dataStart, ref letter); // Gross total
                        ws.XRangeBold(row, 1, row, totalIdx);
                        ws.XRangeFmtDec(row, 1, row, totalIdx);
                    }
                }

                ws.ColumnsUsed().AdjustToContents(1, 50);

                try
                {
                    workbook.SaveAs(fname);
                    KryptonMessageBox.Show("Data successfully exported to file.", "Success",
                        KryptonMessageBoxButtons.OK, false);

                }
                catch (Exception ex)
                {
                    KryptonMessageBox.Show("Export failed!\r\nMake sure the file is not already open in another application!\r\n" + ex.Message,
                        @"ERROR", KryptonMessageBoxButtons.OK, false);
                }
            }
        }
    }
}
