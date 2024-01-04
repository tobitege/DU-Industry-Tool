using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace DU_Industry_Tool
{
    public partial class ProductionListForm : KryptonForm
    {
        private bool _changed;
        private string _loadedFile;
        private IndustryManager Manager { get; }
        public KryptonComboBox RecipeNames => ComboRecipeNames;

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
            Column1.Items.AddRange(DUData.RecipeNames.ToArray());
            // wth! AutoComplete popup stays empty no matter what source :(
            //Column1.AutoCompleteCustomSource.AddRange(Manager.RecipeNames.ToArray());
            //Column1.AutoCompleteSource = AutoCompleteSource.ListItems;

            // manually assign click events due to designer occasionally loosing them :(
            BtnAdd.Click += BtnAddOnClick;
            BtnCalculate.Click += BtnCalculateOnClick;
            BtnLoad.Click += BtnLoad_Click;
            BtnSave.Click += BtnSave_Click;
            BtnClear.Click += BtnClear_Click;
        }

        private void BtnAddOnClick(object sender, EventArgs e)
        {
            // make sure the entered text is actually existing in the recipes list
            if (string.IsNullOrEmpty(ComboRecipeNames.Text) ||
                NumUpDownQuantity.Value <= 0 ||
                !DUData.RecipeNames.Any(x => x.Equals(ComboRecipeNames.Text, StringComparison.CurrentCultureIgnoreCase)))
            {
                return;
            }
            // Add new recipe, otherwise increase quantity of existing recipe
            try
            {
                var item = Manager.Databindings.ProductionBindingList.FirstOrDefault(x => x.Name.Equals(ComboRecipeNames.Text, StringComparison.CurrentCultureIgnoreCase));
                if (item == null)
                {
                    Manager.Databindings.ProductionBindingList.Add(new ProductionItem
                    {
                        Name = ComboRecipeNames.Text,
                        Quantity = NumUpDownQuantity.Value
                    });
                    _changed = true;
                    return;
                }
                item.Name = ComboRecipeNames.Text;
                item.Quantity += NumUpDownQuantity.Value;
            }
            finally
            {
                dgvProductionList.Invalidate(true);
            }
        }

        private void BtnCalculateOnClick(object sender, EventArgs e)
        {
            if (_changed &&
                KryptonMessageBox.Show(@"Proceed with calculation? Answer No to be able to save the production list!",
                    @"Proceed with Calculation?", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information) != DialogResult.Yes)
                return;
            this.DialogResult = DialogResult.OK;
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            LoadList(Manager);
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
            if (KryptonMessageBox.Show(@"Really clear the list now?", @"Clear List", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            Manager.Databindings.ProductionBindingList.Clear();
            dgvProductionList.Invalidate(true);
            LblLoaded.Text = "";
            LblLoaded.Visible = false;
            _changed = false;
        }

        // ################################################

        public static bool LoadList(IndustryManager manager)
        {
            var lastDir = Properties.Settings.Default.ProdListDirectory;
            if (string.IsNullOrEmpty(lastDir) || !Directory.Exists(lastDir))
            {
                lastDir = Application.StartupPath;
            }
            Properties.Settings.Default.Save();

            var dlg = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".json",
                FileName = manager.Databindings.GetFilename(),
                Filter = @"JSON|*.json|All files|*.*",
                FilterIndex = 1,
                Title = @"Load Production List",
                InitialDirectory = lastDir,
                ShowHelp = false,
                SupportMultiDottedExtensions = true,
            };
            if (dlg.ShowDialog() != DialogResult.OK) return false;
            if (!File.Exists(dlg.FileName))
            {
                KryptonMessageBox.Show(@"File does not exist!", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            Properties.Settings.Default.ProdListDirectory = Path.GetDirectoryName(dlg.FileName);
            Properties.Settings.Default.Save();

            try
            {
                manager.Databindings.Load(dlg.FileName);
                return true;
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(@"Could not load Production List: "+ Environment.NewLine+ex.Message,
                    @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public static bool SaveList(IndustryManager manager)
        {
            var lastDir = Properties.Settings.Default.ProdListDirectory;
            if (string.IsNullOrEmpty(lastDir) || !Directory.Exists(lastDir))
            {
                lastDir = Application.StartupPath;
            }
            Properties.Settings.Default.Save();

            var dlg = new SaveFileDialog
            {
                AddExtension = true,
                CheckPathExists = true,
                FileName = manager.Databindings.GetFilename(),
                DefaultExt = ".json",
                Filter = @"JSON|*.json|All files|*.*",
                FilterIndex = 1,
                Title = @"Save Production List",
                InitialDirectory = lastDir,
                ShowHelp = false,
                SupportMultiDottedExtensions = true,
                CheckFileExists = false,
                OverwritePrompt = false
            };
            if (dlg.ShowDialog() != DialogResult.OK) return false;

            if (File.Exists(dlg.FileName) &&
                (KryptonMessageBox.Show(@"Overwrite existing file?", @"Overwrite",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes))
            {
                return false;
            }

            Properties.Settings.Default.ProdListDirectory = Path.GetDirectoryName(dlg.FileName);
            Properties.Settings.Default.Save();
            try
            {
                manager.Databindings.Save(dlg.FileName);
                return true;
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(@"Could not save production list! "+Environment.NewLine+ex.Message,
                    @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
    }
}
