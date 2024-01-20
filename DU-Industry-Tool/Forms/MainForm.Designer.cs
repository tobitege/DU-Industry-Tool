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
            this.kryptonRibbon = new Krypton.Ribbon.KryptonRibbon();
            this.buttonUpdateMarketValues = new Krypton.Toolkit.KryptonContextMenuItem();
            this.buttonFilterToMarket = new Krypton.Toolkit.KryptonContextMenuItem();
            this.buttonExportToSpreadsheet = new Krypton.Toolkit.KryptonContextMenuItem();
            this.buttonFactoryBreakdownForSelected = new Krypton.Toolkit.KryptonContextMenuItem();
            this.ribbonAppButtonExit = new Krypton.Toolkit.KryptonContextMenuItem();
            this.rbnTabTools = new Krypton.Ribbon.KryptonRibbonTab();
            this.RibbonGroupProduction = new Krypton.Ribbon.KryptonRibbonGroup();
            this.RibbonTripleProductionList1 = new Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.RbnBtnProductionList = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.RbnBtnProductionListLoad = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.RbnBtnProductionListSave = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.RibbonTripleProductionList2 = new Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.BtnProdListAdd = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.BtnProdListRemove = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.BtnProdListClose = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.RibbonGroupProductionList3 = new Krypton.Ribbon.KryptonRibbonGroupLines();
            this.CbRecentLists = new Krypton.Ribbon.KryptonRibbonGroupComboBox();
            this.BtnClearProdLists = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.RibbonGroupSetup = new Krypton.Ribbon.KryptonRibbonGroup();
            this.kryptonRibbonGroupTriple2 = new Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.BtnTalents = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.BtnOreValues = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.BtnSchematics = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.rbnTabThemes = new Krypton.Ribbon.KryptonRibbonTab();
            this.rbnGrpThemes = new Krypton.Ribbon.KryptonRibbonGroup();
            this.rbnTrippleThemes = new Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.rbnBtnThemeBlue = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.rbnBtnThemeSilver = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.rbnBtnThemeBlack = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.rbnTabOptions = new Krypton.Ribbon.KryptonRibbonTab();
            this.RibbonGroupOptions = new Krypton.Ribbon.KryptonRibbonGroup();
            this.RibbonGroupOptionsLines1 = new Krypton.Ribbon.KryptonRibbonGroupLines();
            this.CbStartupProdList = new Krypton.Ribbon.KryptonRibbonGroupCheckBox();
            this.CbRestoreWindow = new Krypton.Ribbon.KryptonRibbonGroupCheckBox();
            this.CbFullSchematicQty = new Krypton.Ribbon.KryptonRibbonGroupCheckBox();
            this.RibbonGroupAbout = new Krypton.Ribbon.KryptonRibbonGroup();
            this.RibbonGroupAboutLines1 = new Krypton.Ribbon.KryptonRibbonGroupLines();
            this.RibbonBtnAbout = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.cmdThemeBlue = new Krypton.Toolkit.KryptonCommand();
            this.kryptonDockableWorkspace = new Krypton.Docking.KryptonDockableWorkspace();
            this.kryptonPage1 = new Krypton.Navigator.KryptonPage();
            this.treeView = new System.Windows.Forms.TreeView();
            this.searchPanel = new Krypton.Toolkit.KryptonPanel();
            this.LblSearchHint = new Krypton.Toolkit.KryptonLabel();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.lblQty = new Krypton.Toolkit.KryptonLabel();
            this.CbNanoOnly = new Krypton.Toolkit.KryptonCheckBox();
            this.SearchButton = new Krypton.Toolkit.KryptonButton();
            this.QuantityBox = new Krypton.Toolkit.KryptonComboBox();
            this.PreviousButton = new Krypton.Toolkit.KryptonButton();
            this.kryptonWorkspaceCell1 = new Krypton.Workspace.KryptonWorkspaceCell();
            this.kryptonNavigator1 = new Krypton.Navigator.KryptonNavigator();
            this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.kryptonDockingManager = new Krypton.Docking.KryptonDockingManager();
            this.kryptonManager = new Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonRibbonGroupGallery1 = new Krypton.Ribbon.KryptonRibbonGroupGallery();
            this.acMenu = new AutocompleteMenuNS.AutocompleteMenu();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace)).BeginInit();
            this.kryptonDockableWorkspace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).BeginInit();
            this.kryptonPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchPanel)).BeginInit();
            this.searchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuantityBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspaceCell1)).BeginInit();
            this.kryptonWorkspaceCell1.SuspendLayout();
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
            this.kryptonRibbon.RibbonAppButton.AppButtonMenuItems.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.buttonUpdateMarketValues,
            this.buttonFilterToMarket,
            this.buttonExportToSpreadsheet,
            this.buttonFactoryBreakdownForSelected,
            this.ribbonAppButtonExit});
            this.kryptonRibbon.RibbonAppButton.AppButtonShowRecentDocs = false;
            this.kryptonRibbon.RibbonTabs.AddRange(new Krypton.Ribbon.KryptonRibbonTab[] {
            this.rbnTabTools,
            this.rbnTabThemes,
            this.rbnTabOptions});
            this.kryptonRibbon.SelectedTab = this.rbnTabTools;
            this.kryptonRibbon.ShowMinimizeButton = false;
            this.kryptonRibbon.Size = new System.Drawing.Size(1860, 136);
            this.kryptonRibbon.TabIndex = 0;
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
            // ribbonAppButtonExit
            // 
            this.ribbonAppButtonExit.Text = "E&xit";
            this.ribbonAppButtonExit.Click += new System.EventHandler(this.RibbonAppButtonExit_Click);
            // 
            // rbnTabTools
            // 
            this.rbnTabTools.Groups.AddRange(new Krypton.Ribbon.KryptonRibbonGroup[] {
            this.RibbonGroupProduction,
            this.RibbonGroupSetup});
            this.rbnTabTools.Text = "Tools";
            // 
            // RibbonGroupProduction
            // 
            this.RibbonGroupProduction.AllowCollapsed = false;
            this.RibbonGroupProduction.Image = null;
            this.RibbonGroupProduction.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.RibbonTripleProductionList1,
            this.RibbonTripleProductionList2,
            this.RibbonGroupProductionList3});
            this.RibbonGroupProduction.MinimumWidth = 200;
            this.RibbonGroupProduction.TextLine1 = "Production List";
            // 
            // RibbonTripleProductionList1
            // 
            this.RibbonTripleProductionList1.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.RbnBtnProductionList,
            this.RbnBtnProductionListLoad,
            this.RbnBtnProductionListSave});
            // 
            // RbnBtnProductionList
            // 
            this.RbnBtnProductionList.ImageLarge = global::DU_Industry_Tool.Properties.Resources.stock_add_bookmark;
            this.RbnBtnProductionList.ImageSmall = global::DU_Industry_Tool.Properties.Resources.stock_add_bookmark;
            this.RbnBtnProductionList.KeyTip = "P";
            this.RbnBtnProductionList.TextLine1 = "Production";
            this.RbnBtnProductionList.TextLine2 = "List";
            this.RbnBtnProductionList.Click += new System.EventHandler(this.RbnBtnProductionList_Click);
            // 
            // RbnBtnProductionListLoad
            // 
            this.RbnBtnProductionListLoad.ImageLarge = global::DU_Industry_Tool.Properties.Resources.fileopen;
            this.RbnBtnProductionListLoad.ImageSmall = global::DU_Industry_Tool.Properties.Resources.fileopen;
            this.RbnBtnProductionListLoad.KeyTip = "L";
            this.RbnBtnProductionListLoad.TextLine1 = "Load List";
            this.RbnBtnProductionListLoad.Click += new System.EventHandler(this.RbnBtnProductionListLoad_Click);
            // 
            // RbnBtnProductionListSave
            // 
            this.RbnBtnProductionListSave.ImageLarge = global::DU_Industry_Tool.Properties.Resources.filesave;
            this.RbnBtnProductionListSave.ImageSmall = global::DU_Industry_Tool.Properties.Resources.filesave;
            this.RbnBtnProductionListSave.KeyTip = "S";
            this.RbnBtnProductionListSave.TextLine1 = "Save List";
            this.RbnBtnProductionListSave.Click += new System.EventHandler(this.RbnBtnProductionListSave_Click);
            // 
            // RibbonTripleProductionList2
            // 
            this.RibbonTripleProductionList2.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.BtnProdListAdd,
            this.BtnProdListRemove,
            this.BtnProdListClose});
            // 
            // BtnProdListAdd
            // 
            this.BtnProdListAdd.Enabled = false;
            this.BtnProdListAdd.ImageLarge = global::DU_Industry_Tool.Properties.Resources.Add;
            this.BtnProdListAdd.ImageSmall = global::DU_Industry_Tool.Properties.Resources.Add;
            this.BtnProdListAdd.KeyTip = "A";
            this.BtnProdListAdd.TextLine1 = "Add";
            this.BtnProdListAdd.TextLine2 = "to List";
            this.BtnProdListAdd.Click += new System.EventHandler(this.BtnProdListAdd_Click);
            // 
            // BtnProdListRemove
            // 
            this.BtnProdListRemove.Enabled = false;
            this.BtnProdListRemove.ImageLarge = global::DU_Industry_Tool.Properties.Resources.Minus_Red_Button;
            this.BtnProdListRemove.ImageSmall = global::DU_Industry_Tool.Properties.Resources.Minus_Red_Button;
            this.BtnProdListRemove.KeyTip = "R";
            this.BtnProdListRemove.TextLine1 = "Remove";
            this.BtnProdListRemove.TextLine2 = "from List";
            this.BtnProdListRemove.Click += new System.EventHandler(this.BtnProdListRemove_Click);
            // 
            // BtnProdListClose
            // 
            this.BtnProdListClose.Enabled = false;
            this.BtnProdListClose.ImageLarge = global::DU_Industry_Tool.Properties.Resources.dialog_cancel;
            this.BtnProdListClose.ImageSmall = global::DU_Industry_Tool.Properties.Resources.dialog_cancel;
            this.BtnProdListClose.KeyTip = "C";
            this.BtnProdListClose.TextLine1 = "Close";
            this.BtnProdListClose.TextLine2 = "List";
            this.BtnProdListClose.Click += new System.EventHandler(this.BtnProdListClose_Click);
            // 
            // RibbonGroupProductionList3
            // 
            this.RibbonGroupProductionList3.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.CbRecentLists,
            this.BtnClearProdLists});
            this.RibbonGroupProductionList3.MinimumSize = Krypton.Ribbon.GroupItemSize.Large;
            // 
            // CbRecentLists
            // 
            this.CbRecentLists.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbRecentLists.DropDownWidth = 300;
            this.CbRecentLists.FormattingEnabled = false;
            this.CbRecentLists.ItemHeight = 19;
            this.CbRecentLists.MinimumSize = new System.Drawing.Size(300, 0);
            this.CbRecentLists.Text = "";
            this.CbRecentLists.SelectionChangeCommitted += new System.EventHandler(this.CbRecentLists_SelectionChangeCommitted);
            // 
            // BtnClearProdLists
            // 
            this.BtnClearProdLists.ImageLarge = global::DU_Industry_Tool.Properties.Resources.gtk_stop;
            this.BtnClearProdLists.ImageSmall = global::DU_Industry_Tool.Properties.Resources.gtk_stop;
            this.BtnClearProdLists.TextLine1 = "Clear list";
            this.BtnClearProdLists.Click += new System.EventHandler(this.BtnClearProdLists_Click);
            // 
            // RibbonGroupSetup
            // 
            this.RibbonGroupSetup.AllowCollapsed = false;
            this.RibbonGroupSetup.Image = global::DU_Industry_Tool.Properties.Resources.Gear;
            this.RibbonGroupSetup.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple2});
            this.RibbonGroupSetup.TextLine1 = "Setup";
            // 
            // kryptonRibbonGroupTriple2
            // 
            this.kryptonRibbonGroupTriple2.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.BtnTalents,
            this.BtnOreValues,
            this.BtnSchematics});
            // 
            // BtnTalents
            // 
            this.BtnTalents.ImageLarge = global::DU_Industry_Tool.Properties.Resources.Orange_Ball;
            this.BtnTalents.ImageSmall = global::DU_Industry_Tool.Properties.Resources.Orange_Ball;
            this.BtnTalents.KeyTip = "T";
            this.BtnTalents.TextLine1 = "Talents";
            this.BtnTalents.Click += new System.EventHandler(this.BtnTalents_Click);
            // 
            // BtnOreValues
            // 
            this.BtnOreValues.ImageLarge = global::DU_Industry_Tool.Properties.Resources.ore_bauxite;
            this.BtnOreValues.ImageSmall = global::DU_Industry_Tool.Properties.Resources.ore_bauxite;
            this.BtnOreValues.KeyTip = "O";
            this.BtnOreValues.TextLine1 = "Ore Values";
            this.BtnOreValues.Click += new System.EventHandler(this.BtnOreValues_Click);
            // 
            // BtnSchematics
            // 
            this.BtnSchematics.ImageLarge = global::DU_Industry_Tool.Properties.Resources.Blue_Ball;
            this.BtnSchematics.ImageSmall = global::DU_Industry_Tool.Properties.Resources.Blue_Ball;
            this.BtnSchematics.KeyTip = "E";
            this.BtnSchematics.TextLine1 = "Schematics";
            this.BtnSchematics.Click += new System.EventHandler(this.BtnSchematics_Click);
            // 
            // rbnTabThemes
            // 
            this.rbnTabThemes.Groups.AddRange(new Krypton.Ribbon.KryptonRibbonGroup[] {
            this.rbnGrpThemes});
            this.rbnTabThemes.Text = "Themes";
            // 
            // rbnGrpThemes
            // 
            this.rbnGrpThemes.AllowCollapsed = false;
            this.rbnGrpThemes.Image = null;
            this.rbnGrpThemes.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.rbnTrippleThemes});
            this.rbnGrpThemes.TextLine1 = "Themes";
            // 
            // rbnTrippleThemes
            // 
            this.rbnTrippleThemes.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.rbnBtnThemeBlue,
            this.rbnBtnThemeSilver,
            this.rbnBtnThemeBlack});
            this.rbnTrippleThemes.MinimumSize = Krypton.Ribbon.GroupItemSize.Large;
            // 
            // rbnBtnThemeBlue
            // 
            this.rbnBtnThemeBlue.ImageLarge = global::DU_Industry_Tool.Properties.Resources.Blue_Ball;
            this.rbnBtnThemeBlue.ImageSmall = global::DU_Industry_Tool.Properties.Resources.Blue_Ball;
            this.rbnBtnThemeBlue.TextLine1 = "Office 2010 - Blue";
            this.rbnBtnThemeBlue.Click += new System.EventHandler(this.RbOffice2010Blue_Click);
            // 
            // rbnBtnThemeSilver
            // 
            this.rbnBtnThemeSilver.ImageLarge = global::DU_Industry_Tool.Properties.Resources.Grey_Ball;
            this.rbnBtnThemeSilver.ImageSmall = global::DU_Industry_Tool.Properties.Resources.Grey_Ball;
            this.rbnBtnThemeSilver.KeyTip = "S";
            this.rbnBtnThemeSilver.TextLine1 = "Office 2010 - Silver";
            this.rbnBtnThemeSilver.Click += new System.EventHandler(this.RbOffice2010BSilver_Click);
            // 
            // rbnBtnThemeBlack
            // 
            this.rbnBtnThemeBlack.ImageLarge = global::DU_Industry_Tool.Properties.Resources.Yellow_Ball;
            this.rbnBtnThemeBlack.ImageSmall = global::DU_Industry_Tool.Properties.Resources.Yellow_Ball;
            this.rbnBtnThemeBlack.KeyTip = "A";
            this.rbnBtnThemeBlack.TextLine1 = "Office 365 - White";
            this.rbnBtnThemeBlack.Click += new System.EventHandler(this.RbOffice365White_Click);
            // 
            // rbnTabOptions
            // 
            this.rbnTabOptions.Groups.AddRange(new Krypton.Ribbon.KryptonRibbonGroup[] {
            this.RibbonGroupOptions,
            this.RibbonGroupAbout});
            this.rbnTabOptions.KeyTip = "O";
            this.rbnTabOptions.Text = "Options";
            // 
            // RibbonGroupOptions
            // 
            this.RibbonGroupOptions.AllowCollapsed = false;
            this.RibbonGroupOptions.Image = null;
            this.RibbonGroupOptions.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.RibbonGroupOptionsLines1});
            this.RibbonGroupOptions.TextLine1 = "Options";
            // 
            // RibbonGroupOptionsLines1
            // 
            this.RibbonGroupOptionsLines1.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.CbStartupProdList,
            this.CbRestoreWindow,
            this.CbFullSchematicQty});
            // 
            // CbStartupProdList
            // 
            this.CbStartupProdList.KeyTip = "L";
            this.CbStartupProdList.TextLine1 = "Load Prod.list on launch";
            this.CbStartupProdList.CheckedChanged += new System.EventHandler(this.CbStartupProdList_CheckedChanged);
            // 
            // CbRestoreWindow
            // 
            this.CbRestoreWindow.Checked = true;
            this.CbRestoreWindow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbRestoreWindow.TextLine1 = "Restore window";
            this.CbRestoreWindow.ToolTipValues.Description = "Restore window position/size on start";
            this.CbRestoreWindow.ToolTipValues.EnableToolTips = true;
            this.CbRestoreWindow.CheckedChanged += new System.EventHandler(this.CbRestoreWindow_CheckedChanged);
            // 
            // CbFullSchematicQty
            // 
            this.CbFullSchematicQty.TextLine1 = "Full schematic quantities?";
            this.CbFullSchematicQty.CheckedChanged += new System.EventHandler(this.CbFullSchematicQty_CheckedChanged);
            // 
            // RibbonGroupAbout
            // 
            this.RibbonGroupAbout.AllowCollapsed = false;
            this.RibbonGroupAbout.Image = null;
            this.RibbonGroupAbout.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.RibbonGroupAboutLines1});
            this.RibbonGroupAbout.KeyTipDialogLauncher = "OA";
            this.RibbonGroupAbout.KeyTipGroup = "A";
            this.RibbonGroupAbout.TextLine1 = "About";
            // 
            // RibbonGroupAboutLines1
            // 
            this.RibbonGroupAboutLines1.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.RibbonBtnAbout});
            this.RibbonGroupAboutLines1.MinimumSize = Krypton.Ribbon.GroupItemSize.Large;
            // 
            // RibbonBtnAbout
            // 
            this.RibbonBtnAbout.ImageLarge = ((System.Drawing.Image)(resources.GetObject("RibbonBtnAbout.ImageLarge")));
            this.RibbonBtnAbout.ImageSmall = ((System.Drawing.Image)(resources.GetObject("RibbonBtnAbout.ImageSmall")));
            this.RibbonBtnAbout.KeyTip = "A";
            this.RibbonBtnAbout.TextLine1 = "About";
            this.RibbonBtnAbout.Click += new System.EventHandler(this.RibbonButtonAboutClick);
            // 
            // kryptonDockableWorkspace
            // 
            this.kryptonDockableWorkspace.ActivePage = this.kryptonPage1;
            this.kryptonDockableWorkspace.AutoHiddenHost = false;
            this.kryptonDockableWorkspace.CompactFlags = Krypton.Workspace.CompactFlags.AtLeastOneVisibleCell;
            this.kryptonDockableWorkspace.ContainerBackStyle = Krypton.Toolkit.PaletteBackStyle.TabDock;
            this.kryptonDockableWorkspace.Dock = System.Windows.Forms.DockStyle.Left;
            this.kryptonDockableWorkspace.Location = new System.Drawing.Point(0, 136);
            this.kryptonDockableWorkspace.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonDockableWorkspace.MinimumSize = new System.Drawing.Size(420, 400);
            this.kryptonDockableWorkspace.Name = "kryptonDockableWorkspace";
            this.kryptonDockableWorkspace.PaletteMode = Krypton.Toolkit.PaletteMode.Office2010Blue;
            // 
            // 
            // 
            this.kryptonDockableWorkspace.Root.Children.AddRange(new System.ComponentModel.Component[] {
            this.kryptonWorkspaceCell1});
            this.kryptonDockableWorkspace.Root.UniqueName = "D51970B3EA2C496AD51970B3EA2C496A";
            this.kryptonDockableWorkspace.Root.WorkspaceControl = this.kryptonDockableWorkspace;
            this.kryptonDockableWorkspace.SeparatorStyle = Krypton.Toolkit.SeparatorStyle.HighProfile;
            this.kryptonDockableWorkspace.ShowMaximizeButton = false;
            this.kryptonDockableWorkspace.Size = new System.Drawing.Size(468, 860);
            this.kryptonDockableWorkspace.SplitterWidth = 8;
            this.kryptonDockableWorkspace.TabIndex = 0;
            this.kryptonDockableWorkspace.TabStop = true;
            this.kryptonDockableWorkspace.WorkspaceCellAdding += new System.EventHandler<Krypton.Workspace.WorkspaceCellEventArgs>(this.KryptonDockableWorkspace_WorkspaceCellAdding);
            // 
            // kryptonPage1
            // 
            this.kryptonPage1.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage1.Controls.Add(this.treeView);
            this.kryptonPage1.Controls.Add(this.searchPanel);
            this.kryptonPage1.Flags = 32;
            this.kryptonPage1.LastVisibleSet = true;
            this.kryptonPage1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonPage1.MinimumSize = new System.Drawing.Size(400, 450);
            this.kryptonPage1.Name = "kryptonPage1";
            this.kryptonPage1.Size = new System.Drawing.Size(460, 829);
            this.kryptonPage1.Text = "Recipes Explorer";
            this.kryptonPage1.TextDescription = "";
            this.kryptonPage1.TextTitle = "";
            this.kryptonPage1.ToolTipTitle = "Page ToolTip";
            this.kryptonPage1.UniqueName = "38D886AD20CD402D38D886AD20CD402D";
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView.Location = new System.Drawing.Point(0, 116);
            this.treeView.Margin = new System.Windows.Forms.Padding(4);
            this.treeView.MinimumSize = new System.Drawing.Size(399, 399);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(460, 713);
            this.treeView.TabIndex = 3;
            // 
            // searchPanel
            // 
            this.searchPanel.AutoSize = true;
            this.searchPanel.Controls.Add(this.LblSearchHint);
            this.searchPanel.Controls.Add(this.SearchBox);
            this.searchPanel.Controls.Add(this.lblQty);
            this.searchPanel.Controls.Add(this.CbNanoOnly);
            this.searchPanel.Controls.Add(this.SearchButton);
            this.searchPanel.Controls.Add(this.QuantityBox);
            this.searchPanel.Controls.Add(this.PreviousButton);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(0, 0);
            this.searchPanel.Margin = new System.Windows.Forms.Padding(4);
            this.searchPanel.MinimumSize = new System.Drawing.Size(0, 80);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(460, 116);
            this.searchPanel.TabIndex = 0;
            // 
            // LblSearchHint
            // 
            this.LblSearchHint.LabelStyle = Krypton.Toolkit.LabelStyle.GroupBoxCaption;
            this.LblSearchHint.Location = new System.Drawing.Point(87, 40);
            this.LblSearchHint.Margin = new System.Windows.Forms.Padding(2);
            this.LblSearchHint.Name = "LblSearchHint";
            this.LblSearchHint.Size = new System.Drawing.Size(31, 31);
            this.LblSearchHint.StateNormal.AdjacentGap = 2;
            this.LblSearchHint.StateNormal.DrawFocus = Krypton.Toolkit.InheritBool.True;
            this.LblSearchHint.StateNormal.ShortText.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearchHint.StateNormal.ShortText.Hint = Krypton.Toolkit.PaletteTextHint.AntiAlias;
            this.LblSearchHint.TabIndex = 6;
            this.LblSearchHint.ToolTipValues.Description = "Search like in DU, but you can also use * as wildcard! Use up/down keys to select" +
    " an entry, then ENTER to start calculation.";
            this.LblSearchHint.ToolTipValues.EnableToolTips = true;
            this.LblSearchHint.ToolTipValues.Heading = "";
            this.LblSearchHint.Values.Text = "?";
            // 
            // SearchBox
            // 
            this.SearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.acMenu.SetAutocompleteMenu(this.SearchBox, null);
            this.SearchBox.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchBox.Location = new System.Drawing.Point(4, 4);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(449, 31);
            this.SearchBox.TabIndex = 0;
            this.SearchBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchBox_KeyPress);
            // 
            // lblQty
            // 
            this.lblQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQty.Location = new System.Drawing.Point(282, 41);
            this.lblQty.Margin = new System.Windows.Forms.Padding(4);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(43, 24);
            this.lblQty.TabIndex = 5;
            this.lblQty.Values.Text = "Qty.:";
            // 
            // CbNanoOnly
            // 
            this.CbNanoOnly.Location = new System.Drawing.Point(4, 85);
            this.CbNanoOnly.Margin = new System.Windows.Forms.Padding(4);
            this.CbNanoOnly.Name = "CbNanoOnly";
            this.CbNanoOnly.Size = new System.Drawing.Size(160, 24);
            this.CbNanoOnly.TabIndex = 4;
            this.CbNanoOnly.Values.Text = "filter nanocraftable?";
            this.CbNanoOnly.CheckedChanged += new System.EventHandler(this.CbNanoOnly_CheckedChanged);
            // 
            // SearchButton
            // 
            this.SearchButton.AutoSize = true;
            this.SearchButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchButton.Location = new System.Drawing.Point(4, 40);
            this.SearchButton.Margin = new System.Windows.Forms.Padding(4);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(77, 32);
            this.SearchButton.TabIndex = 1;
            this.SearchButton.Values.Text = "Search";
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // QuantityBox
            // 
            this.QuantityBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.QuantityBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.QuantityBox.DropDownWidth = 60;
            this.QuantityBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityBox.IntegralHeight = false;
            this.QuantityBox.Items.AddRange(new object[] {
            "1",
            "2",
            "5",
            "10",
            "20",
            "50",
            "100",
            "200",
            "250",
            "500",
            "1000",
            "2000",
            "5000",
            "10000",
            "20000",
            "50000",
            "100000",
            "200000",
            "500000",
            "1000000",
            "2000000",
            "5000000",
            "10000000"});
            this.QuantityBox.Location = new System.Drawing.Point(326, 40);
            this.QuantityBox.Margin = new System.Windows.Forms.Padding(4, 6, 0, 0);
            this.QuantityBox.MaxDropDownItems = 15;
            this.QuantityBox.MaxLength = 8;
            this.QuantityBox.Name = "QuantityBox";
            this.QuantityBox.Size = new System.Drawing.Size(130, 25);
            this.QuantityBox.TabIndex = 2;
            this.QuantityBox.Text = "10000000";
            this.QuantityBox.SelectionChangeCommitted += new System.EventHandler(this.QuantityBoxOnSelectionChangeCommitted);
            this.QuantityBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.QuantityBox_KeyPress);
            // 
            // PreviousButton
            // 
            this.PreviousButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PreviousButton.AutoSize = true;
            this.PreviousButton.Enabled = false;
            this.PreviousButton.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PreviousButton.Location = new System.Drawing.Point(416, 78);
            this.PreviousButton.Margin = new System.Windows.Forms.Padding(4);
            this.PreviousButton.Name = "PreviousButton";
            this.PreviousButton.Size = new System.Drawing.Size(40, 34);
            this.PreviousButton.TabIndex = 3;
            this.PreviousButton.Values.Text = "<<";
            this.PreviousButton.Click += new System.EventHandler(this.PreviousButton_Click);
            // 
            // kryptonWorkspaceCell1
            // 
            this.kryptonWorkspaceCell1.AllowPageDrag = true;
            this.kryptonWorkspaceCell1.AllowPageReorder = false;
            this.kryptonWorkspaceCell1.AllowTabFocus = false;
            this.kryptonWorkspaceCell1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonWorkspaceCell1.Button.ButtonDisplayLogic = Krypton.Navigator.ButtonDisplayLogic.None;
            this.kryptonWorkspaceCell1.Button.CloseButtonAction = Krypton.Navigator.CloseButtonAction.None;
            this.kryptonWorkspaceCell1.Button.CloseButtonDisplay = Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonWorkspaceCell1.Button.ContextButtonAction = Krypton.Navigator.ContextButtonAction.None;
            this.kryptonWorkspaceCell1.Button.ContextButtonDisplay = Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonWorkspaceCell1.Button.NextButtonAction = Krypton.Navigator.DirectionButtonAction.None;
            this.kryptonWorkspaceCell1.Button.NextButtonDisplay = Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonWorkspaceCell1.Button.PreviousButtonAction = Krypton.Navigator.DirectionButtonAction.None;
            this.kryptonWorkspaceCell1.Button.PreviousButtonDisplay = Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonWorkspaceCell1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonWorkspaceCell1.MaximumSize = new System.Drawing.Size(462, 2000);
            this.kryptonWorkspaceCell1.Name = "kryptonWorkspaceCell1";
            this.kryptonWorkspaceCell1.NavigatorMode = Krypton.Navigator.NavigatorMode.BarCheckButtonGroupOutside;
            this.kryptonWorkspaceCell1.Pages.AddRange(new Krypton.Navigator.KryptonPage[] {
            this.kryptonPage1});
            this.kryptonWorkspaceCell1.SelectedIndex = 0;
            this.kryptonWorkspaceCell1.UniqueName = "B46823ED744B4A87B46823ED744B4A87";
            // 
            // kryptonNavigator1
            // 
            this.kryptonNavigator1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonNavigator1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kryptonNavigator1.Bar.BarMultiline = Krypton.Navigator.BarMultiline.Multiline;
            this.kryptonNavigator1.Bar.TabBorderStyle = Krypton.Toolkit.TabBorderStyle.RoundedEqualMedium;
            this.kryptonNavigator1.Location = new System.Drawing.Point(489, 61);
            this.kryptonNavigator1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonNavigator1.Name = "kryptonNavigator1";
            this.kryptonNavigator1.Size = new System.Drawing.Size(1097, 747);
            this.kryptonNavigator1.StateCommon.CheckButton.Content.Image.ImageH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonNavigator1.StateCommon.CheckButton.Content.Image.ImageV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonNavigator1.StateCommon.CheckButton.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonNavigator1.StateCommon.CheckButton.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.kryptonNavigator1.TabIndex = 7;
            this.kryptonNavigator1.SelectedPageChanged += new System.EventHandler(this.KryptonNavigator1OnSelectedPageChanged);
            this.kryptonNavigator1.TabCountChanged += new System.EventHandler(this.KryptonNavigator1_TabCountChanged);
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
            this.kryptonManager.GlobalPaletteMode = Krypton.Toolkit.PaletteModeManager.Office365White;
            // 
            // kryptonRibbonGroupGallery1
            // 
            this.kryptonRibbonGroupGallery1.ImageList = null;
            // 
            // acMenu
            // 
            this.acMenu.AllowsTabKey = true;
            this.acMenu.AppearInterval = 400;
            this.acMenu.Colors = ((AutocompleteMenuNS.Colors)(resources.GetObject("acMenu.Colors")));
            this.acMenu.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.acMenu.ImageList = null;
            this.acMenu.Items = new string[0];
            this.acMenu.LeftPadding = 2;
            this.acMenu.MaximumSize = new System.Drawing.Size(400, 500);
            this.acMenu.SearchPattern = ".*";
            this.acMenu.TargetControlWrapper = null;
            this.acMenu.Selected += new System.EventHandler<AutocompleteMenuNS.SelectedEventArgs>(this.acMenu_Selected);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1860, 996);
            this.Controls.Add(this.kryptonNavigator1);
            this.Controls.Add(this.kryptonDockableWorkspace);
            this.Controls.Add(this.kryptonRibbon);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1042, 798);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DU Industry Tool (Mercury)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.Resize += new System.EventHandler(this.OnMainformResize);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace)).EndInit();
            this.kryptonDockableWorkspace.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).EndInit();
            this.kryptonPage1.ResumeLayout(false);
            this.kryptonPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchPanel)).EndInit();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuantityBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspaceCell1)).EndInit();
            this.kryptonWorkspaceCell1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).EndInit();
            this.kryptonNavigator1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageListSmall;
        private Krypton.Ribbon.KryptonRibbon kryptonRibbon;
        private Krypton.Ribbon.KryptonRibbonTab rbnTabOptions;
        private Krypton.Ribbon.KryptonRibbonGroup RibbonGroupAbout;
        private Krypton.Ribbon.KryptonRibbonGroupLines RibbonGroupAboutLines1;
        private Krypton.Ribbon.KryptonRibbonGroupButton RibbonBtnAbout;
        private Krypton.Toolkit.KryptonContextMenuItem ribbonAppButtonExit;
        private Krypton.Toolkit.KryptonContextMenuItem buttonUpdateMarketValues;
        private Krypton.Toolkit.KryptonContextMenuItem buttonFilterToMarket;
        private Krypton.Toolkit.KryptonContextMenuItem buttonExportToSpreadsheet;
        private Krypton.Toolkit.KryptonContextMenuItem buttonFactoryBreakdownForSelected;
        private Krypton.Toolkit.KryptonManager kryptonManager;
        private Krypton.Navigator.KryptonNavigator kryptonNavigator1;
        private Krypton.Docking.KryptonDockingManager kryptonDockingManager;
        private Krypton.Docking.KryptonDockableWorkspace kryptonDockableWorkspace;
        private Krypton.Workspace.KryptonWorkspaceCell kryptonWorkspaceCell1;
        private Krypton.Navigator.KryptonPage kryptonPage1;
        private System.Windows.Forms.TreeView treeView;
        private Krypton.Toolkit.KryptonPanel searchPanel;
        private Krypton.Toolkit.KryptonButton SearchButton;
        private Krypton.Toolkit.KryptonButton PreviousButton;
        private Krypton.Toolkit.KryptonComboBox QuantityBox;
        private Krypton.Ribbon.KryptonRibbonTab rbnTabTools;
        private Krypton.Ribbon.KryptonRibbonGroup RibbonGroupProduction;
        private Krypton.Ribbon.KryptonRibbonGroupGallery kryptonRibbonGroupGallery1;
        private Krypton.Ribbon.KryptonRibbonGroupTriple RibbonTripleProductionList1;
        private Krypton.Ribbon.KryptonRibbonGroupButton RbnBtnProductionList;
        private Krypton.Ribbon.KryptonRibbonTab rbnTabThemes;
        private Krypton.Ribbon.KryptonRibbonGroup rbnGrpThemes;
        private Krypton.Ribbon.KryptonRibbonGroupTriple rbnTrippleThemes;
        private Krypton.Ribbon.KryptonRibbonGroupButton rbnBtnThemeBlue;
        private Krypton.Ribbon.KryptonRibbonGroupButton rbnBtnThemeSilver;
        private Krypton.Ribbon.KryptonRibbonGroupButton rbnBtnThemeBlack;
        private Krypton.Toolkit.KryptonCommand cmdThemeBlue;
        private Krypton.Ribbon.KryptonRibbonGroupButton RbnBtnProductionListLoad;
        private Krypton.Ribbon.KryptonRibbonGroupButton RbnBtnProductionListSave;
        private Krypton.Ribbon.KryptonRibbonGroup RibbonGroupSetup;
        private Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple2;
        private Krypton.Ribbon.KryptonRibbonGroupButton BtnOreValues;
        private Krypton.Ribbon.KryptonRibbonGroupButton BtnSchematics;
        private Krypton.Ribbon.KryptonRibbonGroupButton BtnTalents;
        private Krypton.Ribbon.KryptonRibbonGroupTriple RibbonTripleProductionList2;
        private Krypton.Ribbon.KryptonRibbonGroupButton BtnProdListAdd;
        private Krypton.Ribbon.KryptonRibbonGroupButton BtnProdListRemove;
        private Krypton.Ribbon.KryptonRibbonGroupButton BtnProdListClose;
        private Krypton.Ribbon.KryptonRibbonGroup RibbonGroupOptions;
        private Krypton.Ribbon.KryptonRibbonGroupLines RibbonGroupOptionsLines1;
        private Krypton.Ribbon.KryptonRibbonGroupCheckBox CbStartupProdList;
        private Krypton.Ribbon.KryptonRibbonGroupLines RibbonGroupProductionList3;
        private Krypton.Ribbon.KryptonRibbonGroupComboBox CbRecentLists;
        private Krypton.Ribbon.KryptonRibbonGroupCheckBox CbRestoreWindow;
        private Krypton.Toolkit.KryptonCheckBox CbNanoOnly;
        private Krypton.Ribbon.KryptonRibbonGroupCheckBox CbFullSchematicQty;
        private Krypton.Toolkit.KryptonLabel lblQty;
        private System.Windows.Forms.TextBox SearchBox;
        private AutocompleteMenuNS.AutocompleteMenu acMenu;
        private Krypton.Toolkit.KryptonLabel LblSearchHint;
        private Krypton.Ribbon.KryptonRibbonGroupButton BtnClearProdLists;
    }
}