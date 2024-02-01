using System.Windows.Forms;

namespace DU_Industry_Tool
{
    partial class OreValueForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OreValueForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.layoutPanel = new Krypton.Toolkit.KryptonTableLayoutPanel();
            this.oreGrid = new Krypton.Toolkit.KryptonDataGridView();
            this.OreName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quanta = new Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn();
            this.panelBottom = new Krypton.Toolkit.KryptonPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.layoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.oreGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutPanel
            // 
            this.layoutPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("layoutPanel.BackgroundImage")));
            this.layoutPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.layoutPanel.ColumnCount = 1;
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.Controls.Add(this.oreGrid, 0, 0);
            this.layoutPanel.Controls.Add(this.panelBottom, 0, 1);
            this.layoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanel.Location = new System.Drawing.Point(0, 0);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlGroupBox;
            this.layoutPanel.RowCount = 2;
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.layoutPanel.Size = new System.Drawing.Size(585, 746);
            this.layoutPanel.TabIndex = 2;
            // 
            // oreGrid
            // 
            this.oreGrid.AllowUserToAddRows = false;
            this.oreGrid.AllowUserToDeleteRows = false;
            this.oreGrid.AllowUserToResizeColumns = false;
            this.oreGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oreGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.oreGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.oreGrid.ColumnHeadersHeight = 32;
            this.oreGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.oreGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OreName,
            this.Quanta});
            this.oreGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.oreGrid.Location = new System.Drawing.Point(3, 3);
            this.oreGrid.Name = "oreGrid";
            this.oreGrid.RowHeadersWidth = 30;
            this.oreGrid.RowTemplate.Height = 24;
            this.oreGrid.ShowEditingIcon = false;
            this.oreGrid.Size = new System.Drawing.Size(579, 680);
            this.oreGrid.TabIndex = 1;
            // 
            // OreName
            // 
            this.OreName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OreName.DefaultCellStyle = dataGridViewCellStyle1;
            this.OreName.HeaderText = "Ore";
            this.OreName.MaxInputLength = 30;
            this.OreName.MinimumWidth = 60;
            this.OreName.Name = "OreName";
            this.OreName.ReadOnly = true;
            this.OreName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Quanta
            // 
            this.Quanta.AllowDecimals = false;
            this.Quanta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Quanta.DecimalPlaces = 2;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.NullValue = "0";
            this.Quanta.DefaultCellStyle = dataGridViewCellStyle2;
            this.Quanta.HeaderText = "Quanta/L";
            this.Quanta.Maximum = new decimal(new int[] {
            999000000,
            0,
            0,
            0});
            this.Quanta.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Quanta.MinimumWidth = 200;
            this.Quanta.Name = "Quanta";
            this.Quanta.TrailingZeroes = true;
            this.Quanta.Width = 200;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnClose);
            this.panelBottom.Controls.Add(this.btnSave);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottom.Location = new System.Drawing.Point(3, 689);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(579, 54);
            this.panelBottom.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(339, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(125, 36);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSave.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(150, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(125, 36);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // OreValueForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(585, 746);
            this.Controls.Add(this.layoutPanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(590, 700);
            this.Name = "OreValueForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ore && Plasma Prices";
            this.layoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.oreGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonTableLayoutPanel layoutPanel;
        private Krypton.Toolkit.KryptonDataGridView oreGrid;
        private Krypton.Toolkit.KryptonPanel panelBottom;
        private Button btnClose;
        private Button btnSave;
        private DataGridViewTextBoxColumn OreName;
        private Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn Quanta;
    }
}