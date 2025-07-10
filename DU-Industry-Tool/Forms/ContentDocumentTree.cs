using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;
using DU_Helpers;
using DU_Industry_Tool.Interfaces;
using DU_Industry_Tool.Skills;
using Krypton.Toolkit;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;

namespace DU_Industry_Tool
{
    public partial class ContentDocumentTree : UserControl, IContentDocument
    {
        #region Private declarations

        private static string CfgFilepath => Path.Combine(SettingsMgr.Instance.GetSettingsPath(), "calcGridSettings.cfg");

        private bool expand;
        private bool loading;
        private byte[] treeListViewViewState;
        private float fontSize;
        private List<string> _applicableTalents;
        private string industry;
        private SchematicRecipe Recipe { get; set; }
        private CalculatorClass Calc { get; set; }

        dynamic XRow = new ExpandoObject();
        private char XRetailCol = 'B'; // for excel export
        private int XRetailColIdx = 2; // for excel export
        private int XRetailRow = 4; // for excel export
        private int XMarginRow; // for excel export
        
        private Random _rand;

        #endregion

        #region Public declarations

        public bool IsProductionList { get; set; }
        public EventHandler RecalcProductionListClick { get; set; }
        public FontsizeChangedEventHandler FontSizeChanged { get; set; }
        public EventHandler ItemClick { get; set; }
        public EventHandler IndustryClick { get; set; }
        public LinkClickedEventHandler LinkClick { get; set; }
        public decimal Quantity { get; set; }

        public ContentDocumentTree(ThemeChangePublisher themeChangePublisher)
        {
            InitializeComponent();
            fontSize = Font.Size;

            // Load settings from global SettingsMrg for every new tab as defaults
            SettingsMgr.LoadSettings();
            loading = true;
            try
            {
                ApplyGrossMarginCB.Checked = SettingsMgr.GetBool(SettingsEnum.ProdListApplyMargin);
                grossMarginEdit.Value = Utils.ClampDec(SettingsMgr.GetDecimal(SettingsEnum.ProdListGrossMargin), 0, 1000);
                ApplyRoundingCB.Checked = SettingsMgr.GetBool(SettingsEnum.ProdListApplyRounding);
                RoundToCmb.SelectedIndex = DigitsToRoundCmbIndex(SettingsMgr.GetInt(SettingsEnum.ProdListRoundDigits));
            }
            finally
            {
                loading = false;
            }
            HideAll();

            var numLabelWidth = Utils.CalculateDesiredWidth(this, lblCostValue.Font, 10);
            foreach (Control control in HeaderGroup.Panel.Controls)
            {
                if (!(control is KLabel kLabel)) continue;
                if (control.Name.EndsWith("Value") && control.Left < 200)
                {
                    kLabel.AutoSize = false;
                    kLabel.Width = numLabelWidth;
                    kLabel.StateNormal.ShortText.TextH = PaletteRelativeAlign.Far;
                    continue;
                }
                if (control.Name.EndsWith("Suffix") && control.Left > 200)
                {
                    kLabel.Left = 100 + numLabelWidth;
                }
            }
            LblCostSuffix.Width = lblIndustryValue.Left - LblCostSuffix.Left - 8;
            LblSchemCostSuffix.Width = LblCostSuffix.Width;

            this.themeChangePublisher = themeChangePublisher;
            themeChangePublisher.Subscribe(this.OnThemeChange);
        }

        public void SetCalcResult(CalculatorClass calc)
        {
            var optionsOn = ApplyGrossMarginCB.Checked || ApplyRoundingCB.Checked;

            // important: in case of repeat calculations, "remove" event handlers
            lblIndustryValue.Click -= LblIndustryValue_Click;
            lblPerIndustryValue.Click -= LblPerIndustryValue_Click;
            GridTalents.CellEndEdit -= GridTalentsOnCellEndEdit;

            Calc = calc;
            Recipe = Calc?.Recipe;
            if (Recipe == null || Calc == null) return;
            Calc.Mass = Recipe.UnitMass ?? 0m;
            Calc.Volume = Recipe.UnitVolume ?? 0m;

            if (!calc.IsOre && !calc.IsPlasma)
            {
                if (IsProductionList)
                    SetupGrid(calc);
                else
                    BeginInvoke((MethodInvoker)delegate () { SetupGrid(calc); });
            }

            Quantity = calc.Quantity;
            HeaderGroup.ValuesPrimary.Heading = Recipe.Name + (IsProductionList ? "" : $" (T{Recipe.Level})");

            FillUnitDetails(Recipe);

            ShowNanocraftable(Recipe);

            var time = calc.Recipe.Time * (calc.EfficencyFactor ?? 1);
            var newQty = calc.Quantity;
            var batches = 1m;
            var industry = calc.Recipe.Industry;

            // IF ore, then we basically display values from its Pure (except plasma)
            // to have any useful information for it :)
            if (!ShowOreOrPlasma(ref calc, ref newQty, ref time, ref batches))
            {
                _applicableTalents = calc.GetTalents();
            }

            LblHint.Hide();

            SetLabelTextAndVisibility(lblCostForBatch, (optionsOn ? "Retail price:" : "Total cost:"));

            var batchQ = calc.Quantity;
            var fullCost = calc.Retail;
            var netCost = (calc.IsOre ? calc.Quantity : 1) * calc.OreCost + calc.SchematicsCost;

            // Ammo override: ammunition has special batch size of 40.
            // We take the requested amount as # of ammo rounds to be calculated
            // and determine the minimum of batches to be produced
            if (calc.IsAmmo && calc.BatchOutput > 0)
            {
                batchQ /= (calc.BatchOutput ?? 40);
            }

            SetLabelTextAndVisibility(lblCostValue, $"{fullCost:N2} q");
            SetLabelTextAndVisibility(LblCostSuffix, "", ApplyGrossMarginCB.Checked || ApplyRoundingCB.Checked);
            if (ApplyGrossMarginCB.Checked)
            {
                LblCostSuffix.Text = "incl. margin";
            }
            if (ApplyRoundingCB.Checked)
            {
                LblCostSuffix.Text += "; rounded";
            }

            if (!IsProductionList)
            {
                if (calc.IsBatchmode)
                {
                    if (calc.IsAmmo)
                    {
                        LblCostSuffix.Text += $" ({batchQ:N2} batches)";
                    }
                    else
                    {
                        LblCostSuffix.Text += $" (per {batchQ:N2} L)";
                    }
                }
                else
                {
                    LblCostSuffix.Text += $" (per {batchQ:N0})";
                }
            }

            SetLabelTextAndVisibility(lblMargin, null);//, ApplyGrossMarginCB.Checked);
            SetLabelTextAndVisibility(lblMarginValue, $"{calc.Margin:N2} q");//, ApplyGrossMarginCB.Checked);

            SetLabelTextAndVisibility(lblOreCost, calc.IsPlasma ? "Plasma:" : "Ore:");
            SetLabelTextAndVisibility(lblOreCostValue, $"{calc.OreCost:N2} q");
            if (!calc.IsPlasma && !calc.IsOre && !calc.IsPure)
            {
                SetLabelTextAndVisibility(LblOreCostSuffix, $"{(calc.OreCost / calc.Retail * 100):N2} %");
            }

            if (!calc.IsPlasma && !calc.IsOre)
            {
                SetLabelTextAndVisibility(lblSchematicsCost);
                SetLabelTextAndVisibility(lblSchematicsCostValue, $"{calc.SchematicsCost:N2} q");
                if (calc.SchematicsCost > 0.00m)
                {
                    SetLabelTextAndVisibility(LblSchemCostSuffix, $"{(calc.SchematicsCost / calc.Retail * 100):N2} %");
                }
            }

            if (IsProductionList && optionsOn)
            {
                SetLabelTextAndVisibility(lblCostSingle, "Net cost:");
                SetLabelTextAndVisibility(lblCostSingleValue, $"{netCost:N2} q");
            }
            else
            if (!calc.IsOre && !calc.IsPlasma && calc.Quantity > 1)
            {
                SetLabelTextAndVisibility(lblCostSingle, "Cost for 1:");
                SetLabelTextAndVisibility(lblCostSingleValue, (netCost / calc.Quantity).ToString("N2") + " q");
                SetLabelTooltip(lblCostSingleValue,
                    "Retail price: " + (Calc.Retail / calc.Quantity).ToString("N2") + " q",
                    ApplyGrossMarginCB.Checked || ApplyRoundingCB.Checked);
            }

            // Prepare talents grid and only show Recalculate button if any exist
            SetupTalentsGrid();

            BtnExport.Visible = treeListView.Items?.Count > 0;
            BtnRecalc.Visible = true;
            ApplyGrossMarginCB.Visible = BtnRecalc.Visible;
            grossMarginEdit.Visible = BtnRecalc.Visible;
            ApplyRoundingCB.Visible = BtnRecalc.Visible;
            RoundToCmb.Visible = BtnRecalc.Visible;
            BtnSaveOptions.Visible = BtnRecalc.Visible;

            // Show industry if applicable
            if (!IsProductionList && !string.IsNullOrEmpty(industry))
            {
                SetLabelTextAndVisibility(lblIndustryValue, industry, clickEvent: LblIndustryValue_Click);
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

                SetLinkLabel(lblPerIndustryValue, $" {amtS} / day", Recipe.Key + "#" + Math.Ceiling(amt), LblPerIndustryValue_Click);

                lblCraftTime.Show();
                if (IsProductionList) lblPerIndustry.Text = "Production rate:";
                lblPerIndustry.Show();
                return;
            }

            // Do not round these!
            var batchInputVol = 0m;
            var batchOutputVol = 0m;
            if (calc.IsBatchmode)
            {
                // the GetTalents() earlier *should* have set values already, but...
                if (!calc.IsOre && calc.BatchInput > 0 && calc.BatchOutput > 0 && calc.BatchTime > 0)
                {
                    batchInputVol = (decimal)calc.BatchInput;
                    batchOutputVol = (decimal)calc.BatchOutput;
                    time = (decimal)calc.BatchTime;
                }
                else
                {
                    batchInputVol = (calc.IsProduct ? 100 : (calc.IsAmmo ? 1 : 65)) * calc.InputMultiplier + calc.InputAdder;
                    batchOutputVol = (calc.IsProduct ? 75 : (calc.IsAmmo ? 40 : 45)) * calc.OutputMultiplier + calc.OutputAdder;
                    if (calc.IsOre && calc.BatchTime != null)
                    {
                        batchInputVol = (decimal)(calc.BatchInput ?? batchInputVol);
                        batchOutputVol = (decimal)(calc.BatchOutput ?? batchOutputVol);
                    }
                }
            }

            SetLabelTextAndVisibility(lblDefaultCraftTimeValue, Utils.GetReadableTime(time));
            SetLabelTextAndVisibility(lblDefaultCraftTime,
                (calc.IsPart || calc.IsAmmo) ? "Default prod. time: " : "Default refin. time:");

            // display some batch-based numbers
            var batchVol = Math.Max(1, batchOutputVol);
            if (newQty >= 1 && batchInputVol > 0 && batchOutputVol > 0)
            {
                var batchCnt = (int)Math.Floor((calc.IsOre ? calc.Quantity : newQty) / batchVol);
                SetLabelTextAndVisibility(lblCraftTime, batchCnt == 0 ? "0 batches (volume < input volume)" : "Production time");
                if (batchCnt > 0)
                {
                    SetLabelTextAndVisibility(lblCraftTimeValue, Utils.GetReadableTime(time * batchCnt));
                    SetLabelTextAndVisibility(lblBatchesValue, $"{batchCnt:N0} full batches");
                    lblBatches.Show();
                    var overflow = (calc.IsOre ? calc.Quantity : newQty) - (batchCnt * batchVol);
                    if (overflow > 0)
                    {
                        lblCraftTimeInfoValue.Text = (calc.IsOre ? "Unrefined:" : "Not produced:") + $" {overflow:N2}";
                    }
                    if (calc.IsBatchmode)
                    {
                        SetLabelTextAndVisibility(LblPure, calc.IsProduct ? "Product output:" : "Pure output:");
                        SetLabelTextAndVisibility(LblPureValue, $"{batchCnt * batchVol:N2} L");
                    }
                }
            }

            LblBatchSize.Show();
            SetLabelTextAndVisibility(LblBatchSizeValue, $"{batchInputVol:N2} in / {batchOutputVol:N2} out");

            lblPerIndustry.Show();
            var batchesPerDay = 0m;
            if (calc.BatchTime > 0)
            {
                batchesPerDay = Math.Floor(86400 / (decimal)(calc.BatchTime));
            }
            SetLinkLabel(lblPerIndustryValue, $"{batchesPerDay:N0} batches / day",
                calc.Key + "#" + Math.Ceiling(batchesPerDay * batchVol),
                LblPerIndustryValue_Click);
        }

        #endregion

        #region Theme change handling

        public ThemeChangePublisher themeChangePublisher { get; set; }

        public void CheckPaletteChanges(KryptonCustomPaletteBase palette)
        {
            if (palette?.BasePalette == null) return;

            // Update the ContentDocumentTree's appearance based on palette
            treeListView.UseAlternatingBackColors = true;
            treeListView.BackColor = DUData.SecondaryBackColor;
            treeListView.ForeColor = DUData.SecondaryForeColor;
                //ColorHelpers.CalculateForegroundColor(treeListView.BackColor);

            treeListView.AlternateRowBackColor = ColorHelpers.LightenColor(treeListView.BackColor, 5);
            if (treeListView.AlternateRowBackColor.R == treeListView.BackColor.R &&
                treeListView.AlternateRowBackColor.G == treeListView.BackColor.G &&
                treeListView.AlternateRowBackColor.B == treeListView.BackColor.B)
            {
                treeListView.AlternateRowBackColor = ColorHelpers.DarkenColor(treeListView.BackColor, 5);
            }

            // DEV testing code for palette exceptions
            //var sb = new StringBuilder();
            //var errors = 0;
            //foreach (var bstyle in Enum.GetValues(typeof(PaletteBackStyle)))
            //{
            //    errors = 0;
            //    sb.AppendLine("*** PBackStyle " + (PaletteBackStyle)bstyle);
            //    foreach (var pstate in Enum.GetValues(typeof(PaletteState)))
            //    {
            //        try
            //        {
            //            var tmp = palette.BasePalette.GetBackColor1((PaletteBackStyle)bstyle, (PaletteState)pstate);
            //        }
            //        catch (Exception e)
            //        {
            //            errors++;
            //            sb.AppendLine("PBackStyle " + (PaletteBackStyle)bstyle +" PState " + (PaletteState)pstate);
            //        }
            //        if (errors > 10)
            //        {
            //            sb.AppendLine("...");
            //            break;
            //        }
            //    }
            //}
        }

        #endregion

        #region Result display helpers

        public void HideAll()
        {
            FetchOptionsFromForm();
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(HideAll));
                return;
            }
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
            BtnExport.Visible = false;
            BtnSaveOptions.Visible = false;
            ApplyGrossMarginCB.Visible = false;
            grossMarginEdit.Visible = false;
            ApplyRoundingCB.Visible = false;
            RoundToCmb.Visible = false;
            lblCostForBatch.Hide();
            lblCostValue.Hide();
            LblCostSuffix.Hide("");
            lblOreCost.Hide();
            lblOreCostValue.Hide();
            LblOreCostSuffix.Hide();
            LblSchemCostSuffix.Hide();
            lblSchematicsCostValue.Hide();
            lblSchematicsCost.Hide();
            lblMargin.Hide();
            lblMarginValue.Hide();
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
            lblCraftTimeInfoValue.Hide("");
            lblPerIndustry.Hide("Per Industry:");
            lblPerIndustryValue.Tag = null;
            lblPerIndustryValue.Hide("");
            lblIndustryValue.Hide();
            lblBatches.Hide();
            lblBatchesValue.Hide();
            LblOptSaved.Stop();
            this.Refresh();
        }

        private void SetLabelTextAndVisibility(Control label, string text = null, bool isVisible = true, EventHandler clickEvent = null)
        {
            if (text != null) label.Text = text;
            label.Visible = isVisible;
            label.Click += clickEvent;
        }

        private void SetLabelTooltip(KryptonLabel label, string tip = null, bool isEnabled = true)
        {
            label.ToolTipValues.Description = tip ?? "";
            label.ToolTipValues.EnableToolTips = isEnabled;
        }

        private void SetLinkLabel(Control label, string text, string tag, EventHandler handler)
        {
            label.Text = text;
            label.Tag = tag;
            label.Click += handler;
        }

        private void SetPicureAndVisibility(PictureBox box, string imgName, ImageList imgList)
        {
            box.Visible = imgList.Images.ContainsKey(imgName);
            if (box.Visible)
            {
                box.Image = imgList.Images[imgName];
            }
        }

        private void FillUnitDetails(SchematicRecipe recipe)
        {
            // Fill some labels with info about the recipe
            var tmpM = Utils.ReadableDecimal(recipe.UnitMass, 'M', "mass: ");
            var tmpV = Utils.ReadableDecimal(recipe.UnitVolume, 'L', "volume: ");
            if (tmpM == "" && tmpV == "") return;
            var sep = tmpM != "" && tmpV != "" ? " ; " : "";
            lblUnitData.Text = (IsProductionList ? "Total " : "Unit ") + $"{tmpM}{sep}{tmpV}";
        }

        private void ShowNanocraftable(SchematicRecipe recipe)
        {
            // Show green symbol when being nanocraftable
            if (!recipe.Nanocraftable) return;
            lblNano.Show();
            pictureNano.Show();
            pictureNano.Image = Properties.Resources.Green_Ball;
        }
        
        private bool ShowOreOrPlasma(ref CalculatorClass calc, ref decimal newQty,
                                     ref decimal time, ref decimal batches)
        {
            // Show green symbol when being nanocraftable
            if (!calc.IsOre && !calc.IsPlasma) return false;
            if (calc.IsPlasma)
            {
                var extractor = SchematicRecipe.GetByName("Relic Plasma Extractor l");
                if (extractor != null)
                {
                    SetLabelTextAndVisibility(lblIndustryValue, extractor.Name, true, LblIndustryValue_Click);
                }
            }
            else
            {
                // Show ore image if it exists (several are missing like Limestone, Malachite...)
                SetPicureAndVisibility(OrePicture, $"ore_{Recipe.Name}.png".ToLower(), OreImageList);
            }

            var ore = DUData.Ores.FirstOrDefault(x => x.Key == Recipe.Key);
            if (ore == null)
            {
                lblCostValue.Text = "Ore price not found!";
                return false;
            }
            var item = new ProductDetail { Cost = calc.OreCost, Quantity = calc.Quantity, };
            Calculator.CalcRetail(item, ApplyGrossMarginCB.Checked, grossMarginEdit.Value, ApplyRoundingCB.Checked, RoundCmbIndexToDigits(RoundToCmb.SelectedIndex));
            calc.Margin = item.Margin;
            calc.Retail = item.Retail;
            SetLabelTextAndVisibility(LblOreCostSuffix, $"({ore.Value:N2} q/L)");

            // Get pure's data and transfer data to main calc object
            var pureKey = ore.Key.Substring(0, ore.Key.Length - 3) + "Pure";
            if (!Calculator.CreateByKey(pureKey, out var calcP))
            {
                lblCostValue.Text = "Ore-data processing error!!!";
                return false;
            }

            _applicableTalents = calcP.GetTalents();
            calc.CopyBaseValuesFrom(calcP);
            industry = calcP.Recipe.Industry;
            calc.Recipe.Industry = industry;
            var ingrec = Calculator.GetIngredientRecipes(pureKey, calc.Quantity, true);
            newQty = ingrec[0].Quantity;
            if (calc.IsOre && calc.BatchTime != null)
            {
                // Fit as many batches into 3 minutes as possible.
                // If batch time is less than 3 minutes, we must use Ceiling instead of Floor!
                time = (decimal)calc.BatchTime;
                batches = calc.Tier > 1 ? 1 : (time > 180 ? Math.Max(1, Math.Floor(180 / time)) : Math.Ceiling(180 / time));
                calc.BatchTime = Math.Round(time * batches, 0);
                calc.BatchInput *= batches;
                calc.BatchOutput *= batches;
            }

            if (calc.SchematicType != null && calcP.CalcSchematicFromQty(calc.SchematicType, newQty, (decimal)(calc.BatchOutput ?? newQty),
                    out batches, out var minCost, out var _, out var _))
            {
                // store values in main calc!
                calc.AddSchema(calcP.SchematicType, batches, minCost);
                calc.AddSchematicCost(minCost);
            }
            time = calc.BatchTime ?? time;
            LblPure.Show();
            SetLabelTextAndVisibility(LblPureValue, $"{newQty:N2} L");
            return true;
        }

        private void SetupTalentsGrid()
        {
            GridTalents.Rows.Clear();
            try
            {
                if (_applicableTalents?.Any() != true) return;
                foreach (var talent in _applicableTalents.Select(talentKey =>
                             Talents.FirstOrDefault(x => x.Name == talentKey))
                             .Where(talent => talent != null))
                {
                    GridTalents.Rows.Add(CreateTalentsRow(talent));
                    GridTalents.Rows[GridTalents.Rows.Count - 1].Height = 26;
                }
                GridTalents.CellEndEdit += GridTalentsOnCellEndEdit;
            }
            finally
            {
                GridTalents.Visible = GridTalents.RowCount > 0;
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Save options of current tab into global settings store, thus becoming
        /// default values for when the next tab is opened.
        /// </summary>
        private void SaveOptions()
        {
            if (loading) return;
            SettingsMgr.UpdateSettings(SettingsEnum.ProdListApplyMargin, ApplyGrossMarginCB.Checked);
            SettingsMgr.UpdateSettings(SettingsEnum.ProdListGrossMargin, grossMarginEdit.Value);
            SettingsMgr.UpdateSettings(SettingsEnum.ProdListApplyRounding, ApplyRoundingCB.Checked);
            SettingsMgr.UpdateSettings(SettingsEnum.ProdListRoundDigits, RoundCmbIndexToDigits(RoundToCmb.SelectedIndex));
            SettingsMgr.SaveSettings();
            LblOptSaved.FadeOut();
        }

        private void FetchOptionsFromForm()
        {
            CalcOptions.MarginPct = grossMarginEdit.Value;
            CalcOptions.ApplyMargin = ApplyGrossMarginCB.Checked && (CalcOptions.MarginPct > 0.00m);
            CalcOptions.ApplyRnd = ApplyRoundingCB.Checked;
            CalcOptions.RndDigits = RoundCmbIndexToDigits(RoundToCmb.SelectedIndex);
        }

        private void SetupGrid(CalculatorClass calc)
        {
            BtnRestoreState.Visible = true;
            BtnSaveState.Visible = true;
            BtnToggleNodes.Visible = true;

            treeListView.Visible = true;

            BtnSaveState.Enabled = ButtonEnabled.True;
            BtnRestoreState_Click(null, null);

            treeListView.BringToFront();
            HeaderGroup.ValuesPrimary.Heading = calc.Name;

            try
            {
                treeListView.BeginUpdate();
                var c2g = new Calculator2OutputClass(treeListView);
                c2g.Fill(calc);

                olvColumnSection.AspectGetter = x =>
                {
                    if (!(x is RecipeCalculation t)) return "";
                    if (( t.IsSection && t.Section == DUData.SubpartSectionTitle) ||
                        (!t.IsSection && t.Section == DUData.ProductionListTitle) ||
                        (!t.IsSection && t.Section == DUData.SchematicsTitle))
                        return t.Entry ?? "";
                    return t.Section;
                };

                olvColumnSection.ImageGetter = x => x is RecipeCalculation t &&
                    t.IsSection && t.Section != DUData.SubpartSectionTitle ? 4 : -1; // folder

                olvColumnEntry.AspectGetter = x =>
                {
                    if (!(x is RecipeCalculation t)) return "";
                    if (!t.IsSection && (t.Section == DUData.ProductionListTitle || t.Section == DUData.SchematicsTitle))
                        return "";
                    return !t.IsSection && t.Entry != t.Section ? t.Entry : "";
                };

                olvColumnQty.AspectGetter = x => (x is RecipeCalculation t && t.Qty > 0 ? $"{t.Qty:N2}" : "");
                olvColumnAmt.AspectGetter = x =>
                {
                    if (!(x is RecipeCalculation t) || t.Amt <= 0) return "";
                    return t.IsProdItem ? $"{t.Amt + t.AmtSchemata:N2}" : $"{t.Amt:N2}";
                };
                olvColumnMargin.AspectGetter = x => (x is RecipeCalculation t && t.Margin > 0 ? $"{t.Margin:N2}" : "");
                olvColumnRetail.AspectGetter = x => (x is RecipeCalculation t && t.Retail > 0 ? $"{t.Retail:N2}" : "");

                olvColumnMass.AspectGetter = x => (x is RecipeCalculation t && t.Mass > 0 ? $"{t.Mass:N3}" : "");
                olvColumnVol.AspectGetter = x => (x is RecipeCalculation t && t.Vol > 0 ? $"{t.Vol:N3}" : "");

                olvColumnSchemataQ.AspectGetter = x => (x is RecipeCalculation t && t.QtySchemata > 0 
                    ? (DUData.FullSchematicQuantities ? $"{t.QtySchemata:N0}" : $"{t.QtySchemata:N2}") 
                    : "");
                olvColumnSchemataA.AspectGetter = x => (x is RecipeCalculation t && t.AmtSchemata > 0 ? $"{t.AmtSchemata:N2}" : "");

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

        /// <summary>
        /// Depends on items of RoundToCmb entries!
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int RoundCmbIndexToDigits(int index)
        {
            return index < RoundToCmb.Items.Count - 1 ? index + 1 : RoundToCmb.Items.Count - 1;
        }

        /// <summary>
        /// Depends on items of RoundToCmb entries!
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        private int DigitsToRoundCmbIndex(int digits)
        {
            if (digits > RoundToCmb.Items.Count - 1)
            {
                digits = RoundToCmb.Items.Count;
            }
            return digits > 0 ? digits - 1 : 0;
        }

        #endregion

        #region UI events

        private void OnThemeChange(KryptonCustomPaletteBase palette)
        {
            CheckPaletteChanges(palette);
        }

        private void GridTalentsOnCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Any talent change needs to be saved back to file
            if (e.ColumnIndex != 1) return;
            var key = (string)GridTalents.Rows[e.RowIndex].Cells[0].Value;
            var cell = GridTalents.Rows[e.RowIndex].Cells[1];
            var talent = Talents.FirstOrDefault(x => x.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase));
            if (talent == null) return;
            talent.Value = (int)cell.Value;
            TalentsManager.SaveTalentValues();
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
                if (r.Entry != DUData.ProductionListTitle && !DUData.SectionNames.Contains(r.Section))
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
            if (!BtnRecalc.Enabled) return;
            BtnRecalc.Enabled = false;
            try
            {
                HideAll();
                if (IsProductionList)
                {
                    RecalcProductionListClick?.Invoke(sender, e);
                    return;
                }
                if (Calc == null) return;
                LinkClick?.Invoke(sender, new LinkClickedEventArgs(Calc.Key + $"#{Calc.Quantity:N2}"));
            }
            finally
            {
                BtnRecalc.Enabled = true;
            }
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

        private void BtnSaveState_Click(object sender, EventArgs e)
        {
            WriteCfg();
            CheckRestoreState();
        }

        private void BtnRestoreState_Click(object sender, EventArgs e)
        {
            if (!treeListView.Visible || !CheckRestoreState()) return;
            try
            {
                treeListViewViewState = File.ReadAllBytes(CfgFilepath);
                treeListView.RestoreState(treeListViewViewState);
                fontSize = (float)SettingsMgr.GetDecimal(SettingsEnum.ResultsFontSize);
                // lets default invalid values back to 9
                if (fontSize < 8) fontSize = 9f;
                else if (fontSize > 14) fontSize = 9f;
                treeListView.Font = new Font("Verdana", fontSize);
                FontSizeChanged?.Invoke(this, new FontsizeChangedEventArgs { Fontsize = fontSize });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            SettingsMgr.UpdateSettings(SettingsEnum.ResultsFontSize, (decimal)fontSize);
        }

        private void BtnFontUpOnClick(object sender, EventArgs e)
        {
            SetFont(0.5f);
        }

        private void BtnFontDownOnClick(object sender, EventArgs e)
        {
            SetFont(-0.5f);
        }

        private void BtnSaveOptions_Click(object sender, EventArgs e)
        {
            SaveOptions();
        }

        #endregion

        #region Grid settings management

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
                // SaveState() returns a byte array that holds the current state of the columns.
                treeListViewViewState = treeListView.SaveState();
                File.WriteAllBytes(CfgFilepath, treeListViewViewState);
            }
            catch (Exception)
            {
                KryptonMessageBox.Show("Could not write configuration to file!", "Error",
                    KryptonMessageBoxButtons.OK, false);
            }
            SettingsMgr.UpdateSettings(SettingsEnum.ResultsFontSize, (decimal)fontSize);
            SettingsMgr.SaveSettings();
        }

        private void SetFont(float fontDelta)
        {
            if ((fontDelta < 0 && fontSize > 8) || (fontDelta > 0 && fontSize < 14))
            {
                fontSize += fontDelta;
                treeListView.Font = new Font("Verdana", fontSize, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                FontSizeChanged?.Invoke(this, new FontsizeChangedEventArgs { Fontsize = fontSize });
            }
        }

        #endregion

        #region Excel export

        private void exportGetItem(int row)
        {
            
            if (row > treeListView.Items.Count-1)
                throw new ArgumentOutOfRangeException(nameof(row), "exportGetItem(): invalid index value");

            if (!IsProductionList)
            {
                XRow.Section = row >= 0 ? "Ores" : Calc.Name;
                XRow.Entry = "";
                XRow.Tier = $"{Calc.Tier:N0}";
                XRow.Qty = $"{Calc.Quantity:N2}";
                XRow.Amt = $"{Calc.OreCost + Calc.SchematicsCost:N2}";
                XRow.SchemataQ = "0.00";
                XRow.SchemataA = $"{Calc.SchematicsCost:N2}";
                XRow.Mass = $"{Calc.Mass:N2}";
                XRow.Vol = $"{Calc.Volume:N2}";
                XRow.Filler = "";
                XRow.Margin = $"{Calc.Margin:N2}";
                XRow.Retail = $"{Calc.Retail:N2}";
                return;
            }

            var r = treeListView.Items[row];
            XRow.Section = r.SubItems[olvColumnSection.Index].Text;
            XRow.Entry = r.SubItems[olvColumnEntry.Index].Text;
            XRow.Tier = r.SubItems[olvColumnTier.Index].Text;
            XRow.Qty = r.SubItems[olvColumnQty.Index].Text;
            XRow.Amt = r.SubItems[olvColumnAmt.Index].Text;
            XRow.SchemataQ = r.SubItems[olvColumnSchemataQ.Index].Text;
            XRow.SchemataA = r.SubItems[olvColumnSchemataA.Index].Text;
            XRow.Mass = r.SubItems[olvColumnMass.Index].Text;
            XRow.Vol = r.SubItems[olvColumnVol.Index].Text;
            XRow.Filler = r.SubItems[olvColumnFiller.Index].Text;
            XRow.Margin = r.SubItems[olvColumnMargin.Index].Text;
            XRow.Retail = r.SubItems[olvColumnRetail.Index].Text;
        }

        private void exportHeader(IXLWorksheet ws, ref int ix, string title)
        {
            try
            {
                // the XRetail* vars point to cell of gross order total and need to be adapted
                // if below layout ever changes!
                XRetailColIdx = 2;
                XRetailCol = Utils.LetterByIndex(XRetailColIdx);

                ws.CellSet(ix, 1, title, 5, true);

                if (expOptInclMargins.Checked && CalcOptions.ApplyMargin)
                {
                    // output net costs and the applied margin value
                    ix++;
                    ws.CellSet(ix, XRetailColIdx - 1, "Net total:");
                    ws.CellSet(ix, XRetailColIdx, Math.Round(Calc.OreCost + Calc.SchematicsCost, 2));
                    ix++;
                    ws.CellSet(ix, XRetailColIdx - 1, $"Margin ({CalcOptions.MarginPct:N2} %):");
                    ws.XR1C1(ix, XRetailColIdx, $"={XRetailCol}{ix + 1}-{XRetailCol}{ix - 1}");
                    ws.CellSet(ix, XRetailColIdx + 1, CalcOptions.MarginPct);
                    XMarginRow = ix;
                }
                ix++;
                ws.CellSet(ix, XRetailColIdx - 1, "Order total:");
                ws.CellSet(ix, XRetailColIdx, Calc.Retail);
                if (CalcOptions.ApplyRnd)
                {
                    ws.CellSet(ix, XRetailColIdx + 1, "(rounded up)");
                }
                ws.XCellFmtDec(ix, XRetailColIdx);
                XRetailRow = ix; // important, needed in detail section(s)

                ws.XRangeBold(1, 1, ix, 2);
                ws.XRangeFmtDec(2, 2, ix, 3);

                ix += 2;
                if (IsProductionList)
                {
                    ws.CellSet(ix, 1, "Item");
                }
                ws.CellSet(ix, 2, "Quantity");
                var col = 2;
                if (expOptInclMargins.Checked && CalcOptions.ApplyMargin)
                {
                    ws.Cell(ix, ++col).Value = "Ore cost per 1 (q)";
                    ws.Cell(ix, ++col).Value = "Schematics per 1 (q)";
                    ws.Cell(ix, ++col).Value = "Schema. %";
                    ws.Cell(ix, ++col).Value = "per 1 Total (q)";
                    ws.Cell(ix, ++col).Value = "Cost total (q)";
                    ws.Cell(ix, ++col).Value = "Margin (q)";
                    ws.Cell(ix, ++col).Value = "Retail (q)";
                }
                else
                {
                    ws.Cell(ix, ++col).Value = "Item price (q)";
                    ws.Cell(ix, ++col).Value = "Total (q)";
                }
                ws.Cell(ix, ++col).Value = "Mass (t)";
                ws.Cell(ix, ++col).Value = "Volume (KL)";
                ws.XRangeBold(ix, 1, ix, col);
                ws.XRangeRight(ix, 2, ix, col);
            }
            catch(Exception ex)
            {
                KryptonMessageBox.Show("Export header error, please report to developer.\r\n" + ex.Message,
                    "ERROR", KryptonMessageBoxButtons.OK, false);
            }
        }

        private void exportProdItems(IXLWorksheet ws, ref int i, ref int ix, int maxItems)
        {
            try
            {
                // the XRetail* vars point to cell of gross order total and need to be adapted
                // if below layout ever changes!
                XRetailColIdx = 2;
                XRetailCol = Utils.LetterByIndex(XRetailColIdx);

                if (IsProductionList)
                {
                    i++; // skip to first item
                }
                char letter = 'A';
                var col = 1;
                ix++;
                var pStart = ix;
                while (!IsProductionList || i < maxItems)
                {
                    // fetch data for current row
                    exportGetItem(i);

                    var section = (string)XRow.Section;

                    // Summation row on section change
                    if (section == "Ores")
                    {
                        // leave "i" unchanged!
                        col = 2;
                        if (expOptInclMargins.Checked && CalcOptions.ApplyMargin)
                        {
                            ws.XSetCellSum(ref ix, ref col, pStart, ref letter); // Ore cost
                            ws.XSetCellSum(ref ix, ref col, pStart, ref letter); // Schematics cost
                            col++; // Skip schematic %
                            ws.XSetCellSum(ref ix, ref col, pStart, ref letter); // x1 total
                            ws.XSetCellSum(ref ix, ref col, pStart, ref letter); // Line total
                            ws.Cell(XMarginRow - 1, 2).FormulaA1 = $"={letter}{ix}"; // Cost total
                            ws.XSetCellSum(ref ix, ref col, pStart, ref letter); // Line margin
                            ws.Cell(XMarginRow, 2).FormulaA1 = $"={letter}{ix}"; // Update margin total
                        }
                        else
                        {
                            col++;
                        }

                        ws.XSetCellSum(ref ix, ref col, pStart, ref letter); // Retail/Total

                        // Update order GROSS total, depending on margins and rounding options
                        if (!expOptInclMargins.Checked)
                        {
                            ws.CellSet(XRetailRow, XRetailColIdx, Calc.Retail);
                        }
                        else
                        if (expOptInclMargins.Checked && CalcOptions.ApplyMargin)
                        {
                            ws.XA1(XRetailRow, XRetailColIdx, CalcOptions.ApplyRnd
                                ? $"=ROUNDUP({XRetailCol}{XRetailRow - 2}+{XRetailCol}{XRetailRow - 1}, -{CalcOptions.RndDigits})"
                                : $"={letter}{ix}");

                        }
                        else
                        {
                            ws.XA1(XRetailRow, XRetailColIdx, CalcOptions.ApplyRnd
                                ? $"=ROUNDUP({letter}{ix}, -{CalcOptions.RndDigits})"
                                : $"={letter}{ix}");
                        }

                        ws.XSetCellSum(ref ix, ref col, pStart, ref letter); // Mass
                        ws.XSetCellSum(ref ix, ref col, pStart, ref letter); // Volume

                        ws.XRangeFmtInt(pStart, XRetailCol, ix, XRetailCol);
                        ws.XRangeFmtDec(pStart, 3, ix, col);
                        ws.XRangeBold(ix, 1, ix, col);
                        return;
                    }

                    col = 1;
                    if (IsProductionList) // only prod. list needs section name
                    {
                        ws.CellSet(ix, col, section);
                    }
                    col++;
                    if (decimal.TryParse((string)XRow.Qty, out var qty))
                    {
                        ws.CellSet(ix, col, qty);
                    }
                    else
                    {
                        // this should not happen :P
                        i++;
                        continue;
                    }

                    if (expOptInclMargins.Checked && CalcOptions.ApplyMargin)
                    {
                        var schemCostSingle = 0m;
                        if (decimal.TryParse((string)XRow.SchemataA, out var schemCost))
                        {
                            schemCostSingle = Math.Round(schemCost / qty, 2);
                        }
                        if (qty > 0 && decimal.TryParse((string)XRow.Amt, out var total))
                        {
                            var oreCost = Math.Round(total / qty, 2);
                            ws.CellSet(ix, ++col, oreCost); // Ore cost per item
                            col++;
                            // Schematics cost per 1
                            ws.CellSet(ix, col, schemCostSingle);
                            // schematics cost as percentage per 1 item
                            ws.XR1C1(ix, ++col, "=R[0]C[-1]/(R[0]C[-2]+R[0]C[-1])*100");
                            // Item cost per 1
                            ws.XR1C1(ix, ++col, "=R[0]C[-3]+R[0]C[-2]");
                            // Line Item total (cost x qty)
                            ws.XR1C1(ix, ++col, "=R[0]C[-5]*R[0]C[-1]");
                            // Margin for line total
                            letter = Utils.LetterByIndex(col);
                            ws.XR1C1(ix, ++col, $"=(C{XMarginRow}/100*{letter}{ix})");
                        }
                        else
                        {
                            col += 5;
                        }
                    }

                    // Line total retail price
                    if (qty > 0 && decimal.TryParse((string)XRow.Retail, out var retail))
                    {
                        if (expOptInclMargins.Checked && CalcOptions.ApplyMargin)
                        {
                            ws.XR1C1(ix, ++col, "=R[0]C[-2]+R[0]C[-1]");
                        }
                        else
                        {
                            // avoid rounding errors by re-calculating the retail price
                            var itemPrice = Math.Round(retail / qty, 2);
                            ws.CellSet(ix, ++col, itemPrice); // Item price
                            ws.XR1C1(ix, ++col, "=R[0]C[-2]*R[0]C[-1]");
                        }
                    }
                    else
                    {
                        col++;
                    }

                    if (!string.IsNullOrEmpty((string)XRow.Mass) &&
                        decimal.TryParse((string)XRow.Mass, System.Globalization.NumberStyles.Any,
                                         System.Globalization.CultureInfo.CurrentCulture, out var massVal))
                    {
                        ws.CellSet(ix, ++col, massVal);
                    }
                    if (!string.IsNullOrEmpty((string)XRow.Vol) &&
                        decimal.TryParse((string)XRow.Vol, System.Globalization.NumberStyles.Any,
                                         System.Globalization.CultureInfo.CurrentCulture, out var volVal))
                    {
                        ws.CellSet(ix, ++col, volVal);
                    }
                    ix++;
                    i++;
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Export prod. list error, please report to developer.\r\n" + ex.Message,
                    "ERROR", KryptonMessageBoxButtons.OK, false);
            }
        }

        private bool exportDetailSection(IXLWorksheet ws, string currSection, string nextSection,
                                         ref int i, ref int ix, bool orePrice, bool schems)
        {
            try
            {
                var isParts = currSection == "Parts";
                var isSchemSection = currSection == "Schematics";

                ix += 2;
                var col = 1;
                ws.CellSet(ix, col, currSection);
                col++;
                if (!isSchemSection)
                {
                    ws.CellSet(ix, col, "Quantity" + (isParts ? "" : " (L)"));
                }
                if (orePrice)
                {
                    ws.CellInc(ix, ref col, "q / L");
                    ws.CellInc(ix, ref col, "Amount (q)");
                    col++;
                    ws.CellInc(ix, ref col, "Mass (t)");
                    ws.CellInc(ix, ref col, "Volume (KL)");
                }
                if (schems)
                {
                    col = 4;
                    if (expOptInclMargins.Checked)
                    {
                        if (isSchemSection)
                        {
                            ws.CellSet(ix, col, "Schema. %", comment: "Base value for the % is the full total.");
                            col++;
                        }
                        ws.CellInc(ix, ref col, "Schema. cost (q)");
                        ws.CellInc(ix, ref col, "Schematic " + (isSchemSection ? "info" : "type")); // should be 7 by now
                    }
                    else
                    {
                        ws.CellInc(ix, ref col, "Schema. qty.");
                    }
                }
                ws.XRangeBold(ix, 1, ix, col);
                ws.XRangeRight(ix, 2, ix, col);

                i++;
                ix++;
                var dataStart = ix;
                while (i < treeListView.Items.Count)
                {
                    var r = treeListView.Items[i];
                    var section = r.SubItems[olvColumnSection.Index].Text;

                    // we return when next section is reached
                    if (section == nextSection || (i == treeListView.Items.Count-1))
                    {
                        // leave "i" on current row
                        var decFmtEnd = 6; // default: "F"
                        col = 1;
                        var letter = 'A';
                        // note: XSetCellSum() 1st increases col by 1 and sets letter accordingly
                        if (orePrice)
                        {
                            ws.XSetCellSum(ref ix, ref col, dataStart, ref letter); // ore in L
                            col++; // skip "C"
                            ws.XSetCellSum(ref ix, ref col, dataStart, ref letter); // Amount
                            col++; // skip "E"
                            ws.XSetCellSum(ref ix, ref col, dataStart, ref letter); // Mass
                            ws.XSetCellSum(ref ix, ref col, dataStart, ref letter); // Volume
                            decFmtEnd = col; // with ores we go to 7 / "G";
                        }
                        if (expOptInclMargins.Checked)
                        {
                            if (isSchemSection)
                            {
                                col = 3;
                                ws.XSetCellSum(ref ix, ref col, dataStart, ref letter); // Schema %
                            }
                            if (schems)
                            {
                                col = 5;
                                ws.XSetCellSum(ref ix, ref col, dataStart, ref letter); // Schema Amount
                            }
                        }
                        var decFmtStart = isParts ? 3 : 2; // "C" : "B"
                        if (isParts) // Parts are integers
                        {
                            ws.XRangeFmtInt(dataStart, 2, ix, 2);
                        }
                        ws.XRangeFmtDec(dataStart, decFmtStart, ix, decFmtEnd);
                        ws.XRangeBold(ix, decFmtStart, ix, decFmtEnd);
                        return true;
                    }

                    ws.Cell(ix, 1).Value = section;
                    decimal.TryParse(r.SubItems[olvColumnQty.Index].Text, out var qty);
                    if (!isSchemSection)
                    {
                        ws.CellSet(ix, 2, Math.Round(qty, 2, MidpointRounding.AwayFromZero));
                    }
                    if (orePrice)
                    {
                        ws.CellSet(ix, 3, DUData.GetOrePriceByName(section));
                        ws.Cell(ix, 4).FormulaR1C1 = "=(R[0]C[-2]*R[0]C[-1])";
                        var rec = DUData.Recipes.FirstOrDefault(x => x.Value.Name == section);
                        if (rec.Value?.UnitMass != null)
                        {
                            ws.CellSet(ix, 6, (qty * rec.Value.UnitMass) / 1000);
                        }
                        if (rec.Value?.UnitVolume != null)
                        {
                            ws.CellSet(ix, 7, (qty * rec.Value.UnitVolume) / 1000);
                        }
                    }

                    if (schems && decimal.TryParse(r.SubItems[olvColumnSchemataQ.Index].Text, out qty))
                    {
                        ws.CellSet(ix, 5, qty);
                        if (expOptInclMargins.Checked)
                        {
                            if (decimal.TryParse(r.SubItems[olvColumnSchemataA.Index].Text, out var sAmt))
                            {
                                ws.CellSet(ix, 6, sAmt);
                                if (isSchemSection)
                                {
                                    // display % of schematics cost for full order/calculation
                                    ws.Cell(ix, 4).FormulaA1 = $"=F{ix}/B{XRetailRow}*100";
                                }
                            }
                            // schematics' info, like copy time
                            if (!string.IsNullOrEmpty(r.SubItems[olvColumnFiller.Index].Text))
                            {
                                ws.CellSet(ix, 7, r.SubItems[olvColumnFiller.Index].Text);
                                if (isSchemSection)
                                {
                                    ws.XMergeCells(ix, 7, 5);
                                }
                            }
                        }
                    }
                    ix++;
                    i++;
                }
                return true;
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Export detail section error, please report to developer.\r\n" + ex.Message,
                    "ERROR", KryptonMessageBoxButtons.OK, false);
            }
            return false;
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            if (treeListView.Items.Count == 0) return;

            XMarginRow = 0;
            var fname = $"{Calc.Name} (x{Calc.Quantity:N0})";
            var prodListName = "";
            if (IsProductionList)
            {
                var mgr = DUData.IndyMgrInstance;
                prodListName = mgr.Databindings.ListLoaded ? Path.GetFileNameWithoutExtension(mgr.Databindings.GetFilename()) : "";
                fname = DUData.ProductionListTitle + (mgr.Databindings.ListLoaded ? $" - {prodListName}" : "");
            }
            var title = Path.GetFileNameWithoutExtension(fname);
            fname = Utils.PromptSave("Export "+ title, fname, true);
            if (fname == null) return;

            FetchOptionsFromForm();

            using (var workbook = new XLWorkbook())
            {
                var ws = workbook.Worksheets.Add(title.Length > 31 ? title.Substring(0,31) : title);
                var partsEnd = 1;
                try
                {
                    var i = 0;
                    var ix = 1; // start output in this row

                    exportHeader(ws, ref ix, title);

                    // If NOT production list, but single-item calculation:
                    // run once the ProdList* method to achieve the same output
                    if (!IsProductionList)
                    {
                        i = -1;
                        exportProdItems(ws, ref i, ref ix, treeListView.Items.Count);
                        i = 0; // Reset
                    }

                    while (i < treeListView.Items.Count)
                    {
                        var r = treeListView.Items[i];
                        var section = r.SubItems[olvColumnSection.Index].Text;

                        if (IsProductionList && section == DUData.ProductionListTitle)
                        {
                            exportProdItems(ws, ref i, ref ix, treeListView.Items.Count);
                            if (!expOptInclSubSections.Checked)
                            {
                                break;
                            }
                            ix += 2;
                            ws.Cell(ix, 1).Value = "Below are totals for full order!";
                            ws.Row(ix).CellsUsed().Style.Font.SetBold();
                            continue;
                        }
                        if (!expOptInclSubSections.Checked)
                        {
                            break;
                        }
                        switch (section)
                        {
                            case "Ores":
                                if (!exportDetailSection(ws, section, "Pures", ref i, ref ix, true, false))
                                    return;
                                continue;
                            case "Pures":
                                if (!exportDetailSection(ws, section, "Products", ref i, ref ix, false, expOptInclMargins.Checked))
                                    return;
                                continue;
                            case "Products":
                                if (!exportDetailSection(ws, section, "Parts", ref i, ref ix, false, expOptInclMargins.Checked))
                                    return;
                                continue;
                            case "Parts":
                                if (!exportDetailSection(ws, section, "Schematics", ref i, ref ix, false, false))
                                    return;
                                partsEnd = ix; // don't adjust schematics
                                continue;
                            case "Schematics":
                                if (!exportDetailSection(ws, section, "", ref i, ref ix, false, true))
                                    return;
                                break;
                        }
                        i++;
                    }
                    ws.ColumnsUsed().AdjustToContents(1, expOptInclSubSections.Checked ? partsEnd : ix);
                    workbook.SaveAs(fname);

                    KryptonMessageBox.Show("Data successfully exported to file.", "Success",
                        KryptonMessageBoxButtons.OK, false);
                }
                catch (Exception ex)
                {
                    KryptonMessageBox.Show("Could not save calculation!\r\n" + ex.Message,
                        "ERROR", KryptonMessageBoxButtons.OK, false);
                }
            }
        }

        #endregion
    }
}
