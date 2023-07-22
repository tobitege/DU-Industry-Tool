
namespace DU_Industry_Tool
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.panelLeft = new Krypton.Toolkit.KryptonPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TobiReleasesLink = new Krypton.Toolkit.KryptonLinkLabel();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.discordLink = new Krypton.Toolkit.KryptonLinkLabel();
            this.OkButton = new Krypton.Toolkit.KryptonButton();
            this.labelMain = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new Krypton.Toolkit.KryptonLabel();
            this.DimenciaGithubLink = new Krypton.Toolkit.KryptonLinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.panelLeft)).BeginInit();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.pictureBox1);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.FormMain;
            this.panelLeft.Size = new System.Drawing.Size(90, 441);
            this.panelLeft.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(13);
            this.pictureBox1.Size = new System.Drawing.Size(90, 441);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // TobiReleasesLink
            // 
            this.TobiReleasesLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TobiReleasesLink.Location = new System.Drawing.Point(99, 340);
            this.TobiReleasesLink.Name = "TobiReleasesLink";
            this.TobiReleasesLink.Size = new System.Drawing.Size(439, 27);
            this.TobiReleasesLink.TabIndex = 2;
            this.TobiReleasesLink.Values.Text = "https://github.com/tobitege/DU-Industry-Tool/releases";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kryptonLabel1.Location = new System.Drawing.Point(99, 320);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(322, 27);
            this.kryptonLabel1.TabIndex = 3;
            this.kryptonLabel1.Values.Text = "Tobi\'s main sources+binary releases (Github):";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kryptonLabel2.Location = new System.Drawing.Point(99, 196);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(346, 27);
            this.kryptonLabel2.TabIndex = 4;
            this.kryptonLabel2.Values.Text = "Visit the DU Open Source Initiative Discord:";
            // 
            // discordLink
            // 
            this.discordLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.discordLink.Location = new System.Drawing.Point(99, 222);
            this.discordLink.Name = "discordLink";
            this.discordLink.Size = new System.Drawing.Size(239, 27);
            this.discordLink.TabIndex = 5;
            this.discordLink.Values.Text = "https://discord.gg/7psYcmAb";
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OkButton.AutoSize = true;
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(341, 397);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(95, 32);
            this.OkButton.TabIndex = 0;
            this.OkButton.Values.Text = "Ok";
            // 
            // labelMain
            // 
            this.labelMain.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.labelMain.Location = new System.Drawing.Point(96, 12);
            this.labelMain.Name = "labelMain";
            this.labelMain.Size = new System.Drawing.Size(586, 166);
            this.labelMain.TabIndex = 7;
            this.labelMain.Values.Text = "DU Industry Tool vXXX\r\nA tool to find cost for elements in Dual Universe.\r\n\r\nCrea" +
    "ted by Dimencia (2020-2021)\r\nMaintainer: tobitege (2022-)";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kryptonLabel4.Location = new System.Drawing.Point(99, 268);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(307, 27);
            this.kryptonLabel4.TabIndex = 8;
            this.kryptonLabel4.Values.Text = "Dimencia\'s old repository (Github):";
            // 
            // DimenciaGithubLink
            // 
            this.DimenciaGithubLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DimenciaGithubLink.Location = new System.Drawing.Point(99, 290);
            this.DimenciaGithubLink.Name = "DimenciaGithubLink";
            this.DimenciaGithubLink.Size = new System.Drawing.Size(377, 27);
            this.DimenciaGithubLink.TabIndex = 9;
            this.DimenciaGithubLink.Values.Text = "https://github.com/Dimencia/DU-Industry-Tool";
            // 
            // AboutForm
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(672, 441);
            this.Controls.Add(this.DimenciaGithubLink);
            this.Controls.Add(this.kryptonLabel4);
            this.Controls.Add(this.labelMain);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.discordLink);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.kryptonLabel1);
            this.Controls.Add(this.TobiReleasesLink);
            this.Controls.Add(this.panelLeft);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.panelLeft)).EndInit();
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel panelLeft;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Krypton.Toolkit.KryptonLinkLabel TobiReleasesLink;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonLinkLabel discordLink;
        private Krypton.Toolkit.KryptonButton OkButton;
        private Krypton.Toolkit.KryptonLabel labelMain;
        private Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private Krypton.Toolkit.KryptonLinkLabel DimenciaGithubLink;
    }
}