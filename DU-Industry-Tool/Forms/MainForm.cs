// ReSharper disable LocalizableElement
// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using BrightIdeasSoftware;
using ClosedXML.Excel;
using DU_Helpers;
using DU_Industry_Tool.Interfaces;
using Krypton.Navigator;
using Krypton.Ribbon;
using Krypton.Workspace;
using Krypton.Toolkit;
using Newtonsoft.Json;
using Point = System.Drawing.Point;

namespace DU_Industry_Tool
{
    public partial class MainForm : KryptonForm
    {
        private bool _startUp = true;
        private IndustryManager _manager;
        private MarketManager _market;
        private bool _marketFiltered;
        private readonly List<string> _breadcrumbs = new List<string>();
        private bool _navUpdating;
        private decimal _overrideQty;
        private List<string> sortedRecipes;

        public MainForm()
        {
            InitializeComponent();

            CultureInfo.CurrentCulture = new CultureInfo("en-us");
            QuantityBox.SelectedIndex = 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Setup docking functionality
            var w = kryptonDockingManager.ManageWorkspace(kryptonDockableWorkspace);
            if (w != null)
            {
                kryptonDockingManager.ManageControl(kryptonPage1, w);
            }
            kryptonDockingManager.ManageFloating(this);

            // Do not allow the left-side page to be closed or made auto hidden/docked
            kryptonPage1.ClearFlags(KryptonPageFlags.DockingAllowAutoHidden |
                            KryptonPageFlags.DockingAllowDocked |
                            KryptonPageFlags.DockingAllowClose);

            OnMainformResize(sender, e);

            // load settings before IndustryManager
            if (SettingsMgr.LoadSettings())
            {
                SettingsMgr.SaveSettings();
                ApplySettings();
            }

            Utils.ScalingFactor = CurrentAutoScaleDimensions.Width / 96;

            _manager = new IndustryManager();
            _market = new MarketManager();

            kryptonPage1.Flags = 0;
            kryptonPage1.ClearFlags(KryptonPageFlags.DockingAllowDocked);
            kryptonPage1.ClearFlags(KryptonPageFlags.DockingAllowClose);

            kryptonNavigator1.Dock = DockStyle.None;
            kryptonNavigator1.Dock = DockStyle.Fill;
            OnMainformResize(null, null);

            DUData.IndyMgrInstance = _manager;

            SearchHelper.NoResultsIfEmpty = false;
            SearchHelper.SearchableItems = DUData.RecipeNames.ToList();
            SearchHelper.MinimumSearchLength = 2;

            acMenu.SetAutocompleteMenu(SearchBox, acMenu);
            acMenu.SearchPattern = ".*";
            SearchBox.TextChanged += SearchBoxOnTextChanged;
            SearchBox.KeyDown += SearchBoxOnKeyDown;

            _manager.Databindings.ProductionListChanged += ProductionListUpdates;
            ProductionListUpdates(null);

            LoadTree();

            if (CbStartupProdList.Checked)
            {
                LoadAndRunProductionList(SettingsMgr.GetStr(SettingsEnum.LastProductionList));
            }
            
            _startUp = false;
        }

        public IndustryManager IndyManager => _manager;

        private void LoadTree()
        {
            treeView.NodeMouseClick += Treeview_NodeClick;
            LoadTreeData();
        }

        private void LoadTreeData()
        {
            treeView.AfterSelect -= Treeview_AfterSelect;
            sortedRecipes = new List<string>();
            treeView.BeginUpdate();
            treeView.Nodes.Clear();
            foreach(var group in DUData.Groupnames)
            {
                var groupNode = new TreeNode(group);
                foreach(var recipe in DUData.Recipes.Where(
                    x => x.Value?.ParentGroupName?.Equals(group, StringComparison.CurrentCultureIgnoreCase) == true &&
                         (!CbNanoOnly.Checked || x.Value.Nanocraftable))
                          .OrderBy(r => r.Value.Level).ThenBy(r => r.Value.Name)
                          .Select(x => x.Value))
                {
                    sortedRecipes.Add(recipe.Name);
                    var recipeNode = new TreeNode(recipe.Name) { Tag = recipe };
                    recipe.Node = recipeNode;
                    groupNode.Nodes.Add(recipeNode);
                }
                if (groupNode.Nodes.Count > 0)
                {
                    groupNode.Text += $"   ({groupNode.Nodes.Count})";
                    treeView.Nodes.Add(groupNode);
                }
            }
            treeView.EndUpdate();
            sortedRecipes.Sort();
            treeView.AfterSelect += Treeview_AfterSelect;
            CbNanoOnly.Text = $"filter nanocraftable?     ({sortedRecipes.Count})";
        }

        #region Recipe search and selection

        private void CbNanoOnly_CheckedChanged(object sender, EventArgs e)
        {
            LoadTreeData();
        }

        private void Treeview_NodeClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null && treeView.SelectedNode == e.Node)
            {
                SelectRecipe(e.Node);
            }
        }

        private void Treeview_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectRecipe(e?.Node);
        }

        private void SelectRecipe(TreeNode e)
        {
            if (!(e?.Tag is SchematicRecipe recipe))
            {
                return;
            }

            if (_breadcrumbs.Count == 0 || _breadcrumbs.LastOrDefault() != recipe.Name)
            {
                _breadcrumbs.Add(recipe.Name);
            }
            PreviousButton.Enabled = _breadcrumbs.Count > 0;

            if (!DUData.IsIgnorableTitle(recipe.Name))
            {
                SearchBox.Text = recipe.Name;
            }
            _navUpdating = true;

            decimal cnt = 1;
            if (!DUData.ProductionListMode)
            {
                if (_overrideQty > 0)
                {
                    cnt = _overrideQty;
                }
                else
                if (QuantityBox.SelectedItem == null || !decimal.TryParse((string)QuantityBox.SelectedItem, out cnt))
                {
                    decimal.TryParse(QuantityBox.Text, out cnt);
                }
                cnt = Math.Max(1, cnt);
            }

            IContentDocument newDoc = null;
            try
            {
                newDoc = NewDocument(recipe.Name);
                if (newDoc == null) return;
            }
            finally
            {
                _navUpdating = false;
                ProductionListUpdates(null);
            }

            // ***** Primary Calculation *****
            Calculator.ResetRecipeName();
            Calculator.Initialize();
            Calculator.ProductQuantity = cnt;
            Calculator.CalculateRecipe(recipe.Key, cnt, silent: true);
            var calc = Calculator.Get(recipe.Key, Guid.Empty);
            _overrideQty = 0; // must be reset here!

            // Pass data on to newly created tab
            if (DUData.ProductionListMode)
            {
                newDoc.IsProductionList = true;
                newDoc.RecalcProductionListClick = ProductionListRecalc_Click;
            }
            newDoc.ItemClick = OpenRecipe;
            newDoc.IndustryClick = LabelIndustry_Click;
            newDoc.LinkClick = Link_Click;
            newDoc.SetCalcResult(calc);

            OnMainformResize(null, null);
        }

        private FlowLayoutPanel AddFlowLabel(System.Windows.Forms.Control.ControlCollection cc,
            string lblText, FontStyle fstyle = FontStyle.Regular,
            FlowDirection flow = FlowDirection.TopDown)
        {
            var panel = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = flow,
                Padding = new Padding(0)
            };
            if (!string.IsNullOrEmpty(lblText))
            {
                var lbl = new Label
                {
                    AutoSize = true,
                    Font = new Font(this.Font, fstyle),
                    Padding = new Padding(0),
                    Text = lblText
                };
                panel.Controls.Add(lbl);
            }
            cc.Add(panel);
            return panel;
        }

        private KryptonLabel AddKLinkLabel(System.Windows.Forms.Control.ControlCollection cc, string lblText, FontStyle fstyle = FontStyle.Regular)
        {
            var lbl = new KryptonLinkLabel
            {
                AutoSize = true,
                Font = new Font(this.Font, fstyle),
                Padding = new Padding(0),
                Text = lblText
            };
            cc.Add(lbl);
            return lbl;
        }

        private KryptonLabel AddLinkedLabel(System.Windows.Forms.Control.ControlCollection cc, string lblText, string lblKey)
        {
            var lbl = AddKLinkLabel(cc, lblText, FontStyle.Underline);
            //lbl.ForeColor = Color.CornflowerBlue;
            lbl.Text = lblText;
            lbl.Tag = lblKey;
            return lbl;
        }

        private void OpenRecipe(object sender, EventArgs e)
        {
            if (!(sender is TreeListView tree) || !(tree.SelectedItem?.RowObject is RecipeCalculation r))
            {
                return;
            }
            // Remove " (B)" byproduct marker
            var search = (string.IsNullOrEmpty(r.Entry) ? r.Section : r.Entry).TrimLastStr(DUData.ByproductMarker);
            if (DUData.IsIgnorableTitle(search))
            {
                return;
            }
            SearchBox.Text = search;
            _overrideQty = r.Qty;
            SearchForRecipe(search);
        }

        private void SearchByLink(string text)
        {
            if (string.IsNullOrEmpty(text)) return;
            _overrideQty = 0;
            var hashpos = text.IndexOf('#');
            if (hashpos > 0)
            {
                var amount = text.Substring(hashpos+1);
                if (!decimal.TryParse(amount, out _overrideQty))
                {
                    _overrideQty = 0;
                }
                text = text.Substring(0, hashpos);
            }

            if (!DUData.Recipes.ContainsKey(text)) return;
            var r = DUData.Recipes[text];
            if (r.Node != null)
                SelectRecipe(r.Node);
            else
                SearchForRecipe(r.Name);
        }

        private static int RecipeNameComparer(SchematicRecipe x, SchematicRecipe y)
        {
            return string.Compare(x.Name, y.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_breadcrumbs.Count == 0) return;
                var entry = _breadcrumbs.LastOrDefault();
                if (entry == SearchBox.Text)
                {
                    _breadcrumbs.Remove(entry);
                    entry = _breadcrumbs.LastOrDefault();
                }
                if (string.IsNullOrEmpty(entry)) return;
                SearchBox.Text = entry;
                _breadcrumbs.Remove(entry);
                SearchForRecipe(entry);
            }
            finally
            {
                PreviousButton.Enabled = _breadcrumbs.Count > 0;
            }
        }

        private void SearchForRecipe(string searchValue)
        {
            if (string.IsNullOrWhiteSpace(searchValue))
            {
                PreviousButton.Enabled = _breadcrumbs.Count > 0;
                return; // Do nothing
            }

            var outerNodes = treeView.Nodes.OfType<TreeNode>();
            TreeNode firstResult = null;
            treeView.BeginUpdate();
            try
            {
                treeView.CollapseAll();
                foreach (var outerNode in outerNodes)
                {
                    foreach (var innerNode in outerNode.Nodes.OfType<TreeNode>())
                    {
                        if (innerNode.Text.IndexOf(searchValue, StringComparison.InvariantCultureIgnoreCase) < 0)
                            continue;
                        innerNode.EnsureVisible();
                        var isExact = innerNode.Text.Equals(searchValue, StringComparison.InvariantCultureIgnoreCase);
                        if (firstResult == null || isExact)
                        {
                            firstResult = innerNode;
                            if (isExact) break;
                        }
                    }
                }
                if (firstResult != null)
                {
                    treeView.SelectedNode = firstResult;
                    treeView.SelectedNode.EnsureVisible();
                }
            }
            finally
            {
                treeView.EndUpdate();
            }
            treeView.Focus();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchForRecipe(SearchBox.Text);
        }

        private void QuantityBoxOnSelectionChangeCommitted(object sender, EventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate()
            {
                if (treeView.SelectedNode == null)
                {
                    SearchButton_Click(sender, e);
                }
                else
                {
                    SelectRecipe(treeView.SelectedNode);
                }
            });
        }

        private void QuantityBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && ((Keys)e.KeyChar != Keys.Back) &&
                ((Keys)e.KeyChar != Keys.Enter) && ((Keys)e.KeyChar != Keys.Tab))
            {
                e.Handled = true;
                return;
            }
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                SearchForRecipe(SearchBox.Text);
            }
        }

        #endregion

        #region Results page delegates

        private void Link_Click(object sender, LinkClickedEventArgs e)
        {
            SearchByLink(e.LinkText);
        }

        private void Label_Click(object sender, EventArgs e)
        {
            Object tag = null;
            if (sender is KryptonLabel klabel) tag = klabel.Tag;
            if (sender is Label label) tag = label.Tag;
            if (tag == null || string.IsNullOrEmpty((string)tag)) return;
            SearchByLink((string)tag);
        }

        private void LabelIndustry_Click(object sender, EventArgs e)
        {
            var tag = "";
            var text = "";
            if (sender is KryptonLabel klabel)
            {
                if (klabel.Tag is string tg) tag = tg;
                text = klabel.Text;
            }
            else
            if (sender is Label label)
            {
                if (label.Tag is string tg) tag = tg;
                text = label.Text;
            }
            if (string.IsNullOrEmpty(tag) && string.IsNullOrEmpty(text))
                return;

            var products = DUData.Recipes.Where(x => x.Value?.Industry != null &&
                    x.Value?.Industry.Equals(text, StringComparison.InvariantCultureIgnoreCase) == true)
                    .Select(x => x.Value).ToList();
            if (products?.Any() != true)
            {
                return;
            }

            products.Sort(RecipeNameComparer);

            var page = kryptonNavigator1.Pages.FirstOrDefault(x => x.Text.StartsWith(DUData.IndyProductsTabTitle));
            if (page == null)
            {
                page = NewPage(DUData.IndyProductsTabTitle, null);
                kryptonNavigator1.Pages.Insert(0, page);
            }
            if (page == null) return;
            kryptonNavigator1.SelectedPage = page;

            page.Controls.Clear();
            AddTabCloseButton(page);

            var panel = AddFlowLabel(page.Controls, text+" produces:", FontStyle.Bold);
            panel.Dock = DockStyle.Top;

            var grid = new TableLayoutPanel
            {
                AutoScroll = true,
                AutoSize = true,
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = products.Count,
                Margin = new Padding(0, 22, 0, 0),
                Padding = new Padding(0, 22, 0, 10)
            };
            page.Controls.Add(grid);

            grid.SuspendLayout();
            foreach (var prod in products)
            {
                AddLinkedLabel(grid.Controls, prod.Name, prod.Key).Click += Label_Click;
            }
            grid.ResumeLayout();
        }

        #endregion

        #region Tools events

        private void UpdateMarketValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var loadForm = new LoadingForm(_market);
            loadForm.ShowDialog(this);
            if (loadForm.DiscardOres)
            {
                // Get rid of them
                List<ulong> toRemove = new List<ulong>();
                foreach(var order in _market.MarketOrders)
                {
                    var recipe =  DUData.Recipes.Values.FirstOrDefault(r => r.NqId == order.Value.ItemType);
                    if (recipe != null && recipe.ParentGroupName == "Ore")
                        toRemove.Add(order.Key);

                }
                foreach (var key in toRemove)
                    _market.MarketOrders.Remove(key);
                _market.SaveData();
            }
            else
            {
                // Process them and leave them so they show in exports
                foreach (var order in _market.MarketOrders)
                {
                    var recipe =  DUData.Recipes.Values.FirstOrDefault(r => r.NqId == order.Value.ItemType);
                    if (recipe != null && recipe.ParentGroupName == "Ore")
                    {
                        var ore = DUData.Ores.FirstOrDefault(o => o.Key.ToLower() == recipe.Key.ToLower());
                        if (ore != null)
                        {
                            var orders = _market.MarketOrders.Values.Where(o => o.ItemType == recipe.NqId && o.BuyQuantity < 0 && DateTime.Now < o.ExpirationDate && o.Price > 0);

                            var bestOrder = orders.OrderBy(o => o.Price).FirstOrDefault();
                            if (bestOrder != null)
                                ore.Value = bestOrder.Price;
                        }
                    }

                }
                DUData.SaveOreValues();
            }
            loadForm.Dispose();
        }

        private void FilterToMarketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_marketFiltered)
            {
                _marketFiltered = false;
                if (sender is ToolStripMenuItem tsItem) tsItem.Text = "Filter to Market";
                else
                if (sender is KryptonContextMenuItem kBtn) kBtn.Text = "Filter to Market";
                treeView.Nodes.Clear();
                foreach (var group in DUData.Recipes.Values.GroupBy(r => r.ParentGroupName))
                {
                    var groupNode = new TreeNode(group.Key);
                    foreach (var recipe in group)
                    {
                        var recipeNode = new TreeNode(recipe.Name)
                        {
                            Tag = recipe
                        };
                        //recipe.Node = recipeNode;
                        groupNode.Nodes.Add(recipeNode);
                    }
                    treeView.Nodes.Add(groupNode);
                }
            }
            else
            {
                _marketFiltered = true;
                if (sender is ToolStripMenuItem tsItem) tsItem.Text = "Unfilter Market";
                    else
                if (sender is KryptonContextMenuItem kBtn) kBtn.Text = "Unfilter Market";
                treeView.Nodes.Clear();
                foreach (var group in  DUData.Recipes.Values.Where(r => _market.MarketOrders.Values.Any(v => v.ItemType == r.NqId)).GroupBy(r => r.ParentGroupName))
                {
                    var groupNode = new TreeNode(group.Key);
                    foreach (var recipe in group)
                    {
                        var recipeNode = new TreeNode(recipe.Name)
                        {
                            Tag = recipe
                        };
                        //recipe.Node = recipeNode;
                        groupNode.Nodes.Add(recipeNode);
                    }
                    treeView.Nodes.Add(groupNode);
                }
            }
        }

        private void ExportToSpreadsheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If market filtered, only exports items with market values.
            // Exports the following:
            // Name, Cost To Make, Market Cost, Time To Make, Profit Margin (with formula),
            // Profit Per Day (with formula), Units Per Day with formula
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Price Data " + DateTime.Now.ToString("yyyy-MM-dd"));

                worksheet.Cell(1, 1).Value = "Name";
                worksheet.Cell(1, 2).Value = "Ore Cost";
                worksheet.Cell(1, 3).Value = "Market Cost";
                worksheet.Cell(1, 4).Value = "Time To Make";
                worksheet.Cell(1, 5).Value = "Profit Margin";
                worksheet.Cell(1, 6).Value = "Profit Per Day";
                worksheet.Cell(1, 7).Value = "Units Per Day";
                worksheet.Cell(1, 8).Value = "Schematic Cost";
                worksheet.Cell(1, 9).Value = "Total Cost";

                worksheet.Row(1).CellsUsed().Style.Font.SetBold();

                int row = 2;

                var recipes =  DUData.Recipes.Values.Where(x => x.ParentGroupName != "Ore").OrderBy(x => x.Name).ToList();
                if (_marketFiltered)
                {
                    recipes =  DUData.Recipes.Values.Where(r =>
                        _market.MarketOrders.Values.Any(v => v.ItemType == r.NqId)).ToList();
                }

                var cnt = 1m;
                if (QuantityBox.SelectedItem == null || !decimal.TryParse((string)QuantityBox.SelectedItem, out cnt))
                {
                    decimal.TryParse(QuantityBox.Text, out cnt);
                }
                cnt = Math.Max(1, cnt);

                Text = $"0 / {recipes.Count}";
                foreach(var recipe in recipes)
                {
                    worksheet.Cell(row, 1).Value = recipe.Name;
                    Calculator.ResetRecipeName();
                    Calculator.Initialize();
                    Calculator.ProductQuantity = cnt;
                    var costToMake = Calculator.CalculateRecipe(recipe.Key, cnt, silent: true);
                    var calc = Calculator.Get(recipe.Key, Guid.Empty);

                    worksheet.Cell(row, 2).Value = Math.Round(costToMake, 2);

                    var orders = _market.MarketOrders.Values.Where(o => o.ItemType == recipe.NqId && o.BuyQuantity < 0 && DateTime.Now < o.ExpirationDate && o.Price > 0);

                    var mostRecentOrder = orders.OrderBy(o => o.Price).FirstOrDefault();
                    var cost = mostRecentOrder?.Price ?? 0;
                    worksheet.Cell(row, 3).Value = cost;
                    worksheet.Cell(row, 4).Value = recipe.Time;
                    worksheet.Cell(row, 5).FormulaR1C1 = "=((R[0]C[-2]-R[0]C[-3])/R[0]C[-2])";
                    //worksheet.Cell(row, 5).Value = cost = ((mostRecentOrder.Price - costToMake) / mostRecentOrder.Price);
                    //worksheet.Cell(row, 5).FormulaR1C1 = "=IF((R[0]C[-2]<>0),(R[0]C[-2]-R[0]C[-3])/R[0]C[-2],0)";
                    //cost = (mostRecentOrder.Price - costToMake)*(86400/recipe.Time);
                    worksheet.Cell(row, 6).FormulaR1C1 = "=(R[0]C[-3]-R[0]C[-4])*(86400/R[0]C[-2])";
                    worksheet.Cell(row, 7).FormulaR1C1 = "=86400/R[0]C[-3]";
                    worksheet.Cell(row, 8).Value = calc.SchematicsCost;
                    worksheet.Cell(row, 9).Value = Math.Round(costToMake + calc.SchematicsCost, 2);
                    row++;
                    if (Utils.MathMod(row, 10) == 0)
                    {
                        Text = $"Export to CSV: {row} / {recipes.Count}";
                    }
                }
                Calculator.ProductQuantity = 1;
                worksheet.Range("A1:G1").Style.Font.Bold = true;
                worksheet.ColumnsUsed().AdjustToContents(1, 50);
                workbook.SaveAs("Item Export " + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx");
                ProductionListUpdates(null);
                MessageBox.Show("Export 'Item export " + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx' in the same folder as this tool!");
            }
        }

        private void FactoryBreakdownForSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Exports an excel sheet with info about how to setup the factory for the selected recipe (aborts if no recipe selected)
            if (!(treeView.SelectedNode?.Tag is SchematicRecipe recipe)) return;
            // Shows the amount of required components, amount per day required, amount per day per industry, and the number of industries you need of that component to provide for 1 of the parent
            // The number of parent parts can be put in as a value
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Factory");
                worksheet.Cell(1, 1).Value = "Number of industries producing " + recipe.Name;
                worksheet.Cell(1, 2).Value = "Produced/day";
                worksheet.Cell(2, 1).Value = 1;
                worksheet.Cell(2, 2).FormulaR1C1 = $"=R[0]C[-1]*(86400/{recipe.Time})";

                worksheet.Cell(1, 3).Value = "Product";
                worksheet.Cell(1, 4).Value = "Required/day";
                worksheet.Cell(1, 5).Value = "Produced/day/industry";
                worksheet.Cell(1, 6).Value = "Num industries required";
                worksheet.Cell(1, 7).Value = "Actual";

                worksheet.Row(1).Style.Font.SetBold();

                var row = 2;
                var ingredients = Calculator.GetIngredientRecipes(recipe.Key).OrderByDescending(i => i.Tier).GroupBy(i => i.Name);
                if (!ingredients?.Any() == true) return;
                try
                {
                    foreach(var group in ingredients)
                    {
                        var groupSum = group.Sum(g => g.Quantity);
                        worksheet.Cell(row, 3).Value = group.First().Name;
                        worksheet.Cell(row, 4).FormulaA1 = $"=B2*{groupSum}";
                        decimal outputMult = 1;
                        var talents = DUData.Talents.Where(t => t.InputTalent == false &&
                                                                t.ApplicableRecipes.Contains(group.First().Name));
                        if (talents?.Any() == true)
                            outputMult += talents.Sum(t => t.Multiplier);
                        if (group.First().Recipe.ParentGroupName != "Ore")
                        {
                            worksheet.Cell(row, 5).Value = (86400 / group.First().Recipe.Time) * group.First().Recipe.Products.First().Quantity * outputMult;
                            worksheet.Cell(row, 6).FormulaR1C1 = "=R[0]C[-2]/R[0]C[-1]";
                            worksheet.Cell(row, 7).FormulaR1C1 = "=ROUNDUP(R[0]C[-1])";
                        }
                        row++;
                    }

                    worksheet.ColumnsUsed().AdjustToContents();
                    workbook.SaveAs($"Factory Plan {recipe.Name} {DateTime.Now:yyyy-MM-dd}.xlsx");
                    MessageBox.Show($"Exported to 'Factory Plan {recipe.Name} { DateTime.Now:yyyy-MM-dd}.xlsx' in the same folder as the exe!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sorry, an error occured during calculation!", "ERROR", MessageBoxButtons.OK);
                    Console.WriteLine(ex);
                }
            }
        }

        #endregion

        #region Mainform events

        private void OnMainformResize(object sender, EventArgs e)
        {
            kryptonNavigator1.Left = searchPanel.Width + 0;
            kryptonNavigator1.Top = kryptonRibbon.Height + 0;
            kryptonNavigator1.Height = kryptonWorkspaceCell1.Height - 2;
            kryptonNavigator1.Width = ClientSize.Width - searchPanel.Width - 0;
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (!e.Control) return;
            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (e.KeyCode)
            {
                case Keys.F: // Set focus to the SearchBox
                    SearchBox.Focus();
                    e.Handled = true;
                    break;
                case Keys.O: // Open a production list from file
                    RbnBtnProductionListLoad_Click(RbnBtnProductionListLoad, e);
                    e.Handled = true;
                    break;
                case Keys.Q: // Focus quantity box
                    if (QuantityBox.CanFocus) QuantityBox.Focus();
                    e.Handled = true;
                    break;
                case Keys.S: // Save current production list to file
                    RbnBtnProductionListSave_Click(RbnBtnProductionListSave, e);
                    e.Handled = true;
                    break;
                case Keys.W:
                    if (kryptonNavigator1.SelectedPage != null)
                    {
                        kryptonNavigator1.Pages.Remove(kryptonNavigator1.SelectedPage);
                        e.Handled = true;
                    }
                    break;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void SetWindowSettingsIntoScreenArea()
        {
            // https://stackoverflow.com/a/48864640
            // first detect Screen, where we will display the Window
            // second correct bottom and right position
            // then the top and left position.
            // If Size is bigger than current Screen, it's still
            // possible to move and size the Window

            var Default = new Rect(new System.Windows.Point(200, 200),
                                   new System.Windows.Size(1500, 900));
            var LastLeftTop = new Point(SettingsMgr.GetInt(SettingsEnum.LastLeftPos),
                                        SettingsMgr.GetInt(SettingsEnum.LastTopPos));
            var lastSize = new Point(SettingsMgr.GetInt(SettingsEnum.LastWidth),
                                     SettingsMgr.GetInt(SettingsEnum.LastHeight));

            // get the screen to display the window
            var screen = Screen.FromPoint(new Point((int)Default.Left, (int)Default.Top));

            // is bottom position out of screen for more than 1/3 Height of Window?
            if (LastLeftTop.Y + (lastSize.Y / 3) > screen.WorkingArea.Height)
                LastLeftTop.Y = screen.WorkingArea.Height - lastSize.Y;

            // is right position out of screen for more than 1/2 Width of Window?
            if (LastLeftTop.X + (lastSize.X / 2) > screen.WorkingArea.Width)
                LastLeftTop.X = screen.WorkingArea.Width - lastSize.X;

            // is top position out of screen?
            if (LastLeftTop.Y < screen.WorkingArea.Top)
                LastLeftTop.Y = screen.WorkingArea.Top;

            // is left position out of screen?
            if (LastLeftTop.X < screen.WorkingArea.Left)
                LastLeftTop.X = screen.WorkingArea.Left;

            this.Left = LastLeftTop.X;
            this.Top = LastLeftTop.Y;
            this.Width = lastSize.X > 500 ? lastSize.X : 1500;
            this.Height = lastSize.Y > 500 ? lastSize.Y : 900;
        }

        private void ApplySettings()
        {
            CalcOptions.ApplyMargin = SettingsMgr.GetBool(SettingsEnum.ProdListApplyMargin);
            CalcOptions.MarginPct = Utils.ClampDec(SettingsMgr.GetDecimal(SettingsEnum.ProdListGrossMargin), 0, 1000);
            CalcOptions.ApplyRnd = SettingsMgr.GetBool(SettingsEnum.ProdListApplyRounding);
            CalcOptions.RndDigits = SettingsMgr.GetInt(SettingsEnum.ProdListRoundDigits);
 
            CbRestoreWindow.Checked = SettingsMgr.GetBool(SettingsEnum.RestoreWindow);
            CbStartupProdList.Checked = SettingsMgr.GetBool(SettingsEnum.LaunchProdList);
            CbFullSchematicQty.Checked = SettingsMgr.GetBool(SettingsEnum.FullSchematicQuantities);
            var themeId = SettingsMgr.GetInt(SettingsEnum.ThemeId);
            if (themeId > 0)
            {
                kryptonManager.GlobalPaletteMode = (PaletteModeManager)themeId;
            }

            if (!_startUp) return;

            if (CbRestoreWindow.Checked)
            {
                SetWindowSettingsIntoScreenArea();
            }

            DUData.SchemCraftingTalents[0] = SettingsMgr.GetInt(SettingsEnum.SchemCraftCost1);
            DUData.SchemCraftingTalents[1] = SettingsMgr.GetInt(SettingsEnum.SchemCraftCost2);
            DUData.SchemCraftingTalents[2] = SettingsMgr.GetInt(SettingsEnum.SchemCraftOutput1);
            DUData.SchemCraftingTalents[3] = SettingsMgr.GetInt(SettingsEnum.SchemCraftOutput2);

            try
            {
                var recentsList = JsonConvert.DeserializeObject<string[]>((string)SettingsMgr.Settings["RecentProdLists"]);
                if (recentsList != null)
                {
                    CbRecentLists.Items.Clear();
                    CbRecentLists.Items.AddRange(recentsList);
                }
            }
            catch (Exception) { }
        }

        private void SaveSettings()
        {
            if (_startUp) return;
            // LastProductionList is updated in prod.list related events!
            SettingsMgr.UpdateSettings(SettingsEnum.LastLeftPos, Left);
            SettingsMgr.UpdateSettings(SettingsEnum.LastTopPos, Top);
            SettingsMgr.UpdateSettings(SettingsEnum.LastHeight, Height);
            SettingsMgr.UpdateSettings(SettingsEnum.LastWidth, Width);
            SettingsMgr.UpdateSettings(SettingsEnum.LaunchProdList, CbStartupProdList.Checked);
            SettingsMgr.UpdateSettings(SettingsEnum.RestoreWindow, CbRestoreWindow.Checked);
            SettingsMgr.UpdateSettings(SettingsEnum.ThemeId, (int)kryptonManager.GlobalPaletteMode);

            SettingsMgr.UpdateSettings(SettingsEnum.FullSchematicQuantities, DUData.FullSchematicQuantities);
            SettingsMgr.UpdateSettings(SettingsEnum.SchemCraftCost1, DUData.SchemCraftingTalents[0]);
            SettingsMgr.UpdateSettings(SettingsEnum.SchemCraftCost2, DUData.SchemCraftingTalents[1]);
            SettingsMgr.UpdateSettings(SettingsEnum.SchemCraftOutput1, DUData.SchemCraftingTalents[2]);
            SettingsMgr.UpdateSettings(SettingsEnum.SchemCraftOutput2, DUData.SchemCraftingTalents[3]);

            SettingsMgr.UpdateSettings(SettingsEnum.RecentProdLists, JsonConvert.SerializeObject(CbRecentLists.Items));
            SettingsMgr.SaveSettings();
        }

        private void CbRestoreWindow_CheckedChanged(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void CbFullSchematicQty_CheckedChanged(object sender, EventArgs e)
        {
            DUData.FullSchematicQuantities = CbFullSchematicQty.Checked;
            SaveSettings();
        }

        #endregion

        #region Navigator related events

        private static KryptonPage NewPage(string name, IContentDocument content)
        {
            var p = new KryptonPage(name)
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Flags = 0
            };
            p.SetFlags(KryptonPageFlags.DockingAllowDocked | KryptonPageFlags.DockingAllowClose);
            if (content == null) return p;
            ((Control)content).Dock = DockStyle.Fill;
            p.Controls.Add((Control)content);
            return p;
        }

        private IContentDocument NewDocument(string title = null)
        {
            if (kryptonNavigator1 == null) return null;
            var oldPage = kryptonNavigator1.Pages.FirstOrDefault(x => x.Text == title);
            if (oldPage != null && oldPage.Controls.Count > 0)
            {
                if (oldPage.Controls[0] is IContentDocument xDoc)
                {
                    kryptonNavigator1.SelectedPage = oldPage;
                    return xDoc;
                }
            }
            var newDoc = new ContentDocumentTree();
            var page = NewPage(title ?? "Recipe", newDoc);
            kryptonNavigator1.Pages.Add(page);
            kryptonNavigator1.SelectedPage = page;
            AddTabCloseButton(kryptonNavigator1.SelectedPage);
            return newDoc;
        }

        private void AddTabCloseButton(KryptonPage page)
        {
            // Add a close button
            var bsa = new ButtonSpecAny
            {
                Style = PaletteButtonStyle.FormClose,
                Type = PaletteButtonSpecStyle.Close,
                Tag = kryptonNavigator1.SelectedPage,
                Visible = true,
            };
            bsa.Click += BsaOnClick;
            page.ButtonSpecs.Add(bsa);
        }

        private void BsaOnClick(object sender, EventArgs e)
        {
            if (sender is ButtonSpecAny btn && btn.Tag is KryptonPage page)
            {
                kryptonNavigator1.Pages.Remove(page);
            }
        }

        private void KryptonDockableWorkspace_WorkspaceCellAdding(object sender, WorkspaceCellEventArgs e)
        {
            e.Cell.Button.CloseButtonAction = CloseButtonAction.RemovePageAndDispose;
            // Remove the context menu from the tabs bar, as it is not relevant
            e.Cell.Button.ContextButtonDisplay = ButtonDisplay.Hide;
            e.Cell.Button.NextButtonDisplay = ButtonDisplay.Hide;
            e.Cell.Button.PreviousButtonDisplay = ButtonDisplay.Hide;
        }

        private void KryptonNavigator1_TabCountChanged(object sender, EventArgs e)
        {
            if (GetProductionPage() == null)
            {
                ProductionListClose();
            }
            if (kryptonNavigator1.Pages.Count > 0) return;
            Calculator.ResetRecipeName();
            Calculator.Initialize();
        }

        private void KryptonNavigator1OnSelectedPageChanged(object sender, EventArgs e)
        {
            if(_navUpdating || !(sender is KryptonNavigator nav && nav.SelectedPage != null)) return;
            ProductionListUpdates(sender);
            if (nav.SelectedPage.Controls.Count == 0) return;
            if (!DUData.IsIgnorableTitle(nav.SelectedPage.Text))
            {
                SearchBox.Text = nav.SelectedPage.Text;
            }
            OnMainformResize(null, null);
        }

        private KryptonPage GetProductionPage(bool remove = false)
        {
            var page = kryptonNavigator1.Pages.FirstOrDefault(x => x.Text == DUData.ProductionListTitle);
            if (!remove || page == null) return page;
            kryptonNavigator1.Pages.Remove(page);
            _manager.Databindings.Remove(page.Text);
            return null;
        }

        #endregion

        #region Ribbon related events

        private void BtnTalents_Click(object sender, EventArgs e)
        {
            using (var form = new SkillForm())
            {
                form.ShowDialog(this);
            }
        }

        private void BtnOreValues_Click(object sender, EventArgs e)
        {
            using (var form = new OreValueForm())
            {
                form.ShowDialog(this);
            }
        }

        private void BtnSchematics_Click(object sender, EventArgs e)
        {
            var oldTalents = DUData.SchemCraftingTalents.Clone();
            using (var form = new SchematicValueForm())
            {
                form.ShowDialog(this);
            }
            // if schematic crafting talents have changed, reload vanilla talents
            // and re-apply talents:
            if (oldTalents != DUData.SchemCraftingTalents)
            {
                DUData.LoadSchematics();
            }
        }

        private void RibbonAppButtonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RibbonButtonAboutClick(object sender, EventArgs e)
        {
            using (var form = new AboutForm())
            {
                form.ShowDialog(this);
            }
        }

        private void RbOffice2010Blue_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
            SaveSettings();
        }

        private void RbOffice2010BSilver_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
            SaveSettings();
        }

        private void RbOffice365White_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPaletteMode = PaletteModeManager.Office365White;
            SaveSettings();
        }

        //private void CmbThemes_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    var idx = ((KryptonThemeComboBox)CmbThemes.CustomControl).ThemeSelectedIndex;
        //    if (idx >= 0)
        //    {
        //        kryptonManager.GlobalPaletteMode = (PaletteModeManager)idx;
        //    }
        //}

        #endregion

        #region Production List

        private void RbnBtnProductionList_Click(object sender, EventArgs e)
        {
            using (var form = new ProductionListForm(_manager))
            {
                if (form.ShowDialog(this) == DialogResult.Cancel) return;
            }
            ProcessProductionList();
        }

        private void CbRecentLists_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!(sender is KryptonRibbonGroupComboBox cb)) return;
            SearchBox.Focus();
            LoadAndRunProductionList(cb.Text);
        }

        private void CbStartupProdList_CheckedChanged(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void RbnBtnProductionListLoad_Click(object sender, EventArgs e)
        {
            if (!ProductionListForm.LoadList(_manager)) return;
            if (!_manager.Databindings.ListLoaded) return;
            StoreLatestList(_manager.Databindings.Filepath);
            ProcessProductionList();
        }

        private void RbnBtnProductionListSave_Click(object sender, EventArgs e)
        {
            if (!_manager.Databindings.HasData && !_manager.Databindings.ListLoaded) return;
            if (!ProductionListForm.SaveList(_manager)) return;
            StoreLatestList(_manager.Databindings.Filepath);
            KryptonMessageBox.Show("Production list saved to:"+Environment.NewLine+
                _manager.Databindings.Filepath, "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnProdListAdd_Click(object sender, EventArgs e)
        {
            if (kryptonNavigator1.SelectedPage == null) return;
            if (DUData.IsIgnorableTitle(kryptonNavigator1.SelectedPage.Text)) return;
            if (kryptonNavigator1.SelectedPage.Controls[0] is IContentDocument doc)
            {
                var isNew = !_manager.Databindings.HasData && !_manager.Databindings.ListLoaded;
                _manager.Databindings.Add(kryptonNavigator1.SelectedPage.Text, doc.Quantity);
                if (!isNew) return;
                ProcessProductionList();
            }
        }

        private void BtnProdListRemove_Click(object sender, EventArgs e)
        {
            if (kryptonNavigator1.SelectedPage == null) return;
            _manager.Databindings.Remove(kryptonNavigator1.SelectedPage.Text);
        }

        private void BtnProdListClose_Click(object sender, EventArgs e)
        {
            if (KryptonMessageBox.Show("Really close current production list?",
                "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                GetProductionPage(true);
            }
        }

        private void BtnClearProdLists_Click(object sender, EventArgs e)
        {
            if (CbRecentLists.Items.Count == 0 ||
                KryptonMessageBox.Show("Really clear list of recent production lists?",
                "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }
            CbRecentLists.Items.Clear();
            CbRecentLists.SelectedIndex = -1;
            CbRecentLists.Text = "";
            SettingsMgr.UpdateSettings(SettingsEnum.LastProductionList, "");
            SettingsMgr.SaveSettings();
        }

        private void ProductionListRecalc_Click(object sender, EventArgs e)
        {
            ProcessProductionList();
        }

        private void ProcessProductionList()
        {
            // Let Manager prepare the compound recipe which includes
            // all items' ingredients and the items as products.
            BeginInvoke((MethodInvoker)delegate()
            {
                try
                {
                    Calculator.Initialize();
                    if (!_manager.Databindings.PrepareProductListRecipe())
                    {
                        KryptonMessageBox.Show("Production List could not be prepared!", "Failure");
                        return;
                    }

                    DUData.ProductionListMode = true;
                    SelectRecipe(new TreeNode
                    {
                        Text = DUData.ProductionListTitle,
                        Tag = DUData.CompoundRecipe
                    });
                }
                finally
                {
                    DUData.ProductionListMode = false;
                }
            });
        }

        private void ProductionListClose()
        {
            _manager.Databindings.Clear();
            CbRecentLists.SelectedIndex = -1;
            CbRecentLists.Text = "";
            SettingsMgr.UpdateSettings(SettingsEnum.LastProductionList, "");
            SettingsMgr.SaveSettings();
            ProductionListUpdates(null);
        }

        private void ProductionListUpdates(object sender)
        {
            BtnProdListAdd.Enabled = !DUData.IsIgnorableTitle(kryptonNavigator1.SelectedPage?.Text);
            BtnProdListClose.Enabled = _manager.Databindings.HasData || _manager.Databindings.ListLoaded;
            BtnProdListRemove.Enabled = BtnProdListAdd.Enabled && _manager.Databindings.HasData;
            RbnBtnProductionListSave.Enabled = BtnProdListClose.Enabled;
            if (_navUpdating) return;
            Text = "DU Industry Tool " + Utils.GetVersion();
            if (!RbnBtnProductionListSave.Enabled) return;
            Text += " (";
            if (_manager.Databindings.ListLoaded)
            {
                Text += DUData.ProductionListTitle + ": " + _manager.Databindings.GetFilename() +
                        " / " + _manager.Databindings.Count;
            }
            else
            {
                Text += DUData.ProductionListTitle + " " + _manager.Databindings.Count;
            }
            Text += ")";
            if (_manager.Databindings.ListLoaded)
            {
                StoreLatestList(_manager.Databindings.Filepath);
            }
        }

        private void LoadAndRunProductionList(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return;
            if (!File.Exists(filename))
            {
                while (CbRecentLists.Items.Count > 0 && CbRecentLists.Items.Contains(filename))
                {
                    CbRecentLists.Items.Remove(filename);
                }
                SettingsMgr.UpdateSettings(SettingsEnum.LastProductionList, "");
                SaveSettings();
                return;
            }
            try
            {
                if (!_manager.Databindings.Load(filename) || !_manager.Databindings.ListLoaded)
                {
                    return;
                }
                StoreLatestList(filename);
                ProcessProductionList();
            }
            catch (Exception) { }
        }

        private void StoreLatestList(string filename)
        {
            if (string.IsNullOrEmpty(filename) || !File.Exists(filename)) return;
            SettingsMgr.UpdateSettings(SettingsEnum.LastProductionList, filename);
            try
            {
                while (CbRecentLists.Items.Count > 0 && CbRecentLists.Items.Contains(filename))
                {
                    CbRecentLists.Items.Remove(filename);
                }
                CbRecentLists.Items.Insert(0, filename);
                CbRecentLists.SelectedIndex = 0;
            }
            finally
            {
                SaveSettings();
            }
        }

        #endregion Production List

        #region Search box + autocomplete

        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                if (acMenu.Visible) acMenu.Close();
                SearchForRecipe(SearchBox.Text);
            }
        }

        private void SearchBoxOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Escape || acMenu.Visible) return;
            if (sender is TextBox tb) tb.Clear();
        }

        private bool _changing = false;
        private void SearchBoxOnTextChanged(object sender, EventArgs e)
        {
            if (_changing || !(sender is TextBox tb)) return;
            _changing = true;
            try
            {
                if (tb.Text.Length < SearchHelper.MinimumSearchLength) return;
                var matchingItems = SearchHelper.SearchItems(tb.Text);
                acMenu.SetAutocompleteItems(matchingItems.Select(item => new RecipeAutocompleteItem(item)).ToList());
            }
            finally
            {
                _changing = false;
            }
        }

        private void acMenu_Selected(object sender, AutocompleteMenuNS.SelectedEventArgs e)
        {
            if (string.IsNullOrEmpty(e?.Item?.Text)) return;
            if (!SearchHelper.SearchableItems.Contains(e.Item.Text)) return;
            SearchBox.Text = e.Item.Text;
            SearchButton.PerformClick();
        }

        #endregion

    } // Mainform
}
