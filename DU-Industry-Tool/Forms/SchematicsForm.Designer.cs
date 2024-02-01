namespace DU_Industry_Tool
{
    partial class SchematicValueForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel = new Krypton.Toolkit.KryptonPanel();
            this.schematicsGrid = new Krypton.Toolkit.KryptonDataGridView();
            this.RoundToCmb = new Krypton.Toolkit.KryptonComboBox();
            this.ApplyRoundingCB = new Krypton.Toolkit.KryptonCheckBox();
            this.duCraftImportBtn = new Krypton.Toolkit.KryptonButton();
            this.grossMarginEdit = new Krypton.Toolkit.KryptonNumericUpDown();
            this.ApplyGrossMarginCB = new Krypton.Toolkit.KryptonCheckBox();
            this.BtnSave = new Krypton.Toolkit.KryptonButton();
            this.kCmdSaveList = new Krypton.Toolkit.KryptonCommand();
            this.BtnClear = new Krypton.Toolkit.KryptonButton();
            this.kCmdClearList = new Krypton.Toolkit.KryptonCommand();
            this.BtnLoad = new Krypton.Toolkit.KryptonButton();
            this.kCmdLoadList = new Krypton.Toolkit.KryptonCommand();
            this.exportKBtn = new Krypton.Toolkit.KryptonDropButton();
            this.exportMenu = new Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuHeading1 = new Krypton.Toolkit.KryptonContextMenuHeading();
            this.expOptInclTalents = new Krypton.Toolkit.KryptonContextMenuCheckBox();
            this.expOptInclSchemDetails = new Krypton.Toolkit.KryptonContextMenuCheckBox();
            this.expOptInclQtySums = new Krypton.Toolkit.KryptonContextMenuCheckBox();
            this.expOptOnlyWithQty = new Krypton.Toolkit.KryptonContextMenuCheckBox();
            this.kryptonContextMenuSeparator1 = new Krypton.Toolkit.KryptonContextMenuSeparator();
            this.expCustSheet = new Krypton.Toolkit.KryptonContextMenuCheckButton();
            this.applyBtn = new Krypton.Toolkit.KryptonButton();
            this.CloseBtn = new Krypton.Toolkit.KryptonButton();
            this.kryptonContextMenuItems1 = new Krypton.Toolkit.KryptonContextMenuItems();
            this.kLabel6 = new DU_Industry_Tool.KLabel();
            this.tbSkill4 = new DU_Industry_Tool.ButtonRow();
            this.tbSkill3 = new DU_Industry_Tool.ButtonRow();
            this.tbSkill2 = new DU_Industry_Tool.ButtonRow();
            this.tbSkill1 = new DU_Industry_Tool.ButtonRow();
            this.totalSumLabel = new DU_Industry_Tool.KLabel();
            this.kLabel5 = new DU_Industry_Tool.KLabel();
            this.craftingLbl = new DU_Industry_Tool.KLabel();
            this.skill4 = new DU_Industry_Tool.KLabel();
            this.skill3 = new DU_Industry_Tool.KLabel();
            this.skill2 = new DU_Industry_Tool.KLabel();
            this.skill1 = new DU_Industry_Tool.KLabel();
            this.kLabel4 = new DU_Industry_Tool.KLabel();
            this.kLabel3 = new DU_Industry_Tool.KLabel();
            this.kLabel2 = new DU_Industry_Tool.KLabel();
            this.kLabel1 = new DU_Industry_Tool.KLabel();
            this.KEY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchematicName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quanta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn();
            this.Sum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panel)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.schematicsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RoundToCmb)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.schematicsGrid);
            this.panel.Controls.Add(this.kLabel6);
            this.panel.Controls.Add(this.RoundToCmb);
            this.panel.Controls.Add(this.ApplyRoundingCB);
            this.panel.Controls.Add(this.duCraftImportBtn);
            this.panel.Controls.Add(this.grossMarginEdit);
            this.panel.Controls.Add(this.ApplyGrossMarginCB);
            this.panel.Controls.Add(this.BtnSave);
            this.panel.Controls.Add(this.BtnClear);
            this.panel.Controls.Add(this.BtnLoad);
            this.panel.Controls.Add(this.tbSkill4);
            this.panel.Controls.Add(this.tbSkill3);
            this.panel.Controls.Add(this.tbSkill2);
            this.panel.Controls.Add(this.tbSkill1);
            this.panel.Controls.Add(this.totalSumLabel);
            this.panel.Controls.Add(this.exportKBtn);
            this.panel.Controls.Add(this.kLabel5);
            this.panel.Controls.Add(this.craftingLbl);
            this.panel.Controls.Add(this.skill4);
            this.panel.Controls.Add(this.skill3);
            this.panel.Controls.Add(this.skill2);
            this.panel.Controls.Add(this.skill1);
            this.panel.Controls.Add(this.kLabel4);
            this.panel.Controls.Add(this.kLabel3);
            this.panel.Controls.Add(this.kLabel2);
            this.panel.Controls.Add(this.kLabel1);
            this.panel.Controls.Add(this.applyBtn);
            this.panel.Controls.Add(this.CloseBtn);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Margin = new System.Windows.Forms.Padding(4);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(954, 925);
            this.panel.TabIndex = 0;
            // 
            // schematicsGrid
            // 
            this.schematicsGrid.AllowUserToAddRows = false;
            this.schematicsGrid.AllowUserToDeleteRows = false;
            this.schematicsGrid.AllowUserToResizeRows = false;
            this.schematicsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.schematicsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.schematicsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.schematicsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KEY,
            this.SchematicName,
            this.BatchSize,
            this.BatchCost,
            this.Quanta,
            this.Qty,
            this.Sum});
            this.schematicsGrid.Location = new System.Drawing.Point(9, 272);
            this.schematicsGrid.Margin = new System.Windows.Forms.Padding(4);
            this.schematicsGrid.MultiSelect = false;
            this.schematicsGrid.Name = "schematicsGrid";
            this.schematicsGrid.RowHeadersWidth = 21;
            this.schematicsGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.schematicsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.schematicsGrid.Size = new System.Drawing.Size(934, 567);
            this.schematicsGrid.TabIndex = 10;
            this.schematicsGrid.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.schematicsGrid_CellValidated);
            // 
            // RoundToCmb
            // 
            this.RoundToCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RoundToCmb.DropDownWidth = 175;
            this.RoundToCmb.IntegralHeight = false;
            this.RoundToCmb.Items.AddRange(new object[] {
            "next 10",
            "next 100",
            "next 1 000",
            "next 10 000",
            "next 100 000",
            "next 1 000 000"});
            this.RoundToCmb.Location = new System.Drawing.Point(620, 121);
            this.RoundToCmb.MaxLength = 3;
            this.RoundToCmb.Name = "RoundToCmb";
            this.RoundToCmb.Size = new System.Drawing.Size(175, 26);
            this.RoundToCmb.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.RoundToCmb.TabIndex = 35;
            // 
            // ApplyRoundingCB
            // 
            this.ApplyRoundingCB.Location = new System.Drawing.Point(620, 89);
            this.ApplyRoundingCB.Name = "ApplyRoundingCB";
            this.ApplyRoundingCB.Size = new System.Drawing.Size(260, 25);
            this.ApplyRoundingCB.TabIndex = 34;
            this.ApplyRoundingCB.ToolTipValues.Description = "% on top of single price before calculating sum";
            this.ApplyRoundingCB.ToolTipValues.Heading = "Gross Margin %";
            this.ApplyRoundingCB.Values.Text = "Round up sums to x digits?";
            this.ApplyRoundingCB.CheckStateChanged += new System.EventHandler(this.ApplyRoundingCB_CheckStateChanged);
            // 
            // duCraftImportBtn
            // 
            this.duCraftImportBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.duCraftImportBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.duCraftImportBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.duCraftImportBtn.Location = new System.Drawing.Point(206, 881);
            this.duCraftImportBtn.Margin = new System.Windows.Forms.Padding(4);
            this.duCraftImportBtn.Name = "duCraftImportBtn";
            this.duCraftImportBtn.Size = new System.Drawing.Size(147, 35);
            this.duCraftImportBtn.TabIndex = 33;
            this.duCraftImportBtn.ToolTipValues.Description = "Import skills from clipboard if copied from du-craft.online website";
            this.duCraftImportBtn.ToolTipValues.Heading = "du-craft skills import";
            this.duCraftImportBtn.Values.Text = "DU-Craft skills...";
            this.duCraftImportBtn.Click += new System.EventHandler(this.duCraftImportBtn_Click);
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
            this.grossMarginEdit.Location = new System.Drawing.Point(620, 54);
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
            this.grossMarginEdit.MinimumSize = new System.Drawing.Size(90, 0);
            this.grossMarginEdit.Name = "grossMarginEdit";
            this.grossMarginEdit.Size = new System.Drawing.Size(90, 27);
            this.grossMarginEdit.TabIndex = 32;
            this.grossMarginEdit.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // ApplyGrossMarginCB
            // 
            this.ApplyGrossMarginCB.Location = new System.Drawing.Point(620, 24);
            this.ApplyGrossMarginCB.Name = "ApplyGrossMarginCB";
            this.ApplyGrossMarginCB.Size = new System.Drawing.Size(242, 25);
            this.ApplyGrossMarginCB.TabIndex = 31;
            this.ApplyGrossMarginCB.ToolTipValues.Description = "% on top of single price before calculating sum";
            this.ApplyGrossMarginCB.ToolTipValues.Heading = "Gross Margin %";
            this.ApplyGrossMarginCB.Values.Text = "Apply Gross Margin (%)?";
            this.ApplyGrossMarginCB.CheckStateChanged += new System.EventHandler(this.applyGrossMarginCB_CheckStateChanged);
            // 
            // BtnSave
            // 
            this.BtnSave.KryptonCommand = this.kCmdSaveList;
            this.BtnSave.Location = new System.Drawing.Point(664, 230);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(36, 36);
            this.BtnSave.StateNormal.Border.Color1 = System.Drawing.Color.Black;
            this.BtnSave.StateNormal.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.BtnSave.StateNormal.Border.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.BtnSave.StateNormal.Border.Rounding = 2F;
            this.BtnSave.TabIndex = 30;
            this.BtnSave.ToolTipValues.Description = "Save schematics which have quantities to file";
            this.BtnSave.ToolTipValues.EnableToolTips = true;
            this.BtnSave.ToolTipValues.Heading = "Save";
            this.BtnSave.Values.Text = "";
            // 
            // kCmdSaveList
            // 
            this.kCmdSaveList.ImageLarge = global::DU_Industry_Tool.Properties.Resources.filesave;
            this.kCmdSaveList.ImageSmall = global::DU_Industry_Tool.Properties.Resources.filesave;
            // 
            // BtnClear
            // 
            this.BtnClear.KryptonCommand = this.kCmdClearList;
            this.BtnClear.Location = new System.Drawing.Point(708, 230);
            this.BtnClear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(36, 36);
            this.BtnClear.StateNormal.Border.Color1 = System.Drawing.Color.Black;
            this.BtnClear.StateNormal.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.BtnClear.StateNormal.Border.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.BtnClear.StateNormal.Border.Rounding = 2F;
            this.BtnClear.TabIndex = 29;
            this.BtnClear.ToolTipValues.Description = "Reset quantities and sums";
            this.BtnClear.ToolTipValues.EnableToolTips = true;
            this.BtnClear.ToolTipValues.Heading = "Clear";
            this.BtnClear.Values.Text = "";
            // 
            // kCmdClearList
            // 
            this.kCmdClearList.ImageLarge = global::DU_Industry_Tool.Properties.Resources.dialog_cancel;
            this.kCmdClearList.ImageSmall = global::DU_Industry_Tool.Properties.Resources.dialog_cancel;
            // 
            // BtnLoad
            // 
            this.BtnLoad.KryptonCommand = this.kCmdLoadList;
            this.BtnLoad.Location = new System.Drawing.Point(620, 230);
            this.BtnLoad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnLoad.Name = "BtnLoad";
            this.BtnLoad.Size = new System.Drawing.Size(36, 36);
            this.BtnLoad.StateNormal.Border.Color1 = System.Drawing.Color.Black;
            this.BtnLoad.StateNormal.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.BtnLoad.StateNormal.Border.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.BtnLoad.StateNormal.Border.Rounding = 2F;
            this.BtnLoad.TabIndex = 28;
            this.BtnLoad.ToolTipValues.Description = "Load quantities from file";
            this.BtnLoad.ToolTipValues.EnableToolTips = true;
            this.BtnLoad.ToolTipValues.Heading = "Load";
            this.BtnLoad.Values.Text = "";
            // 
            // kCmdLoadList
            // 
            this.kCmdLoadList.ImageLarge = global::DU_Industry_Tool.Properties.Resources.fileopen;
            this.kCmdLoadList.ImageSmall = global::DU_Industry_Tool.Properties.Resources.fileopen;
            // 
            // exportKBtn
            // 
            this.exportKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exportKBtn.KryptonContextMenu = this.exportMenu;
            this.exportKBtn.Location = new System.Drawing.Point(12, 881);
            this.exportKBtn.Margin = new System.Windows.Forms.Padding(4);
            this.exportKBtn.Name = "exportKBtn";
            this.exportKBtn.Size = new System.Drawing.Size(165, 35);
            this.exportKBtn.TabIndex = 26;
            this.exportKBtn.Values.Text = "Export to Excel";
            this.exportKBtn.Click += new System.EventHandler(this.exportKBtn_Click);
            // 
            // exportMenu
            // 
            this.exportMenu.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuHeading1,
            this.expOptInclTalents,
            this.expOptInclSchemDetails,
            this.expOptInclQtySums,
            this.expOptOnlyWithQty,
            this.kryptonContextMenuSeparator1,
            this.expCustSheet});
            // 
            // kryptonContextMenuHeading1
            // 
            this.kryptonContextMenuHeading1.ExtraText = "";
            this.kryptonContextMenuHeading1.Text = "Options";
            // 
            // expOptInclTalents
            // 
            this.expOptInclTalents.Checked = true;
            this.expOptInclTalents.CheckState = System.Windows.Forms.CheckState.Checked;
            this.expOptInclTalents.ExtraText = "";
            this.expOptInclTalents.Text = "Incl. talents";
            // 
            // expOptInclSchemDetails
            // 
            this.expOptInclSchemDetails.Checked = true;
            this.expOptInclSchemDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.expOptInclSchemDetails.ExtraText = "";
            this.expOptInclSchemDetails.Text = "Incl. batch size, prices";
            // 
            // expOptInclQtySums
            // 
            this.expOptInclQtySums.ExtraText = "";
            this.expOptInclQtySums.Text = "Incl. quantities and sums";
            // 
            // expOptOnlyWithQty
            // 
            this.expOptOnlyWithQty.ExtraText = "";
            this.expOptOnlyWithQty.Text = "Only schematics with Qty > 0";
            // 
            // expCustSheet
            // 
            this.expCustSheet.AutoClose = true;
            this.expCustSheet.Text = "Customer sheet";
            // 
            // applyBtn
            // 
            this.applyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.applyBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.applyBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applyBtn.Location = new System.Drawing.Point(396, 881);
            this.applyBtn.Margin = new System.Windows.Forms.Padding(4);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(100, 35);
            this.applyBtn.TabIndex = 11;
            this.applyBtn.Values.Text = "&Apply";
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
            // 
            // CloseBtn
            // 
            this.CloseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseBtn.Location = new System.Drawing.Point(841, 881);
            this.CloseBtn.Margin = new System.Windows.Forms.Padding(4);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(100, 35);
            this.CloseBtn.TabIndex = 12;
            this.CloseBtn.Values.Text = "&Close";
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // kLabel6
            // 
            this.kLabel6.Location = new System.Drawing.Point(620, 153);
            this.kLabel6.Margin = new System.Windows.Forms.Padding(4);
            this.kLabel6.Name = "kLabel6";
            this.kLabel6.Size = new System.Drawing.Size(303, 25);
            this.kLabel6.TabIndex = 36;
            this.kLabel6.TabStop = false;
            this.kLabel6.Text = "Note: options are only used here!";
            this.kLabel6.Values.Text = "Note: options are only used here!";
            // 
            // tbSkill4
            // 
            this.tbSkill4.AutoSize = true;
            this.tbSkill4.BackColor = System.Drawing.Color.Transparent;
            this.tbSkill4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSkill4.Location = new System.Drawing.Point(389, 181);
            this.tbSkill4.Margin = new System.Windows.Forms.Padding(0);
            this.tbSkill4.Name = "tbSkill4";
            this.tbSkill4.Padding = new System.Windows.Forms.Padding(2);
            this.tbSkill4.Size = new System.Drawing.Size(208, 38);
            this.tbSkill4.TabIndex = 3;
            this.tbSkill4.Tag = "3";
            this.tbSkill4.ValueSelected += new System.EventHandler<int>(this.buttonRow1_ValueSelected);
            // 
            // tbSkill3
            // 
            this.tbSkill3.AutoSize = true;
            this.tbSkill3.BackColor = System.Drawing.Color.Transparent;
            this.tbSkill3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSkill3.Location = new System.Drawing.Point(389, 130);
            this.tbSkill3.Margin = new System.Windows.Forms.Padding(0);
            this.tbSkill3.Name = "tbSkill3";
            this.tbSkill3.Padding = new System.Windows.Forms.Padding(2);
            this.tbSkill3.Size = new System.Drawing.Size(208, 38);
            this.tbSkill3.TabIndex = 2;
            this.tbSkill3.Tag = "2";
            this.tbSkill3.ValueSelected += new System.EventHandler<int>(this.buttonRow1_ValueSelected);
            // 
            // tbSkill2
            // 
            this.tbSkill2.AutoSize = true;
            this.tbSkill2.BackColor = System.Drawing.Color.Transparent;
            this.tbSkill2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSkill2.Location = new System.Drawing.Point(389, 75);
            this.tbSkill2.Margin = new System.Windows.Forms.Padding(0);
            this.tbSkill2.Name = "tbSkill2";
            this.tbSkill2.Padding = new System.Windows.Forms.Padding(2);
            this.tbSkill2.Size = new System.Drawing.Size(208, 38);
            this.tbSkill2.TabIndex = 1;
            this.tbSkill2.Tag = "1";
            this.tbSkill2.ValueSelected += new System.EventHandler<int>(this.buttonRow1_ValueSelected);
            // 
            // tbSkill1
            // 
            this.tbSkill1.AutoSize = true;
            this.tbSkill1.BackColor = System.Drawing.Color.Transparent;
            this.tbSkill1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSkill1.Location = new System.Drawing.Point(389, 26);
            this.tbSkill1.Margin = new System.Windows.Forms.Padding(0);
            this.tbSkill1.Name = "tbSkill1";
            this.tbSkill1.Padding = new System.Windows.Forms.Padding(2);
            this.tbSkill1.Size = new System.Drawing.Size(208, 38);
            this.tbSkill1.TabIndex = 0;
            this.tbSkill1.Tag = "0";
            this.tbSkill1.ValueSelected += new System.EventHandler<int>(this.buttonRow1_ValueSelected);
            // 
            // totalSumLabel
            // 
            this.totalSumLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalSumLabel.Location = new System.Drawing.Point(825, 849);
            this.totalSumLabel.Margin = new System.Windows.Forms.Padding(4);
            this.totalSumLabel.Name = "totalSumLabel";
            this.totalSumLabel.Size = new System.Drawing.Size(116, 25);
            this.totalSumLabel.StateCommon.ShortText.Font = new System.Drawing.Font("Verdana", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalSumLabel.TabIndex = 27;
            this.totalSumLabel.TabStop = false;
            this.totalSumLabel.Text = "Total: 0.00";
            this.totalSumLabel.Values.Text = "Total: 0.00";
            // 
            // kLabel5
            // 
            this.kLabel5.Location = new System.Drawing.Point(13, 237);
            this.kLabel5.Margin = new System.Windows.Forms.Padding(4);
            this.kLabel5.Name = "kLabel5";
            this.kLabel5.Size = new System.Drawing.Size(339, 25);
            this.kLabel5.StateCommon.ShortText.Font = new System.Drawing.Font("Verdana", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kLabel5.TabIndex = 9;
            this.kLabel5.TabStop = false;
            this.kLabel5.Text = "Schematic Details and Calculation";
            this.kLabel5.Values.Text = "Schematic Details and Calculation";
            // 
            // craftingLbl
            // 
            this.craftingLbl.Location = new System.Drawing.Point(13, 4);
            this.craftingLbl.Margin = new System.Windows.Forms.Padding(4);
            this.craftingLbl.Name = "craftingLbl";
            this.craftingLbl.Size = new System.Drawing.Size(282, 25);
            this.craftingLbl.StateCommon.ShortText.Font = new System.Drawing.Font("Verdana", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.craftingLbl.TabIndex = 0;
            this.craftingLbl.TabStop = false;
            this.craftingLbl.Text = "Schematics Crafting Talents";
            this.craftingLbl.Values.Text = "Schematics Crafting Talents";
            // 
            // skill4
            // 
            this.skill4.AutoSize = false;
            this.skill4.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.skill4.Location = new System.Drawing.Point(362, 186);
            this.skill4.Margin = new System.Windows.Forms.Padding(4);
            this.skill4.Name = "skill4";
            this.skill4.Size = new System.Drawing.Size(22, 24);
            this.skill4.TabIndex = 24;
            this.skill4.TabStop = false;
            this.skill4.Text = "0";
            this.skill4.Values.Text = "0";
            // 
            // skill3
            // 
            this.skill3.AutoSize = false;
            this.skill3.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.skill3.Location = new System.Drawing.Point(362, 134);
            this.skill3.Margin = new System.Windows.Forms.Padding(4);
            this.skill3.Name = "skill3";
            this.skill3.Size = new System.Drawing.Size(22, 24);
            this.skill3.TabIndex = 23;
            this.skill3.TabStop = false;
            this.skill3.Text = "0";
            this.skill3.Values.Text = "0";
            // 
            // skill2
            // 
            this.skill2.AutoSize = false;
            this.skill2.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.skill2.Location = new System.Drawing.Point(362, 79);
            this.skill2.Margin = new System.Windows.Forms.Padding(4);
            this.skill2.Name = "skill2";
            this.skill2.Size = new System.Drawing.Size(22, 24);
            this.skill2.TabIndex = 21;
            this.skill2.TabStop = false;
            this.skill2.Text = "0";
            this.skill2.Values.Text = "0";
            // 
            // skill1
            // 
            this.skill1.AutoSize = false;
            this.skill1.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.skill1.Location = new System.Drawing.Point(362, 31);
            this.skill1.Margin = new System.Windows.Forms.Padding(4);
            this.skill1.Name = "skill1";
            this.skill1.Size = new System.Drawing.Size(22, 24);
            this.skill1.TabIndex = 20;
            this.skill1.TabStop = false;
            this.skill1.Text = "0";
            this.skill1.UseMnemonic = false;
            this.skill1.Values.Text = "0";
            // 
            // kLabel4
            // 
            this.kLabel4.Location = new System.Drawing.Point(13, 186);
            this.kLabel4.Margin = new System.Windows.Forms.Padding(4);
            this.kLabel4.Name = "kLabel4";
            this.kLabel4.Size = new System.Drawing.Size(328, 25);
            this.kLabel4.TabIndex = 7;
            this.kLabel4.TabStop = false;
            this.kLabel4.Text = "Adv. Schematic Output Prod. (+2%)";
            this.kLabel4.Values.Text = "Adv. Schematic Output Prod. (+2%)";
            // 
            // kLabel3
            // 
            this.kLabel3.Location = new System.Drawing.Point(13, 134);
            this.kLabel3.Margin = new System.Windows.Forms.Padding(4);
            this.kLabel3.Name = "kLabel3";
            this.kLabel3.Size = new System.Drawing.Size(340, 25);
            this.kLabel3.TabIndex = 5;
            this.kLabel3.Text = "Schematic Output Productivity (+3%)";
            this.kLabel3.Values.Text = "Schematic Output Productivity (+3%)";
            // 
            // kLabel2
            // 
            this.kLabel2.Location = new System.Drawing.Point(13, 79);
            this.kLabel2.Margin = new System.Windows.Forms.Padding(4);
            this.kLabel2.Name = "kLabel2";
            this.kLabel2.Size = new System.Drawing.Size(330, 25);
            this.kLabel2.TabIndex = 3;
            this.kLabel2.TabStop = false;
            this.kLabel2.Text = "Adv. Schematic Cost Optim. (-3% q)";
            this.kLabel2.Values.Text = "Adv. Schematic Cost Optim. (-3% q)";
            // 
            // kLabel1
            // 
            this.kLabel1.Location = new System.Drawing.Point(13, 31);
            this.kLabel1.Margin = new System.Windows.Forms.Padding(4);
            this.kLabel1.Name = "kLabel1";
            this.kLabel1.Size = new System.Drawing.Size(336, 25);
            this.kLabel1.TabIndex = 1;
            this.kLabel1.TabStop = false;
            this.kLabel1.Text = "Schematic Cost Optimization (-5% q)";
            this.kLabel1.Values.Text = "Schematic Cost Optimization (-5% q)";
            // 
            // KEY
            // 
            this.KEY.Frozen = true;
            this.KEY.HeaderText = "KEY";
            this.KEY.MinimumWidth = 6;
            this.KEY.Name = "KEY";
            this.KEY.ReadOnly = true;
            this.KEY.Visible = false;
            this.KEY.Width = 50;
            // 
            // SchematicName
            // 
            this.SchematicName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SchematicName.DefaultCellStyle = dataGridViewCellStyle1;
            this.SchematicName.HeaderText = "Schematic Name";
            this.SchematicName.MinimumWidth = 220;
            this.SchematicName.Name = "SchematicName";
            this.SchematicName.ReadOnly = true;
            this.SchematicName.Width = 220;
            // 
            // BatchSize
            // 
            this.BatchSize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.BatchSize.DefaultCellStyle = dataGridViewCellStyle2;
            this.BatchSize.HeaderText = "Batch-Size";
            this.BatchSize.MinimumWidth = 60;
            this.BatchSize.Name = "BatchSize";
            this.BatchSize.ReadOnly = true;
            this.BatchSize.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BatchSize.Width = 112;
            // 
            // BatchCost
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.BatchCost.DefaultCellStyle = dataGridViewCellStyle3;
            this.BatchCost.HeaderText = "q / Batch";
            this.BatchCost.MinimumWidth = 120;
            this.BatchCost.Name = "BatchCost";
            this.BatchCost.ReadOnly = true;
            this.BatchCost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BatchCost.Width = 120;
            // 
            // Quanta
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.Quanta.DefaultCellStyle = dataGridViewCellStyle4;
            this.Quanta.HeaderText = "q / x1";
            this.Quanta.MinimumWidth = 130;
            this.Quanta.Name = "Quanta";
            this.Quanta.ReadOnly = true;
            this.Quanta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Quanta.Width = 130;
            // 
            // Qty
            // 
            this.Qty.AllowDecimals = false;
            this.Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = null;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle5;
            this.Qty.HeaderText = "Qty.";
            this.Qty.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.Qty.MinimumWidth = 80;
            this.Qty.Name = "Qty";
            this.Qty.Width = 80;
            // 
            // Sum
            // 
            this.Sum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.Sum.DefaultCellStyle = dataGridViewCellStyle6;
            this.Sum.HeaderText = "Sum";
            this.Sum.MinimumWidth = 160;
            this.Sum.Name = "Sum";
            this.Sum.ReadOnly = true;
            // 
            // SchematicValueForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(954, 925);
            this.Controls.Add(this.panel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(950, 747);
            this.Name = "SchematicValueForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.StateCommon.Border.Rounding = 4F;
            this.Text = "Schematic crafting and talents";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SchematicValueForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.panel)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.schematicsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RoundToCmb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonDataGridView schematicsGrid;
        private Krypton.Toolkit.KryptonPanel panel;
        private Krypton.Toolkit.KryptonButton CloseBtn;
        private Krypton.Toolkit.KryptonButton applyBtn;
        private KLabel kLabel1;
        private KLabel kLabel2;
        private KLabel kLabel3;
        private KLabel kLabel4;
        private KLabel skill1;
        private KLabel skill2;
        private KLabel skill3;
        private KLabel skill4;
        private KLabel craftingLbl;
        private KLabel kLabel5;
        private Krypton.Toolkit.KryptonDropButton exportKBtn;
        private Krypton.Toolkit.KryptonContextMenu exportMenu;
        private Krypton.Toolkit.KryptonContextMenuHeading kryptonContextMenuHeading1;
        private Krypton.Toolkit.KryptonContextMenuCheckBox expOptInclTalents;
        private Krypton.Toolkit.KryptonContextMenuCheckBox expOptInclSchemDetails;
        private Krypton.Toolkit.KryptonContextMenuCheckBox expOptInclQtySums;
        private Krypton.Toolkit.KryptonContextMenuCheckBox expOptOnlyWithQty;
        private KLabel totalSumLabel;
        private ButtonRow tbSkill1;
        private ButtonRow tbSkill2;
        private ButtonRow tbSkill3;
        private ButtonRow tbSkill4;
        private Krypton.Toolkit.KryptonCommand kCmdLoadList;
        private Krypton.Toolkit.KryptonCommand kCmdSaveList;
        private Krypton.Toolkit.KryptonCommand kCmdClearList;
        private Krypton.Toolkit.KryptonButton BtnClear;
        private Krypton.Toolkit.KryptonButton BtnLoad;
        private Krypton.Toolkit.KryptonButton BtnSave;
        private Krypton.Toolkit.KryptonCheckBox ApplyGrossMarginCB;
        private Krypton.Toolkit.KryptonNumericUpDown grossMarginEdit;
        private Krypton.Toolkit.KryptonButton duCraftImportBtn;
        private Krypton.Toolkit.KryptonCheckBox ApplyRoundingCB;
        private Krypton.Toolkit.KryptonComboBox RoundToCmb;
        private KLabel kLabel6;
        private Krypton.Toolkit.KryptonContextMenuSeparator kryptonContextMenuSeparator1;
        private Krypton.Toolkit.KryptonContextMenuCheckButton expCustSheet;
        private Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
        private System.Windows.Forms.DataGridViewTextBoxColumn KEY;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchematicName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quanta;
        private Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sum;
    }
}