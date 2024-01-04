using System;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;
using Krypton.Toolkit;

namespace DU_Industry_Tool
{
    public partial class SchematicValueForm : KryptonForm
    {
        public SchematicValueForm()
        {
            InitializeComponent();
            PopulateCraftingTalents();
            PopulateGrid();
        }
        
        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Commit talent values to global DUData store, reload schematics
        /// and repopulate the grid for cost display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void applyBtn_Click(object sender, EventArgs e)
        {
            applyBtn.Enabled = false;
            try
            {
                ApplyTalentValues();
                DUData.LoadSchematics();
                PopulateGrid();
            }
            finally
            {
                applyBtn.Enabled = true;
            }
        }

        /// <summary>
        /// Trackbar event handler to display value as number in corresponding label.
        /// The trackbar's Tag property is used as index and name identifier.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSkill_ValueChanged(object sender, EventArgs e)
        {
            if (!(sender is KryptonTrackBar tb)) return;
            var idx = 0;
            switch (tb.Tag)
            {
                case int tag:
                    idx = tag;
                    break;
                case string tagS:
                {
                    if (!int.TryParse(tagS, out idx)) return;
                    break;
                }
                default:
                    return;
            }
            if (idx < 0 || idx > 3) return;
                
            var el = Controls.Find("skill" + (idx+1), true).FirstOrDefault();
            if (el is KLabel lbl)
            {
                lbl.Text = tb.Value.ToString();
            }
        }

        /// <summary>
        /// Fill grid with all schematics' cost as informational value (readonly).
        /// </summary>
        private void PopulateGrid()
        {
            schematicsGrid.SuspendLayout();
            try
            {
                schematicsGrid.Rows.Clear();
                foreach (var schema in DUData.Schematics.OrderBy(o => o.Key))
                {
                    schematicsGrid.Rows.Add(schema.Value.Name, schema.Value.BatchCost, schema.Value.Cost, schema.Value.BatchSize);
                }
                schematicsGrid.AutoSize = true;
            }
            finally
            {
                schematicsGrid.ResumeLayout();
            }
        }

        private void PopulateCraftingTalents()
        {
            tbSkill1.Value = DUData.SchemCraftingTalents[0];
            tbSkill2.Value = DUData.SchemCraftingTalents[1];
            tbSkill3.Value = DUData.SchemCraftingTalents[2];
            tbSkill4.Value = DUData.SchemCraftingTalents[3];
        }

        private void ApplyTalentValues()
        {
            DUData.SchemCraftingTalents[0] = tbSkill1.Value;
            DUData.SchemCraftingTalents[1] = tbSkill2.Value;
            DUData.SchemCraftingTalents[2] = tbSkill3.Value;
            DUData.SchemCraftingTalents[3] = tbSkill4.Value;
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Schematics Values " + DateTime.Now.ToString("yyyy-MM-dd"));

                int row = 1;

                worksheet.Cell(row, 1).Value = "Schematic Cost Optimization (-5% q)";
                worksheet.Cell(row, 2).Value = DUData.SchemCraftingTalents[0];

                row++;
                worksheet.Cell(row, 1).Value = "Adv. Schematic Cost Optim. (-3% q)";
                worksheet.Cell(row, 2).Value = DUData.SchemCraftingTalents[1];

                row++;
                worksheet.Cell(row, 1).Value = "Schematic Output Productivity (+3%)";
                worksheet.Cell(row, 2).Value = DUData.SchemCraftingTalents[2];

                row++;
                worksheet.Cell(row, 1).Value = "Adv. Schematic Output Prod. (+2%)";
                worksheet.Cell(row, 2).Value = DUData.SchemCraftingTalents[3];

                row++;
                row++;
                worksheet.Cell(row, 1).Value = "Name";
                worksheet.Cell(row, 2).Value = "Batch Cost";
                worksheet.Cell(row, 3).Value = "Cost per 1";
                worksheet.Cell(row, 4).Value = "Batch Size";
                worksheet.Cell(row, 5).Value = "Time (seconds)";
                worksheet.Cell(row, 6).Value = "Item ID";

                worksheet.Row(row).CellsUsed().Style.Font.SetBold();

                row++;
                foreach (var schem in DUData.Schematics)
                {
                    worksheet.Cell(row, 1).Value = schem.Value.Name;
                    worksheet.Cell(row, 2).Value = schem.Value.BatchCost;
                    worksheet.Cell(row, 3).Value = schem.Value.Cost;
                    worksheet.Cell(row, 4).Value = schem.Value.BatchSize;
                    worksheet.Cell(row, 5).Value = schem.Value.BatchTime;
                    worksheet.Cell(row, 6).Value = schem.Value.NqId;
                    row++;
                }
                worksheet.Range("A1:A4").Style.Font.Bold = true;
                worksheet.ColumnsUsed().AdjustToContents(1, 50);
                var fname = "Schematics Export " + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
                workbook.SaveAs(fname);
                MessageBox.Show($"Exported '{fname}' in the same folder as this tool!");
            }

        }
    }
}
