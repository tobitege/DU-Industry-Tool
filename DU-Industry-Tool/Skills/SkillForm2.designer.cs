namespace DU_Industry_Tool.Skills
{
    sealed partial class SkillForm2
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Fuels");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Ammunition");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Parts");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Pures");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Products");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Pure Honeycombs");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Product Honeycombs");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Honeycombs", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Scraps");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Crafting", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode8,
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Elements");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Schematics");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Industry", new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SkillForm2));
            this.TimerLoad = new System.Windows.Forms.Timer(this.components);
            this.splitContainer = new Krypton.Toolkit.KryptonSplitContainer();
            this.treeView = new Krypton.Toolkit.KryptonTreeView();
            this.tableLayout = new Krypton.Toolkit.KryptonTableLayoutPanel();
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.sectionTitle = new Krypton.Toolkit.KryptonPanel();
            this.CmbLevel = new Krypton.Toolkit.KryptonComboBox();
            this.BtnSetAll = new Krypton.Toolkit.KryptonButton();
            this.LblSection = new Krypton.Toolkit.KryptonLabel();
            this.PnlFooter = new Krypton.Toolkit.KryptonPanel();
            this.exportKBtn = new Krypton.Toolkit.KryptonDropButton();
            this.BtnReload = new Krypton.Toolkit.KryptonButton();
            this.BtnClose = new Krypton.Toolkit.KryptonButton();
            this.BtnSave = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel1)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel2)).BeginInit();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sectionTitle)).BeginInit();
            this.sectionTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PnlFooter)).BeginInit();
            this.PnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // TimerLoad
            // 
            this.TimerLoad.Interval = 1000;
            this.TimerLoad.Tick += new System.EventHandler(this.TimerLoad_Tick);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.ContainerBackStyle = Krypton.Toolkit.PaletteBackStyle.FormMain;
            this.splitContainer.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeView);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(4);
            this.splitContainer.Panel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.TabOneNote;
            this.splitContainer.Panel1MinSize = 150;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tableLayout);
            this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(4);
            this.splitContainer.Panel2.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.TabOneNote;
            this.splitContainer.Panel2MinSize = 400;
            this.splitContainer.Size = new System.Drawing.Size(1202, 946);
            this.splitContainer.SplitterDistance = 300;
            this.splitContainer.TabIndex = 0;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.FullRowSelect = true;
            this.treeView.HideSelection = false;
            this.treeView.Location = new System.Drawing.Point(4, 4);
            this.treeView.MinimumSize = new System.Drawing.Size(200, 0);
            this.treeView.Name = "treeView";
            treeNode1.Name = "sectFuels";
            treeNode1.Tag = "Fuels";
            treeNode1.Text = "Fuels";
            treeNode2.Name = "sectAmmo";
            treeNode2.Tag = "Ammunition";
            treeNode2.Text = "Ammunition";
            treeNode3.Name = "sectParts";
            treeNode3.Tag = "Parts";
            treeNode3.Text = "Parts";
            treeNode4.Name = "sectPures";
            treeNode4.Tag = "Pures";
            treeNode4.Text = "Pures";
            treeNode5.Name = "sectProducts";
            treeNode5.Tag = "Products";
            treeNode5.Text = "Products";
            treeNode6.Name = "sectPureHC";
            treeNode6.Tag = "Pure Honeycombs";
            treeNode6.Text = "Pure Honeycombs";
            treeNode7.Name = "sectProdHC";
            treeNode7.Tag = "Product Honeycombs";
            treeNode7.Text = "Product Honeycombs";
            treeNode8.Name = "dmyHC";
            treeNode8.Text = "Honeycombs";
            treeNode9.Name = "sectScraps";
            treeNode9.Tag = "Scraps";
            treeNode9.Text = "Scraps";
            treeNode10.Name = "topCrafting";
            treeNode10.Text = "Crafting";
            treeNode11.Name = "sectElements";
            treeNode11.Tag = "Elements";
            treeNode11.Text = "Elements";
            treeNode12.Name = "sectSchematics";
            treeNode12.Tag = "Schematics";
            treeNode12.Text = "Schematics";
            treeNode13.Name = "topIndustry";
            treeNode13.Tag = "Industry";
            treeNode13.Text = "Industry";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode13});
            this.treeView.OverrideFocus.Node.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.treeView.Size = new System.Drawing.Size(292, 938);
            this.treeView.StateCommon.Node.Content.ShortText.Font = new System.Drawing.Font("Verdana", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // tableLayout
            // 
            this.tableLayout.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tableLayout.BackgroundImage")));
            this.tableLayout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tableLayout.ColumnCount = 1;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Controls.Add(this.flowPanel, 0, 1);
            this.tableLayout.Controls.Add(this.sectionTitle, 0, 0);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(4, 4);
            this.tableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.tableLayout.RowCount = 2;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Size = new System.Drawing.Size(889, 938);
            this.tableLayout.TabIndex = 2;
            // 
            // flowPanel
            // 
            this.flowPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowPanel.BackColor = System.Drawing.Color.Transparent;
            this.flowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanel.Location = new System.Drawing.Point(2, 50);
            this.flowPanel.Margin = new System.Windows.Forms.Padding(2);
            this.flowPanel.Name = "flowPanel";
            this.flowPanel.Size = new System.Drawing.Size(885, 886);
            this.flowPanel.TabIndex = 3;
            // 
            // sectionTitle
            // 
            this.sectionTitle.Controls.Add(this.CmbLevel);
            this.sectionTitle.Controls.Add(this.BtnSetAll);
            this.sectionTitle.Controls.Add(this.LblSection);
            this.sectionTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sectionTitle.Location = new System.Drawing.Point(3, 3);
            this.sectionTitle.Name = "sectionTitle";
            this.sectionTitle.Size = new System.Drawing.Size(883, 42);
            this.sectionTitle.TabIndex = 2;
            // 
            // CmbLevel
            // 
            this.CmbLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CmbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbLevel.DropDownWidth = 56;
            this.CmbLevel.IntegralHeight = false;
            this.CmbLevel.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.CmbLevel.Location = new System.Drawing.Point(687, 5);
            this.CmbLevel.Name = "CmbLevel";
            this.CmbLevel.Size = new System.Drawing.Size(56, 28);
            this.CmbLevel.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.CmbLevel.TabIndex = 4;
            // 
            // BtnSetAll
            // 
            this.BtnSetAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSetAll.Enabled = false;
            this.BtnSetAll.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSetAll.Location = new System.Drawing.Point(756, 2);
            this.BtnSetAll.Margin = new System.Windows.Forms.Padding(10);
            this.BtnSetAll.Name = "BtnSetAll";
            this.BtnSetAll.Size = new System.Drawing.Size(115, 36);
            this.BtnSetAll.TabIndex = 2;
            this.BtnSetAll.ToolTipValues.Description = "Set all shown talents to selected value";
            this.BtnSetAll.ToolTipValues.EnableToolTips = true;
            this.BtnSetAll.ToolTipValues.Heading = "";
            this.BtnSetAll.Values.Text = "Set &all";
            this.BtnSetAll.Click += new System.EventHandler(this.BtnSetAll_Click);
            // 
            // LblSection
            // 
            this.LblSection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblSection.AutoSize = false;
            this.LblSection.Location = new System.Drawing.Point(4, 0);
            this.LblSection.Name = "LblSection";
            this.LblSection.Size = new System.Drawing.Size(678, 34);
            this.LblSection.StateNormal.ShortText.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSection.StateNormal.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.LblSection.StateNormal.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.LblSection.TabIndex = 0;
            this.LblSection.Values.Text = "...";
            // 
            // PnlFooter
            // 
            this.PnlFooter.Controls.Add(this.exportKBtn);
            this.PnlFooter.Controls.Add(this.BtnReload);
            this.PnlFooter.Controls.Add(this.BtnClose);
            this.PnlFooter.Controls.Add(this.BtnSave);
            this.PnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlFooter.Location = new System.Drawing.Point(0, 946);
            this.PnlFooter.Name = "PnlFooter";
            this.PnlFooter.Size = new System.Drawing.Size(1204, 58);
            this.PnlFooter.TabIndex = 1;
            // 
            // exportKBtn
            // 
            this.exportKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exportKBtn.Location = new System.Drawing.Point(13, 10);
            this.exportKBtn.Margin = new System.Windows.Forms.Padding(4);
            this.exportKBtn.Name = "exportKBtn";
            this.exportKBtn.Size = new System.Drawing.Size(165, 35);
            this.exportKBtn.TabIndex = 27;
            this.exportKBtn.Values.Text = "Export to Excel";
            this.exportKBtn.Visible = false;
            // 
            // BtnReload
            // 
            this.BtnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnReload.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReload.Location = new System.Drawing.Point(825, 10);
            this.BtnReload.Margin = new System.Windows.Forms.Padding(10);
            this.BtnReload.Name = "BtnReload";
            this.BtnReload.Size = new System.Drawing.Size(159, 38);
            this.BtnReload.TabIndex = 1;
            this.BtnReload.ToolTipValues.Description = "Reload this section\'s talents";
            this.BtnReload.ToolTipValues.EnableToolTips = true;
            this.BtnReload.ToolTipValues.Heading = "";
            this.BtnReload.Values.Text = "&Reload";
            this.BtnReload.Click += new System.EventHandler(this.BtnReload_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnClose.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClose.Location = new System.Drawing.Point(1026, 10);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(10);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(159, 38);
            this.BtnClose.TabIndex = 2;
            this.BtnClose.Values.Text = "&Close";
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.Enabled = false;
            this.BtnSave.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(646, 10);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(10);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(159, 38);
            this.BtnSave.TabIndex = 0;
            this.BtnSave.Values.Text = "&Save";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // SkillForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1204, 1004);
            this.Controls.Add(this.PnlFooter);
            this.Controls.Add(this.splitContainer);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1750, 1600);
            this.MinimumSize = new System.Drawing.Size(700, 690);
            this.Name = "SkillForm2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.StateCommon.Border.Rounding = 4F;
            this.Text = "Talents Input";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SkillForm2_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SkillForm2_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel1)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel2)).EndInit();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tableLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sectionTitle)).EndInit();
            this.sectionTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CmbLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PnlFooter)).EndInit();
            this.PnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer TimerLoad;
        private Krypton.Toolkit.KryptonSplitContainer splitContainer;
        private Krypton.Toolkit.KryptonTreeView treeView;
        private Krypton.Toolkit.KryptonTableLayoutPanel tableLayout;
        private Krypton.Toolkit.KryptonPanel sectionTitle;
        private Krypton.Toolkit.KryptonLabel LblSection;
        private System.Windows.Forms.FlowLayoutPanel flowPanel;
        private Krypton.Toolkit.KryptonPanel PnlFooter;
        private Krypton.Toolkit.KryptonButton BtnClose;
        private Krypton.Toolkit.KryptonButton BtnSave;
        private Krypton.Toolkit.KryptonButton BtnSetAll;
        private Krypton.Toolkit.KryptonComboBox CmbLevel;
        private Krypton.Toolkit.KryptonButton BtnReload;
        private Krypton.Toolkit.KryptonDropButton exportKBtn;
    }
}