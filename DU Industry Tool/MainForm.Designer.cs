﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using ComponentFactory.Krypton.Navigator;

namespace DU_Industry_Tool
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.kryptonRibbon = new ComponentFactory.Krypton.Ribbon.KryptonRibbon();
            this.buttonOreValues = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.buttonSchematicValues = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.buttonSkillLevels = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.buttonUpdateMarketValues = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.buttonFilterToMarket = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.buttonExportToSpreadsheet = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.buttonFactoryBreakdownForSelected = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.buttonConvertLua2JsonFile = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.ribbonAppButtonExit = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonDockableWorkspace = new ComponentFactory.Krypton.Docking.KryptonDockableWorkspace();
            this.kryptonWorkspaceCell1 = new ComponentFactory.Krypton.Workspace.KryptonWorkspaceCell();
            this.kryptonPage1 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.searchPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.QuantityBox = new System.Windows.Forms.ComboBox();
            this.PreviousButton = new System.Windows.Forms.Button();
            this.treeView = new System.Windows.Forms.TreeView();
            this.kryptonNavigator1 = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.kryptonDockingManager = new ComponentFactory.Krypton.Docking.KryptonDockingManager();
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace)).BeginInit();
            this.kryptonDockableWorkspace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspaceCell1)).BeginInit();
            this.kryptonWorkspaceCell1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).BeginInit();
            this.kryptonPage1.SuspendLayout();
            this.searchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).BeginInit();
            this.kryptonNavigator1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonRibbon
            // 
            this.kryptonRibbon.AllowMinimizedChange = false;
            this.kryptonRibbon.InDesignHelperMode = true;
            this.kryptonRibbon.MinimizedMode = true;
            this.kryptonRibbon.Name = "kryptonRibbon";
            this.kryptonRibbon.RibbonAppButton.AppButtonMenuItems.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.buttonOreValues,
            this.buttonSchematicValues,
            this.buttonSkillLevels,
            this.buttonUpdateMarketValues,
            this.buttonFilterToMarket,
            this.buttonExportToSpreadsheet,
            this.buttonFactoryBreakdownForSelected,
            this.buttonConvertLua2JsonFile,
            this.ribbonAppButtonExit});
            this.kryptonRibbon.RibbonAppButton.AppButtonShowRecentDocs = false;
            this.kryptonRibbon.SelectedContext = null;
            this.kryptonRibbon.SelectedTab = null;
            this.kryptonRibbon.ShowMinimizeButton = false;
            this.kryptonRibbon.Size = new System.Drawing.Size(1850, 134);
            this.kryptonRibbon.TabIndex = 0;
            // 
            // buttonOreValues
            // 
            this.buttonOreValues.Text = "&Ore/Plasma Values";
            this.buttonOreValues.Click += new System.EventHandler(this.InputOreValuesToolStripMenuItem_Click);
            // 
            // buttonSchematicValues
            // 
            this.buttonSchematicValues.Text = "&Schematics";
            this.buttonSchematicValues.Click += new System.EventHandler(this.SchematicValuesToolStripMenuItem_Click);
            // 
            // buttonSkillLevels
            // 
            this.buttonSkillLevels.Text = "&Talents";
            this.buttonSkillLevels.Click += new System.EventHandler(this.SkillLevelsToolStripMenuItem_Click);
            // 
            // buttonUpdateMarketValues
            // 
            this.buttonUpdateMarketValues.Text = "&Update Market Values";
            this.buttonUpdateMarketValues.Click += new System.EventHandler(this.UpdateMarketValuesToolStripMenuItem_Click);
            // 
            // buttonFilterToMarket
            // 
            this.buttonFilterToMarket.Text = "&Filter to Market";
            this.buttonFilterToMarket.Click += new System.EventHandler(this.FilterToMarketToolStripMenuItem_Click);
            // 
            // buttonExportToSpreadsheet
            // 
            this.buttonExportToSpreadsheet.Text = "&Export to CSV";
            this.buttonExportToSpreadsheet.Click += new System.EventHandler(this.ExportToSpreadsheetToolStripMenuItem_Click);
            // 
            // buttonFactoryBreakdownForSelected
            // 
            this.buttonFactoryBreakdownForSelected.Text = "&Factory Breakdown for Selected";
            this.buttonFactoryBreakdownForSelected.Click += new System.EventHandler(this.FactoryBreakdownForSelectedToolStripMenuItem_Click);
            // 
            // buttonConvertLua2JsonFile
            // 
            this.buttonConvertLua2JsonFile.Text = "&Convert LUA table file to JSON file";
            this.buttonConvertLua2JsonFile.Click += new System.EventHandler(this.ConvertLua2JsonFile_Click);
            // 
            // ribbonAppButtonExit
            // 
            this.ribbonAppButtonExit.Text = "E&xit";
            this.ribbonAppButtonExit.Click += new System.EventHandler(this.RibbonAppButtonExit_Click);
            // 
            // kryptonDockableWorkspace
            // 
            this.kryptonDockableWorkspace.AutoHiddenHost = false;
            this.kryptonDockableWorkspace.CompactFlags = ComponentFactory.Krypton.Workspace.CompactFlags.AtLeastOneVisibleCell;
            this.kryptonDockableWorkspace.ContainerBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.TabDock;
            this.kryptonDockableWorkspace.Dock = System.Windows.Forms.DockStyle.Left;
            this.kryptonDockableWorkspace.Location = new System.Drawing.Point(0, 134);
            this.kryptonDockableWorkspace.MinimumSize = new System.Drawing.Size(300, 400);
            this.kryptonDockableWorkspace.Name = "kryptonDockableWorkspace";
            this.kryptonDockableWorkspace.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.kryptonDockableWorkspace.Root.Children.AddRange(new System.ComponentModel.Component[] {
            this.kryptonWorkspaceCell1});
            this.kryptonDockableWorkspace.Root.UniqueName = "D51970B3EA2C496AD51970B3EA2C496A";
            this.kryptonDockableWorkspace.Root.WorkspaceControl = this.kryptonDockableWorkspace;
            this.kryptonDockableWorkspace.SeparatorStyle = ComponentFactory.Krypton.Toolkit.SeparatorStyle.HighProfile;
            this.kryptonDockableWorkspace.ShowMaximizeButton = false;
            this.kryptonDockableWorkspace.Size = new System.Drawing.Size(420, 1120);
            this.kryptonDockableWorkspace.SplitterWidth = 8;
            this.kryptonDockableWorkspace.TabIndex = 0;
            this.kryptonDockableWorkspace.TabStop = true;
            this.kryptonDockableWorkspace.WorkspaceCellAdding += new System.EventHandler<ComponentFactory.Krypton.Workspace.WorkspaceCellEventArgs>(this.KryptonDockableWorkspace_WorkspaceCellAdding);
            // 
            // kryptonWorkspaceCell1
            // 
            this.kryptonWorkspaceCell1.AllowPageDrag = true;
            this.kryptonWorkspaceCell1.AllowPageReorder = false;
            this.kryptonWorkspaceCell1.AllowTabFocus = false;
            this.kryptonWorkspaceCell1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kryptonWorkspaceCell1.Button.ButtonDisplayLogic = ComponentFactory.Krypton.Navigator.ButtonDisplayLogic.None;
            this.kryptonWorkspaceCell1.Button.CloseButtonAction = ComponentFactory.Krypton.Navigator.CloseButtonAction.None;
            this.kryptonWorkspaceCell1.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonWorkspaceCell1.Button.ContextButtonAction = ComponentFactory.Krypton.Navigator.ContextButtonAction.None;
            this.kryptonWorkspaceCell1.Button.ContextButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonWorkspaceCell1.Button.NextButtonAction = ComponentFactory.Krypton.Navigator.DirectionButtonAction.None;
            this.kryptonWorkspaceCell1.Button.NextButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonWorkspaceCell1.Button.PreviousButtonAction = ComponentFactory.Krypton.Navigator.DirectionButtonAction.None;
            this.kryptonWorkspaceCell1.Button.PreviousButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonWorkspaceCell1.MaximumSize = new System.Drawing.Size(500, 0);
            this.kryptonWorkspaceCell1.Name = "kryptonWorkspaceCell1";
            this.kryptonWorkspaceCell1.NavigatorMode = ComponentFactory.Krypton.Navigator.NavigatorMode.BarCheckButtonGroupOutside;
            this.kryptonWorkspaceCell1.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.kryptonPage1});
            this.kryptonWorkspaceCell1.SelectedIndex = 0;
            this.kryptonWorkspaceCell1.Size = new System.Drawing.Size(420, 1120);
            this.kryptonWorkspaceCell1.UniqueName = "B46823ED744B4A87B46823ED744B4A87";
            // 
            // kryptonPage1
            // 
            this.kryptonPage1.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage1.AutoSizeMode = AutoSizeMode.GrowOnly;
            this.kryptonPage1.Dock = DockStyle.Fill;
            this.kryptonPage1.Controls.Add(this.searchPanel);
            this.kryptonPage1.Controls.Add(this.treeView);
            this.kryptonPage1.Flags = 32;
            this.kryptonPage1.LastVisibleSet = true;
            this.kryptonPage1.MinimumSize = new System.Drawing.Size(400, 450);
            this.kryptonPage1.Name = "kryptonPage1";
            this.kryptonPage1.Size = new System.Drawing.Size(420, 600);
            this.kryptonPage1.Text = "Recipes Explorer";
            this.kryptonPage1.TextDescription = "";
            this.kryptonPage1.TextTitle = "";
            this.kryptonPage1.ToolTipTitle = "Page ToolTip";
            this.kryptonPage1.UniqueName = "38D886AD20CD402D38D886AD20CD402D";
            // 
            // searchPanel
            // 
            this.searchPanel.AutoSize = true;
            this.searchPanel.Controls.Add(this.SearchBox);
            this.searchPanel.Controls.Add(this.SearchButton);
            this.searchPanel.Controls.Add(this.QuantityBox);
            this.searchPanel.Controls.Add(this.PreviousButton);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(0, 0);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(417, 36);
            this.searchPanel.TabIndex = 0;
            // 
            // SearchBox
            // 
            this.SearchBox.Location = new System.Drawing.Point(4, 6);
            this.SearchBox.Margin = new System.Windows.Forms.Padding(4, 6, 0, 0);
            this.SearchBox.MaxLength = 30;
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(200, 28);
            this.SearchBox.TabIndex = 0;
            // 
            // SearchButton
            // 
            this.SearchButton.AutoSize = true;
            this.SearchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchButton.Location = new System.Drawing.Point(207, 3);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(80, 30);
            this.SearchButton.TabIndex = 1;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // QuantityBox
            // 
            this.QuantityBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.QuantityBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityBox.Items.AddRange(new object[] {
            "1",
            "2",
            "5",
            "10",
            "20",
            "50",
            "100",
            "200",
            "500",
            "1000"});
            this.QuantityBox.Location = new System.Drawing.Point(294, 6);
            this.QuantityBox.Margin = new System.Windows.Forms.Padding(4, 6, 0, 0);
            this.QuantityBox.MaxDropDownItems = 10;
            this.QuantityBox.MaxLength = 4;
            this.QuantityBox.Name = "QuantityBox";
            this.QuantityBox.Size = new System.Drawing.Size(50, 28);
            this.QuantityBox.TabIndex = 2;
            // 
            // PreviousButton
            // 
            this.PreviousButton.AutoSize = true;
            this.PreviousButton.Enabled = false;
            this.PreviousButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PreviousButton.Location = new System.Drawing.Point(347, 3);
            this.PreviousButton.Name = "PreviousButton";
            this.PreviousButton.Size = new System.Drawing.Size(39, 30);
            this.PreviousButton.TabIndex = 3;
            this.PreviousButton.Text = "<<";
            this.PreviousButton.UseVisualStyleBackColor = true;
            this.PreviousButton.Click += new System.EventHandler(this.PreviousButton_Click);
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                                                                          | System.Windows.Forms.AnchorStyles.Left) 
                                                                         | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.Location = new System.Drawing.Point(0, 40);
            this.treeView.MinimumSize = new System.Drawing.Size(400, 400);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(415, 1050);
            this.treeView.TabIndex = 3;
            // 
            // kryptonNavigator1
            // 
            this.kryptonNavigator1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonNavigator1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kryptonNavigator1.Bar.BarMultiline = ComponentFactory.Krypton.Navigator.BarMultiline.Multiline;
            this.kryptonNavigator1.Bar.TabBorderStyle = ComponentFactory.Krypton.Toolkit.TabBorderStyle.RoundedEqualMedium;
            this.kryptonNavigator1.Location = new System.Drawing.Point(422, 62);
            this.kryptonNavigator1.Name = "kryptonNavigator1";
            this.kryptonNavigator1.Size = new System.Drawing.Size(1422, 930);
            this.kryptonNavigator1.StateCommon.CheckButton.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonNavigator1.StateCommon.CheckButton.Content.Image.ImageV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonNavigator1.StateCommon.CheckButton.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonNavigator1.StateCommon.CheckButton.Content.ShortText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.kryptonNavigator1.TabIndex = 7;
            this.kryptonNavigator1.Text = "kryptonNavigator1";
            this.kryptonNavigator1.SelectedPageChanged += KryptonNavigator1OnSelectedPageChanged;
            // 
            // imageListSmall
            // 
            this.imageListSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSmall.ImageStream")));
            this.imageListSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSmall.Images.SetKeyName(0, "document_plain.png");
            this.imageListSmall.Images.SetKeyName(1, "preferences.png");
            this.imageListSmall.Images.SetKeyName(2, "information2.png");
            // 
            // kryptonManager
            // 
            this.kryptonManager.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Office2010Silver;
            // 
            // MainForm
            // 
            this.AcceptButton = this.SearchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1850, 1000);
            this.Controls.Add(this.kryptonNavigator1);
            this.Controls.Add(this.kryptonDockableWorkspace);
            this.Controls.Add(this.kryptonRibbon);
            this.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "DU Industry Tool (Mercury)";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace)).EndInit();
            this.kryptonDockableWorkspace.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspaceCell1)).EndInit();
            this.kryptonWorkspaceCell1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).EndInit();
            this.kryptonPage1.ResumeLayout(false);
            this.kryptonPage1.PerformLayout();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).EndInit();
            this.kryptonNavigator1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
            this.Resize += OnMainformResize;
        }

        #endregion

        private System.Windows.Forms.ImageList imageListSmall;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbon kryptonRibbon;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem ribbonAppButtonExit;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem buttonOreValues;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem buttonSchematicValues;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem buttonSkillLevels;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem buttonUpdateMarketValues;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem buttonFilterToMarket;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem buttonExportToSpreadsheet;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem buttonFactoryBreakdownForSelected;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem buttonConvertLua2JsonFile;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Navigator.KryptonNavigator kryptonNavigator1;
        private ComponentFactory.Krypton.Docking.KryptonDockingManager kryptonDockingManager;
        private ComponentFactory.Krypton.Docking.KryptonDockableWorkspace kryptonDockableWorkspace;
        private ComponentFactory.Krypton.Workspace.KryptonWorkspaceCell kryptonWorkspaceCell1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage1;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.FlowLayoutPanel searchPanel;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button PreviousButton;
        private System.Windows.Forms.ComboBox QuantityBox;
    }
}