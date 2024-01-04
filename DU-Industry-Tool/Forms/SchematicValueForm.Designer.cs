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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.schematicsGrid = new System.Windows.Forms.DataGridView();
            this.closeBtn = new System.Windows.Forms.Button();
            this.applyBtn = new System.Windows.Forms.Button();
            this.tbSkill1 = new Krypton.Toolkit.KryptonTrackBar();
            this.tbSkill2 = new Krypton.Toolkit.KryptonTrackBar();
            this.tbSkill3 = new Krypton.Toolkit.KryptonTrackBar();
            this.tbSkill4 = new Krypton.Toolkit.KryptonTrackBar();
            this.SchematicName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quanta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.exportBtn = new System.Windows.Forms.Button();
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
            this.SchematicName,
            this.BatchCost,
            this.Quanta,
            this.BatchSize});
            this.schematicsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.schematicsGrid.GridColor = System.Drawing.SystemColors.Window;
            this.schematicsGrid.Location = new System.Drawing.Point(8, 261);
            this.schematicsGrid.MultiSelect = false;
            this.schematicsGrid.Name = "schematicsGrid";
            this.schematicsGrid.RowHeadersVisible = false;
            this.schematicsGrid.RowHeadersWidth = 40;
            this.schematicsGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.schematicsGrid.RowTemplate.Height = 24;
            this.schematicsGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.schematicsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.schematicsGrid.ShowEditingIcon = false;
            this.schematicsGrid.Size = new System.Drawing.Size(630, 371);
            this.schematicsGrid.TabIndex = 10;
            // 
            // closeBtn
            // 
            this.closeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.closeBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeBtn.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeBtn.Location = new System.Drawing.Point(542, 638);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(96, 31);
            this.closeBtn.TabIndex = 12;
            this.closeBtn.Text = "&Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // applyBtn
            // 
            this.applyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.applyBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.applyBtn.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applyBtn.Location = new System.Drawing.Point(273, 638);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(96, 31);
            this.applyBtn.TabIndex = 11;
            this.applyBtn.Text = "&Apply";
            this.applyBtn.UseVisualStyleBackColor = true;
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
            // 
            // tbSkill1
            // 
            this.tbSkill1.LargeChange = 1;
            this.tbSkill1.Location = new System.Drawing.Point(374, 32);
            this.tbSkill1.Maximum = 5;
            this.tbSkill1.Name = "tbSkill1";
            this.tbSkill1.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.tbSkill1.Size = new System.Drawing.Size(179, 34);
            this.tbSkill1.TabIndex = 2;
            this.tbSkill1.Tag = "0";
            this.tbSkill1.TrackBarSize = Krypton.Toolkit.PaletteTrackBarSize.Large;
            this.tbSkill1.ValueChanged += new System.EventHandler(this.tbSkill_ValueChanged);
            // 
            // tbSkill2
            // 
            this.tbSkill2.LargeChange = 1;
            this.tbSkill2.Location = new System.Drawing.Point(374, 81);
            this.tbSkill2.Maximum = 5;
            this.tbSkill2.Name = "tbSkill2";
            this.tbSkill2.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.tbSkill2.Size = new System.Drawing.Size(179, 34);
            this.tbSkill2.TabIndex = 4;
            this.tbSkill2.Tag = "1";
            this.tbSkill2.TrackBarSize = Krypton.Toolkit.PaletteTrackBarSize.Large;
            this.tbSkill2.ValueChanged += new System.EventHandler(this.tbSkill_ValueChanged);
            // 
            // tbSkill3
            // 
            this.tbSkill3.LargeChange = 1;
            this.tbSkill3.Location = new System.Drawing.Point(374, 131);
            this.tbSkill3.Maximum = 5;
            this.tbSkill3.Name = "tbSkill3";
            this.tbSkill3.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.tbSkill3.Size = new System.Drawing.Size(179, 34);
            this.tbSkill3.TabIndex = 6;
            this.tbSkill3.Tag = "2";
            this.tbSkill3.TrackBarSize = Krypton.Toolkit.PaletteTrackBarSize.Large;
            this.tbSkill3.ValueChanged += new System.EventHandler(this.tbSkill_ValueChanged);
            // 
            // tbSkill4
            // 
            this.tbSkill4.LargeChange = 1;
            this.tbSkill4.Location = new System.Drawing.Point(374, 183);
            this.tbSkill4.Maximum = 5;
            this.tbSkill4.Name = "tbSkill4";
            this.tbSkill4.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.tbSkill4.Size = new System.Drawing.Size(179, 34);
            this.tbSkill4.TabIndex = 8;
            this.tbSkill4.Tag = "3";
            this.tbSkill4.TrackBarSize = Krypton.Toolkit.PaletteTrackBarSize.Large;
            this.tbSkill4.ValueChanged += new System.EventHandler(this.tbSkill_ValueChanged);
            // 
            // SchematicName
            // 
            this.SchematicName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SchematicName.DefaultCellStyle = dataGridViewCellStyle5;
            this.SchematicName.HeaderText = "Schematic Name";
            this.SchematicName.MinimumWidth = 150;
            this.SchematicName.Name = "SchematicName";
            this.SchematicName.ReadOnly = true;
            // 
            // BatchCost
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BatchCost.DefaultCellStyle = dataGridViewCellStyle6;
            this.BatchCost.HeaderText = "q / Batch";
            this.BatchCost.MinimumWidth = 125;
            this.BatchCost.Name = "BatchCost";
            this.BatchCost.ReadOnly = true;
            this.BatchCost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BatchCost.Width = 125;
            // 
            // Quanta
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Quanta.DefaultCellStyle = dataGridViewCellStyle7;
            this.Quanta.HeaderText = "q / x1";
            this.Quanta.MinimumWidth = 125;
            this.Quanta.Name = "Quanta";
            this.Quanta.ReadOnly = true;
            this.Quanta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Quanta.Width = 125;
            // 
            // BatchSize
            // 
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BatchSize.DefaultCellStyle = dataGridViewCellStyle8;
            this.BatchSize.HeaderText = "Batch-Size";
            this.BatchSize.MinimumWidth = 125;
            this.BatchSize.Name = "BatchSize";
            this.BatchSize.ReadOnly = true;
            this.BatchSize.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BatchSize.Width = 125;
            // 
            // kLabel5
            // 
            this.kLabel5.Location = new System.Drawing.Point(8, 225);
            this.kLabel5.Name = "kLabel5";
            this.kLabel5.Size = new System.Drawing.Size(141, 26);
            this.kLabel5.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kLabel5.TabIndex = 9;
            this.kLabel5.TabStop = false;
            this.kLabel5.Text = "Schematic Prices";
            this.kLabel5.Values.Text = "Schematic Prices";
            // 
            // craftingLbl
            // 
            this.craftingLbl.Location = new System.Drawing.Point(8, 3);
            this.craftingLbl.Name = "craftingLbl";
            this.craftingLbl.Size = new System.Drawing.Size(226, 26);
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
            this.skill4.Location = new System.Drawing.Point(347, 187);
            this.skill4.Name = "skill4";
            this.skill4.Size = new System.Drawing.Size(22, 23);
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
            this.skill3.Location = new System.Drawing.Point(347, 135);
            this.skill3.Name = "skill3";
            this.skill3.Size = new System.Drawing.Size(22, 23);
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
            this.skill2.Location = new System.Drawing.Point(347, 85);
            this.skill2.Name = "skill2";
            this.skill2.Size = new System.Drawing.Size(22, 23);
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
            this.skill1.Location = new System.Drawing.Point(347, 36);
            this.skill1.Name = "skill1";
            this.skill1.Size = new System.Drawing.Size(22, 23);
            this.skill1.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 11.89565F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.skill1.TabIndex = 20;
            this.skill1.TabStop = false;
            this.skill1.Text = "0";
            this.skill1.UseMnemonic = false;
            this.skill1.Values.Text = "0";
            // 
            // kLabel4
            // 
            this.kLabel4.Location = new System.Drawing.Point(8, 183);
            this.kLabel4.Name = "kLabel4";
            this.kLabel4.Size = new System.Drawing.Size(286, 27);
            this.kLabel4.TabIndex = 7;
            this.kLabel4.TabStop = false;
            this.kLabel4.Text = "Adv. Schematic Output Prod. (+2%)";
            this.kLabel4.Values.Text = "Adv. Schematic Output Prod. (+2%)";
            // 
            // kLabel3
            // 
            this.kLabel3.Location = new System.Drawing.Point(8, 135);
            this.kLabel3.Name = "kLabel3";
            this.kLabel3.Size = new System.Drawing.Size(298, 27);
            this.kLabel3.TabIndex = 5;
            this.kLabel3.Text = "Schematic Output Productivity (+3%)";
            this.kLabel3.Values.Text = "Schematic Output Productivity (+3%)";
            // 
            // kLabel2
            // 
            this.kLabel2.Location = new System.Drawing.Point(8, 85);
            this.kLabel2.Name = "kLabel2";
            this.kLabel2.Size = new System.Drawing.Size(287, 27);
            this.kLabel2.TabIndex = 3;
            this.kLabel2.TabStop = false;
            this.kLabel2.Text = "Adv. Schematic Cost Optim. (-3% q)";
            this.kLabel2.Values.Text = "Adv. Schematic Cost Optim. (-3% q)";
            // 
            // kLabel1
            // 
            this.kLabel1.Location = new System.Drawing.Point(8, 36);
            this.kLabel1.Name = "kLabel1";
            this.kLabel1.Size = new System.Drawing.Size(296, 27);
            this.kLabel1.TabIndex = 1;
            this.kLabel1.TabStop = false;
            this.kLabel1.Text = "Schematic Cost Optimization (-5% q)";
            this.kLabel1.Values.Text = "Schematic Cost Optimization (-5% q)";
            // 
            // exportBtn
            // 
            this.exportBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exportBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.exportBtn.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportBtn.Location = new System.Drawing.Point(8, 638);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(189, 31);
            this.exportBtn.TabIndex = 25;
            this.exportBtn.Text = "&Export to Excel";
            this.exportBtn.UseVisualStyleBackColor = true;
            this.exportBtn.Click += new System.EventHandler(this.exportBtn_Click);
            // 
            // SchematicValueForm
            // 
            this.AcceptButton = this.applyBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(115F, 115F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.closeBtn;
            this.ClientSize = new System.Drawing.Size(650, 675);
            this.Controls.Add(this.exportBtn);
            this.Controls.Add(this.kLabel5);
            this.Controls.Add(this.craftingLbl);
            this.Controls.Add(this.skill4);
            this.Controls.Add(this.skill3);
            this.Controls.Add(this.skill2);
            this.Controls.Add(this.skill1);
            this.Controls.Add(this.tbSkill4);
            this.Controls.Add(this.tbSkill3);
            this.Controls.Add(this.tbSkill2);
            this.Controls.Add(this.kLabel4);
            this.Controls.Add(this.kLabel3);
            this.Controls.Add(this.kLabel2);
            this.Controls.Add(this.kLabel1);
            this.Controls.Add(this.tbSkill1);
            this.Controls.Add(this.applyBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.schematicsGrid);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(668, 720);
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
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button applyBtn;
        private Krypton.Toolkit.KryptonTrackBar tbSkill1;
        private KLabel kLabel1;
        private KLabel kLabel2;
        private KLabel kLabel3;
        private KLabel kLabel4;
        private Krypton.Toolkit.KryptonTrackBar tbSkill2;
        private Krypton.Toolkit.KryptonTrackBar tbSkill3;
        private Krypton.Toolkit.KryptonTrackBar tbSkill4;
        private KLabel skill1;
        private KLabel skill2;
        private KLabel skill3;
        private KLabel skill4;
        private KLabel craftingLbl;
        private KLabel kLabel5;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchematicName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quanta;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchSize;
        private System.Windows.Forms.Button exportBtn;
    }
}