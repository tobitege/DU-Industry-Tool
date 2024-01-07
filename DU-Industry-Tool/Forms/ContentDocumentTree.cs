using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;
using DU_Industry_Tool.Interfaces;
using Krypton.Toolkit;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;

namespace DU_Industry_Tool
{
    public partial class ContentDocumentTree : UserControl, IContentDocument
    {
        private bool expand = false;
        private byte[] treeListViewViewState;
        private float fontSize;

        private Random _rand;

        public bool IsProductionList { get; set; }
        public EventHandler RecalcProductionListClick { get; set; }
        public EventHandler ItemClick { get; set; }
        public EventHandler IndustryClick { get; set; }
        public LinkClickedEventHandler LinkClick { get; set; }
        public decimal Quantity { get; set; }

        private SchematicRecipe Recipe { get; set; }
        private CalculatorClass Calc { get; set; }

        public ContentDocumentTree()
        {
            InitializeComponent();
            HideAll();
            fontSize = Font.Size;
        }

        public void HideAll()
        {
            // This can be called repeatedly for recalculation,
            // thus all the resetting of controls!
            _rand = new Random(DateTime.Now.Millisecond);
            LblHint.Text = Utils.FunHints[_rand.Next(Utils.FunHints.Length)];
            LblHint.Show();
            treeListView.Visible = false;
            GridTalents.Visible = false;
            BtnRestoreState.Visible = false;
            BtnSaveState.Visible = false;
            BtnToggleNodes.Visible = false;
            BtnRecalc.Visible = false;
            lblCostForBatch.Hide();
            lblCostValue.Hide();
            lblBasicCost.Hide();
            lblBasicCostValue.Hide();
            lblCostSingle.Hide();
            lblCostSingleValue.Hide();
            lblNano.Hide();
            pictureNano.Hide();
            lblUnitData.Hide();
            LblPure.Hide();
            LblPureValue.Hide();
            LblBatchSize.Hide();
            LblBatchSizeValue.Hide();
            lblDefaultCraftTime.Hide();
            lblDefaultCraftTimeValue.Hide();
            lblCraftTime.Hide("Production time:");
            lblCraftTimeValue.Hide("");
            lblCraftTimeInfoValue.Hide();
            lblPerIndustry.Hide("Per Industry:");
            lblPerIndustryValue.Tag = null;
            lblPerIndustryValue.Hide("");
            lblIndustry.Hide();
            lblIndustryValue.Hide();
            lblBatches.Hide();
            lblBatchesValue.Hide();
        }

        public void SetCalcResult(CalculatorClass calc)
        {
            // important: in case of repeat calculations, "remove" event handlers
            lblIndustryValue.Click -= LblIndustryValue_Click;
            lblPerIndustryValue.Click -= LblPerIndustryValue_Click;
            GridTalents.CellEndEdit -= GridTalentsOnCellEndEdit;

            Calc = calc;
            Recipe = Calc?.Recipe;
            if (Recipe == null) return;

            if (!calc.IsOre && !calc.IsPlasma)
            {
                if (IsProductionList)
                    SetupGrid(calc);
                else
                    BeginInvoke((MethodInvoker)delegate() { SetupGrid(calc); });
            }

            Quantity = calc.Quantity;
            kryptonHeaderGroup1.ValuesPrimary.Heading = Recipe.Name + (IsProductionList ? "" : $" (T{Recipe.Level})");

            // Fill some labels with info about the recipe
            var tmp = Recipe.UnitMass > 0 ? $"mass: {Recipe.UnitMass:N2} " : "";
            tmp += Recipe.UnitVolume > 0 ? $"volume: {Recipe.UnitVolume:N2} " : "";
            if (tmp != "")
            {
                lblUnitData.Text = "Unit " + tmp;
            }

            // Show green symbol when being nanocraftable
            if (Recipe.Nanocraftable)
            {
                lblNano.Show();
                pictureNano.Show();
                pictureNano.Image = Properties.Resources.Green_Ball;
            }

            // Show ore image if it exists (several are missing like Limestone, Malachite...)
            if (calc.IsOre)
            {
                var oreImg = $"ore_{Recipe.Name}.png".ToLower();
                OrePicture.Visible = OreImageList.Images.ContainsKey(oreImg);
                if (OrePicture.Visible)
                {
                    OrePicture.Image = OreImageList.Images[oreImg];
                }
            }

            List<string> applicableTalents;
            var time = calc.Recipe.Time * (calc.EfficencyFactor ?? 1);
            var newQty = calc.Quantity;

            // IF ore, then we basically display values from its Pure (except plasma)
            // to have any useful information for it :)
            Ore ore = null;
            var batches = 1m;
            var industry = calc.Recipe.Industry;

            if (calc.IsOre || calc.IsPlasma)
            {
                if (calc.IsPlasma)
                {
                    var extractor = SchematicRecipe.GetByName("Relic Plasma Extractor l");
                    if (extractor != null)
                    {
                        lblIndustryValue.Text = extractor.Name;
                        lblIndustry.Show();
                        lblIndustryValue.Click += LblIndustryValue_Click;
                    }
                }

                ore = DUData.Ores.FirstOrDefault(x => x.Key == Recipe.Key);
                if (ore == null)
                {
                    lblCostValue.Text = "Ore price not found!";
                    return;
                }
                calc.OreCost = ore.Value;

                // Get pure's data and transfer data to main calc object
                var pureKey = ore.Key.Substring(0, ore.Key.Length-3)+"Pure";
                if (Calculator.CreateByKey(pureKey, out var calcP))
                {
                    applicableTalents = calcP.GetTalents();
                    calc.CopyBaseValuesFrom(calcP);
                    calc.Recipe.Industry = calcP.Recipe.Industry;
                    industry = calcP.Recipe.Industry;
                    var ingrec = Calculator.GetIngredientRecipes(pureKey, calc.Quantity, true);
                    newQty = ingrec[0].Quantity;
                    if (calcP.CalcSchematicFromQty(calc.SchematicType, newQty, (decimal)(calc.BatchOutput ?? newQty),
                            out batches , out var minCost, out var _, out var _))
                    {
                        // store values in main calc!
                        calc.AddSchema(calcP.SchematicType, batches, minCost);
                        calc.AddSchematicCost(minCost);
                    }
                    time = calc.BatchTime ?? time;

                    LblPure.Show();
                    LblPureValue.Text = $"{newQty:N2} L";
                }
                else
                {
                    lblCostValue.Text = "Ore-data processing error!!!";
                    return;
                }
            }
            else
            {
                applicableTalents = calc.GetTalents();
            }

            LblHint.Hide();

            lblCostForBatch.Show();
            var batchQ = calc.Quantity;
            var fullCost = calc.OreCost + calc.SchematicsCost;
            // Ammo override: ammunition has special batch size of 40.
            // We take the requested amount as # of ammo rounds to be calculated
            // and determine the minimum of batches to be produced
            if (calc.IsAmmo && calc.BatchOutput > 0)
            {
                // TODO: full batch (Ceiling) or fractional batch??
                batchQ = batchQ / (decimal)(calc.BatchOutput ?? 40);
            }

            lblCostValue.Text = fullCost.ToString("N2") + " q ";
            if (calc.IsBatchmode)
            {
                if (calc.IsAmmo)
                {
                    lblCostValue.Text += $"({batchQ:N2} batches)";
                }
                else
                {
                    lblCostValue.Text += $"(x{batchQ:N2} / L)";
                }
            }
            lblBasicCost.Show();
            lblBasicCostValue.Text = $"{calc.OreCost:N2} q";
            if (calc.IsBatchmode && !calc.IsAmmo)
            {
                lblBasicCostValue.Text += " / L";
            }

            if (calc.Quantity > 1)
            {
                lblCostSingle.Show();
                lblCostSingleValue.Show();
                lblCostSingleValue.Text = (fullCost / calc.Quantity).ToString("N2") + " q ";
            }

            // Prepare talents grid and only show Recalculate button if any exist
            GridTalents.Rows.Clear();
            if (applicableTalents?.Any() == true)
            {
                foreach (var talent in applicableTalents.Select(talentKey =>
                             DUData.Talents.FirstOrDefault(x => x.Name == talentKey))
                             .Where(talent => talent != null))
                {
                    GridTalents.Rows.Add(CreateTalentsRow(talent));
                    GridTalents.Rows[GridTalents.Rows.Count - 1].Height = 26;
                }
                GridTalents.CellEndEdit += GridTalentsOnCellEndEdit;
            }

            GridTalents.Visible = GridTalents.RowCount > 0;
            BtnRecalc.Visible = GridTalents.Visible;

            // Show industry if applicable
            if (!IsProductionList && !string.IsNullOrEmpty(industry))
            {
                lblIndustry.Show();
                lblIndustryValue.Text = industry;
                lblIndustryValue.Click += LblIndustryValue_Click;
            }

            // Only with a given time we can show any production/batch times etc.
            if (time < 1) return;

            // For anything other than ores, pures, products, we can only display
            // basic production rate. Anything else is covered by tree data.
            if (!calc.IsBatchmode)
            {
                lblCraftTimeValue.Text = Utils.GetReadableTime(time);
                if (calc.Quantity > 1)
                {
                    lblCraftTimeInfoValue.Text = $"x{calc.Quantity:N2} = " + Utils.GetReadableTime(time * calc.Quantity);
                }
                var amt = 86400 / time;
                var amtS = $"{amt:N3}";
                if (amtS.EndsWith(".000")) amtS = amtS.Substring(0, amtS.Length - 4);
                if (amtS.EndsWith(".00")) amtS = amtS.Substring(0, amtS.Length - 3);
                lblPerIndustryValue.Text = $" {amtS} / day";
                lblPerIndustryValue.Tag = Recipe.Key + "#" + Math.Ceiling(amt);
                lblPerIndustryValue.Click += LblPerIndustryValue_Click;
                lblCraftTime.Show();
                if (IsProductionList) lblPerIndustry.Text = "Production rate:";
                lblPerIndustry.Show();
                return;
            }

            // Do not round these!
            var batchInputVol = (calc.IsProduct ? 100 : 65) * calc.InputMultiplier  + calc.InputAdder;
            var batchOutputVol = (calc.IsProduct ? 75 : 45) * calc.OutputMultiplier + calc.OutputAdder;

            lblDefaultCraftTime.Values.Text = "Default refin. time:";
            if (calc.IsAmmo)
            {
                batchInputVol = calc.BatchInput ?? 1;
                batchOutputVol = calc.BatchOutput ?? 40;
            }
            if (calc.IsPart || calc.IsProduct || calc.IsAmmo)
            {
                lblDefaultCraftTime.Values.Text = "Default prod. time: ";
            }
            if (calc.IsOre && calc.Tier == 1)
            {
                // Fit as many batches into 3 minutes as possible
                // In that case we must use Ceiling instead of Floor!
                batches = (int)(time > 180 ? Math.Ceiling(180 / time) : Math.Floor(180 / time));
                time = Math.Round(time * Math.Max(1, batches), 0);
            }

            lblDefaultCraftTimeValue.Values.Text = Utils.GetReadableTime(time);
            lblDefaultCraftTimeValue.Show();
            lblDefaultCraftTime.Show();

            // display some batch-based numbers
            var batchVol = Math.Max(1, (calc.IsOre ? batchInputVol : batchOutputVol));
            if (newQty >= 1 && batchInputVol > 0 && batchOutputVol > 0)
            {
                var batchCnt = (int)Math.Floor((calc.IsOre ? calc.Quantity : newQty) / batchVol);
                if (batchCnt == 0)
                {
                    lblCraftTime.Text = "0 batches (volume < input volume)";
                }
                else
                {
                    var overflow = (calc.IsOre ? calc.Quantity : newQty) - (batchCnt * batchVol);
                    lblCraftTimeValue.Text = Utils.GetReadableTime(time * batchCnt);
                    lblBatchesValue.Text = $"{batchCnt:N0} full batches";
                    lblBatchesValue.Show();
                    lblBatches.Show();
                    if (overflow > 0)
                    {
                        lblCraftTimeInfoValue.Text = (calc.IsOre ? "Unrefined:" : "Not produced:") + $" {overflow:N2}";
                    }
                    if (calc.IsBatchmode && !calc.IsOre)
                    {
                        LblPure.Text = calc.IsPure ? "Pure output:" : (calc.IsProduct ? "Product output:" : "Output:");
                        LblPureValue.Text = $"{batchCnt * batchVol:N2}";
                    }
                }
                lblCraftTime.Show();
            }

            var batchesPerDay = Math.Floor(86400 / (decimal)(calc.BatchTime ?? 86400));
            LblBatchSize.Show();
            lblPerIndustry.Show();
            LblBatchSizeValue.Text = $"{batchInputVol:N2} in / {batchOutputVol:N2} out";
            lblPerIndustryValue.Text = $"{batchesPerDay:N0} batches / day";
            lblPerIndustryValue.Tag = calc.Key + "#" + Math.Ceiling(batchesPerDay*batchVol);
            lblPerIndustryValue.Click += LblPerIndustryValue_Click;
        }

        private void SetupGrid(CalculatorClass calc)
        {
            BtnRestoreState.Visible = true;
            BtnSaveState.Visible = true;
            BtnToggleNodes.Visible = true;

            treeListView.Visible = true;

            BtnSaveState.Enabled = ButtonEnabled.True;
            ButtonRestoreState_Click(null, null);

            treeListView.BringToFront();
            kryptonHeaderGroup1.ValuesPrimary.Heading = calc.Name;

            try
            {
                treeListView.BeginUpdate();
                var c2g = new Calculator2OutputClass(treeListView);
                c2g.Fill(calc);

                olvColumnSection.AspectGetter = x =>
                {
                    if (x is RecipeCalculation t)
                    {
                        if ((t.IsSection && t.Section == DUData.SubpartSectionTitle) ||
                            (!t.IsSection && t.Section == DUData.ProductionListTitle) ||
                            (!t.IsSection && t.Section == DUData.SchematicsTitle))
                            return t.Entry ?? "";
                        return t.Section;
                    }
                    return "";
                };

                olvColumnSection.ImageGetter = x => x is RecipeCalculation t &&
                    t.IsSection && t.Section != DUData.SubpartSectionTitle ? 4 : -1; // folder

                olvColumnEntry.AspectGetter = x =>
                {
                    if (x is RecipeCalculation t)
                    {
                        if (!t.IsSection && (t.Section == DUData.ProductionListTitle || t.Section == DUData.SchematicsTitle))
                            return "";
                        return !t.IsSection && t.Entry != t.Section ? t.Entry : "";
                    }
                    return "";
                };

                olvColumnQty.AspectGetter = x => (x is RecipeCalculation t && t.Qty > 0 ? $"{t.Qty:N3}" : "");
                olvColumnAmt.AspectGetter = x => (x is RecipeCalculation t && t.Amt > 0 ? $"{t.Amt:N3}" : "");
                olvColumnMass.AspectGetter = x => (x is RecipeCalculation t && t.Mass > 0 ? $"{t.Mass:N3}" : "");
                olvColumnVol.AspectGetter = x => (x is RecipeCalculation t && t.Vol > 0 ? $"{t.Vol:N3}" : "");

                olvColumnSchemataQ.AspectGetter = x => (x is RecipeCalculation t && t.QtySchemata > 0 
                    ? (DUData.FullSchematicQuantities ? $"{t.QtySchemata:N0}" : $"{t.QtySchemata:N3}") 
                    : "");
                olvColumnSchemataA.AspectGetter = x => (x is RecipeCalculation t && t.AmtSchemata > 0 ? $"{t.AmtSchemata:N3}" : "");

                olvColumnTier.AspectGetter = x => (x is RecipeCalculation t && t.Tier > 0 ? $"{t.Tier}" : "");

                // Change drawing color of connection lines
                var renderer = treeListView.TreeColumnRenderer;
                renderer.LinePen = new Pen(Color.Firebrick, 0.8f) { DashStyle = DashStyle.Dash };
                renderer.IsShowGlyphs = true;
                renderer.UseTriangles = true;
            }
            finally
            {
                treeListView.EndUpdate();
            }

            expand = true;
            BtnToggleNodes_Click(null, null);
            if (treeListView.Items.Count > 0)
            {
                treeListView.Items[0].EnsureVisible();
            }
        }

        private static DataGridViewRow CreateTalentsRow(Talent talent)
        {
            var row = new DataGridViewRow { Height = 32 };
            row.Cells.Add(new DataGridViewTextBoxCell());
            row.Cells.Add(new KryptonDataGridViewNumericUpDownCell());
            row.Cells[0].ValueType = typeof(string);
            row.Cells[0].Value = talent.Name;
            if (row.Cells[1] is KryptonDataGridViewNumericUpDownCell cell)
            {
                cell.MaxInputLength = 1; // currently doesn't work :(
                cell.Maximum = 5;
                cell.Minimum = 0;
                cell.Value = talent.Value;
                cell.ValueType = typeof(int);
            }
            return row;
        }

        private void GridTalentsOnCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Any talent change needs to be saved back to file
            if (e.ColumnIndex != 1) return;
            var key = (string)GridTalents.Rows[e.RowIndex].Cells[0].Value;
            var cell = GridTalents.Rows[e.RowIndex].Cells[1];
            var talent = DUData.Talents.FirstOrDefault(x => x.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase));
            if (talent == null) return;
            talent.Value = (int)cell.Value;
            DUData.SaveTalents();
        }

        private void LblPerIndustryValue_Click(object sender, EventArgs e)
        {
            LinkClick?.Invoke(sender, new LinkClickedEventArgs((string)lblPerIndustryValue.Tag));
        }

        private void LblIndustryValue_Click(object sender, EventArgs e)
        {
            IndustryClick?.Invoke(sender, new LinkClickedEventArgs(lblIndustryValue.Text));
        }

        private void TreeListView_ItemActivate(object sender, EventArgs e)
        {
            if (ItemClick != null && treeListView.SelectedObject is RecipeCalculation r)
            {
                if (!DUData.SectionNames.Contains(r.Section))
                {
                    ItemClick.Invoke(sender, e);
                    return;
                }
            }
            if (treeListView.SelectedObject != null)
            {
                treeListView.ToggleExpansion(treeListView.SelectedObject);
            }
        }

        private void BtnRecalc_Click(object sender, EventArgs e)
        {
            if (IsProductionList)
            {
                RecalcProductionListClick?.Invoke(sender, e);
                return;
            }
            LinkClick?.Invoke(sender, new LinkClickedEventArgs(Calc.Key + $"#{Calc.Quantity:N2}"));
        }

        private void BtnToggleNodes_Click(object sender, EventArgs e)
        {
            if (treeListView.Roots == null) return;
            // Only expand/collapse the root items
            treeListView.BeginUpdate();
            foreach (var root in treeListView.Roots)
            {
                if (expand)
                    treeListView.Expand(root);
                else
                    treeListView.Collapse(root);
            }
            // This would be for ALL nodes:
            //if (expand)
            //    treeListView.ExpandAll();
            //else
            //    treeListView.CollapseAll();
            expand = !expand;
            treeListView.EndUpdate();
        }

        private void ButtonSaveState_Click(object sender, EventArgs e)
        {
            // SaveState() returns a byte array that holds the current state of the columns.
            treeListViewViewState = treeListView.SaveState();
            WriteCfg();
            CheckRestoreState();
        }

        private void ButtonRestoreState_Click(object sender, EventArgs e)
        {
            if (!treeListView.Visible || !CheckRestoreState()) return;
            try
            {
                treeListViewViewState = File.ReadAllBytes(CfgFilepath);
                treeListView.RestoreState(treeListViewViewState);
            }
            catch (Exception)
            {
            }
        }

        private bool CheckRestoreState()
        {
            var result = File.Exists(CfgFilepath);
            BtnRestoreState.Enabled = result ? ButtonEnabled.True : ButtonEnabled.False;
            return result;
        }

        private void WriteCfg()
        {
            try
            {
                if (File.Exists(CfgFilepath))
                {
                    File.Delete(CfgFilepath);
                }
                File.WriteAllBytes(CfgFilepath, treeListViewViewState);
            }
            catch (Exception)
            {
                KryptonMessageBox.Show("Could not write configuration to file!", "Error",
                    MessageBoxButtons.OK, KryptonMessageBoxIcon.ERROR);
            }
        }

        private static string CfgFilepath => Path.Combine(Application.StartupPath, "calcGridSettings.cfg");

        private void BtnFontUpOnClick(object sender, EventArgs e)
        {
            SetFont(1);
        }

        private void BtnFontDownOnClick(object sender, EventArgs e)
        {
            SetFont(-1);
        }

        private void SetFont(float fontDelta)
        {
            if ((fontDelta < 0 && fontSize > 9) || (fontDelta > 0 && fontSize < 18))
            {
                fontSize += fontDelta;
                treeListView.Font = new Font("Segoe UI", fontSize, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            }
        }

        private readonly string exl_int_format = "#,##0";
        private readonly string exl_num_format = "#,##0.00";
        
        private void exportProdListSection(IXLWorksheet worksheet, ref int i, ref int ix)
        {
            try
            {
                i++;
                ix += 2;
                worksheet.Cell(ix, 1).Value = "Item";
                worksheet.Cell(ix, 2).Value = "Quantity";
                worksheet.Cell(ix, 3).Value = "Item cost (q)";
                worksheet.Cell(ix, 4).Value = "Total (q)";
                worksheet.Cell(ix, 5).Value = "contained schematics (q)";
                worksheet.Cell(ix, 6).Value = "Mass (t)";
                worksheet.Cell(ix, 7).Value = "Volume (KL)";
                worksheet.Row(ix).CellsUsed().Style.Font.SetBold();
                var range = worksheet.Range($"B{ix}:G{ix}");
                range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                ix++;
                var prodListStart = ix;
                while (i < treeListView.Items.Count)
                {
                    var r = treeListView.Items[i];
                    var section = r.SubItems[olvColumnSection.Index].Text;

                    // we return when next section is reached
                    if (section == "Ores")
                    {
                        // leave "i" on current row
                        worksheet.Cell(ix, 4).FormulaA1 = $"=SUM(D{prodListStart}:D{ix - 1})"; // Total
                        worksheet.Cell(ix, 5).FormulaA1 = $"=SUM(E{prodListStart}:E{ix - 1})"; // Total schematics
                        worksheet.Cell(ix, 6).FormulaA1 = $"=SUM(F{prodListStart}:F{ix - 1})"; // Mass
                        worksheet.Cell(ix, 7).FormulaA1 = $"=SUM(G{prodListStart}:G{ix - 1})"; // Volume
                        range = worksheet.Range($"B{prodListStart}:B{ix}");
                        range.Style.NumberFormat.Format = exl_int_format;
                        range = worksheet.Range($"C{prodListStart}:G{ix}");
                        range.Style.NumberFormat.Format = exl_num_format;
                        worksheet.Row(ix).CellsUsed().Style.Font.SetBold();
                        return;
                    }

                    worksheet.Cell(ix, 1).Value = section;
                    var qty = 0m;
                    if (!string.IsNullOrEmpty(r.SubItems[olvColumnQty.Index].Text))
                    {
                        qty = decimal.Parse(r.SubItems[olvColumnQty.Index].Text);
                        worksheet.Cell(ix, 2).Value = qty;
                    }
                    if (qty > 0 && !string.IsNullOrEmpty(r.SubItems[olvColumnAmt.Index].Text))
                    {
                        var total = decimal.Parse(r.SubItems[olvColumnAmt.Index].Text);
                        var itemCost = Math.Round(total / qty, 2, MidpointRounding.AwayFromZero);
                        worksheet.Cell(ix, 3).Value = itemCost;
                        worksheet.Cell(ix, 4).FormulaR1C1 = "=(R[0]C[-2]*R[0]C[-1])";
                    }
                    if (!string.IsNullOrEmpty(r.SubItems[olvColumnSchemataA.Index].Text))
                    {
                        worksheet.Cell(ix, 5).Value = decimal.Parse(r.SubItems[olvColumnSchemataA.Index].Text);
                    }
                    if (!string.IsNullOrEmpty(r.SubItems[olvColumnMass.Index].Text))
                    {
                        worksheet.Cell(ix, 6).Value = decimal.Parse(r.SubItems[olvColumnMass.Index].Text);
                    }
                    if (!string.IsNullOrEmpty(r.SubItems[olvColumnVol.Index].Text))
                    {
                        worksheet.Cell(ix, 7).Value = decimal.Parse(r.SubItems[olvColumnVol.Index].Text);
                    }
                    ix++;
                    i++;
                }
            }
            catch { }
        }

        private void exportOresPuresSection(IXLWorksheet worksheet, string currSection, string nextSection, ref int i, ref int ix, bool orePrice, bool schems)
        {
            try
            {
                ix += 2;
                worksheet.Cell(ix, 1).Value = currSection;
                worksheet.Cell(ix, 2).Value = "Quantity (L)";
                if (orePrice)
                {
                    worksheet.Cell(ix, 3).Value = "q / L";
                    worksheet.Cell(ix, 4).Value = "Amount (q)";
                    worksheet.Cell(ix, 6).Value = "Mass (t)";
                    worksheet.Cell(ix, 7).Value = "Volume (KL)";
                }
                if (schems)
                {
                    worksheet.Cell(ix, 5).Value = "Schema qty.";
                    worksheet.Cell(ix, 6).Value = "Schema cost (q)";
                    worksheet.Cell(ix, 7).Value = "Schema type";
                }
                worksheet.Row(ix).CellsUsed().Style.Font.SetBold();
                var range = worksheet.Range($"B{ix}:G{ix}");
                range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                ix++;
                var dataStart = ix;
                i++;
                while (i < treeListView.Items.Count)
                {
                    var r = treeListView.Items[i];
                    var section = r.SubItems[olvColumnSection.Index].Text;

                    // we return when next section is reached
                    if (section == nextSection)
                    {
                        // leave "i" on current row
                        if (orePrice)
                        {
                            worksheet.Cell(ix, 2).FormulaA1 = $"=SUM(B{dataStart}:B{ix - 1})"; // L ore
                            worksheet.Cell(ix, 4).FormulaA1 = $"=SUM(D{dataStart}:D{ix - 1})"; // Amount
                            worksheet.Cell(ix, 6).FormulaA1 = $"=SUM(F{dataStart}:F{ix - 1})"; // Mass
                            worksheet.Cell(ix, 7).FormulaA1 = $"=SUM(G{dataStart}:G{ix - 1})"; // Volume
                        }
                        if (schems)
                        {
                            worksheet.Cell(ix, 6).FormulaA1 = $"=SUM(F{dataStart}:F{ix - 1})"; // Schema Amount
                        }
                        range = worksheet.Range($"B{dataStart}:G{ix}");
                        range.Style.NumberFormat.Format = exl_num_format;
                        worksheet.Row(ix).CellsUsed().Style.Font.SetBold();
                        return;
                    }

                    worksheet.Cell(ix, 1).Value = section;
                    decimal.TryParse(r.SubItems[olvColumnQty.Index].Text, out var qty);
                    worksheet.Cell(ix, 2).Value = Math.Round(qty, 2, MidpointRounding.AwayFromZero);
                    if (orePrice)
                    {
                        worksheet.Cell(ix, 3).Value = DUData.GetOrePriceByName(section);
                        worksheet.Cell(ix, 4).FormulaR1C1 = "=(R[0]C[-2]*R[0]C[-1])";
                        var rec = DUData.Recipes.FirstOrDefault(x => x.Value.Name == section);
                        if (rec.Value?.UnitMass != null)
                        {
                            worksheet.Cell(ix, 6).Value = (qty * rec.Value.UnitMass) / 1000;
                        }
                        if (rec.Value?.UnitVolume != null)
                        {
                            worksheet.Cell(ix, 7).Value = (qty * rec.Value.UnitVolume) / 1000;
                        }
                    }

                    if (schems && decimal.TryParse(r.SubItems[olvColumnSchemataQ.Index].Text, out qty))
                    {
                        worksheet.Cell(ix, 5).Value = qty;
                        if (decimal.TryParse(r.SubItems[olvColumnSchemataA.Index].Text, out qty))
                        {
                            worksheet.Cell(ix, 6).Value = qty;
                        }
                        if (!string.IsNullOrEmpty(r.SubItems[olvColumnFiller.Index].Text))
                        {
                            worksheet.Cell(ix, 7).Value = r.SubItems[olvColumnFiller.Index].Text;
                        }
                    }
                    ix++;
                    i++;
                }
            }
            catch { }
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            if (treeListView.Items.Count == 0) return;

            var dlg = new SaveFileDialog
            {
                AddExtension = true,
                CheckPathExists = true,
                DefaultExt = ".xlsx",
                Filter = @"XLSX|*.xlsx|All files|*.*",
                FilterIndex = 1,
                Title = @"Export",
                InitialDirectory = "",
                ShowHelp = false,
                SupportMultiDottedExtensions = true,
                CheckFileExists = false,
                OverwritePrompt = false
            };
            if (IsProductionList)
            {
                var mgr = DUData.IndyMgrInstance;
                dlg.FileName = DUData.ProductionListTitle;
                dlg.FileName += mgr.Databindings.ListLoaded ? " - " + Path.ChangeExtension(mgr.Databindings.GetFilename(), "") : ".";
            }
            else
            {
                dlg.FileName = $"Calculation {Calc.Name} (x{Calc.Quantity}).";
            }
            dlg.FileName += "xlsx";
            if (dlg.ShowDialog() != DialogResult.OK) return;

            if (File.Exists(dlg.FileName) &&
                (KryptonMessageBox.Show(@"Overwrite existing file?", @"Overwrite",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes))
            {
                return;
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Calculation");
                var ix = 1;

                worksheet.Cell(ix, 1).Value = IsProductionList ? DUData.ProductionListTitle : "Item calculation";
                worksheet.Row(ix).CellsUsed().Style.Font.SetBold();

                if (!IsProductionList)
                {
                    ix += 2;
                    worksheet.Cell(ix, 1).Value = "Item";
                    worksheet.Cell(ix, 2).Value = "Amount (q)";
                    worksheet.Cell(ix, 3).Value = "Quantity";
                    worksheet.Cell(ix, 4).Value = "Total (q)";
                    worksheet.Row(ix).CellsUsed().Style.Font.SetBold();

                    ix++;
                    worksheet.Cell(ix, 1).Value = kryptonHeaderGroup1.ValuesPrimary.Heading;
                    var total = Math.Round(Calc.OreCost + Calc.SchematicsCost, 2);
                    worksheet.Cell(ix, 2).Value = Calc.Quantity == 1 ? total : Math.Round(total / Calc.Quantity, 2);
                    worksheet.Cell(ix, 3).Value = Calc.Quantity;
                    worksheet.Cell(ix, 4).Value = total;

                    ix += 2;
                    worksheet.Cell(ix, 1).Value = "Item details";
                    worksheet.Row(ix).CellsUsed().Style.Font.SetBold();
                }

                var schemSection = false;
                var prodListEndIdx = 1;
                try
                {
                    var i = 0;
                    while (i < treeListView.Items.Count)
                    {
                        var r = treeListView.Items[i];
                        var section = r.SubItems[olvColumnSection.Index].Text;

                        if (section == DUData.ProductionListTitle)
                        {
                            exportProdListSection(worksheet, ref i, ref ix);

                            ix += 2;
                            worksheet.Cell(ix, 1).Value = "Below are totals for full order!";
                            worksheet.Row(ix).CellsUsed().Style.Font.SetBold();
                            prodListEndIdx = ix+1;
                            continue;
                        }
                        
                        if (section == "Ores")
                        {
                            if (!IsProductionList) prodListEndIdx = ix;
                            exportOresPuresSection(worksheet, section, "Pures", ref i, ref ix, true, false);
                            continue;
                        }
                        
                        if (section == "Pures")
                        {
                            exportOresPuresSection(worksheet, section, "Products", ref i, ref ix, false, true);
                            continue;
                        }
                        
                        if ((schemSection || (r.SubItems.Count == 10 && !string.IsNullOrEmpty(r.SubItems[3].Text))))
                        {
                            ix++;
                            worksheet.Cell(ix, 1).Value = r.SubItems[0].Text;
                            if (!string.IsNullOrEmpty(r.SubItems[olvColumnQty.Index].Text))
                            {
                                worksheet.Cell(ix, 2).Value = decimal.Parse(r.SubItems[olvColumnQty.Index].Text);
                            }
                            if (!string.IsNullOrEmpty(r.SubItems[olvColumnAmt.Index].Text))
                            {
                                worksheet.Cell(ix, 3).Value = decimal.Parse(r.SubItems[olvColumnAmt.Index].Text);
                            }
                            if (!string.IsNullOrEmpty(r.SubItems[olvColumnSchemataQ.Index].Text))
                            {
                                worksheet.Cell(ix, 5).Value = decimal.Parse(r.SubItems[olvColumnSchemataQ.Index].Text);
                            }
                            if (!string.IsNullOrEmpty(r.SubItems[olvColumnSchemataA.Index].Text))
                            {
                                worksheet.Cell(ix, 6).Value = decimal.Parse(r.SubItems[olvColumnSchemataA.Index].Text);
                            }
                            if (!string.IsNullOrEmpty(r.SubItems[olvColumnFiller.Index].Text))
                            {
                                worksheet.Cell(ix, 7).Value = r.SubItems[olvColumnFiller.Index].Text;
                            }
                        }
                        else
                        {
                            ix += 2;
                            worksheet.Cell(ix, 1).Value = section;
                            worksheet.Row(ix).CellsUsed().Style.Font.SetBold();
                            if (section == "Schematics")
                            {
                                schemSection = true;
                            }
                        }
                        i++;
                    }
                    var range = worksheet.Range($"B{prodListEndIdx}:G{ix}");
                    range.Style.NumberFormat.Format = exl_num_format;
                    worksheet.ColumnsUsed().AdjustToContents(1, 50);
                    workbook.SaveAs(dlg.FileName);

                    KryptonMessageBox.Show("Data successfully exported to file.", "Success",
                        MessageBoxButtons.OK, KryptonMessageBoxIcon.INFORMATION);
                }
                catch (Exception ex)
                {
                    KryptonMessageBox.Show(@"Could not save calculation!" + Environment.NewLine + ex.Message,
                        @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
