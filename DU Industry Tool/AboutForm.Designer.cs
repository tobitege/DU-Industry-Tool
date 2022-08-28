﻿
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
            this.panelLeft = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TobiReleasesLink = new ComponentFactory.Krypton.Toolkit.KryptonLinkLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.discordLink = new ComponentFactory.Krypton.Toolkit.KryptonLinkLabel();
            this.OkButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.labelMain = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DimenciaGithubLink = new ComponentFactory.Krypton.Toolkit.KryptonLinkLabel();
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
            this.panelLeft.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.FormMain;
            this.panelLeft.Size = new System.Drawing.Size(90, 415);
            this.panelLeft.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(13, 13, 13, 13);
            this.pictureBox1.Size = new System.Drawing.Size(90, 415);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // TobiReleasesLink
            // 
            this.TobiReleasesLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TobiReleasesLink.Location = new System.Drawing.Point(99, 317);
            this.TobiReleasesLink.Name = "TobiReleasesLink";
            this.TobiReleasesLink.Size = new System.Drawing.Size(388, 24);
            this.TobiReleasesLink.TabIndex = 2;
            this.TobiReleasesLink.Values.Text = "https://github.com/tobitege/DU-Industry-Tool/releases";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kryptonLabel1.Location = new System.Drawing.Point(99, 297);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(285, 24);
            this.kryptonLabel1.TabIndex = 3;
            this.kryptonLabel1.Values.Text = "Tobi\'s binary releases v0.500+ (Github)s:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kryptonLabel2.Location = new System.Drawing.Point(99, 173);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(306, 24);
            this.kryptonLabel2.TabIndex = 4;
            this.kryptonLabel2.Values.Text = "Visit the DU Open Source Initiative Discord:";
            // 
            // discordLink
            // 
            this.discordLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.discordLink.Location = new System.Drawing.Point(99, 199);
            this.discordLink.Name = "discordLink";
            this.discordLink.Size = new System.Drawing.Size(211, 24);
            this.discordLink.TabIndex = 5;
            this.discordLink.Values.Text = "https://discord.gg/7psYcmAb";
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OkButton.AutoSize = true;
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(276, 371);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(95, 32);
            this.OkButton.TabIndex = 0;
            this.OkButton.Values.Text = "Ok";
            // 
            // labelMain
            // 
            this.labelMain.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitleControl;
            this.labelMain.Location = new System.Drawing.Point(96, 12);
            this.labelMain.Name = "labelMain";
            this.labelMain.Size = new System.Drawing.Size(545, 155);
            this.labelMain.TabIndex = 7;
            this.labelMain.Values.Text = "DU Industry Tool v0.640\r\nA tool to find cost for elements in Dual Universe.\r\n\r\nCr" +
    "eated by Dimencia (2020-2021)\r\nContributor: tobitege (2022)\r\n";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kryptonLabel4.Location = new System.Drawing.Point(99, 245);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(272, 24);
            this.kryptonLabel4.TabIndex = 8;
            this.kryptonLabel4.Values.Text = "Dimencia\'s master repository (Github):";
            // 
            // DimenciaGithubLink
            // 
            this.DimenciaGithubLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DimenciaGithubLink.Location = new System.Drawing.Point(99, 267);
            this.DimenciaGithubLink.Name = "DimenciaGithubLink";
            this.DimenciaGithubLink.Size = new System.Drawing.Size(334, 24);
            this.DimenciaGithubLink.TabIndex = 9;
            this.DimenciaGithubLink.Values.Text = "https://github.com/Dimencia/DU-Industry-Tool";
            // 
            // AboutForm
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(637, 415);
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
            this.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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

        private ComponentFactory.Krypton.Toolkit.KryptonPanel panelLeft;
        private System.Windows.Forms.PictureBox pictureBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonLinkLabel TobiReleasesLink;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLinkLabel discordLink;
        private ComponentFactory.Krypton.Toolkit.KryptonButton OkButton;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel labelMain;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLinkLabel DimenciaGithubLink;
    }
}