using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AutocompleteMenuNS;
using DU_Helpers;
using Krypton.Toolkit;

namespace DU_Industry_Tool
{
    public partial class ProductionListForm : KryptonForm
    {
        private bool _changed = false;
        private string _loadedFile;
        private IndustryManager Manager { get; }

        public ProductionListForm(IndustryManager manager)
        {
            InitializeComponent();
            Text = DUData.ProductionListTitle;

            Manager = manager;
            if (Manager.Databindings.ProductionBindingList == null)
            {
                Manager.Databindings.ProductionBindingList = new BindingList<ProductionItem>
                {
                    AllowEdit = true,
                    AllowNew = true,
                    AllowRemove = true,
                    RaiseListChangedEvents = true
                };
            }
            dgvProductionList.DataSource = Manager.Databindings.ProductionBindingList;
            dgvProductionList.KeyDown += DgvProductionListOnKeyDown;

            // manually assign click events due to designer occasionally loosing them :(
            BtnAdd.Click += BtnAddOnClick;
            BtnCalculate.Click += BtnCalculateOnClick;
            BtnLoad.Click += BtnLoad_Click;
            BtnSave.Click += BtnSave_Click;
            BtnClear.Click += BtnClear_Click;
            BtnRemoveEntry.Click += BtnRemove_Click;

            acMenu.MaximumSize = new Size(recipeSearchBox.Size.Width, 400);
            acMenu.SetAutocompleteMenu(recipeSearchBox, acMenu);

            recipeSearchBox.TextChanged += recipeSearchBoxOnTextChanged;
            recipeSearchBox.KeyDown += recipeSearchBoxOnKeyDown;
        }

        private void RemoveGridEntry()
        {
            if (dgvProductionList.CurrentRow == null) return;
            Manager.Databindings.ProductionBindingList.RemoveAt(dgvProductionList.CurrentRow.Index);
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
                var item = Manager.Databindings.ProductionBindingList.FirstOrDefault(x => x.Name.Equals(recipeSearchBox.Text, StringComparison.CurrentCultureIgnoreCase));
                if (item == null)
                {
                    Manager.Databindings.ProductionBindingList.Add(new ProductionItem
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
                        "Proceed with Calculation?", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information) != DialogResult.Yes)
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
                LoadList(Manager);
            }
            finally
            {
                dgvProductionList.DataSource = Manager.Databindings.ProductionBindingList;
            }
            UpdateFileDisplay();
            dgvProductionList.Invalidate(true);
            _changed = false;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveList(Manager);
            UpdateFileDisplay();
            _changed = false;
        }

        private void UpdateFileDisplay()
        {
            _loadedFile = Manager.Databindings.GetFilename();
            LblLoaded.Text = _loadedFile == "" ? "" : "Loaded: " + _loadedFile;
            LblLoaded.Visible = _loadedFile != "";
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (KryptonMessageBox.Show("Really clear the list now?", "Clear List", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            Manager.Databindings.ProductionBindingList.Clear();
            dgvProductionList.Invalidate(true);
            LblLoaded.Text = "";
            LblLoaded.Visible = false;
            _changed = false;
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            RemoveGridEntry();
        }

        #region list load/save
        
        public static bool LoadList(IndustryManager manager)
        {
            var fname = Utils.PromptOpen("Load Production List");
            if (fname == null) return false;
            
            try
            {
                manager.Databindings.Load(fname);
                return true;
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Could not load file:" + Environment.NewLine + ex.Message,
                    "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public static bool SaveList(IndustryManager manager)
        {
            var fname = Utils.PromptSave("Save Production List", manager.Databindings.GetFilename());
            if (fname == null) return false;

            try
            {
                manager.Databindings.Save(fname);
                return true;
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Could not save file!" + Environment.NewLine + ex.Message,
                    "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                acMenu.SetAutocompleteItems(matchingItems.Select(item => new RecipeAutocompleteItem(item)).ToList());
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
    }
}