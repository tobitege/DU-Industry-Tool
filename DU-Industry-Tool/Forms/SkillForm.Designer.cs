namespace DU_Industry_Tool
{
    sealed partial class SkillForm
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
            this.BtnSave = new System.Windows.Forms.Button();
            this.kryptonPalette1 = new Krypton.Toolkit.KryptonPalette(this.components);
            this.FlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.LblHint = new Krypton.Toolkit.KryptonLabel();
            this.TimerLoad = new System.Windows.Forms.Timer(this.components);
            this.FlowPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnSave
            // 
            this.BtnSave.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BtnSave.Enabled = false;
            this.BtnSave.Location = new System.Drawing.Point(0, 608);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(6);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(482, 35);
            this.BtnSave.TabIndex = 0;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // FlowPanel
            // 
            this.FlowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FlowPanel.AutoScroll = true;
            this.FlowPanel.Controls.Add(this.LblHint);
            this.FlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FlowPanel.Location = new System.Drawing.Point(0, 2);
            this.FlowPanel.Name = "FlowPanel";
            this.FlowPanel.Size = new System.Drawing.Size(480, 600);
            this.FlowPanel.TabIndex = 1;
            this.FlowPanel.WrapContents = false;
            // 
            // LblHint
            // 
            this.LblHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblHint.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.LblHint.Location = new System.Drawing.Point(140, 3);
            this.LblHint.Margin = new System.Windows.Forms.Padding(140, 3, 3, 3);
            this.LblHint.MinimumSize = new System.Drawing.Size(0, 500);
            this.LblHint.Name = "LblHint";
            this.LblHint.Size = new System.Drawing.Size(201, 500);
            this.LblHint.TabIndex = 0;
            this.LblHint.Values.Text = "Loading talents...";
            // 
            // TimerLoad
            // 
            this.TimerLoad.Interval = 1000;
            this.TimerLoad.Tick += new System.EventHandler(this.TimerLoad_Tick);
            // 
            // SkillForm
            // 
            this.AcceptButton = this.BtnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(482, 643);
            this.Controls.Add(this.FlowPanel);
            this.Controls.Add(this.BtnSave);
            this.CornerRoundingRadius = 4F;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximumSize = new System.Drawing.Size(500, 1000);
            this.MinimumSize = new System.Drawing.Size(500, 690);
            this.Name = "SkillForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.StateCommon.Border.Rounding = 4F;
            this.Text = "Talents Input";
            this.FlowPanel.ResumeLayout(false);
            this.FlowPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnSave;
        private Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.FlowLayoutPanel FlowPanel;
        private Krypton.Toolkit.KryptonLabel LblHint;
        private System.Windows.Forms.Timer TimerLoad;
    }
}