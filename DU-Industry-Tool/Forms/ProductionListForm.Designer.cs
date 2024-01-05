using Krypton.Toolkit;

namespace DU_Industry_Tool
{
    partial class ProductionListForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductionListForm));
            this.MainPanel = new Krypton.Toolkit.KryptonPanel();
            this.BtnCalculate = new Krypton.Toolkit.KryptonButton();
            this.kCmdCalculate = new Krypton.Toolkit.KryptonCommand();
            this.BtnClose = new Krypton.Toolkit.KryptonButton();
            this.kCmdClose = new Krypton.Toolkit.KryptonCommand();
            this.dgvProductionList = new Krypton.Toolkit.KryptonDataGridView();
            this.Column1 = new Krypton.Toolkit.KryptonDataGridViewComboBoxColumn();
            this.productionListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Column2 = new Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn();
            this.PanelTop = new Krypton.Toolkit.KryptonPanel();
            this.BtnClear = new Krypton.Toolkit.KryptonButton();
            this.kCmdClearList = new Krypton.Toolkit.KryptonCommand();
            this.BtnSave = new Krypton.Toolkit.KryptonButton();
            this.kCmdSaveList = new Krypton.Toolkit.KryptonCommand();
            this.BtnLoad = new Krypton.Toolkit.KryptonButton();
            this.kCmdLoadList = new Krypton.Toolkit.KryptonCommand();
            this.lblQty = new Krypton.Toolkit.KryptonLabel();
            this.lblRecipe = new Krypton.Toolkit.KryptonLabel();
            this.NumUpDownQuantity = new Krypton.Toolkit.KryptonNumericUpDown();
            this.BtnAdd = new Krypton.Toolkit.KryptonButton();
            this.kCmdAddToList = new Krypton.Toolkit.KryptonCommand();
            this.ComboRecipeNames = new Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.LblLoaded = new Krypton.Toolkit.KryptonLabel();
            this.recipeNamesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MainPanel)).BeginInit();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductionList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productionListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelTop)).BeginInit();
            this.PanelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ComboRecipeNames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recipeNamesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.BtnCalculate);
            this.MainPanel.Controls.Add(this.BtnClose);
            this.MainPanel.Controls.Add(this.dgvProductionList);
            this.MainPanel.Controls.Add(this.PanelTop);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(532, 605);
            this.MainPanel.TabIndex = 0;
            // 
            // BtnCalculate
            // 
            this.BtnCalculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCalculate.KryptonCommand = this.kCmdCalculate;
            this.BtnCalculate.Location = new System.Drawing.Point(235, 544);
            this.BtnCalculate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.BtnCalculate.Name = "BtnCalculate";
            this.BtnCalculate.Size = new System.Drawing.Size(150, 42);
            this.BtnCalculate.TabIndex = 4;
            this.BtnCalculate.Values.Text = "Calculate";
            // 
            // kCmdCalculate
            // 
            this.kCmdCalculate.ImageLarge = global::DU_Industry_Tool.Properties.Resources.Add;
            this.kCmdCalculate.ImageSmall = global::DU_Industry_Tool.Properties.Resources.Add;
            this.kCmdCalculate.Text = "Calculate";
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnClose.KryptonCommand = this.kCmdClose;
            this.BtnClose.Location = new System.Drawing.Point(401, 544);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(105, 42);
            this.BtnClose.TabIndex = 3;
            this.BtnClose.Values.Text = "Add";
            // 
            // kCmdClose
            // 
            this.kCmdClose.ImageLarge = global::DU_Industry_Tool.Properties.Resources.Minus_Red_Button;
            this.kCmdClose.ImageSmall = global::DU_Industry_Tool.Properties.Resources.Minus_Red_Button;
            this.kCmdClose.Text = "Close";
            // 
            // dgvProductionList
            // 
            this.dgvProductionList.AllowUserToAddRows = false;
            this.dgvProductionList.AllowUserToResizeRows = false;
            this.dgvProductionList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProductionList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductionList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgvProductionList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgvProductionList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductionList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dgvProductionList.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Sheet;
            this.dgvProductionList.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet;
            this.dgvProductionList.GridStyles.StyleColumn = Krypton.Toolkit.GridStyle.Sheet;
            this.dgvProductionList.GridStyles.StyleDataCells = Krypton.Toolkit.GridStyle.Sheet;
            this.dgvProductionList.GridStyles.StyleRow = Krypton.Toolkit.GridStyle.Sheet;
            this.dgvProductionList.Location = new System.Drawing.Point(0, 189);
            this.dgvProductionList.Margin = new System.Windows.Forms.Padding(0);
            this.dgvProductionList.MultiSelect = false;
            this.dgvProductionList.Name = "dgvProductionList";
            this.dgvProductionList.RowHeadersWidth = 20;
            this.dgvProductionList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvProductionList.RowTemplate.Height = 24;
            this.dgvProductionList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvProductionList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvProductionList.Size = new System.Drawing.Size(532, 338);
            this.dgvProductionList.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Column1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.DataPropertyName = "Name";
            this.Column1.DataSource = this.productionListBindingSource;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column1.DisplayMember = "Name";
            this.Column1.DropDownWidth = 300;
            this.Column1.HeaderText = "Recipe Name";
            this.Column1.MaxDropDownItems = 15;
            this.Column1.MinimumWidth = 300;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.Width = 412;
            // 
            // productionListBindingSource
            // 
            this.productionListBindingSource.DataMember = "ProductionBindingList";
            this.productionListBindingSource.DataSource = typeof(DU_Industry_Tool.DUDataBindings);
            // 
            // Column2
            // 
            this.Column2.AllowDecimals = false;
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2.DataPropertyName = "Quantity";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column2.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column2.FillWeight = 50F;
            this.Column2.HeaderText = "Quantity";
            this.Column2.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.Column2.MinimumWidth = 100;
            this.Column2.Name = "Column2";
            this.Column2.Width = 100;
            // 
            // PanelTop
            // 
            this.PanelTop.Controls.Add(this.BtnClear);
            this.PanelTop.Controls.Add(this.BtnSave);
            this.PanelTop.Controls.Add(this.BtnLoad);
            this.PanelTop.Controls.Add(this.lblQty);
            this.PanelTop.Controls.Add(this.lblRecipe);
            this.PanelTop.Controls.Add(this.NumUpDownQuantity);
            this.PanelTop.Controls.Add(this.BtnAdd);
            this.PanelTop.Controls.Add(this.ComboRecipeNames);
            this.PanelTop.Controls.Add(this.kryptonLabel1);
            this.PanelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTop.Location = new System.Drawing.Point(0, 0);
            this.PanelTop.Margin = new System.Windows.Forms.Padding(0);
            this.PanelTop.Name = "PanelTop";
            this.PanelTop.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelCustom1;
            this.PanelTop.Size = new System.Drawing.Size(532, 189);
            this.PanelTop.TabIndex = 0;
            // 
            // BtnClear
            // 
            this.BtnClear.KryptonCommand = this.kCmdClearList;
            this.BtnClear.Location = new System.Drawing.Point(90, 149);
            this.BtnClear.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(32, 32);
            this.BtnClear.StateNormal.Border.Color1 = System.Drawing.Color.Black;
            this.BtnClear.StateNormal.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.BtnClear.StateNormal.Border.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.BtnClear.StateNormal.Border.Rounding = 2F;
            this.BtnClear.TabIndex = 8;
            this.BtnClear.ToolTipValues.Description = "Clear the current production list.";
            this.BtnClear.ToolTipValues.EnableToolTips = true;
            this.BtnClear.ToolTipValues.Heading = "Clear";
            this.BtnClear.Values.Text = "Clear List";
            // 
            // kCmdClearList
            // 
            this.kCmdClearList.ImageLarge = global::DU_Industry_Tool.Properties.Resources.dialog_cancel;
            this.kCmdClearList.ImageSmall = global::DU_Industry_Tool.Properties.Resources.dialog_cancel;
            // 
            // BtnSave
            // 
            this.BtnSave.KryptonCommand = this.kCmdSaveList;
            this.BtnSave.Location = new System.Drawing.Point(52, 149);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(32, 32);
            this.BtnSave.StateNormal.Border.Color1 = System.Drawing.Color.Black;
            this.BtnSave.StateNormal.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.BtnSave.StateNormal.Border.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.BtnSave.StateNormal.Border.Rounding = 2F;
            this.BtnSave.TabIndex = 7;
            this.BtnSave.ToolTipValues.Description = "Save the current production list to a file. That file could be shared with other " +
    "users.";
            this.BtnSave.ToolTipValues.EnableToolTips = true;
            this.BtnSave.ToolTipValues.Heading = "Save";
            this.BtnSave.Values.Text = "Save List";
            // 
            // kCmdSaveList
            // 
            this.kCmdSaveList.ImageLarge = global::DU_Industry_Tool.Properties.Resources.filesave;
            this.kCmdSaveList.ImageSmall = global::DU_Industry_Tool.Properties.Resources.filesave;
            // 
            // BtnLoad
            // 
            this.BtnLoad.KryptonCommand = this.kCmdLoadList;
            this.BtnLoad.Location = new System.Drawing.Point(12, 149);
            this.BtnLoad.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.BtnLoad.Name = "BtnLoad";
            this.BtnLoad.Size = new System.Drawing.Size(32, 32);
            this.BtnLoad.StateNormal.Border.Color1 = System.Drawing.Color.Black;
            this.BtnLoad.StateNormal.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.BtnLoad.StateNormal.Border.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.BtnLoad.StateNormal.Border.Rounding = 2F;
            this.BtnLoad.TabIndex = 6;
            this.BtnLoad.ToolTipValues.Description = "Load a production list from a file";
            this.BtnLoad.ToolTipValues.EnableToolTips = true;
            this.BtnLoad.ToolTipValues.Heading = "Load";
            this.BtnLoad.Values.Text = "Load List";
            // 
            // kCmdLoadList
            // 
            this.kCmdLoadList.ImageLarge = global::DU_Industry_Tool.Properties.Resources.fileopen;
            this.kCmdLoadList.ImageSmall = global::DU_Industry_Tool.Properties.Resources.fileopen;
            // 
            // lblQty
            // 
            this.lblQty.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblQty.Location = new System.Drawing.Point(9, 102);
            this.lblQty.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(89, 27);
            this.lblQty.TabIndex = 5;
            this.lblQty.Values.Text = "Quantity:";
            // 
            // lblRecipe
            // 
            this.lblRecipe.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblRecipe.Location = new System.Drawing.Point(12, 61);
            this.lblRecipe.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblRecipe.Name = "lblRecipe";
            this.lblRecipe.Size = new System.Drawing.Size(72, 27);
            this.lblRecipe.TabIndex = 4;
            this.lblRecipe.Values.Text = "Recipe:";
            // 
            // NumUpDownQuantity
            // 
            this.NumUpDownQuantity.Location = new System.Drawing.Point(100, 100);
            this.NumUpDownQuantity.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.NumUpDownQuantity.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.NumUpDownQuantity.Name = "NumUpDownQuantity";
            this.NumUpDownQuantity.Size = new System.Drawing.Size(87, 31);
            this.NumUpDownQuantity.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.NumUpDownQuantity.StateCommon.Border.Rounding = 2F;
            this.NumUpDownQuantity.TabIndex = 1;
            this.NumUpDownQuantity.UpDownButtonStyle = Krypton.Toolkit.ButtonStyle.Standalone;
            this.NumUpDownQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // BtnAdd
            // 
            this.BtnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAdd.KryptonCommand = this.kCmdAddToList;
            this.BtnAdd.Location = new System.Drawing.Point(331, 95);
            this.BtnAdd.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(168, 40);
            this.BtnAdd.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.BtnAdd.StateCommon.Border.Rounding = 2F;
            this.BtnAdd.TabIndex = 2;
            this.BtnAdd.Values.Text = "";
            // 
            // kCmdAddToList
            // 
            this.kCmdAddToList.ImageLarge = global::DU_Industry_Tool.Properties.Resources.Add;
            this.kCmdAddToList.ImageSmall = global::DU_Industry_Tool.Properties.Resources.Add;
            this.kCmdAddToList.Text = "Add to List";
            // 
            // ComboRecipeNames
            // 
            this.ComboRecipeNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboRecipeNames.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ComboRecipeNames.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboRecipeNames.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ComboRecipeNames.DropDownHeight = 300;
            this.ComboRecipeNames.DropDownWidth = 350;
            this.ComboRecipeNames.IntegralHeight = false;
            this.ComboRecipeNames.Location = new System.Drawing.Point(100, 61);
            this.ComboRecipeNames.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ComboRecipeNames.MaxDropDownItems = 15;
            this.ComboRecipeNames.MaxLength = 40;
            this.ComboRecipeNames.Name = "ComboRecipeNames";
            this.ComboRecipeNames.Size = new System.Drawing.Size(400, 27);
            this.ComboRecipeNames.Sorted = true;
            this.ComboRecipeNames.StateCommon.ComboBox.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.ComboRecipeNames.StateCommon.ComboBox.Border.Rounding = 2F;
            this.ComboRecipeNames.StateCommon.ComboBox.Content.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboRecipeNames.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.ComboRecipeNames.TabIndex = 0;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.kryptonLabel1.Location = new System.Drawing.Point(10, 18);
            this.kryptonLabel1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(435, 23);
            this.kryptonLabel1.StateCommon.LongText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel1.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Pick a recipe, the quantity and click Add button to add to the list.";
            // 
            // LblLoaded
            // 
            this.LblLoaded.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.LblLoaded.Location = new System.Drawing.Point(9, 560);
            this.LblLoaded.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.LblLoaded.Name = "LblLoaded";
            this.LblLoaded.Size = new System.Drawing.Size(73, 27);
            this.LblLoaded.TabIndex = 6;
            this.LblLoaded.Values.Text = "Loaded:";
            this.LblLoaded.Visible = false;
            // 
            // recipeNamesBindingSource
            // 
            this.recipeNamesBindingSource.DataMember = "RecipeNamesBindingList";
            this.recipeNamesBindingSource.DataSource = typeof(DU_Industry_Tool.DUDataBindings);
            // 
            // ProductionListForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(532, 605);
            this.Controls.Add(this.LblLoaded);
            this.Controls.Add(this.MainPanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MinimumSize = new System.Drawing.Size(550, 650);
            this.Name = "ProductionListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.MainPanel)).EndInit();
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductionList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productionListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelTop)).EndInit();
            this.PanelTop.ResumeLayout(false);
            this.PanelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ComboRecipeNames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recipeNamesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel MainPanel;
        private Krypton.Toolkit.KryptonPanel PanelTop;
        private Krypton.Toolkit.KryptonButton BtnClear;
        private Krypton.Toolkit.KryptonCommand kCmdClearList;
        private Krypton.Toolkit.KryptonButton BtnSave;
        private Krypton.Toolkit.KryptonCommand kCmdSaveList;
        private Krypton.Toolkit.KryptonButton BtnLoad;
        private Krypton.Toolkit.KryptonCommand kCmdLoadList;
        private Krypton.Toolkit.KryptonLabel lblQty;
        private Krypton.Toolkit.KryptonLabel lblRecipe;
        private Krypton.Toolkit.KryptonNumericUpDown NumUpDownQuantity;
        private Krypton.Toolkit.KryptonButton BtnAdd;
        private Krypton.Toolkit.KryptonCommand kCmdAddToList;
        private Krypton.Toolkit.KryptonComboBox ComboRecipeNames;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonButton BtnCalculate;
        private Krypton.Toolkit.KryptonCommand kCmdCalculate;
        private Krypton.Toolkit.KryptonButton BtnClose;
        private Krypton.Toolkit.KryptonCommand kCmdClose;
        private System.Windows.Forms.BindingSource productionListBindingSource;
        private System.Windows.Forms.BindingSource recipeNamesBindingSource;
        private KryptonDataGridView dgvProductionList;
        private KryptonDataGridViewComboBoxColumn Column1;
        private KryptonDataGridViewNumericUpDownColumn Column2;
        private KryptonLabel LblLoaded;
    }
}