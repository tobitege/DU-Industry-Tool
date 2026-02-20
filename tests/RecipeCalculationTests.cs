using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using DU_Industry_Tool;

namespace DU_Industry_Tool.Tests
{
    public class RecipeCalculationTests
    {
        [Fact]
        public void RecipeCalculation_NullSection_ThrowsCorrectArgNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new RecipeCalculation(string.Empty));
            Assert.Equal("section", exception.ParamName);
        }

        [Fact]
        public void QtySchemata_SetToSameValue_RaisesPropertyChangedOnlyOnce()
        {
            var calc = new RecipeCalculation("Schematics");
            var raised = 0;

            calc.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == "QtySchemata")
                {
                    raised++;
                }
            };

            calc.QtySchemata = 5m;
            calc.QtySchemata = 5m;

            Assert.Equal(1, raised);
        }

        [Fact]
        public void AmtSchemata_SetToSameValue_RaisesPropertyChangedOnlyOnce()
        {
            var calc = new RecipeCalculation("Schematics");
            var raised = 0;

            calc.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == "AmtSchemata")
                {
                    raised++;
                }
            };

            calc.AmtSchemata = 12.5m;
            calc.AmtSchemata = 12.5m;

            Assert.Equal(1, raised);
        }

        [Fact]
        public void GetChildren_ForDataSection_ReturnsTypedChildrenAndParsesTierPrefix()
        {
            Calculator.Initialize();

            var data = new SortedDictionary<string, CalcEntry>
            {
                ["T2 Engine"] = new CalcEntry
                {
                    Qty = 3.5m,
                    Amt = 42m,
                    SchematicType = "T2P"
                }
            };

            var section = new RecipeCalculation("Products", data)
            {
                SumType = SummationType.INGREDIENTS
            };

            var children = section.GetChildren();
            var typedChildren = Assert.IsType<List<RecipeCalculation>>(children);
            var child = Assert.Single(typedChildren);

            Assert.Equal("Engine", child.Entry);
            Assert.Equal(2, child.Tier);
            Assert.Equal(3.5m, child.Qty);
            Assert.Equal(42m, child.Amt);
        }

        [Fact]
        public void GetChildren_WithShortTierLikeKey_DoesNotThrowAndKeepsEntry()
        {
            Calculator.Initialize();

            var data = new SortedDictionary<string, CalcEntry>
            {
                ["T2"] = new CalcEntry
                {
                    Qty = 1m,
                    Amt = 2m
                }
            };

            var section = new RecipeCalculation("Products", data)
            {
                SumType = SummationType.INGREDIENTS
            };

            var exception = Record.Exception(() => section.GetChildren());
            Assert.Null(exception);

            var child = Assert.Single(section.GetChildren().Cast<RecipeCalculation>());
            Assert.Equal("T2", child.Entry);
            Assert.Equal(0, child.Tier);
        }

        [Fact]
        public void GetChildren_ForSchematicsSection_OnlyReturnsKnownSchematicEntries()
        {
            Calculator.Initialize();

            DUData.Schematics.Clear();
            DUData.Schematics["T2P"] = new Schematic
            {
                Key = "T2P",
                Name = "T2 Product Schematic",
                Cost = 100m,
                BatchSize = 5,
                BatchTime = 120
            };

            var storeEntry = new CalculatorClass(new SchematicRecipe
            {
                Key = "test",
                Name = "Test Recipe",
                Level = 2,
                SchemaType = "T2P"
            });
            storeEntry.SumSchemClass.Add("T2P", Tuple.Create(6m, 0m));
            storeEntry.SumSchemClass.Add("T9X", Tuple.Create(3m, 0m)); // intentionally missing in DUData.Schematics
            Calculator.All.Add(storeEntry.Id, storeEntry);

            var section = new RecipeCalculation(DUData.SchematicsTitle)
            {
                IsSection = true,
                ParentId = storeEntry.Id
            };

            var children = section.GetChildren().Cast<RecipeCalculation>().ToList();
            var onlyChild = Assert.Single(children);

            Assert.Equal("T2P", onlyChild.Entry);
            Assert.Equal(2, onlyChild.Tier);
            Assert.Equal(6m, onlyChild.QtySchemata);
            Assert.True(onlyChild.AmtSchemata > 0);
        }
    }
}
