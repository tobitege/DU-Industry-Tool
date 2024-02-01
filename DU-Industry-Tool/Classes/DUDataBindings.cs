using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace DU_Industry_Tool
{
    public class DUDataBindings
    {
        public const string NO_FILE = "unnamed";
        public BindingList<ProductionItem> ProductionBindingList { get; set; }

        public bool ListLoaded { get; private set; }
        public string Filepath { get; set; }
        public string LastErrorMsg { get; set; }

        public event ProductionListHandler ProductionListChanged;

        public bool HasData => ProductionBindingList?.Any() == true;
        public int Count => ProductionBindingList?.Count ?? 0;

        private void CheckInstance()
        {
            ProductionBindingList = ProductionBindingList ?? new BindingList<ProductionItem>();
        }

        public void Add(string itemName, decimal qty)
        {
            CheckInstance();
            if (string.IsNullOrEmpty(itemName) || ProductionBindingList.Any(x => x.Name == itemName)) return;
            var item = new ProductionItem
            {
                Name = itemName,
                Quantity = Math.Max(1, qty)
            };
            ProductionBindingList.Add(item);
            Notify();
        }

        public void Remove(string itemName)
        {
            CheckInstance();
            var item = ProductionBindingList.FirstOrDefault(x => x.Name == itemName);
            if (item == null) return;
            ProductionBindingList.Remove(item);
            Notify();
        }

        public void Clear()
        {
            ProductionBindingList = new BindingList<ProductionItem>();
            Filepath = "";
            ListLoaded = false;
            LastErrorMsg = "";
            Notify();
        }

        public bool Load(string filename)
        {
            if (!File.Exists(filename)) return false;
            try
            {
                var tmp = JsonConvert.DeserializeObject<List<ProductionItem>>(File.ReadAllText(filename));
                if (tmp == null) return false;

                ProductionBindingList = new BindingList<ProductionItem>();
                foreach (var entry in tmp.Where(entry => !string.IsNullOrEmpty(entry?.Name)))
                {
                    ProductionBindingList.Add(entry);
                }
                ListLoaded = true;
                Filepath = filename;
                Notify();
                return true;
            }
            catch (Exception ex)
            {
                LastErrorMsg = ex.Message;
                throw;
            }
        }

        public bool Save(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return false;
            try
            {
                File.WriteAllText(filename, JsonConvert.SerializeObject(ProductionBindingList));
                Filepath = filename;
                ListLoaded = true;
                Notify();
                return true;
            }
            catch (Exception ex)
            {
                LastErrorMsg = ex.Message;
                throw;
            }
        }

        private void Notify()
        {
            ProductionListChanged?.Invoke(ProductionBindingList);
        }

        public string GetFilename()
        {
            if (!ListLoaded || string.IsNullOrEmpty(Filepath)) return NO_FILE;
            return Path.GetFileName(Filepath);
        }

        public bool PrepareProductListRecipe()
        {
            if (ProductionBindingList.Count < 1) return false;
            var cmp = new SchematicRecipe
            {
                Key = DUData.CompoundName,
                Name = DUData.ProductionListTitle
            };
            var cnt = 0;
            foreach (var prodItem in ProductionBindingList.OrderBy(x => x.Name))
            {
                if (prodItem.Quantity < 1) continue;

                // 2024.1.8 - individual production list items have to be calculated now to get the item price at cost.
                // In earlier patches, this wasn't available as the sum of all ingredients was calculated.
                Calculator.Initialize();
                Calculator.ProductQuantity = prodItem.Quantity;
                var recipe = SchematicRecipe.GetByName(prodItem.Name);
                if (string.IsNullOrEmpty(recipe?.Name))
                {
                    Debug.WriteLine($"Error: Recipe {prodItem.Name} not found!");
                    continue;
                }
                Calculator.CalculateRecipe(recipe.Key, prodItem.Quantity, silent: true);
                var calc = Calculator.Get(recipe.Key, Guid.Empty);

                if (calc.Recipe.Ingredients?.Any() != true || calc.Recipe.Products?.Any() != true)
                {
                    continue;
                }

                // Add items to the overall products list
                var batchCount = 1m;
                foreach (var prod in calc.Recipe.Products)
                {
                    /* Example: Production List item: 5000 L Pure Silver
                     * Talents: lvl 3 for both refining (-15% in) and productivity (+9% out)
                     * Results (rounded):
                     *   Input                Output
                     *   6029 L Acanthite     5000 L Pure Silver
                     *                        1667 L Pure Sulfur
                     */
                    if (prod.Name.Equals(prodItem.Name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        prod.Quantity = prodItem.Quantity;
                        prod.Level = calc.Tier;
                        prod.SchemaType = calc.SchematicType;
                        prod.Mass = prodItem.Quantity * (calc.Recipe.UnitMass ?? 0);
                        prod.Volume = prodItem.Quantity * (calc.Recipe.UnitVolume ?? 0);
                        // TODO check why BatchOutput is occasionally null here
                        //var batchOutput = calc.IsBatchmode ? (decimal)(calc.BatchOutput ?? 0) : prod.Quantity;
                        //if (calc.CalcSchematicFromQty(prod.SchemaType, prod.Quantity, batchOutput,
                        //        out batchCount , out var minCost, out var _, out var _))
                        //{
                        //    prod.SchemaQty = batchCount;
                        //    prod.SchemaAmt = minCost;
                        //}
                        prod.SchemaAmt = calc.SchematicsCost;
                        prod.Cost = calc.OreCost;
                        prod.Margin = calc.Margin;
                        prod.Retail = calc.Retail;
                        if (calc.BatchTime == null)
                        {
                            calc.BatchTime = calc.Recipe.Time * (calc.EfficencyFactor ?? 1);
                        }
                        cmp.Time += (decimal)calc.BatchTime * batchCount;
                        cmp.Products.Add(prod);
                        continue;
                    }

                    // Add Byproducts
                    if (!DUData.GetRecipeCloneByKey(prod.Type, out var rec2))
                        continue;
                    if (rec2 == null || rec2.IsPlasma) continue;
                    prod.IsByproduct = true;
                    //prod.Name = prod.Name.TrimLastStr(DUData.ByproductMarker);
                    //prod.Name += DUData.ByproductMarker;
                    if (calc.IsBatchmode)
                    {
                        var batches = calc.Quantity / (calc.BatchInput ?? 1);
                        prod.Quantity = batches * prod.Quantity * calc.OutputMultiplier;
                    }
                    else
                    {
                        prod.Quantity *= calc.OutputMultiplier;
                    }
                    prod.Quantity *= batchCount;
                    cmp.Products.Add(prod);
                }

                // this returns ingredients complete with batch sizes and times according to talents
                var ingredientsList = Calculator.GetIngredientRecipes(calc.Key, prodItem.Quantity, calc.IsOre);

                // Sum up top-level ingredients
                foreach (var ing in ingredientsList)
                {
                    // check if ingredient already exists (add/update)
                    var tmp = cmp.Ingredients.FirstOrDefault(x => x.Name.Equals(ing.Name, StringComparison.InvariantCultureIgnoreCase));
                    if (tmp != null)
                    {
                        tmp.Quantity += ing.Quantity;
                    }
                    else
                    {
                        var newIng = new ProductDetail(ing);
                        cmp.Ingredients.Add(newIng);
                    }
                }

                cnt++;
            }
            if (cnt == 0) return false;
            cmp.UnitMass = Math.Round(cmp.Products.Sum(x => x.Mass), 3);
            cmp.UnitVolume = Math.Round(cmp.Products.Sum(x => x.Volume), 3);
            DUData.CompoundRecipe = cmp;

            // Add compound recipe to main recipe list, check first to remove an existing one
            DUData.Recipes.Remove(DUData.CompoundName);
            DUData.Recipes[DUData.CompoundName] = cmp;
            return true;
        }
    }
}