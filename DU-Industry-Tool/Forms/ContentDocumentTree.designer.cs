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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.kryptonPanel = new Krypton.Toolkit.KryptonPanel();
            this.treeListView = new BrightIdeasSoftware.TreeListView();
            this.olvColumnSection = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnEntry = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnTier = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnQty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnAmt = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnSchemataQ = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnSchemataA = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnFiller = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.largeImageList = new System.Windows.Forms.ImageList(this.components);
            this.smallImageList = new System.Windows.Forms.ImageList(this.components);
            this.kryptonHeaderGroup1 = new Krypton.Toolkit.KryptonHeaderGroup();
            this.BtnRestoreState = new Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.BtnSaveState = new Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.BtnToggleNodes = new Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.BtnFontUp = new Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.BtnFontDown = new Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.LblHint = new System.Windows.Forms.Label();
            this.OrePicture = new System.Windows.Forms.PictureBox();
            this.BtnRecalc = new Krypton.Toolkit.KryptonButton();
            this.pictureNano = new System.Windows.Forms.PictureBox();
            this.GridTalents = new Krypton.Toolkit.KryptonDataGridView();
            this.ColTitle = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.ColValue = new Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.OreImageList = new System.Windows.Forms.ImageList(this.components);
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
            this.lblBasicCostValue = new DU_Industry_Tool.KLabel();
            this.lblCostValue = new DU_Industry_Tool.KLabel();
            this.lblCostSingle = new DU_Industry_Tool.KLabel();
            this.lblCostSingleValue = new DU_Industry_Tool.KLabel();
            this.lblIndustry = new DU_Industry_Tool.KLabel();
            this.lblNano = new DU_Industry_Tool.KLabel();
            this.lblPerIndustry = new DU_Industry_Tool.KLabel();
            this.lblBasicCost = new DU_Industry_Tool.KLabel();
            this.lblCostForBatch = new DU_Industry_Tool.KLabel();
            this.lblUnitData = new DU_Industry_Tool.KLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OrePicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureNano)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridTalents)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.treeListView);
            this.kryptonPanel.Controls.Add(this.kryptonHeaderGroup1);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonPanel.Size = new System.Drawing.Size(1103, 795);
            this.kryptonPanel.TabIndex = 2;
            // 
            // treeListView
            // 
            this.treeListView.AllColumns.Add(this.olvColumnSection);
            this.treeListView.AllColumns.Add(this.olvColumnEntry);
            this.treeListView.AllColumns.Add(this.olvColumnTier);
            this.treeListView.AllColumns.Add(this.olvColumnQty);
            this.treeListView.AllColumns.Add(this.olvColumnAmt);
            this.treeListView.AllColumns.Add(this.olvColumnSchemataQ);
            this.treeListView.AllColumns.Add(this.olvColumnSchemataA);
            this.treeListView.AllColumns.Add(this.olvColumnFiller);
            this.treeListView.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.treeListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeListView.BackColor = System.Drawing.SystemColors.Window;
            this.treeListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnSection,
            this.olvColumnEntry,
            this.olvColumnTier,
            this.olvColumnQty,
            this.olvColumnAmt,
            this.olvColumnSchemataQ,
            this.olvColumnSchemataA,
            this.olvColumnFiller});
            this.treeListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView.EmptyListMsg = "No recipe available!";
            this.treeListView.EmptyListMsgFont = new System.Drawing.Font("Segoe UI", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListView.GridLines = true;
            this.treeListView.HeaderMinimumHeight = 28;
            this.treeListView.HeaderWordWrap = true;
            this.treeListView.HideSelection = false;
            this.treeListView.IsSearchOnSortColumn = false;
            this.treeListView.LargeImageList = this.largeImageList;
            this.treeListView.Location = new System.Drawing.Point(0, 335);
            this.treeListView.Margin = new System.Windows.Forms.Padding(2);
            this.treeListView.MinimumSize = new System.Drawing.Size(350, 171);
            this.treeListView.MultiSelect = false;
            this.treeListView.Name = "treeListView";
            this.treeListView.SelectAllOnControlA = false;
            this.treeListView.SelectColumnsOnRightClick = false;
            this.treeListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.None;
            this.treeListView.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.treeListView.ShowGroups = false;
            this.treeListView.ShowImagesOnSubItems = true;
            this.treeListView.ShowItemCountOnGroups = true;
            this.treeListView.Size = new System.Drawing.Size(1102, 459);
            this.treeListView.SmallImageList = this.smallImageList;
            this.treeListView.TabIndex = 2;
            this.treeListView.UseCompatibleStateImageBehavior = false;
            this.treeListView.UseFilterIndicator = true;
            this.treeListView.UseFiltering = true;
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
            this.olvColumnEntry.MinimumWidth = 20;
            this.olvColumnEntry.Text = "Entry";
            this.olvColumnEntry.Width = 148;
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
            this.olvColumnAmt.MinimumWidth = 50;
            this.olvColumnAmt.Sortable = false;
            this.olvColumnAmt.Text = "Amt (q)";
            this.olvColumnAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnAmt.Width = 120;
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
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.ButtonSpecs.AddRange(new Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.BtnRestoreState,
            this.BtnSaveState,
            this.BtnToggleNodes,
            this.BtnFontUp,
            this.BtnFontDown});
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderGroup1.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.GridDataCellList;
            this.kryptonHeaderGroup1.GroupBorderStyle = Krypton.Toolkit.PaletteBorderStyle.GridDataCellList;
            this.kryptonHeaderGroup1.HeaderStylePrimary = Krypton.Toolkit.HeaderStyle.Form;
            this.kryptonHeaderGroup1.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.LblHint);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.OrePicture);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.lblBatches);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.lblBatchesValue);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.lblCraftTimeInfoValue);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.BtnRecalc);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.lblCraftTime);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.lblDefaultCraftTimeValue);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.LblPure);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.LblPureValue);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.LblBatchSizeValue);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.LblBatchSize);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.lblCraftTimeValue);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.lblDefaultCraftTime);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.lblIndustryValue);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.lblPerIndustryValue);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.lblBasicCostValue);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.lblCostValue);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.lblCostSingle);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.lblCostSingleValue);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.lblIndustry);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.pictureNano);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.lblNano);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.lblPerIndustry);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.lblBasicCost);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.lblCostForBatch);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.lblUnitData);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.GridTalents);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(1103, 330);
            this.kryptonHeaderGroup1.TabIndex = 2;
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "Calculation";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = null;
            // 
            // BtnRestoreState
            // 
            this.BtnRestoreState.Text = "Load Config";
            this.BtnRestoreState.UniqueName = "8759F4DBDA4544F62EA5239B1C0DEC24";
            this.BtnRestoreState.Click += new System.EventHandler(this.ButtonRestoreState_Click);
            // 
            // BtnSaveState
            // 
            this.BtnSaveState.Text = "Save Config";
            this.BtnSaveState.UniqueName = "B803E19DF9944F1593A01EF944CD3DCB";
            this.BtnSaveState.Click += new System.EventHandler(this.ButtonSaveState_Click);
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
            this.LblHint.Font = new System.Drawing.Font("Segoe UI", 30F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.LblHint.ForeColor = System.Drawing.Color.Black;
            this.LblHint.Location = new System.Drawing.Point(4, 123);
            this.LblHint.Name = "LblHint";
            this.LblHint.Padding = new System.Windows.Forms.Padding(5);
            this.LblHint.Size = new System.Drawing.Size(1094, 86);
            this.LblHint.TabIndex = 36;
            this.LblHint.Text = "Preparing data, please wait...";
            this.LblHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblHint.Visible = false;
            // 
            // OrePicture
            // 
            this.OrePicture.BackColor = System.Drawing.SystemColors.Window;
            this.OrePicture.Location = new System.Drawing.Point(750, 128);
            this.OrePicture.Name = "OrePicture";
            this.OrePicture.Size = new System.Drawing.Size(36, 34);
            this.OrePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.OrePicture.TabIndex = 35;
            this.OrePicture.TabStop = false;
            this.OrePicture.Visible = false;
            // 
            // BtnRecalc
            // 
            this.BtnRecalc.Location = new System.Drawing.Point(502, 158);
            this.BtnRecalc.Name = "BtnRecalc";
            this.BtnRecalc.Size = new System.Drawing.Size(108, 46);
            this.BtnRecalc.TabIndex = 31;
            this.BtnRecalc.Values.Text = "Recalculate";
            this.BtnRecalc.Click += new System.EventHandler(this.BtnRecalc_Click);
            // 
            // pictureNano
            // 
            this.pictureNano.Image = global::DU_Industry_Tool.Properties.Resources.Green_Ball;
            this.pictureNano.InitialImage = null;
            this.pictureNano.Location = new System.Drawing.Point(730, 12);
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
            this.GridTalents.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.None;// AllHeaders;
            this.GridTalents.ColumnHeadersHeight = 32;
            this.GridTalents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTitle,
            this.ColValue});
            this.GridTalents.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridTalents.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.GridTalents.Location = new System.Drawing.Point(4, 128);
            this.GridTalents.MultiSelect = false;
            this.GridTalents.Name = "GridTalents";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.GridTalents.RowsDefaultCellStyle = dataGridViewCellStyle2;
            //this.GridTalents.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            //this.GridTalents.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.GridTalents.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridTalents.RowTemplate.Height = 16;
            //this.GridTalents.RowTemplate.MinimumHeight = 20;
            this.GridTalents.RowTemplate.ReadOnly = true;
            this.GridTalents.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GridTalents.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.GridTalents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GridTalents.ShowEditingIcon = false;
            this.GridTalents.Size = new System.Drawing.Size(492, 153);
            this.GridTalents.StateSelected.DataCell.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.GridTalents.StateSelected.DataCell.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.GridTalents.TabIndex = 15;
            // 
            // ColTitle
            // 
            this.ColTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColTitle.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColTitle.HeaderText = "Related Talents";
            this.ColTitle.MinimumWidth = 300;
            this.ColTitle.Name = "ColTitle";
            this.ColTitle.ReadOnly = true;
            this.ColTitle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColTitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColTitle.Width = 322;
            // 
            // ColValue
            // 
            this.ColValue.AllowDecimals = false;
            this.ColValue.FillWeight = 50F;
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
            // lblBatches
            // 
            this.lblBatches.Location = new System.Drawing.Point(330, 96);
            this.lblBatches.Name = "lblBatches";
            this.lblBatches.Size = new System.Drawing.Size(63, 23);
            this.lblBatches.TabIndex = 34;
            this.lblBatches.Text = "Batches:";
            this.lblBatches.Values.Text = "Batches:";
            // 
            // lblBatchesValue
            // 
            this.lblBatchesValue.Location = new System.Drawing.Point(504, 96);
            this.lblBatchesValue.Name = "lblBatchesValue";
            this.lblBatchesValue.Size = new System.Drawing.Size(17, 23);
            this.lblBatchesValue.TabIndex = 33;
            this.lblBatchesValue.Text = "_";
            this.lblBatchesValue.Values.Text = "_";
            // 
            // lblCraftTimeInfoValue
            // 
            this.lblCraftTimeInfoValue.Location = new System.Drawing.Point(750, 96);
            this.lblCraftTimeInfoValue.Name = "lblCraftTimeInfoValue";
            this.lblCraftTimeInfoValue.Size = new System.Drawing.Size(17, 23);
            this.lblCraftTimeInfoValue.TabIndex = 32;
            this.lblCraftTimeInfoValue.Text = "_";
            this.toolTip1.SetToolTip(this.lblCraftTimeInfoValue, "Remaining quantity that is below batch input volume.");
            this.lblCraftTimeInfoValue.Values.Text = "_";
            // 
            // lblCraftTime
            // 
            this.lblCraftTime.Location = new System.Drawing.Point(750, 68);
            this.lblCraftTime.Name = "lblCraftTime";
            this.lblCraftTime.Size = new System.Drawing.Size(116, 23);
            this.lblCraftTime.TabIndex = 30;
            this.lblCraftTime.Text = "Production time: ";
            this.lblCraftTime.Values.Text = "Production time: ";
            // 
            // lblDefaultCraftTimeValue
            // 
            this.lblDefaultCraftTimeValue.Location = new System.Drawing.Point(152, 96);
            this.lblDefaultCraftTimeValue.Name = "lblDefaultCraftTimeValue";
            this.lblDefaultCraftTimeValue.Size = new System.Drawing.Size(17, 23);
            this.lblDefaultCraftTimeValue.TabIndex = 29;
            this.lblDefaultCraftTimeValue.Text = "_";
            this.toolTip1.SetToolTip(this.lblDefaultCraftTimeValue, "Ore refine time with talents applied");
            this.lblDefaultCraftTimeValue.Values.Text = "_";
            // 
            // LblPure
            // 
            this.LblPure.Location = new System.Drawing.Point(330, 68);
            this.LblPure.Name = "LblPure";
            this.LblPure.Size = new System.Drawing.Size(90, 23);
            this.LblPure.TabIndex = 28;
            this.LblPure.Text = "Pure output:";
            this.LblPure.Values.Text = "Pure output:";
            // 
            // LblPureValue
            // 
            this.LblPureValue.Location = new System.Drawing.Point(504, 68);
            this.LblPureValue.Name = "LblPureValue";
            this.LblPureValue.Size = new System.Drawing.Size(30, 23);
            this.LblPureValue.TabIndex = 26;
            this.LblPureValue.Text = "0 L";
            this.LblPureValue.Values.Text = "0 L";
            // 
            // LblBatchSizeValue
            // 
            this.LblBatchSizeValue.Location = new System.Drawing.Point(504, 42);
            this.LblBatchSizeValue.Name = "LblBatchSizeValue";
            this.LblBatchSizeValue.Size = new System.Drawing.Size(30, 23);
            this.LblBatchSizeValue.TabIndex = 27;
            this.LblBatchSizeValue.Text = "0 L";
            this.LblBatchSizeValue.Values.Text = "0 L";
            // 
            // LblBatchSize
            // 
            this.LblBatchSize.Location = new System.Drawing.Point(330, 42);
            this.LblBatchSize.Name = "LblBatchSize";
            this.LblBatchSize.Size = new System.Drawing.Size(83, 23);
            this.LblBatchSize.TabIndex = 25;
            this.LblBatchSize.Text = "Batch sizes:";
            this.LblBatchSize.Values.Text = "Batch sizes:";
            // 
            // lblCraftTimeValue
            // 
            this.lblCraftTimeValue.Location = new System.Drawing.Point(880, 68);
            this.lblCraftTimeValue.Name = "lblCraftTimeValue";
            this.lblCraftTimeValue.Size = new System.Drawing.Size(17, 23);
            this.lblCraftTimeValue.TabIndex = 24;
            this.lblCraftTimeValue.Text = "_";
            this.toolTip1.SetToolTip(this.lblCraftTimeValue, "Time with talents applied");
            this.lblCraftTimeValue.Values.Text = "_";
            // 
            // lblDefaultCraftTime
            // 
            this.lblDefaultCraftTime.Location = new System.Drawing.Point(4, 96);
            this.lblDefaultCraftTime.Name = "lblDefaultCraftTime";
            this.lblDefaultCraftTime.Size = new System.Drawing.Size(133, 23);
            this.lblDefaultCraftTime.TabIndex = 23;
            this.lblDefaultCraftTime.Text = "Default refine time: ";
            this.lblDefaultCraftTime.Values.Text = "Default refine time: ";
            // 
            // lblPerIndustryValue
            // 
            this.lblPerIndustryValue.Location = new System.Drawing.Point(504, 12);
            this.lblPerIndustryValue.Name = "lblPerIndustryValue";
            this.lblPerIndustryValue.Size = new System.Drawing.Size(55, 23);
            this.lblPerIndustryValue.TabIndex = 13;
            this.lblPerIndustryValue.Text = "0 / day";
            this.lblPerIndustryValue.Values.Text = "0 / day";
            this.lblPerIndustryValue.Click += new System.EventHandler(this.LblPerIndustryValue_Click);
            // 
            // lblNano
            // 
            this.lblNano.Location = new System.Drawing.Point(750, 12);
            this.lblNano.Name = "lblNano";
            this.lblNano.Size = new System.Drawing.Size(101, 23);
            this.lblNano.TabIndex = 8;
            this.lblNano.Text = "Nanocraftable";
            this.lblNano.Values.Text = "Nanocraftable";
            // 
            // lblCostForBatch
            // 
            this.lblCostForBatch.Location = new System.Drawing.Point(4, 12);
            this.lblCostForBatch.Name = "lblCostForBatch";
            this.lblCostForBatch.Size = new System.Drawing.Size(78, 23);
            this.lblCostForBatch.TabIndex = 4;
            this.lblCostForBatch.Text = "Total Cost: ";
            this.lblCostForBatch.Values.Text = "Total Cost: ";
            // 
            // lblCostValue
            // 
            this.lblCostValue.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblCostValue.Location = new System.Drawing.Point(98, 12);
            this.lblCostValue.Name = "lblCostValue";
            this.lblCostValue.Size = new System.Drawing.Size(17, 23);
            this.lblCostValue.TabIndex = 11;
            this.lblCostValue.Text = "-";
            this.lblCostValue.Values.Text = "-";
            // 
            // lblBasicCost
            // 
            this.lblBasicCost.Location = new System.Drawing.Point(4, 40);
            this.lblBasicCost.Name = "lblBasicCost";
            this.lblBasicCost.Size = new System.Drawing.Size(78, 23);
            this.lblBasicCost.TabIndex = 6;
            this.lblBasicCost.Text = "Basic Cost: ";
            this.lblBasicCost.Values.Text = "Basic Cost: ";
            // 
            // lblBasicCostValue
            // 
            this.lblBasicCostValue.Location = new System.Drawing.Point(98, 40);
            this.lblBasicCostValue.Name = "lblBasicCostValue";
            this.lblBasicCostValue.Size = new System.Drawing.Size(31, 23);
            this.lblBasicCostValue.TabIndex = 12;
            this.lblBasicCostValue.Text = "0 q";
            this.toolTip1.SetToolTip(this.lblBasicCostValue, "Ore cost without schematics");
            this.lblBasicCostValue.Values.Text = "0 q";

            // 
            // lblCostSingle
            // 
            this.lblCostSingle.Location = new System.Drawing.Point(4, 68);
            this.lblCostSingle.Name = "lblCostSingle";
            this.lblCostSingle.Size = new System.Drawing.Size(78, 23);
            this.lblCostSingle.TabIndex = 4;
            this.lblCostSingle.Text = "Cost for 1: ";
            this.lblCostSingle.Values.Text = "Cost for 1: ";
            // 
            // lblCostSingleValue
            // 
            this.lblCostSingleValue.Location = new System.Drawing.Point(98, 68);
            this.lblCostSingleValue.Name = "lblCostSingleValue";
            this.lblCostSingleValue.Size = new System.Drawing.Size(17, 23);
            this.lblCostSingleValue.TabIndex = 11;
            this.lblCostSingleValue.Text = "-";
            this.lblCostSingleValue.Values.Text = "-";

            // 
            // lblIndustry
            // 
            this.lblIndustry.Location = new System.Drawing.Point(4, 92);
            this.lblIndustry.Name = "lblIndustry";
            this.lblIndustry.Size = new System.Drawing.Size(66, 23);
            this.lblIndustry.TabIndex = 10;
            this.lblIndustry.Text = "Industry: ";
            this.lblIndustry.Values.Text = "Industry: ";
            // 
            // lblIndustryValue
            // 
            this.lblIndustryValue.Location = new System.Drawing.Point(98, 92);
            this.lblIndustryValue.Name = "lblIndustryValue";
            this.lblIndustryValue.Size = new System.Drawing.Size(17, 23);
            this.lblIndustryValue.TabIndex = 14;
            this.lblIndustryValue.Text = "-";
            this.lblIndustryValue.Values.Text = "-";
            // 
            // lblPerIndustry
            // 
            this.lblPerIndustry.Location = new System.Drawing.Point(330, 12);
            this.lblPerIndustry.Name = "lblPerIndustry";
            this.lblPerIndustry.Size = new System.Drawing.Size(87, 23);
            this.lblPerIndustry.TabIndex = 7;
            this.lblPerIndustry.Text = "Per Industry";
            this.lblPerIndustry.Values.Text = "Per Industry";
            // 
            // lblUnitData
            // 
            this.lblUnitData.Location = new System.Drawing.Point(750, 40);
            this.lblUnitData.Name = "lblUnitData";
            this.lblUnitData.Size = new System.Drawing.Size(38, 23);
            this.lblUnitData.TabIndex = 2;
            this.lblUnitData.Text = "Unit ";
            this.lblUnitData.Values.Text = "Unit ";
            // 
            // ContentDocumentTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ContentDocumentTree";
            this.Size = new System.Drawing.Size(1103, 795);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OrePicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureNano)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridTalents)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        public Krypton.Toolkit.KryptonPanel kryptonPanel;
        private Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
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
        private BrightIdeasSoftware.OLVColumn olvColumnFiller;
        private System.Windows.Forms.ToolTip toolTip1;
        private KLabel lblUnitData;
        private KLabel lblCostForBatch;
        private KLabel lblPerIndustry;
        private KLabel lblBasicCost;
        private ImageList largeImageList;
        private ImageList smallImageList;
        private PictureBox pictureNano;
        private KLabel lblNano;
        private KLabel lblIndustry;
        private KLabel lblCostValue;
        private KLabel lblCostSingle;
        private KLabel lblCostSingleValue;
        private KLabel lblBasicCostValue;
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
        private Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ColTitle;
        private Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn ColValue;
        private KLabel lblCraftTimeInfoValue;
        private KLabel lblBatchesValue;
        private KLabel lblBatches;
        private PictureBox OrePicture;
        private ImageList OreImageList;
        private Label LblHint;
    }
}
