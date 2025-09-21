using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AutocompleteMenuNS;
using Newtonsoft.Json;
using Krypton.Toolkit;
using DU_Helpers;

namespace DU_Industry_Tool
{
    public partial class ProductionListForm : KryptonForm
    {
        private bool _changed = false;
        private string _loadedFile;
        private IndustryMgr mgr { get; }

        public ProductionListForm(IndustryMgr mgr)
        {
            InitializeComponent();
            Text = DUData.ProductionListTitle;

            this.mgr = mgr;
            if (this.mgr.Databindings.ProductionBindingList == null)
            {
                this.mgr.Databindings.ProductionBindingList = new BindingList<ProductionItem>
                {
                    AllowEdit = true,
                    AllowNew = true,
                    AllowRemove = true,
                    RaiseListChangedEvents = true
                };
            }
            dgvProductionList.DataSource = this.mgr.Databindings.ProductionBindingList;
            dgvProductionList.KeyDown += DgvProductionListOnKeyDown;

            // manually assign click events due to designer occasionally loosing them :(
            BtnAdd.Click += BtnAddOnClick;
            BtnCalculate.Click += BtnCalculateOnClick;
            BtnLoad.Click += BtnLoad_Click;
            BtnSave.Click += BtnSave_Click;
            BtnClear.Click += BtnClear_Click;
            BtnRemoveEntry.Click += BtnRemove_Click;
            BtnPaste.Click += BtnPaste_Click;

            acMenu.MaximumSize = new Size(recipeSearchBox.Size.Width, 400);
            acMenu.SetAutocompleteMenu(recipeSearchBox, acMenu);

            recipeSearchBox.TextChanged += recipeSearchBoxOnTextChanged;
            recipeSearchBox.KeyDown += recipeSearchBoxOnKeyDown;

            // drag & drop import
            AllowDrop = true;
            DragEnter += OnDragEnter;
            DragDrop += OnDragDrop;
            dgvProductionList.AllowDrop = true;
            dgvProductionList.DragEnter += OnDragEnter;
            dgvProductionList.DragDrop += OnDragDrop;
        }

        private void RemoveGridEntry()
        {
            if (dgvProductionList.CurrentRow == null) return;
            mgr.Databindings.ProductionBindingList.RemoveAt(dgvProductionList.CurrentRow.Index);
            _changed = true;
        }

        private void DgvProductionListOnKeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Control && e.KeyCode == Keys.Delete)) return;
            RemoveGridEntry();
        }

        private void BtnAddOnClick(object sender, EventArgs e)
        {
            // make sure the entered text is actually existing in the recipes list
            if (string.IsNullOrEmpty(recipeSearchBox.Text) ||
                NumUpDownQuantity.Value <= 0 ||
                !DUData.RecipeNames.Any(x => x.Equals(recipeSearchBox.Text, StringComparison.CurrentCultureIgnoreCase)))
            {
                return;
            }
            // Add new recipe, otherwise increase quantity of existing recipe
            try
            {
                var item = mgr.Databindings.ProductionBindingList.FirstOrDefault(x => x.Name.Equals(recipeSearchBox.Text, StringComparison.CurrentCultureIgnoreCase));
                if (item == null)
                {
                    mgr.Databindings.ProductionBindingList.Add(new ProductionItem
                    {
                        Name = recipeSearchBox.Text,
                        Quantity = NumUpDownQuantity.Value
                    });
                    _changed = true;
                    return;
                }
                item.Name = recipeSearchBox.Text;
                item.Quantity += NumUpDownQuantity.Value;
            }
            finally
            {
                dgvProductionList.Invalidate(true);
            }
        }

        private void BtnCalculateOnClick(object sender, EventArgs e)
        {
            if (_changed)
            {
                if (KryptonMessageBox.Show("Proceed with calculation? Answer 'No' to be able to save the production list!",
                        "Proceed with Calculation?", KryptonMessageBoxButtons.YesNo,
                        KryptonMessageBoxIcon.Information) != DialogResult.Yes)
                {
                    return;
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            dgvProductionList.DataSource = null;
            try
            {
                LoadList(mgr);
            }
            finally
            {
                dgvProductionList.DataSource = mgr.Databindings.ProductionBindingList;
            }
            UpdateFileDisplay();
            dgvProductionList.Invalidate(true);
            _changed = false;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveList(mgr);
            UpdateFileDisplay();
            _changed = false;
        }

        private void UpdateFileDisplay()
        {
            _loadedFile = mgr.Databindings.GetFilename();
            LblLoaded.Text = _loadedFile == "" ? "" : "Loaded: " + _loadedFile;
            LblLoaded.Visible = _loadedFile != "";
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (KryptonMessageBox.Show("Really clear the list now?", "Clear List", 
                    KryptonMessageBoxButtons.YesNo, KryptonMessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            mgr.Databindings.ProductionBindingList.Clear();
            dgvProductionList.Invalidate(true);
            LblLoaded.Text = "";
            LblLoaded.Visible = false;
            _changed = false;
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            RemoveGridEntry();
        }

        private void BtnPaste_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Clipboard.ContainsText())
                {
                    KryptonMessageBox.Show("Clipboard does not contain text.", "Paste", KryptonMessageBoxButtons.OK, false);
                    return;
                }
                var text = Clipboard.GetText();
                if (string.IsNullOrWhiteSpace(text))
                {
                    KryptonMessageBox.Show("Clipboard text is empty.", "Paste", KryptonMessageBoxButtons.OK, false);
                    return;
                }
                if (ImportListFromJson(mgr, text))
                {
                    _changed = true;
                    dgvProductionList.Invalidate(true);
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Could not import from clipboard!" + Environment.NewLine + ex.Message,
                    "ERROR", KryptonMessageBoxButtons.OK, false);
            }
        }

        #region list load/save

        public static bool LoadList(IndustryMgr mgr)
        {
            var fname = Utils.PromptOpen("Load Production List");
            if (fname == null) return false;

            try
            {
                mgr.Databindings.Load(fname);
                return true;
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Could not load file:" + Environment.NewLine + ex.Message,
                    "ERROR", KryptonMessageBoxButtons.OK, false);
            }
            return false;
        }

        public static bool SaveList(IndustryMgr mgr)
        {
            var fname = Utils.PromptSave("Save Production List", mgr.Databindings.GetFilename());
            if (fname == null) return false;

            try
            {
                mgr.Databindings.Save(fname);
                return true;
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Could not save file!" + Environment.NewLine + ex.Message,
                    "ERROR", KryptonMessageBoxButtons.OK, false);
            }
            return false;
        }

        #endregion

        #region search autocomplete

        private bool _changing = false;
        private void recipeSearchBoxOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && !acMenu.Visible)
            {
                if (sender is TextBox tb) tb.Clear();
            }
        }

        private void recipeSearchBoxOnTextChanged(object sender, EventArgs e)
        {
            if (_changing || !(sender is TextBox tb)) return;
            _changing = true;
            try
            {
                if (tb.Text.Length < SearchHelper.MinimumSearchLength) return;
                var matchingItems = SearchHelper.SearchItems(tb.Text);
                acMenu.SetAutocompleteItems(matchingItems.Select(item => new SearchResultItem(item)).ToList());
            }
            finally
            {
                _changing = false;
            }
        }

        private void acMenu_Selected(object sender, SelectedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e?.Item?.Text))
            {
                recipeSearchBox.Text = e.Item.Text;
            }
        }

        #endregion

        #region drag & drop import

        private class ExternalImportItem
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public decimal Quantity { get; set; }
            public string Category { get; set; }
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (files?.Any(x => x.EndsWith(".json", StringComparison.InvariantCultureIgnoreCase)) == true)
                {
                    e.Effect = DragDropEffects.Copy;
                    return;
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void OnDragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            var files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files == null || files.Length == 0) return;

            var anyImported = false;
            foreach (var file in files.Where(f => f.EndsWith(".json", StringComparison.InvariantCultureIgnoreCase)))
            {
                if (ImportList(mgr, file)) anyImported = true;
            }

            if (anyImported)
            {
                _changed = true;
                dgvProductionList.Invalidate(true);
            }
        }

        public static bool ImportList(IndustryMgr mgr, string filename)
        {
            if (string.IsNullOrEmpty(filename)) return false;
            try
            {
                if (!File.Exists(filename)) throw new FileNotFoundException("File not found", filename);
                var json = File.ReadAllText(filename);
                if (string.IsNullOrWhiteSpace(json)) throw new Exception("File is empty.");

                var items = JsonConvert.DeserializeObject<BindingList<ExternalImportItem>>(json);
                if (items?.Any() != true) throw new Exception("No items in file.");

                var added = 0;
                foreach (var ext in items)
                {
                    if (string.IsNullOrEmpty(ext?.Name)) continue;
                    if (ext.Quantity <= 0) ext.Quantity = 1;

                    var resolvedName = ext.Name;
                    var nameMatch = DUData.RecipeNames.Any(x => x.Equals(ext.Name, StringComparison.CurrentCultureIgnoreCase));
                    if (!nameMatch)
                    {
                        if (!string.IsNullOrEmpty(ext.Id) && ulong.TryParse(ext.Id, out var id))
                        {
                            var rec = DUData.Recipes.FirstOrDefault(x => x.Value.NqId == id);
                            if (rec.Key != null && !string.IsNullOrEmpty(rec.Value?.Name))
                            {
                                resolvedName = rec.Value.Name;
                                nameMatch = true;
                            }
                        }
                    }

                    if (!nameMatch)
                    {
                        continue;
                    }

                    var existing = mgr.Databindings.ProductionBindingList.FirstOrDefault(x => x.Name.Equals(resolvedName, StringComparison.CurrentCultureIgnoreCase));
                    if (existing == null)
                    {
                        mgr.Databindings.ProductionBindingList.Add(new ProductionItem { Name = resolvedName, Quantity = ext.Quantity });
                    }
                    else
                    {
                        existing.Quantity += ext.Quantity;
                    }
                    added++;
                }

                if (added == 0)
                {
                    KryptonMessageBox.Show("No valid items were found in the file.", "Import", KryptonMessageBoxButtons.OK, false);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Could not import JSON file!" + Environment.NewLine + ex.Message,
                    "ERROR", KryptonMessageBoxButtons.OK, false);
            }
            return false;
        }

        public static bool ImportListFromJson(IndustryMgr mgr, string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return false;
            try
            {
                var items = JsonConvert.DeserializeObject<BindingList<ExternalImportItem>>(json);
                if (items?.Any() != true) throw new Exception("No items in clipboard data.");

                var added = 0;
                var notFoundNames = new List<string>();
                foreach (var ext in items)
                {
                    if (string.IsNullOrEmpty(ext?.Name)) continue;
                    if (ext.Quantity <= 0) ext.Quantity = 1;

                    var resolvedName = ext.Name;
                    var nameMatch = DUData.RecipeNames.Any(x => x.Equals(ext.Name, StringComparison.CurrentCultureIgnoreCase));
                    if (!nameMatch)
                    {
                        if (!string.IsNullOrEmpty(ext.Id) && ulong.TryParse(ext.Id, out var id))
                        {
                            var rec = DUData.Recipes.FirstOrDefault(x => x.Value.NqId == id);
                            if (rec.Key != null && !string.IsNullOrEmpty(rec.Value?.Name))
                            {
                                resolvedName = rec.Value.Name;
                                nameMatch = true;
                            }
                        }
                    }

                    if (!nameMatch)
                    {
                        notFoundNames.Add(ext.Name);
                        continue;
                    }

                    var existing = mgr.Databindings.ProductionBindingList.FirstOrDefault(x => x.Name.Equals(resolvedName, StringComparison.CurrentCultureIgnoreCase));
                    if (existing == null)
                    {
                        mgr.Databindings.ProductionBindingList.Add(new ProductionItem { Name = resolvedName, Quantity = ext.Quantity });
                    }
                    else
                    {
                        existing.Quantity += ext.Quantity;
                    }
                    added++;
                }

                if (notFoundNames.Count > 0)
                {
                    var distinct = new List<string>(new HashSet<string>(notFoundNames, StringComparer.CurrentCultureIgnoreCase));
                    var preview = string.Join(Environment.NewLine, distinct.Take(10));
                    var msg = $"{notFoundNames.Count} item(s) could not be imported:" + Environment.NewLine + preview;
                    KryptonMessageBox.Show(msg, "Import", KryptonMessageBoxButtons.OK, false);
                }

                if (added == 0)
                {
                    KryptonMessageBox.Show("No valid items were found in the clipboard data.", "Import", KryptonMessageBoxButtons.OK, false);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Could not import JSON from clipboard!" + Environment.NewLine + ex.Message,
                    "ERROR", KryptonMessageBoxButtons.OK, false);
            }
            return false;
        }

        #endregion
    }
}