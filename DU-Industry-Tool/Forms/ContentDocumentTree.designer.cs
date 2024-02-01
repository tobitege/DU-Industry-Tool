using System.Windows.Forms;

namespace DU_Industry_Tool
{
    partial class ContentDocumentTree
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContentDocumentTree));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.kryptonPanel = new Krypton.Toolkit.KryptonPanel();
            this.treeListView = new BrightIdeasSoftware.TreeListView();
            this.olvColumnSection = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnEntry = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnTier = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnQty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnAmt = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnMargin = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnRetail = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnMass = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnVol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnSchemataQ = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnSchemataA = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnFiller = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.largeImageList = new System.Windows.Forms.ImageList(this.components);
            this.smallImageList = new System.Windows.Forms.ImageList(this.components);
            this.HeaderGroup = new Krypton.Toolkit.KryptonHeaderGroup();
            this.BtnRestoreState = new Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.BtnSaveState = new Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.BtnToggleNodes = new Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.BtnFontUp = new Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.BtnFontDown = new Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.LblHint = new System.Windows.Forms.Label();
            this.BtnSaveOptions = new Krypton.Toolkit.KryptonButton();
            this.kCmdSaveOptions = new Krypton.Toolkit.KryptonCommand();
            this.BtnExport = new Krypton.Toolkit.KryptonDropButton();
            this.exportMenu = new Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuHeading1 = new Krypton.Toolkit.KryptonContextMenuHeading();
            this.expOptInclSubSections = new Krypton.Toolkit.KryptonContextMenuCheckBox();
            this.expOptInclMargins = new Krypton.Toolkit.KryptonContextMenuCheckBox();
            this.RoundToCmb = new Krypton.Toolkit.KryptonComboBox();
            this.ApplyRoundingCB = new Krypton.Toolkit.KryptonCheckBox();
            this.grossMarginEdit = new Krypton.Toolkit.KryptonNumericUpDown();
            this.ApplyGrossMarginCB = new Krypton.Toolkit.KryptonCheckBox();
            this.OrePicture = new System.Windows.Forms.PictureBox();
            this.BtnRecalc = new Krypton.Toolkit.KryptonButton();
            this.pictureNano = new System.Windows.Forms.PictureBox();
            this.GridTalents = new Krypton.Toolkit.KryptonDataGridView();
            this.ColTitle = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.ColValue = new Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.OreImageList = new System.Windows.Forms.ImageList(this.components);
            this.LblSchemCostSuffix = new DU_Industry_Tool.KLabel();
            this.LblOreCostSuffix = new DU_Industry_Tool.KLabel();
            this.LblCostSuffix = new DU_Industry_Tool.KLabel();
            this.lblSchematicsCostValue = new DU_Industry_Tool.KLabel();
            this.lblSchematicsCost = new DU_Industry_Tool.KLabel();
            this.lblMarginValue = new DU_Industry_Tool.KLabel();
            this.lblMargin = new DU_Industry_Tool.KLabel();
            this.LblOptSaved = new DU_Industry_Tool.KLabel();
            this.lblBatches = new DU_Industry_Tool.KLabel();
            this.lblBatchesValue = new DU_Industry_Tool.KLabel();
            this.lblCraftTimeInfoValue = new DU_Industry_Tool.KLabel();
            this.lblCraftTime = new DU_Industry_Tool.KLabel();
            this.lblDefaultCraftTimeValue = new DU_Industry_Tool.KLabel();
            this.LblPure = new DU_Industry_Tool.KLabel();
            this.LblPureValue = new DU_Industry_Tool.KLabel();
            this.LblBatchSizeValue = new DU_Industry_Tool.KLabel();
            this.LblBatchSize = new DU_Industry_Tool.KLabel();
            this.lblCraftTimeValue = new DU_Industry_Tool.KLabel();
            this.lblDefaultCraftTime = new DU_Industry_Tool.KLabel();
            this.lblIndustryValue = new DU_Industry_Tool.KLinkLabel();
            this.lblPerIndustryValue = new DU_Industry_Tool.KLinkLabel();
            this.lblOreCostValue = new DU_Industry_Tool.KLabel();
            this.lblCostValue = new DU_Industry_Tool.KLabel();
            this.lblCostSingle = new DU_Industry_Tool.KLabel();
            this.lblCostSingleValue = new DU_Industry_Tool.KLabel();
            this.lblNano = new DU_Industry_Tool.KLabel();
            this.lblPerIndustry = new DU_Industry_Tool.KLabel();
            this.lblOreCost = new DU_Industry_Tool.KLabel();
            this.lblCostForBatch = new DU_Industry_Tool.KLabel();
            this.lblUnitData = new DU_Industry_Tool.KLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroup.Panel)).BeginInit();
            this.HeaderGroup.Panel.SuspendLayout();
            this.HeaderGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RoundToCmb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrePicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureNano)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridTalents)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.treeListView);
            this.kryptonPanel.Controls.Add(this.HeaderGroup);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(1253, 775);
            this.kryptonPanel.TabIndex = 2;
            // 
            // treeListView
            // 
            this.treeListView.AllColumns.Add(this.olvColumnSection);
            this.treeListView.AllColumns.Add(this.olvColumnEntry);
            this.treeListView.AllColumns.Add(this.olvColumnTier);
            this.treeListView.AllColumns.Add(this.olvColumnQty);
            this.treeListView.AllColumns.Add(this.olvColumnAmt);
            this.treeListView.AllColumns.Add(this.olvColumnMargin);
            this.treeListView.AllColumns.Add(this.olvColumnRetail);
            this.treeListView.AllColumns.Add(this.olvColumnMass);
            this.treeListView.AllColumns.Add(this.olvColumnVol);
            this.treeListView.AllColumns.Add(this.olvColumnSchemataQ);
            this.treeListView.AllColumns.Add(this.olvColumnSchemataA);
            this.treeListView.AllColumns.Add(this.olvColumnFiller);
            this.treeListView.AllowCellEditorsToProcessMouseWheel = false;
            this.treeListView.AlternateRowBackColor = System.Drawing.Color.FloralWhite;
            this.treeListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeListView.BackColor = System.Drawing.Color.AliceBlue;
            this.treeListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnSection,
            this.olvColumnEntry,
            this.olvColumnTier,
            this.olvColumnQty,
            this.olvColumnAmt,
            this.olvColumnMargin,
            this.olvColumnRetail,
            this.olvColumnMass,
            this.olvColumnVol,
            this.olvColumnSchemataQ,
            this.olvColumnSchemataA,
            this.olvColumnFiller});
            this.treeListView.CopySelectionOnControlCUsesDragSource = false;
            this.treeListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView.EmptyListMsg = "No recipe available!";
            this.treeListView.EmptyListMsgFont = new System.Drawing.Font("Verdana", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListView.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListView.FullRowSelect = true;
            this.treeListView.GridLines = true;
            this.treeListView.HeaderMinimumHeight = 28;
            this.treeListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.treeListView.HeaderUsesThemes = true;
            this.treeListView.HeaderWordWrap = true;
            this.treeListView.HideSelection = false;
            this.treeListView.IsSearchOnSortColumn = false;
            this.treeListView.LargeImageList = this.largeImageList;
            this.treeListView.Location = new System.Drawing.Point(0, 360);
            this.treeListView.Margin = new System.Windows.Forms.Padding(2);
            this.treeListView.MinimumSize = new System.Drawing.Size(350, 171);
            this.treeListView.Name = "treeListView";
            this.treeListView.SelectColumnsOnRightClick = false;
            this.treeListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.None;
            this.treeListView.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.treeListView.ShowGroups = false;
            this.treeListView.ShowImagesOnSubItems = true;
            this.treeListView.ShowItemCountOnGroups = true;
            this.treeListView.ShowSortIndicators = false;
            this.treeListView.Size = new System.Drawing.Size(1249, 413);
            this.treeListView.SmallImageList = this.smallImageList;
            this.treeListView.TabIndex = 2;
            this.treeListView.UseAlternatingBackColors = true;
            this.treeListView.UseCompatibleStateImageBehavior = false;
            this.treeListView.View = System.Windows.Forms.View.Details;
            this.treeListView.VirtualMode = true;
            this.treeListView.Visible = false;
            this.treeListView.DoubleClick += new System.EventHandler(this.TreeListView_ItemActivate);
            // 
            // olvColumnSection
            // 
            this.olvColumnSection.AspectName = "Section";
            this.olvColumnSection.AutoCompleteEditor = false;
            this.olvColumnSection.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvColumnSection.ButtonPadding = new System.Drawing.Size(1, 1);
            this.olvColumnSection.Hideable = false;
            this.olvColumnSection.IsEditable = false;
            this.olvColumnSection.IsTileViewColumn = true;
            this.olvColumnSection.MaximumWidth = 800;
            this.olvColumnSection.MinimumWidth = 100;
            this.olvColumnSection.Sortable = false;
            this.olvColumnSection.Text = "Section";
            this.olvColumnSection.Width = 121;
            // 
            // olvColumnEntry
            // 
            this.olvColumnEntry.AspectName = "Entry";
            this.olvColumnEntry.AutoCompleteEditor = false;
            this.olvColumnEntry.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvColumnEntry.Groupable = false;
            this.olvColumnEntry.Hideable = false;
            this.olvColumnEntry.IsEditable = false;
            this.olvColumnEntry.MaximumWidth = 1;
            this.olvColumnEntry.MinimumWidth = 1;
            this.olvColumnEntry.Text = "";
            this.olvColumnEntry.Width = 1;
            this.olvColumnEntry.WordWrap = true;
            // 
            // olvColumnTier
            // 
            this.olvColumnTier.AspectName = "Tier";
            this.olvColumnTier.AutoCompleteEditor = false;
            this.olvColumnTier.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvColumnTier.Groupable = false;
            this.olvColumnTier.Hideable = false;
            this.olvColumnTier.IsEditable = false;
            this.olvColumnTier.MaximumWidth = 50;
            this.olvColumnTier.MinimumWidth = 10;
            this.olvColumnTier.Text = "Tier";
            this.olvColumnTier.Width = 20;
            // 
            // olvColumnQty
            // 
            this.olvColumnQty.AspectName = "Qty";
            this.olvColumnQty.Groupable = false;
            this.olvColumnQty.Hideable = false;
            this.olvColumnQty.IsEditable = false;
            this.olvColumnQty.IsTileViewColumn = true;
            this.olvColumnQty.MinimumWidth = 50;
            this.olvColumnQty.Sortable = false;
            this.olvColumnQty.Text = "Qty.";
            this.olvColumnQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnQty.Width = 89;
            // 
            // olvColumnAmt
            // 
            this.olvColumnAmt.AspectName = "Amt";
            this.olvColumnAmt.Groupable = false;
            this.olvColumnAmt.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnAmt.Hideable = false;
            this.olvColumnAmt.IsEditable = false;
            this.olvColumnAmt.MinimumWidth = 60;
            this.olvColumnAmt.Sortable = false;
            this.olvColumnAmt.Text = "Amt (q)";
            this.olvColumnAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnAmt.Width = 120;
            // 
            // olvColumnMargin
            // 
            this.olvColumnMargin.AspectName = "Margin (q)";
            this.olvColumnMargin.Groupable = false;
            this.olvColumnMargin.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnMargin.IsEditable = false;
            this.olvColumnMargin.MinimumWidth = 60;
            this.olvColumnMargin.Sortable = false;
            this.olvColumnMargin.Text = "Margin (q)";
            this.olvColumnMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnMargin.Width = 84;
            // 
            // olvColumnRetail
            // 
            this.olvColumnRetail.AspectName = "Retail (q)";
            this.olvColumnRetail.Hideable = false;
            this.olvColumnRetail.IsEditable = false;
            this.olvColumnRetail.Sortable = false;
            this.olvColumnRetail.Text = "Retail (q)";
            this.olvColumnRetail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnRetail.Width = 77;
            // 
            // olvColumnMass
            // 
            this.olvColumnMass.AspectName = "Mass";
            this.olvColumnMass.Groupable = false;
            this.olvColumnMass.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnMass.Hideable = false;
            this.olvColumnMass.IsEditable = false;
            this.olvColumnMass.MinimumWidth = 50;
            this.olvColumnMass.Sortable = false;
            this.olvColumnMass.Text = "Mass (t)";
            this.olvColumnMass.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnMass.Width = 120;
            // 
            // olvColumnVol
            // 
            this.olvColumnVol.AspectName = "Vol.";
            this.olvColumnVol.Groupable = false;
            this.olvColumnVol.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnVol.Hideable = false;
            this.olvColumnVol.IsEditable = false;
            this.olvColumnVol.MinimumWidth = 50;
            this.olvColumnVol.Sortable = false;
            this.olvColumnVol.Text = "Vol (KL)";
            this.olvColumnVol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnVol.Width = 120;
            // 
            // olvColumnSchemataQ
            // 
            this.olvColumnSchemataQ.AspectName = "QtySchemata";
            this.olvColumnSchemataQ.Groupable = false;
            this.olvColumnSchemataQ.IsEditable = false;
            this.olvColumnSchemataQ.IsTileViewColumn = true;
            this.olvColumnSchemataQ.MinimumWidth = 40;
            this.olvColumnSchemataQ.Text = "Schema Qty.";
            this.olvColumnSchemataQ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnSchemataQ.Width = 120;
            // 
            // olvColumnSchemataA
            // 
            this.olvColumnSchemataA.AspectName = "AmtSchemata";
            this.olvColumnSchemataA.Groupable = false;
            this.olvColumnSchemataA.IsEditable = false;
            this.olvColumnSchemataA.MinimumWidth = 50;
            this.olvColumnSchemataA.Text = "Schema Amt.";
            this.olvColumnSchemataA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnSchemataA.Width = 170;
            // 
            // olvColumnFiller
            // 
            this.olvColumnFiller.AspectName = "Comment";
            this.olvColumnFiller.FillsFreeSpace = true;
            this.olvColumnFiller.Groupable = false;
            this.olvColumnFiller.IsEditable = false;
            this.olvColumnFiller.MinimumWidth = 50;
            this.olvColumnFiller.Text = "";
            this.olvColumnFiller.Width = 200;
            // 
            // largeImageList
            // 
            this.largeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("largeImageList.ImageStream")));
            this.largeImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.largeImageList.Images.SetKeyName(0, "user");
            this.largeImageList.Images.SetKeyName(1, "compass");
            this.largeImageList.Images.SetKeyName(2, "down");
            this.largeImageList.Images.SetKeyName(3, "find");
            this.largeImageList.Images.SetKeyName(4, "folder");
            this.largeImageList.Images.SetKeyName(5, "movie");
            this.largeImageList.Images.SetKeyName(6, "music");
            this.largeImageList.Images.SetKeyName(7, "no");
            this.largeImageList.Images.SetKeyName(8, "readonly");
            this.largeImageList.Images.SetKeyName(9, "public");
            this.largeImageList.Images.SetKeyName(10, "recycle");
            this.largeImageList.Images.SetKeyName(11, "spanner");
            this.largeImageList.Images.SetKeyName(12, "star");
            this.largeImageList.Images.SetKeyName(13, "tick");
            this.largeImageList.Images.SetKeyName(14, "archive");
            this.largeImageList.Images.SetKeyName(15, "system");
            this.largeImageList.Images.SetKeyName(16, "hidden");
            this.largeImageList.Images.SetKeyName(17, "temporary");
            // 
            // smallImageList
            // 
            this.smallImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("smallImageList.ImageStream")));
            this.smallImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.smallImageList.Images.SetKeyName(0, "compass");
            this.smallImageList.Images.SetKeyName(1, "down");
            this.smallImageList.Images.SetKeyName(2, "user");
            this.smallImageList.Images.SetKeyName(3, "find");
            this.smallImageList.Images.SetKeyName(4, "folder");
            this.smallImageList.Images.SetKeyName(5, "movie");
            this.smallImageList.Images.SetKeyName(6, "music");
            this.smallImageList.Images.SetKeyName(7, "no");
            this.smallImageList.Images.SetKeyName(8, "readonly");
            this.smallImageList.Images.SetKeyName(9, "public");
            this.smallImageList.Images.SetKeyName(10, "recycle");
            this.smallImageList.Images.SetKeyName(11, "spanner");
            this.smallImageList.Images.SetKeyName(12, "star");
            this.smallImageList.Images.SetKeyName(13, "tick");
            this.smallImageList.Images.SetKeyName(14, "archive");
            this.smallImageList.Images.SetKeyName(15, "system");
            this.smallImageList.Images.SetKeyName(16, "hidden");
            this.smallImageList.Images.SetKeyName(17, "temporary");
            // 
            // HeaderGroup
            // 
            this.HeaderGroup.ButtonSpecs.Add(this.BtnRestoreState);
            this.HeaderGroup.ButtonSpecs.Add(this.BtnSaveState);
            this.HeaderGroup.ButtonSpecs.Add(this.BtnToggleNodes);
            this.HeaderGroup.ButtonSpecs.Add(this.BtnFontUp);
            this.HeaderGroup.ButtonSpecs.Add(this.BtnFontDown);
            this.HeaderGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderGroup.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlGroupBox;
            this.HeaderGroup.GroupBorderStyle = Krypton.Toolkit.PaletteBorderStyle.ControlGroupBox;
            this.HeaderGroup.HeaderStylePrimary = Krypton.Toolkit.HeaderStyle.Form;
            this.HeaderGroup.HeaderVisibleSecondary = false;
            this.HeaderGroup.Location = new System.Drawing.Point(0, 0);
            this.HeaderGroup.Name = "HeaderGroup";
            // 
            // HeaderGroup.Panel
            // 
            this.HeaderGroup.Panel.Controls.Add(this.LblSchemCostSuffix);
            this.HeaderGroup.Panel.Controls.Add(this.LblOreCostSuffix);
            this.HeaderGroup.Panel.Controls.Add(this.LblCostSuffix);
            this.HeaderGroup.Panel.Controls.Add(this.lblSchematicsCostValue);
            this.HeaderGroup.Panel.Controls.Add(this.lblSchematicsCost);
            this.HeaderGroup.Panel.Controls.Add(this.lblMarginValue);
            this.HeaderGroup.Panel.Controls.Add(this.lblMargin);
            this.HeaderGroup.Panel.Controls.Add(this.LblOptSaved);
            this.HeaderGroup.Panel.Controls.Add(this.LblHint);
            this.HeaderGroup.Panel.Controls.Add(this.BtnSaveOptions);
            this.HeaderGroup.Panel.Controls.Add(this.BtnExport);
            this.HeaderGroup.Panel.Controls.Add(this.RoundToCmb);
            this.HeaderGroup.Panel.Controls.Add(this.ApplyRoundingCB);
            this.HeaderGroup.Panel.Controls.Add(this.grossMarginEdit);
            this.HeaderGroup.Panel.Controls.Add(this.ApplyGrossMarginCB);
            this.HeaderGroup.Panel.Controls.Add(this.OrePicture);
            this.HeaderGroup.Panel.Controls.Add(this.lblBatches);
            this.HeaderGroup.Panel.Controls.Add(this.lblBatchesValue);
            this.HeaderGroup.Panel.Controls.Add(this.lblCraftTimeInfoValue);
            this.HeaderGroup.Panel.Controls.Add(this.BtnRecalc);
            this.HeaderGroup.Panel.Controls.Add(this.lblCraftTime);
            this.HeaderGroup.Panel.Controls.Add(this.lblDefaultCraftTimeValue);
            this.HeaderGroup.Panel.Controls.Add(this.LblPure);
            this.HeaderGroup.Panel.Controls.Add(this.LblPureValue);
            this.HeaderGroup.Panel.Controls.Add(this.LblBatchSizeValue);
            this.HeaderGroup.Panel.Controls.Add(this.LblBatchSize);
            this.HeaderGroup.Panel.Controls.Add(this.lblCraftTimeValue);
            this.HeaderGroup.Panel.Controls.Add(this.lblDefaultCraftTime);
            this.HeaderGroup.Panel.Controls.Add(this.lblIndustryValue);
            this.HeaderGroup.Panel.Controls.Add(this.lblPerIndustryValue);
            this.HeaderGroup.Panel.Controls.Add(this.lblOreCostValue);
            this.HeaderGroup.Panel.Controls.Add(this.lblCostValue);
            this.HeaderGroup.Panel.Controls.Add(this.lblCostSingle);
            this.HeaderGroup.Panel.Controls.Add(this.lblCostSingleValue);
            this.HeaderGroup.Panel.Controls.Add(this.pictureNano);
            this.HeaderGroup.Panel.Controls.Add(this.lblNano);
            this.HeaderGroup.Panel.Controls.Add(this.lblPerIndustry);
            this.HeaderGroup.Panel.Controls.Add(this.lblOreCost);
            this.HeaderGroup.Panel.Controls.Add(this.lblCostForBatch);
            this.HeaderGroup.Panel.Controls.Add(this.lblUnitData);
            this.HeaderGroup.Panel.Controls.Add(this.GridTalents);
            this.HeaderGroup.Size = new System.Drawing.Size(1253, 355);
            this.HeaderGroup.TabIndex = 2;
            this.HeaderGroup.ValuesPrimary.Heading = "Calculation";
            this.HeaderGroup.ValuesPrimary.Image = null;
            // 
            // BtnRestoreState
            // 
            this.BtnRestoreState.Text = "Load Config";
            this.BtnRestoreState.UniqueName = "8759F4DBDA4544F62EA5239B1C0DEC24";
            this.BtnRestoreState.Click += new System.EventHandler(this.BtnRestoreState_Click);
            // 
            // BtnSaveState
            // 
            this.BtnSaveState.Text = "Save Config";
            this.BtnSaveState.UniqueName = "B803E19DF9944F1593A01EF944CD3DCB";
            this.BtnSaveState.Click += new System.EventHandler(this.BtnSaveState_Click);
            // 
            // BtnToggleNodes
            // 
            this.BtnToggleNodes.Text = "Toogle root nodes";
            this.BtnToggleNodes.UniqueName = "52F142270C854D89D9886479CF81F2F8";
            this.BtnToggleNodes.Click += new System.EventHandler(this.BtnToggleNodes_Click);
            // 
            // BtnFontUp
            // 
            this.BtnFontUp.Text = "F+";
            this.BtnFontUp.UniqueName = "0c7ee9533b814f25bec78110e4266301";
            this.BtnFontUp.Click += new System.EventHandler(this.BtnFontUpOnClick);
            // 
            // BtnFontDown
            // 
            this.BtnFontDown.Text = "F-";
            this.BtnFontDown.UniqueName = "a891a02e203346f0bddb32c969db17ef";
            this.BtnFontDown.Click += new System.EventHandler(this.BtnFontDownOnClick);
            // 
            // LblHint
            // 
            this.LblHint.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblHint.BackColor = System.Drawing.Color.White;
            this.LblHint.Font = new System.Drawing.Font("Verdana", 30F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.LblHint.ForeColor = System.Drawing.Color.Black;
            this.LblHint.Location = new System.Drawing.Point(4, 240);
            this.LblHint.Name = "LblHint";
            this.LblHint.Padding = new System.Windows.Forms.Padding(10);
            this.LblHint.Size = new System.Drawing.Size(1242, 88);
            this.LblHint.TabIndex = 36;
            this.LblHint.Text = "Preparing data, please wait...";
            this.LblHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnSaveOptions
            // 
            this.BtnSaveOptions.KryptonCommand = this.kCmdSaveOptions;
            this.BtnSaveOptions.Location = new System.Drawing.Point(991, 174);
            this.BtnSaveOptions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnSaveOptions.Name = "BtnSaveOptions";
            this.BtnSaveOptions.Size = new System.Drawing.Size(36, 36);
            this.BtnSaveOptions.StateNormal.Border.Color1 = System.Drawing.Color.Black;
            this.BtnSaveOptions.StateNormal.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.BtnSaveOptions.StateNormal.Border.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.BtnSaveOptions.StateNormal.Border.Rounding = 2F;
            this.BtnSaveOptions.TabIndex = 43;
            this.BtnSaveOptions.ToolTipValues.Description = "Save current options as default";
            this.BtnSaveOptions.ToolTipValues.EnableToolTips = true;
            this.BtnSaveOptions.ToolTipValues.Heading = "Save";
            this.BtnSaveOptions.Values.Text = "";
            this.BtnSaveOptions.Click += new System.EventHandler(this.BtnSaveOptions_Click);
            // 
            // kCmdSaveOptions
            // 
            this.kCmdSaveOptions.ImageLarge = global::DU_Industry_Tool.Properties.Resources.filesave;
            this.kCmdSaveOptions.ImageSmall = global::DU_Industry_Tool.Properties.Resources.filesave;
            // 
            // BtnExport
            // 
            this.BtnExport.KryptonContextMenu = this.exportMenu;
            this.BtnExport.Location = new System.Drawing.Point(504, 211);
            this.BtnExport.Margin = new System.Windows.Forms.Padding(4);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(165, 35);
            this.BtnExport.TabIndex = 42;
            this.BtnExport.Values.Text = "Export to Excel";
            this.BtnExport.Click += new System.EventHandler(this.exportBtn_Click);
            // 
            // exportMenu
            // 
            this.exportMenu.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuHeading1,
            this.expOptInclSubSections,
            this.expOptInclMargins});
            // 
            // kryptonContextMenuHeading1
            // 
            this.kryptonContextMenuHeading1.ExtraText = "";
            this.kryptonContextMenuHeading1.Text = "Export";
            // 
            // expOptInclSubSections
            // 
            this.expOptInclSubSections.Checked = true;
            this.expOptInclSubSections.CheckState = System.Windows.Forms.CheckState.Checked;
            this.expOptInclSubSections.ExtraText = "";
            this.expOptInclSubSections.Text = "Incl. sub-sections";
            // 
            // expOptInclMargins
            // 
            this.expOptInclMargins.Checked = true;
            this.expOptInclMargins.CheckState = System.Windows.Forms.CheckState.Checked;
            this.expOptInclMargins.ExtraText = "";
            this.expOptInclMargins.Text = "Incl. margins q/%";
            // 
            // RoundToCmb
            // 
            this.RoundToCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RoundToCmb.DropDownWidth = 150;
            this.RoundToCmb.IntegralHeight = false;
            this.RoundToCmb.Items.AddRange(new object[] {
            "next 10",
            "next 100",
            "next 1 000",
            "next 10 000",
            "next 100 000",
            "next 1 000 000"});
            this.RoundToCmb.Location = new System.Drawing.Point(770, 267);
            this.RoundToCmb.MaxLength = 3;
            this.RoundToCmb.Name = "RoundToCmb";
            this.RoundToCmb.Size = new System.Drawing.Size(150, 24);
            this.RoundToCmb.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.RoundToCmb.TabIndex = 41;
            // 
            // ApplyRoundingCB
            // 
            this.ApplyRoundingCB.Location = new System.Drawing.Point(750, 235);
            this.ApplyRoundingCB.Name = "ApplyRoundingCB";
            this.ApplyRoundingCB.Size = new System.Drawing.Size(294, 23);
            this.ApplyRoundingCB.TabIndex = 40;
            this.ApplyRoundingCB.ToolTipValues.Description = "% on top of single price before calculating sum";
            this.ApplyRoundingCB.ToolTipValues.Heading = "Gross Margin %";
            this.ApplyRoundingCB.Values.Text = "Rounds topline sums up to x digits";
            // 
            // grossMarginEdit
            // 
            this.grossMarginEdit.AllowDecimals = true;
            this.grossMarginEdit.DecimalPlaces = 2;
            this.grossMarginEdit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.grossMarginEdit.Location = new System.Drawing.Point(770, 200);
            this.grossMarginEdit.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.grossMarginEdit.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.grossMarginEdit.MinimumSize = new System.Drawing.Size(88, 0);
            this.grossMarginEdit.Name = "grossMarginEdit";
            this.grossMarginEdit.Size = new System.Drawing.Size(88, 25);
            this.grossMarginEdit.TabIndex = 39;
            this.grossMarginEdit.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // ApplyGrossMarginCB
            // 
            this.ApplyGrossMarginCB.Location = new System.Drawing.Point(750, 170);
            this.ApplyGrossMarginCB.Name = "ApplyGrossMarginCB";
            this.ApplyGrossMarginCB.Size = new System.Drawing.Size(220, 23);
            this.ApplyGrossMarginCB.TabIndex = 38;
            this.ApplyGrossMarginCB.ToolTipValues.Description = "% on top of single price before calculating sum";
            this.ApplyGrossMarginCB.ToolTipValues.Heading = "Gross Margin %";
            this.ApplyGrossMarginCB.Values.Text = "Apply Gross Margin (%)?";
            // 
            // OrePicture
            // 
            this.OrePicture.BackColor = System.Drawing.SystemColors.Window;
            this.OrePicture.Location = new System.Drawing.Point(898, 130);
            this.OrePicture.Name = "OrePicture";
            this.OrePicture.Size = new System.Drawing.Size(36, 34);
            this.OrePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.OrePicture.TabIndex = 35;
            this.OrePicture.TabStop = false;
            this.OrePicture.Visible = false;
            // 
            // BtnRecalc
            // 
            this.BtnRecalc.Location = new System.Drawing.Point(504, 158);
            this.BtnRecalc.Name = "BtnRecalc";
            this.BtnRecalc.Size = new System.Drawing.Size(165, 35);
            this.BtnRecalc.TabIndex = 31;
            this.BtnRecalc.Values.Text = "Recalculate";
            this.BtnRecalc.Click += new System.EventHandler(this.BtnRecalc_Click);
            // 
            // pictureNano
            // 
            this.pictureNano.Image = global::DU_Industry_Tool.Properties.Resources.Green_Ball;
            this.pictureNano.InitialImage = null;
            this.pictureNano.Location = new System.Drawing.Point(952, 136);
            this.pictureNano.Margin = new System.Windows.Forms.Padding(2);
            this.pictureNano.MaximumSize = new System.Drawing.Size(20, 20);
            this.pictureNano.MinimumSize = new System.Drawing.Size(20, 20);
            this.pictureNano.Name = "pictureNano";
            this.pictureNano.Size = new System.Drawing.Size(20, 20);
            this.pictureNano.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureNano.TabIndex = 9;
            this.pictureNano.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureNano, "Green, if the recipe is nano-craftable, else red.");
            // 
            // GridTalents
            // 
            this.GridTalents.AllowUserToAddRows = false;
            this.GridTalents.AllowUserToDeleteRows = false;
            this.GridTalents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridTalents.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridTalents.ColumnHeadersHeight = 32;
            this.GridTalents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTitle,
            this.ColValue});
            this.GridTalents.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.GridTalents.Location = new System.Drawing.Point(4, 156);
            this.GridTalents.MultiSelect = false;
            this.GridTalents.Name = "GridTalents";
            this.GridTalents.RowHeadersWidth = 26;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.GridTalents.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.GridTalents.RowTemplate.Height = 16;
            this.GridTalents.RowTemplate.ReadOnly = true;
            this.GridTalents.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GridTalents.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.GridTalents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GridTalents.ShowEditingIcon = false;
            this.GridTalents.Size = new System.Drawing.Size(492, 160);
            this.GridTalents.StateSelected.DataCell.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.GridTalents.StateSelected.DataCell.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.GridTalents.TabIndex = 15;
            // 
            // ColTitle
            // 
            this.ColTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColTitle.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColTitle.HeaderText = "Related Talents";
            this.ColTitle.MinimumWidth = 300;
            this.ColTitle.Name = "ColTitle";
            this.ColTitle.ReadOnly = true;
            this.ColTitle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColTitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColTitle.Width = 416;
            // 
            // ColValue
            // 
            this.ColValue.AllowDecimals = false;
            this.ColValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColValue.HeaderText = "Level";
            this.ColValue.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.ColValue.MinimumWidth = 50;
            this.ColValue.Name = "ColValue";
            this.ColValue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColValue.Width = 50;
            // 
            // OreImageList
            // 
            this.OreImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("OreImageList.ImageStream")));
            this.OreImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.OreImageList.Images.SetKeyName(0, "ore_bauxite.png");
            this.OreImageList.Images.SetKeyName(1, "ore_calcium.png");
            this.OreImageList.Images.SetKeyName(2, "ore_carbon.png");
            this.OreImageList.Images.SetKeyName(3, "ore_chromite.png");
            this.OreImageList.Images.SetKeyName(4, "ore_coal.png");
            this.OreImageList.Images.SetKeyName(5, "ore_cobalt.png");
            this.OreImageList.Images.SetKeyName(6, "ore_copper.png");
            this.OreImageList.Images.SetKeyName(7, "ore_fluorine.png");
            this.OreImageList.Images.SetKeyName(8, "ore_gold.png");
            this.OreImageList.Images.SetKeyName(9, "ore_iron.png");
            this.OreImageList.Images.SetKeyName(10, "ore_lithium.png");
            this.OreImageList.Images.SetKeyName(11, "ore_manganese.png");
            this.OreImageList.Images.SetKeyName(12, "ore_nickel.png");
            this.OreImageList.Images.SetKeyName(13, "ore_niobium.png");
            this.OreImageList.Images.SetKeyName(14, "ore_quartz.png");
            this.OreImageList.Images.SetKeyName(15, "ore_scandium.png");
            this.OreImageList.Images.SetKeyName(16, "ore_silicon.png");
            this.OreImageList.Images.SetKeyName(17, "ore_silver.png");
            this.OreImageList.Images.SetKeyName(18, "ore_sodium.png");
            this.OreImageList.Images.SetKeyName(19, "ore_sulfur.png");
            this.OreImageList.Images.SetKeyName(20, "ore_titanium.png");
            this.OreImageList.Images.SetKeyName(21, "ore_vanadium.png");
            // 
            // LblSchemCostSuffix
            // 
            this.LblSchemCostSuffix.AutoSize = false;
            this.LblSchemCostSuffix.Location = new System.Drawing.Point(238, 125);
            this.LblSchemCostSuffix.MinimumSize = new System.Drawing.Size(160, 24);
            this.LblSchemCostSuffix.Name = "LblSchemCostSuffix";
            this.LblSchemCostSuffix.Size = new System.Drawing.Size(160, 27);
            this.LblSchemCostSuffix.TabIndex = 51;
            this.LblSchemCostSuffix.Text = "_";
            this.LblSchemCostSuffix.Values.Text = "_";
            this.LblSchemCostSuffix.Visible = false;
            // 
            // LblOreCostSuffix
            // 
            this.LblOreCostSuffix.AutoSize = false;
            this.LblOreCostSuffix.Location = new System.Drawing.Point(238, 98);
            this.LblOreCostSuffix.MinimumSize = new System.Drawing.Size(160, 24);
            this.LblOreCostSuffix.Name = "LblOreCostSuffix";
            this.LblOreCostSuffix.Size = new System.Drawing.Size(160, 27);
            this.LblOreCostSuffix.TabIndex = 50;
            this.LblOreCostSuffix.Text = "_";
            this.LblOreCostSuffix.Values.Text = "_";
            this.LblOreCostSuffix.Visible = false;
            // 
            // LblCostSuffix
            // 
            this.LblCostSuffix.AutoSize = false;
            this.LblCostSuffix.Location = new System.Drawing.Point(238, 12);
            this.LblCostSuffix.MinimumSize = new System.Drawing.Size(160, 24);
            this.LblCostSuffix.Name = "LblCostSuffix";
            this.LblCostSuffix.Size = new System.Drawing.Size(260, 27);
            this.LblCostSuffix.TabIndex = 49;
            this.LblCostSuffix.Text = "_";
            this.LblCostSuffix.Values.Text = "_";
            this.LblCostSuffix.Visible = false;
            // 
            // lblSchematicsCostValue
            // 
            this.lblSchematicsCostValue.Location = new System.Drawing.Point(98, 125);
            this.lblSchematicsCostValue.Name = "lblSchematicsCostValue";
            this.lblSchematicsCostValue.Size = new System.Drawing.Size(36, 23);
            this.lblSchematicsCostValue.TabIndex = 48;
            this.lblSchematicsCostValue.Text = "0 q";
            this.toolTip1.SetToolTip(this.lblSchematicsCostValue, "Total schematics cost");
            this.lblSchematicsCostValue.Values.Text = "0 q";
            // 
            // lblSchematicsCost
            // 
            this.lblSchematicsCost.Location = new System.Drawing.Point(6, 125);
            this.lblSchematicsCost.Name = "lblSchematicsCost";
            this.lblSchematicsCost.Size = new System.Drawing.Size(107, 23);
            this.lblSchematicsCost.TabIndex = 47;
            this.lblSchematicsCost.Text = "Schematics: ";
            this.lblSchematicsCost.Values.Text = "Schematics: ";
            // 
            // lblMarginValue
            // 
            this.lblMarginValue.Location = new System.Drawing.Point(98, 42);
            this.lblMarginValue.Name = "lblMarginValue";
            this.lblMarginValue.Size = new System.Drawing.Size(36, 23);
            this.lblMarginValue.TabIndex = 46;
            this.lblMarginValue.Text = "0 q";
            this.toolTip1.SetToolTip(this.lblMarginValue, "Margin in q");
            this.lblMarginValue.Values.Text = "0 q";
            // 
            // lblMargin
            // 
            this.lblMargin.Location = new System.Drawing.Point(6, 42);
            this.lblMargin.Name = "lblMargin";
            this.lblMargin.Size = new System.Drawing.Size(71, 23);
            this.lblMargin.TabIndex = 45;
            this.lblMargin.Text = "Margin:";
            this.lblMargin.Values.Text = "Margin:";
            // 
            // LblOptSaved
            // 
            this.LblOptSaved.LabelStyle = Krypton.Toolkit.LabelStyle.Custom1;
            this.LblOptSaved.Location = new System.Drawing.Point(1034, 179);
            this.LblOptSaved.Name = "LblOptSaved";
            this.LblOptSaved.Size = new System.Drawing.Size(127, 23);
            this.LblOptSaved.StateNormal.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.LblOptSaved.StateNormal.ShortText.Color2 = System.Drawing.Color.Red;
            this.LblOptSaved.StateNormal.ShortText.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Rounding2;
            this.LblOptSaved.TabIndex = 44;
            this.LblOptSaved.Text = "Options saved.";
            this.LblOptSaved.Values.Text = "Options saved.";
            this.LblOptSaved.Visible = false;
            // 
            // lblBatches
            // 
            this.lblBatches.Location = new System.Drawing.Point(400, 120);
            this.lblBatches.Name = "lblBatches";
            this.lblBatches.Size = new System.Drawing.Size(80, 23);
            this.lblBatches.TabIndex = 34;
            this.lblBatches.Text = "Batches:";
            this.lblBatches.Values.Text = "Batches:";
            // 
            // lblBatchesValue
            // 
            this.lblBatchesValue.Location = new System.Drawing.Point(504, 120);
            this.lblBatchesValue.Name = "lblBatchesValue";
            this.lblBatchesValue.Size = new System.Drawing.Size(21, 23);
            this.lblBatchesValue.TabIndex = 33;
            this.lblBatchesValue.Text = "_";
            this.lblBatchesValue.Values.Text = "_";
            // 
            // lblCraftTimeInfoValue
            // 
            this.lblCraftTimeInfoValue.Location = new System.Drawing.Point(504, 98);
            this.lblCraftTimeInfoValue.Name = "lblCraftTimeInfoValue";
            this.lblCraftTimeInfoValue.Size = new System.Drawing.Size(21, 23);
            this.lblCraftTimeInfoValue.TabIndex = 32;
            this.lblCraftTimeInfoValue.Text = "_";
            this.toolTip1.SetToolTip(this.lblCraftTimeInfoValue, "Remaining quantity that is below batch input volume.");
            this.lblCraftTimeInfoValue.Values.Text = "_";
            // 
            // lblCraftTime
            // 
            this.lblCraftTime.Location = new System.Drawing.Point(750, 98);
            this.lblCraftTime.Name = "lblCraftTime";
            this.lblCraftTime.Size = new System.Drawing.Size(141, 23);
            this.lblCraftTime.TabIndex = 30;
            this.lblCraftTime.Text = "Production time: ";
            this.lblCraftTime.Values.Text = "Production time: ";
            // 
            // lblDefaultCraftTimeValue
            // 
            this.lblDefaultCraftTimeValue.Location = new System.Drawing.Point(904, 72);
            this.lblDefaultCraftTimeValue.Name = "lblDefaultCraftTimeValue";
            this.lblDefaultCraftTimeValue.Size = new System.Drawing.Size(21, 23);
            this.lblDefaultCraftTimeValue.TabIndex = 29;
            this.lblDefaultCraftTimeValue.Text = "_";
            this.toolTip1.SetToolTip(this.lblDefaultCraftTimeValue, "Ore refine time with talents applied");
            this.lblDefaultCraftTimeValue.Values.Text = "_";
            // 
            // LblPure
            // 
            this.LblPure.Location = new System.Drawing.Point(400, 72);
            this.LblPure.Name = "LblPure";
            this.LblPure.Size = new System.Drawing.Size(72, 23);
            this.LblPure.TabIndex = 28;
            this.LblPure.Text = "Output:";
            this.LblPure.Values.Text = "Output:";
            // 
            // LblPureValue
            // 
            this.LblPureValue.Location = new System.Drawing.Point(504, 72);
            this.LblPureValue.Name = "LblPureValue";
            this.LblPureValue.Size = new System.Drawing.Size(21, 23);
            this.LblPureValue.TabIndex = 26;
            this.LblPureValue.Text = "0";
            this.LblPureValue.Values.Text = "0";
            // 
            // LblBatchSizeValue
            // 
            this.LblBatchSizeValue.Location = new System.Drawing.Point(504, 42);
            this.LblBatchSizeValue.Name = "LblBatchSizeValue";
            this.LblBatchSizeValue.Size = new System.Drawing.Size(21, 23);
            this.LblBatchSizeValue.TabIndex = 27;
            this.LblBatchSizeValue.Text = "0";
            this.LblBatchSizeValue.Values.Text = "0";
            // 
            // LblBatchSize
            // 
            this.LblBatchSize.Location = new System.Drawing.Point(400, 42);
            this.LblBatchSize.Name = "LblBatchSize";
            this.LblBatchSize.Size = new System.Drawing.Size(105, 23);
            this.LblBatchSize.TabIndex = 25;
            this.LblBatchSize.Text = "Batch sizes:";
            this.LblBatchSize.Values.Text = "Batch sizes:";
            // 
            // lblCraftTimeValue
            // 
            this.lblCraftTimeValue.Location = new System.Drawing.Point(904, 98);
            this.lblCraftTimeValue.Name = "lblCraftTimeValue";
            this.lblCraftTimeValue.Size = new System.Drawing.Size(21, 23);
            this.lblCraftTimeValue.TabIndex = 24;
            this.lblCraftTimeValue.Text = "_";
            this.toolTip1.SetToolTip(this.lblCraftTimeValue, "Time with talents applied");
            this.lblCraftTimeValue.Values.Text = "_";
            // 
            // lblDefaultCraftTime
            // 
            this.lblDefaultCraftTime.Location = new System.Drawing.Point(750, 72);
            this.lblDefaultCraftTime.Name = "lblDefaultCraftTime";
            this.lblDefaultCraftTime.Size = new System.Drawing.Size(161, 23);
            this.lblDefaultCraftTime.TabIndex = 23;
            this.lblDefaultCraftTime.Text = "Default prod. time: ";
            this.lblDefaultCraftTime.Values.Text = "Default prod. time: ";
            // 
            // lblIndustryValue
            // 
            this.lblIndustryValue.Location = new System.Drawing.Point(504, 12);
            this.lblIndustryValue.Name = "lblIndustryValue";
            this.lblIndustryValue.Size = new System.Drawing.Size(21, 23);
            this.lblIndustryValue.TabIndex = 14;
            this.lblIndustryValue.Text = "_";
            this.lblIndustryValue.Values.Text = "_";
            this.lblIndustryValue.Visible = false;
            // 
            // lblPerIndustryValue
            // 
            this.lblPerIndustryValue.Location = new System.Drawing.Point(904, 12);
            this.lblPerIndustryValue.Name = "lblPerIndustryValue";
            this.lblPerIndustryValue.Size = new System.Drawing.Size(67, 23);
            this.lblPerIndustryValue.TabIndex = 13;
            this.lblPerIndustryValue.Text = "0 / day";
            this.lblPerIndustryValue.Values.Text = "0 / day";
            this.lblPerIndustryValue.Click += new System.EventHandler(this.LblPerIndustryValue_Click);
            // 
            // lblOreCostValue
            // 
            this.lblOreCostValue.Location = new System.Drawing.Point(98, 98);
            this.lblOreCostValue.Name = "lblOreCostValue";
            this.lblOreCostValue.Size = new System.Drawing.Size(36, 23);
            this.lblOreCostValue.TabIndex = 12;
            this.lblOreCostValue.Text = "0 q";
            this.toolTip1.SetToolTip(this.lblOreCostValue, "Total ore cost");
            this.lblOreCostValue.Values.Text = "0 q";
            // 
            // lblCostValue
            // 
            this.lblCostValue.Location = new System.Drawing.Point(98, 12);
            this.lblCostValue.Name = "lblCostValue";
            this.lblCostValue.Size = new System.Drawing.Size(36, 23);
            this.lblCostValue.TabIndex = 11;
            this.lblCostValue.Text = "0 q";
            this.lblCostValue.Values.Text = "0 q";
            // 
            // lblCostSingle
            // 
            this.lblCostSingle.Location = new System.Drawing.Point(6, 72);
            this.lblCostSingle.Name = "lblCostSingle";
            this.lblCostSingle.Size = new System.Drawing.Size(95, 23);
            this.lblCostSingle.TabIndex = 4;
            this.lblCostSingle.Text = "Cost for 1: ";
            this.lblCostSingle.Values.Text = "Cost for 1: ";
            // 
            // lblCostSingleValue
            // 
            this.lblCostSingleValue.Location = new System.Drawing.Point(98, 72);
            this.lblCostSingleValue.Name = "lblCostSingleValue";
            this.lblCostSingleValue.Size = new System.Drawing.Size(36, 23);
            this.lblCostSingleValue.TabIndex = 11;
            this.lblCostSingleValue.Text = "0 q";
            this.lblCostSingleValue.ToolTipValues.Description = "";
            this.lblCostSingleValue.ToolTipValues.Heading = "";
            this.lblCostSingleValue.Values.Text = "0 q";
            // 
            // lblNano
            // 
            this.lblNano.Location = new System.Drawing.Point(972, 135);
            this.lblNano.Name = "lblNano";
            this.lblNano.Size = new System.Drawing.Size(119, 23);
            this.lblNano.TabIndex = 8;
            this.lblNano.Text = "Nanocraftable";
            this.lblNano.Values.Text = "Nanocraftable";
            // 
            // lblPerIndustry
            // 
            this.lblPerIndustry.Location = new System.Drawing.Point(750, 12);
            this.lblPerIndustry.Name = "lblPerIndustry";
            this.lblPerIndustry.Size = new System.Drawing.Size(108, 23);
            this.lblPerIndustry.TabIndex = 7;
            this.lblPerIndustry.Text = "Per Industry";
            this.lblPerIndustry.Values.Text = "Per Industry";
            // 
            // lblOreCost
            // 
            this.lblOreCost.Location = new System.Drawing.Point(6, 98);
            this.lblOreCost.Name = "lblOreCost";
            this.lblOreCost.Size = new System.Drawing.Size(46, 23);
            this.lblOreCost.TabIndex = 6;
            this.lblOreCost.Text = "Ore:";
            this.lblOreCost.Values.Text = "Ore:";
            // 
            // lblCostForBatch
            // 
            this.lblCostForBatch.Location = new System.Drawing.Point(6, 12);
            this.lblCostForBatch.Name = "lblCostForBatch";
            this.lblCostForBatch.Size = new System.Drawing.Size(97, 23);
            this.lblCostForBatch.TabIndex = 4;
            this.lblCostForBatch.Text = "Total Cost: ";
            this.lblCostForBatch.Values.Text = "Total Cost: ";
            // 
            // lblUnitData
            // 
            this.lblUnitData.Location = new System.Drawing.Point(750, 42);
            this.lblUnitData.Name = "lblUnitData";
            this.lblUnitData.Size = new System.Drawing.Size(43, 23);
            this.lblUnitData.TabIndex = 2;
            this.lblUnitData.Text = "Unit ";
            this.lblUnitData.Values.Text = "Unit ";
            // 
            // ContentDocumentTree
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.kryptonPanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ContentDocumentTree";
            this.Size = new System.Drawing.Size(1253, 775);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroup.Panel)).EndInit();
            this.HeaderGroup.Panel.ResumeLayout(false);
            this.HeaderGroup.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroup)).EndInit();
            this.HeaderGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RoundToCmb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrePicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureNano)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridTalents)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        public Krypton.Toolkit.KryptonPanel kryptonPanel;
        private Krypton.Toolkit.KryptonHeaderGroup HeaderGroup;
        private Krypton.Toolkit.ButtonSpecHeaderGroup BtnSaveState;
        private Krypton.Toolkit.ButtonSpecHeaderGroup BtnRestoreState;
        private Krypton.Toolkit.ButtonSpecHeaderGroup BtnToggleNodes;
        private BrightIdeasSoftware.TreeListView treeListView;
        private BrightIdeasSoftware.OLVColumn olvColumnSection;
        private BrightIdeasSoftware.OLVColumn olvColumnEntry;
        private BrightIdeasSoftware.OLVColumn olvColumnTier;
        private BrightIdeasSoftware.OLVColumn olvColumnQty;
        private BrightIdeasSoftware.OLVColumn olvColumnAmt;
        private BrightIdeasSoftware.OLVColumn olvColumnSchemataQ;
        private BrightIdeasSoftware.OLVColumn olvColumnSchemataA;
        private BrightIdeasSoftware.OLVColumn olvColumnMass;
        private BrightIdeasSoftware.OLVColumn olvColumnVol;
        private BrightIdeasSoftware.OLVColumn olvColumnFiller;
        private System.Windows.Forms.ToolTip toolTip1;
        private KLabel lblUnitData;
        private KLabel lblCostForBatch;
        private KLabel lblPerIndustry;
        private KLabel lblOreCost;
        private ImageList largeImageList;
        private ImageList smallImageList;
        private PictureBox pictureNano;
        private KLabel lblNano;
        private KLabel lblCostValue;
        private KLabel lblCostSingle;
        private KLabel lblCostSingleValue;
        private KLabel lblOreCostValue;
        private KLinkLabel lblPerIndustryValue;
        private KLinkLabel lblIndustryValue;
        private Krypton.Toolkit.ButtonSpecHeaderGroup BtnFontUp;
        private Krypton.Toolkit.ButtonSpecHeaderGroup BtnFontDown;
        private KLabel lblCraftTime;
        private KLabel lblDefaultCraftTimeValue;
        private KLabel LblPure;
        private KLabel LblPureValue;
        private KLabel LblBatchSizeValue;
        private KLabel LblBatchSize;
        private KLabel lblCraftTimeValue;
        private KLabel lblDefaultCraftTime;
        private Krypton.Toolkit.KryptonButton BtnRecalc;
        private Krypton.Toolkit.KryptonDataGridView GridTalents;
        private KLabel lblCraftTimeInfoValue;
        private KLabel lblBatchesValue;
        private KLabel lblBatches;
        private PictureBox OrePicture;
        private ImageList OreImageList;
        private Label LblHint;
        private Krypton.Toolkit.KryptonComboBox RoundToCmb;
        private Krypton.Toolkit.KryptonCheckBox ApplyRoundingCB;
        private Krypton.Toolkit.KryptonNumericUpDown grossMarginEdit;
        private Krypton.Toolkit.KryptonCheckBox ApplyGrossMarginCB;
        private BrightIdeasSoftware.OLVColumn olvColumnMargin;
        private BrightIdeasSoftware.OLVColumn olvColumnRetail;
        private Krypton.Toolkit.KryptonDropButton BtnExport;
        private Krypton.Toolkit.KryptonContextMenu exportMenu;
        private Krypton.Toolkit.KryptonContextMenuHeading kryptonContextMenuHeading1;
        private Krypton.Toolkit.KryptonContextMenuCheckBox expOptInclSubSections;
        private Krypton.Toolkit.KryptonContextMenuCheckBox expOptInclMargins;
        private Krypton.Toolkit.KryptonCommand kCmdSaveOptions;
        private Krypton.Toolkit.KryptonButton BtnSaveOptions;
        private KLabel LblOptSaved;
        private KLabel lblMarginValue;
        private KLabel lblMargin;
        private KLabel lblSchematicsCostValue;
        private KLabel lblSchematicsCost;
        private KLabel LblCostSuffix;
        private KLabel LblOreCostSuffix;
        private Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ColTitle;
        private Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn ColValue;
        private KLabel LblSchemCostSuffix;
    }
}
