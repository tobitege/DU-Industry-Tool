using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DU_Helpers;

// ReSharper disable UnusedVariable

namespace DU_Industry_Tool
{
    public class RecipeCalculation : INotifyPropertyChanged
    {
        #region Properties

        private readonly SortedDictionary<string, CalcEntry> Data;
        private Guid Id { get; set; }
        private int Depth { get; set; }
        public Guid ParentId { get; set; }
        public SummationType? SumType { get; set; }
        public int Tier { get; set; }
        public bool IsActive = true;
        public bool IsSection { get; set; }
        public bool IsProdItem { get; set; }
        public bool HasData => Data?.Any() == true;

        #endregion

        #region Constructors

        public RecipeCalculation(string section)
        {
            if (string.IsNullOrEmpty(section)) throw new ArgumentNullException(nameof(RecipeCalculation));
            Section = section;
            Id = Guid.NewGuid();
            ParentId = Guid.Empty;
        }

        public RecipeCalculation(string section, SortedDictionary<string, CalcEntry> ce)
        {
            if (string.IsNullOrEmpty(section)) throw new ArgumentNullException(@"RecipeCalculation");
            Section = section;
            Data = ce;
            IsSection = HasData;
            Id = Guid.NewGuid();
            ParentId = Guid.Empty;
        }

        /*
        public RecipeCalculation(string section, string entry, decimal qty, decimal amt,
            decimal? qtyS=null, decimal? amtS=null, string comment=null)
        {
            Section = section;
            Entry = entry;
            Qty = qty;
            Amt = amt;
            QtySchemata = qtyS;
            AmtSchemata = amtS;
            Comment = comment;
            Id = Guid.NewGuid();
            ParentId = Guid.Empty;
        }

        public RecipeCalculation(RecipeCalculation other)
        {
            Section = other.Section;
            Entry = other.Entry;
            Qty = other.Qty;
            Amt = other.Amt;
            QtySchemata = other.QtySchemata;
            AmtSchemata = other.AmtSchemata;
            Comment = other.Comment;
            Id = Guid.NewGuid();
            ParentId = Guid.Empty;
        }
        */
        #endregion

        #region NotifyProperty Fields
        public string Section
        {
            get => section;
            set
            {
                if (section == value) return;
                section = value;
                OnPropertyChanged("Section");
            }
        }
        private string section;

        public string Entry
        {
            get => entry;
            set
            {
                if (Entry == value) return;
                entry = value;
                OnPropertyChanged("Entry");
            }
        }
        private string entry;

        public decimal Qty
        {
            get => qty;
            set
            {
                if (qty.IsEqualDec(value)) return;
                qty = value;
                OnPropertyChanged("Qty");
            }
        }
        private decimal qty;

        public decimal Margin
        {
            get => margin;
            set
            {
                if (margin.IsEqualDec(value)) return;
                margin = value;
                OnPropertyChanged("Margin");
            }
        }
        private decimal margin;

        public decimal Amt
        {
            get => amt;
            set
            {
                if (amt.IsEqualDec(value)) return;
                amt = value;
                OnPropertyChanged("Amt");
            }
        }
        private decimal amt;

        public decimal Retail
        {
            get => retail;
            set
            {
                if (retail.IsEqualDec(value)) return;
                retail = value;
                OnPropertyChanged("Retail");
            }
        }
        private decimal retail;

        public decimal Mass
        {
            get => mass;
            set
            {
                if (mass.IsEqualDec(value)) return;
                mass = value;
                OnPropertyChanged("Mass");
            }
        }
        private decimal mass;

        public decimal Vol
        {
            get => vol;
            set
            {
                if (vol.IsEqualDec(value)) return;
                vol = value;
                OnPropertyChanged("Vol");
            }
        }
        private decimal vol;

        public decimal? QtySchemata
        {
            get => qtySchemata;
            set
            {
                qtySchemata = value;
                OnPropertyChanged("QtySchemata");
            }
        }
        private decimal? qtySchemata;

        public decimal? AmtSchemata
        {
            get => amtSchemata;
            set
            {
                amtSchemata = value;
                OnPropertyChanged("AmtSchemata");
            }
        }
        private decimal? amtSchemata;

        public string Industry
        {
            get => industry;
            set
            {
                if (industry == value) return;
                industry = value;
                OnPropertyChanged("Industry");
            }
        }
        private string industry;

        public string Comment
        {
            get => comment;
            set
            {
                if (comment == value) return;
                comment = value;
                OnPropertyChanged("Comment");
            }
        }
        private string comment;
        #endregion

        private void CopyFrom(CalcEntry ce)
        {
            Qty = ce.Qty;
            Amt = ce.Amt;
            QtySchemata = ce.QtySchemata;
            AmtSchemata = ce.AmtSchemata;
        }

        private static string GetRealKey(string key, out int tier)
        {
            var realKey = key;
            tier = 0;
            if (realKey[0] == 'T' && char.IsDigit(realKey[1]))
            {
                tier = int.Parse($"{realKey[1]}");
                realKey = realKey.Substring(3);
            }
            return realKey;
        }

        public IEnumerable GetChildren()
        {
            var children = new ArrayList();
            if (!IsSection) return children;

            // Production List -> add products (elements) as children
            if (Depth == 0 && Section == DUData.ProductionListTitle)
            {
                if (!Calculator.GetFromStoreWithProducts(ParentId, out var calc))
                    return children;
                // must sum up costs at this time
                foreach (var child in calc.Recipe.Products.Select(prd => new RecipeCalculation(Section, null)
                     {
                         IsProdItem = prd.IsProdItem,
                         Entry = prd.Name,
                         ParentId = Id,
                         Tier = prd.Level,
                         Amt = Math.Round(prd.Cost, 2),
                         Qty = Math.Round(prd.Quantity, 2, MidpointRounding.AwayFromZero),
                         AmtSchemata = Math.Round(prd.SchemaAmt, 2),
                         Mass = Math.Round(prd.Mass / 1000, 2),
                         Vol = Math.Round(prd.Volume / 1000, 2),
                         Margin = prd.Margin,
                         Retail = prd.Retail,
                         comment = prd.IsByproduct ? "Byproduct" : prd.SchemaType + (prd.SchemaQty > 0 ? " (total schematics cost)" : "")
                     }))
                {
                    children.Add(child);
                }
                return children;
            }

            // For a recipe-driven entry, add sections (depth is even)
            // or the individual entries (depth is odd)
            // We only want Ores, Pures, Products and Parts here, though.
            if (Depth > 0)
            {
                if (!Calculator.GetFromStoreWithNodes(Id, out var calc))
                    return children;
                foreach (var subsection in calc.Nodes.Select(node => new RecipeCalculation(node.Value.Name)
                     {
                         Id = node.Value.Id,
                         Depth = this.Depth + 1,
                         IsSection = (node.Value.Nodes?.Any() == true),
                         ParentId = Id,
                         Entry = node.Value.Name,
                         Tier = node.Value.RecipeExists ? node.Value.Recipe.Level : 0,
                         Qty = node.Value.Quantity
                     }))
                {
                    children.Add(subsection);
                }
                return children;
            }

            // Schematics list with totals by type
            if (Section == DUData.SchematicsTitle)
            {
                if (!Calculator.GetFromStoreWithSchemas(ParentId, out var calc))
                    return children;
                foreach (var schemaItem in calc.SumSchemClass)
                {
                    if (!Calculator.CalcSchematic(schemaItem.Key, Math.Round(schemaItem.Value.Item1, 3),
                            out var minCost, out var maxCost, out var copies))
                    {
                        continue;
                    }
                    var s = DUData.Schematics.FirstOrDefault(x => x.Key == schemaItem.Key).Value;
                    var copyTime = copies * s.BatchTime;
                    var child = new RecipeCalculation(Section, null)
                    {
                        Entry = schemaItem.Key,
                        QtySchemata = schemaItem.Value.Item1,
                        AmtSchemata = Math.Round(minCost, 3),
                        Comment = "C: "+Utils.GetReadableTime(copyTime)+
                                  $" (x{copies:N3}) {s.Cost * s.BatchSize:N3} q for {s.BatchSize}",
                        ParentId = Id
                    };
                    if (char.IsDigit(child.Entry[1]))
                    {
                        child.Tier = int.Parse($"{child.Entry[1]}");
                    }
                    children.Add(child);
                }
                return children;
            }

            // Recipes like Relic Plasma offer no data
            if (Data == null) return children;

            foreach (var dataItem in Data)
            {
                var child = new RecipeCalculation(Section);
                var realKey = GetRealKey(dataItem.Key, out var t);
                child.Tier = t;
                child.Entry = realKey;
                child.CopyFrom(dataItem.Value);
                child.ParentId = Id;
                child.Depth = this.Depth + 1;
                child.Comment = dataItem.Value.SchematicType;

                // Exclude ores and special pures from drilldown
                var exclude = SumType == SummationType.PRODUCTS &&
                              (realKey.StartsWith("Catalyst", StringComparison.InvariantCultureIgnoreCase) ||
                               realKey.Contains("Hydrogen") ||
                               realKey.Contains("Oxygen"));
                if (SumType == SummationType.ORES || exclude)
                {
                    child.Section = child.Entry;
                    child.Entry = "";
                    if (exclude)
                    {
                        child.QtySchemata = 0;
                        child.AmtSchemata = 0;
                    }
                    children.Add(child);
                    continue;
                }

                if (SumType != SummationType.INGREDIENTS)
                {
                    var y = Calculator.All.FirstOrDefault(x => x.Value.Name.Equals(realKey, StringComparison.InvariantCultureIgnoreCase)).Value;
                    if (y != null)
                    {
                        child.Id = y.Id; // important!
                        child.IsSection = (y.Nodes?.Any() == true);
                        child.Section = DUData.SubpartSectionTitle;
                    }
                }
                children.Add(child);
            }
            return children;
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
