using System;
using System.Linq;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace DU_Industry_Tool
{
    public partial class OreValueForm : KryptonForm
    {
        public OreValueForm()
        {
            InitializeComponent();
            // This is intended to have a DataGrid that they can paste the ore values into
            // So we parse out the names and store them back in after they save
            foreach(var ore in DUData.Ores.OrderBy(o => o.Name))
            {
                oreGrid.Rows.Add(ore.Name, ore.Value);
            }
            oreGrid.AutoSize = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach(var values in oreGrid.Rows)
            {
                if (!(values is DataGridViewRow row)) continue;
                var oreName = row.Cells[0].Value as string;
                var oreValueString = row.Cells[1].Value as string;
                if (!decimal.TryParse(oreValueString, out var oreValue)) continue;
                var oreRecipe = DUData.Ores.FirstOrDefault(o => o.Name.Equals(oreName, StringComparison.InvariantCultureIgnoreCase));
                if (oreRecipe != null)
                    oreRecipe.Value = oreValue;
            }
            DUData.SaveOreValues();
        }
    }
}
