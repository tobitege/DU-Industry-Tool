using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DU_Industry_Tool
{
    public class RecipeCalculation : INotifyPropertyChanged
    {
        #region Properties

        public Guid Id { get; private set; }
        public Guid ParentId { get; set; }
        public SummationType? SumType { get; set; }
        public int Tier { get; set; }
        public int Depth { get; set; }
        public bool IsActive = true;
        public bool IsSection { get; set; }
        public bool HasData => Data?.Any() == true;
        public readonly SortedDictionary<string, CalcEntry> Data;

        #endregion

        #region Constructors

        public RecipeCalculation(string section)
        {
            if (string.IsNullOrEmpty(section)) throw new ArgumentNullException(@"RecipeCalculation");
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
                if (qty.IsEqual(value)) return;
                qty = value;
                OnPropertyChanged("Qty");
            }
        }
        private decimal qty;

        public decimal Amt
        {
            get => amt;
            set
            {
                if (amt.IsEqual(value)) return;
                amt = value;
                OnPropertyChanged("Amt");
            }
        }
        private decimal amt;

        public decimal Mass
        {
            get => mass;
            set
            {
                if (mass.IsEqual(value)) return;
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
                if (vol.IsEqual(value)) return;
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

        public void CopyFrom(CalcEntry ce)
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
                foreach (var prd in calc.Recipe.Products)
                {
                    //var copyTime = "";
                    var amtS = 0m;
                    var s = DUData.Schematics.FirstOrDefault(x => x.Key == prd.SchemaType).Value;
                    if (s != null && Calculator.CalcSchematic(s.Key, prd.SchemaQty, out amtS, out _, out var copies))
                    {
                        //copyTime = " C: " + Utils.GetReadableTime(copies * s.BatchTime);
                    }
                    var child = new RecipeCalculation(Section, null)
                    {
                        Entry = prd.Name,
                        Qty = prd.Quantity,
                        ParentId = Id,
                        Tier = prd.Level,
                        QtySchemata = prd.SchemaQty,
                        AmtSchemata = amtS,
                        Mass = Math.Round(prd.Mass / 1000, 3),
                        Vol = Math.Round(prd.Volume / 1000, 3),
                        comment = prd.IsByproduct ? "Byproduct" : prd.SchemaType + (prd.SchemaQty > 0 ? " (cost broken down)" : "")
                    };
                    children.Add(child);
                }
                return children;
            }

            // For a recipe-driven entry, add sections (depth is even)
            // or the individual entries (depth is odd)
            // We only want Pures, Products and Parts here, though.
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
                    if (!Calculator.CalcSchematic(schemaItem.Key, schemaItem.Value.Item1,
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
                        AmtSchemata = maxCost, //minCost,
                        Comment = "C: "+Utils.GetReadableTime(copyTime)+
                                  $" (x{copies}) {s.Cost * s.BatchSize:N2}q for {s.BatchSize}",
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
                    //child.IsSection = SumType != SummationType.ORES;
                    //child.Section = DUData.SubpartSectionTitle;
                    //if (Calculator.GetFromStoreByName(ParentId, realKey, out var xxx))
                    //{
                    //}

                    var y = Calculator.All.FirstOrDefault(x => x.Value.Name.Equals(realKey, StringComparison.InvariantCultureIgnoreCase)).Value;
                    if (y != null)
                    {
                        child.Id = y.Id; // important!
                        child.IsSection = (y.Nodes?.Any() == true);
                        child.Section = DUData.SubpartSectionTitle;
                    }
                }

                // Set schematic cost for T2+ pures and all products
                //if (drilldown && (SumType == SummationType.PURES || SumType == SummationType.PRODUCTS))
                //{
                //    if (Calculator.GetFromStoreWithSums(ParentId, out var calc) &&
                //       (SumType == SummationType.PRODUCTS || child.Tier > 1))
                //    {
                //        var tmp = calc.Sums[(SummationType)SumType].FirstOrDefault(x =>
                //                    x.Key.Equals(dataItem.Key, StringComparison.InvariantCultureIgnoreCase));
                //        if (tmp.Key != null)
                //        {
                //            child.QtySchemata = tmp.Value.QtySchemata;
                //            child.AmtSchemata = tmp.Value.AmtSchemata;
                //        }
                //    }
                //}
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
