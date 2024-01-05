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
            this.schematicsGrid = new System.Windows.Forms.DataGridView();
            this.KEY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchematicName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quanta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn();
            this.Sum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.closeBtn = new Krypton.Toolkit.KryptonButton();
            this.applyBtn = new Krypton.Toolkit.KryptonButton();
            this.exportKBtn = new Krypton.Toolkit.KryptonDropButton();
            this.exportMenu = new Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuHeading1 = new Krypton.Toolkit.KryptonContextMenuHeading();
            this.expOptInclTalents = new Krypton.Toolkit.KryptonContextMenuCheckBox();
            this.expOptInclSchemDetails = new Krypton.Toolkit.KryptonContextMenuCheckBox();
            this.expOptInclQtySums = new Krypton.Toolkit.KryptonContextMenuCheckBox();
            this.expOptOnlyWithQty = new Krypton.Toolkit.KryptonContextMenuCheckBox();
            this.kCmdLoadList = new Krypton.Toolkit.KryptonCommand();
            this.kCmdSaveList = new Krypton.Toolkit.KryptonCommand();
            this.kCmdClearList = new Krypton.Toolkit.KryptonCommand();
            this.BtnClear = new Krypton.Toolkit.KryptonButton();
            this.BtnLoad = new Krypton.Toolkit.KryptonButton();
            this.BtnSave = new Krypton.Toolkit.KryptonButton();
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
            ((System.ComponentModel.ISupportInitialize)(this.schematicsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // schematicsGrid
            // 
            this.schematicsGrid.AllowUserToAddRows = false;
            this.schematicsGrid.AllowUserToDeleteRows = false;
            this.schematicsGrid.AllowUserToResizeColumns = false;
            this.schematicsGrid.AllowUserToResizeRows = false;
            this.schematicsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.schematicsGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.schematicsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.schematicsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KEY,
            this.SchematicName,
            this.BatchSize,
            this.BatchCost,
            this.Quanta,
            this.Qty,
            this.Sum});
            this.schematicsGrid.GridColor = System.Drawing.SystemColors.Window;
            this.schematicsGrid.Location = new System.Drawing.Point(7, 218);
            this.schematicsGrid.MultiSelect = false;
            this.schematicsGrid.Name = "schematicsGrid";
            this.schematicsGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.schematicsGrid.RowHeadersVisible = false;
            this.schematicsGrid.RowHeadersWidth = 21;
            this.schematicsGrid.RowTemplate.Height = 24;
            this.schematicsGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.schematicsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.schematicsGrid.ShowEditingIcon = false;
            this.schematicsGrid.Size = new System.Drawing.Size(686, 427);
            this.schematicsGrid.TabIndex = 10;
            this.schematicsGrid.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.schematicsGrid_RowValidating);
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SchematicName.DefaultCellStyle = dataGridViewCellStyle1;
            this.SchematicName.HeaderText = "Schematic Name";
            this.SchematicName.MinimumWidth = 200;
            this.SchematicName.Name = "SchematicName";
            this.SchematicName.ReadOnly = true;
            this.SchematicName.Width = 200;
            // 
            // BatchSize
            // 
            this.BatchSize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.BatchSize.DefaultCellStyle = dataGridViewCellStyle2;
            this.BatchSize.HeaderText = "Batch-Size";
            this.BatchSize.MinimumWidth = 60;
            this.BatchSize.Name = "BatchSize";
            this.BatchSize.ReadOnly = true;
            this.BatchSize.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BatchSize.Width = 68;
            // 
            // BatchCost
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.BatchCost.DefaultCellStyle = dataGridViewCellStyle3;
            this.BatchCost.HeaderText = "q / Batch";
            this.BatchCost.MinimumWidth = 125;
            this.BatchCost.Name = "BatchCost";
            this.BatchCost.ReadOnly = true;
            this.BatchCost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BatchCost.Width = 125;
            // 
            // Quanta
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.Quanta.DefaultCellStyle = dataGridViewCellStyle4;
            this.Quanta.HeaderText = "q / x1";
            this.Quanta.MinimumWidth = 100;
            this.Quanta.Name = "Quanta";
            this.Quanta.ReadOnly = true;
            this.Quanta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Quanta.Width = 120;
            // 
            // Qty
            // 
            this.Qty.AllowDecimals = false;
            this.Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = null;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle5;
            this.Qty.HeaderText = "Qty.";
            this.Qty.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.Qty.MinimumWidth = 70;
            this.Qty.Name = "Qty";
            this.Qty.Width = 70;
            // 
            // Sum
            // 
            this.Sum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.Sum.DefaultCellStyle = dataGridViewCellStyle6;
            this.Sum.HeaderText = "Sum";
            this.Sum.MinimumWidth = 120;
            this.Sum.Name = "Sum";
            this.Sum.ReadOnly = true;
            // 
            // closeBtn
            // 
            this.closeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeBtn.Location = new System.Drawing.Point(609, 678);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(80, 28);
            this.closeBtn.TabIndex = 12;
            this.closeBtn.Values.Text = "&Close";
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // applyBtn
            // 
            this.applyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.applyBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.applyBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applyBtn.Location = new System.Drawing.Point(317, 678);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(80, 28);
            this.applyBtn.TabIndex = 11;
            this.applyBtn.Values.Text = "&Apply";
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
            // 
            // exportKBtn
            // 
            this.exportKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exportKBtn.KryptonContextMenu = this.exportMenu;
            this.exportKBtn.Location = new System.Drawing.Point(10, 678);
            this.exportKBtn.Name = "exportKBtn";
            this.exportKBtn.Size = new System.Drawing.Size(132, 28);
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
            this.expOptOnlyWithQty});
            // 
            // kryptonContextMenuHeading1
            // 
            this.kryptonContextMenuHeading1.ExtraText = "";
            this.kryptonContextMenuHeading1.Text = "Export";
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
            // kCmdLoadList
            // 
            this.kCmdLoadList.ImageLarge = global::DU_Industry_Tool.Properties.Resources.fileopen;
            this.kCmdLoadList.ImageSmall = global::DU_Industry_Tool.Properties.Resources.fileopen;
            // 
            // kCmdSaveList
            // 
            this.kCmdSaveList.ImageLarge = global::DU_Industry_Tool.Properties.Resources.filesave;
            this.kCmdSaveList.ImageSmall = global::DU_Industry_Tool.Properties.Resources.filesave;
            // 
            // kCmdClearList
            // 
            this.kCmdClearList.ImageLarge = global::DU_Industry_Tool.Properties.Resources.dialog_cancel;
            this.kCmdClearList.ImageSmall = global::DU_Industry_Tool.Properties.Resources.dialog_cancel;
            // 
            // BtnClear
            // 
            this.BtnClear.KryptonCommand = this.kCmdClearList;
            this.BtnClear.Location = new System.Drawing.Point(572, 184);
            this.BtnClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(29, 29);
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
            // BtnLoad
            // 
            this.BtnLoad.KryptonCommand = this.kCmdLoadList;
            this.BtnLoad.Location = new System.Drawing.Point(507, 184);
            this.BtnLoad.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnLoad.Name = "BtnLoad";
            this.BtnLoad.Size = new System.Drawing.Size(29, 29);
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
            // BtnSave
            // 
            this.BtnSave.KryptonCommand = this.kCmdSaveList;
            this.BtnSave.Location = new System.Drawing.Point(540, 184);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(29, 29);
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
            // tbSkill4
            // 
            this.tbSkill4.AutoSize = true;
            this.tbSkill4.Location = new System.Drawing.Point(311, 140);
            this.tbSkill4.Margin = new System.Windows.Forms.Padding(0);
            this.tbSkill4.Name = "tbSkill4";
            this.tbSkill4.Padding = new System.Windows.Forms.Padding(2);
            this.tbSkill4.Size = new System.Drawing.Size(208, 37);
            this.tbSkill4.TabIndex = 3;
            this.tbSkill4.Tag = "3";
            this.tbSkill4.ValueSelected += new System.EventHandler<int>(this.buttonRow1_ValueSelected);
            // 
            // tbSkill3
            // 
            this.tbSkill3.AutoSize = true;
            this.tbSkill3.Location = new System.Drawing.Point(311, 99);
            this.tbSkill3.Margin = new System.Windows.Forms.Padding(0);
            this.tbSkill3.Name = "tbSkill3";
            this.tbSkill3.Padding = new System.Windows.Forms.Padding(2);
            this.tbSkill3.Size = new System.Drawing.Size(208, 37);
            this.tbSkill3.TabIndex = 2;
            this.tbSkill3.Tag = "2";
            this.tbSkill3.ValueSelected += new System.EventHandler<int>(this.buttonRow1_ValueSelected);
            // 
            // tbSkill2
            // 
            this.tbSkill2.AutoSize = true;
            this.tbSkill2.Location = new System.Drawing.Point(311, 55);
            this.tbSkill2.Margin = new System.Windows.Forms.Padding(0);
            this.tbSkill2.Name = "tbSkill2";
            this.tbSkill2.Padding = new System.Windows.Forms.Padding(2);
            this.tbSkill2.Size = new System.Drawing.Size(208, 37);
            this.tbSkill2.TabIndex = 1;
            this.tbSkill2.Tag = "1";
            this.tbSkill2.ValueSelected += new System.EventHandler<int>(this.buttonRow1_ValueSelected);
            // 
            // tbSkill1
            // 
            this.tbSkill1.AutoSize = true;
            this.tbSkill1.Location = new System.Drawing.Point(311, 13);
            this.tbSkill1.Margin = new System.Windows.Forms.Padding(0);
            this.tbSkill1.Name = "tbSkill1";
            this.tbSkill1.Padding = new System.Windows.Forms.Padding(2);
            this.tbSkill1.Size = new System.Drawing.Size(208, 37);
            this.tbSkill1.TabIndex = 0;
            this.tbSkill1.Tag = "0";
            this.tbSkill1.ValueSelected += new System.EventHandler<int>(this.buttonRow1_ValueSelected);
            // 
            // totalSumLabel
            // 
            this.totalSumLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalSumLabel.Location = new System.Drawing.Point(611, 654);
            this.totalSumLabel.Name = "totalSumLabel";
            this.totalSumLabel.Size = new System.Drawing.Size(79, 22);
            this.totalSumLabel.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalSumLabel.TabIndex = 27;
            this.totalSumLabel.TabStop = false;
            this.totalSumLabel.Text = "Total: 0.00";
            this.totalSumLabel.Values.Text = "Total: 0.00";
            // 
            // kLabel5
            // 
            this.kLabel5.Location = new System.Drawing.Point(8, 190);
            this.kLabel5.Name = "kLabel5";
            this.kLabel5.Size = new System.Drawing.Size(229, 22);
            this.kLabel5.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kLabel5.TabIndex = 9;
            this.kLabel5.TabStop = false;
            this.kLabel5.Text = "Schematic Details and Calculation";
            this.kLabel5.Values.Text = "Schematic Details and Calculation";
            // 
            // craftingLbl
            // 
            this.craftingLbl.Location = new System.Drawing.Point(7, 3);
            this.craftingLbl.Name = "craftingLbl";
            this.craftingLbl.Size = new System.Drawing.Size(190, 22);
            this.craftingLbl.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.craftingLbl.TabIndex = 0;
            this.craftingLbl.TabStop = false;
            this.craftingLbl.Text = "Schematics Crafting Talents";
            this.craftingLbl.Values.Text = "Schematics Crafting Talents";
            // 
            // skill4
            // 
            this.skill4.AutoSize = false;
            this.skill4.LabelStyle = Krypton.Toolkit.LabelStyle.Custom1;
            this.skill4.Location = new System.Drawing.Point(290, 149);
            this.skill4.Name = "skill4";
            this.skill4.Size = new System.Drawing.Size(18, 19);
            this.skill4.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 11.89565F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.skill4.TabIndex = 24;
            this.skill4.TabStop = false;
            this.skill4.Text = "0";
            this.skill4.Values.Text = "0";
            // 
            // skill3
            // 
            this.skill3.AutoSize = false;
            this.skill3.LabelStyle = Krypton.Toolkit.LabelStyle.Custom1;
            this.skill3.Location = new System.Drawing.Point(290, 107);
            this.skill3.Name = "skill3";
            this.skill3.Size = new System.Drawing.Size(18, 19);
            this.skill3.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 11.89565F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.skill3.TabIndex = 23;
            this.skill3.TabStop = false;
            this.skill3.Text = "0";
            this.skill3.Values.Text = "0";
            // 
            // skill2
            // 
            this.skill2.AutoSize = false;
            this.skill2.LabelStyle = Krypton.Toolkit.LabelStyle.Custom1;
            this.skill2.Location = new System.Drawing.Point(290, 63);
            this.skill2.Name = "skill2";
            this.skill2.Size = new System.Drawing.Size(18, 19);
            this.skill2.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 11.89565F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.skill2.TabIndex = 21;
            this.skill2.TabStop = false;
            this.skill2.Text = "0";
            this.skill2.Values.Text = "0";
            // 
            // skill1
            // 
            this.skill1.AutoSize = false;
            this.skill1.LabelStyle = Krypton.Toolkit.LabelStyle.Custom1;
            this.skill1.Location = new System.Drawing.Point(290, 20);
            this.skill1.Name = "skill1";
            this.skill1.Size = new System.Drawing.Size(18, 19);
            this.skill1.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 11.89565F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.skill1.TabIndex = 20;
            this.skill1.TabStop = false;
            this.skill1.Text = "0";
            this.skill1.UseMnemonic = false;
            this.skill1.Values.Text = "0";
            // 
            // kLabel4
            // 
            this.kLabel4.Location = new System.Drawing.Point(7, 149);
            this.kLabel4.Name = "kLabel4";
            this.kLabel4.Size = new System.Drawing.Size(204, 20);
            this.kLabel4.TabIndex = 7;
            this.kLabel4.TabStop = false;
            this.kLabel4.Text = "Adv. Schematic Output Prod. (+2%)";
            this.kLabel4.Values.Text = "Adv. Schematic Output Prod. (+2%)";
            // 
            // kLabel3
            // 
            this.kLabel3.Location = new System.Drawing.Point(7, 109);
            this.kLabel3.Name = "kLabel3";
            this.kLabel3.Size = new System.Drawing.Size(213, 20);
            this.kLabel3.TabIndex = 5;
            this.kLabel3.Text = "Schematic Output Productivity (+3%)";
            this.kLabel3.Values.Text = "Schematic Output Productivity (+3%)";
            // 
            // kLabel2
            // 
            this.kLabel2.Location = new System.Drawing.Point(8, 68);
            this.kLabel2.Name = "kLabel2";
            this.kLabel2.Size = new System.Drawing.Size(205, 20);
            this.kLabel2.TabIndex = 3;
            this.kLabel2.TabStop = false;
            this.kLabel2.Text = "Adv. Schematic Cost Optim. (-3% q)";
            this.kLabel2.Values.Text = "Adv. Schematic Cost Optim. (-3% q)";
            // 
            // kLabel1
            // 
            this.kLabel1.Location = new System.Drawing.Point(7, 30);
            this.kLabel1.Name = "kLabel1";
            this.kLabel1.Size = new System.Drawing.Size(211, 20);
            this.kLabel1.TabIndex = 1;
            this.kLabel1.TabStop = false;
            this.kLabel1.Text = "Schematic Cost Optimization (-5% q)";
            this.kLabel1.Values.Text = "Schematic Cost Optimization (-5% q)";
            // 
            // SchematicValueForm
            // 
            this.AcceptButton = this.applyBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(705, 714);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.BtnClear);
            this.Controls.Add(this.BtnLoad);
            this.Controls.Add(this.tbSkill4);
            this.Controls.Add(this.tbSkill3);
            this.Controls.Add(this.tbSkill2);
            this.Controls.Add(this.tbSkill1);
            this.Controls.Add(this.totalSumLabel);
            this.Controls.Add(this.exportKBtn);
            this.Controls.Add(this.kLabel5);
            this.Controls.Add(this.craftingLbl);
            this.Controls.Add(this.skill4);
            this.Controls.Add(this.skill3);
            this.Controls.Add(this.skill2);
            this.Controls.Add(this.skill1);
            this.Controls.Add(this.kLabel4);
            this.Controls.Add(this.kLabel3);
            this.Controls.Add(this.kLabel2);
            this.Controls.Add(this.kLabel1);
            this.Controls.Add(this.applyBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.schematicsGrid);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(721, 607);
            this.Name = "SchematicValueForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Schematic crafting and details";
            ((System.ComponentModel.ISupportInitialize)(this.schematicsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView schematicsGrid;
        private Krypton.Toolkit.KryptonButton closeBtn;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn KEY;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchematicName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quanta;
        private Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sum;
        private Krypton.Toolkit.KryptonCommand kCmdLoadList;
        private Krypton.Toolkit.KryptonCommand kCmdSaveList;
        private Krypton.Toolkit.KryptonCommand kCmdClearList;
        private Krypton.Toolkit.KryptonButton BtnClear;
        private Krypton.Toolkit.KryptonButton BtnLoad;
        private Krypton.Toolkit.KryptonButton BtnSave;
    }
}