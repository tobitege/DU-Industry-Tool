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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.oreGrid = new System.Windows.Forms.DataGridView();
            this.BtnSave = new System.Windows.Forms.Button();
            this.OreName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Test = new Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn();
            ((System.ComponentModel.ISupportInitialize)(this.oreGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // oreGrid
            // 
            this.oreGrid.AllowUserToAddRows = false;
            this.oreGrid.AllowUserToDeleteRows = false;
            this.oreGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oreGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.oreGrid.ColumnHeadersHeight = 32;
            this.oreGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.oreGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OreName,
            this.Test});
            this.oreGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.oreGrid.Location = new System.Drawing.Point(13, 13);
            this.oreGrid.Name = "oreGrid";
            this.oreGrid.RowHeadersWidth = 51;
            this.oreGrid.RowTemplate.Height = 24;
            this.oreGrid.Size = new System.Drawing.Size(538, 636);
            this.oreGrid.TabIndex = 0;
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnSave.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(232, 655);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(96, 31);
            this.BtnSave.TabIndex = 1;
            this.BtnSave.Text = "&Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.Button1_Click);
            // 
            // OreName
            // 
            this.OreName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OreName.DefaultCellStyle = dataGridViewCellStyle1;
            this.OreName.HeaderText = "Name";
            this.OreName.MaxInputLength = 30;
            this.OreName.MinimumWidth = 60;
            this.OreName.Name = "OreName";
            this.OreName.ReadOnly = true;
            this.OreName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Test
            // 
            this.Test.DecimalPlaces = 2;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.NullValue = "0";
            this.Test.DefaultCellStyle = dataGridViewCellStyle2;
            this.Test.HeaderText = "Quanta/L";
            this.Test.Maximum = new decimal(new int[] {
            999000000,
            0,
            0,
            0});
            this.Test.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Test.MinimumWidth = 60;
            this.Test.Name = "Test";
            this.Test.TrailingZeroes = true;
            this.Test.Width = 200;
            // 
            // OreValueForm
            // 
            this.AcceptButton = this.BtnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(115F, 115F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(565, 692);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.oreGrid);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "OreValueForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ore && Plasma Prices";
            ((System.ComponentModel.ISupportInitialize)(this.oreGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView oreGrid;
        private System.Windows.Forms.Button BtnSave;
        private DataGridViewTextBoxColumn OreName;
        private Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn Test;
    }
}