using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Krypton.Toolkit;

namespace DU_Industry_Tool
{
    public class Calculator2OutputClass
    {
        private readonly TreeListView _tlview;

        public Calculator2OutputClass(TreeListView tlv)
        {
            _tlview = tlv;
        }

        public void Fill(CalculatorClass calc)
        {
            if (_tlview == null || calc == null) return;

            // Important settings to ensure drawing of +/- glyphs
            _tlview.OwnerDraw = true;
            _tlview.ShowImagesOnSubItems = true;
            _tlview.VirtualMode = true;

            // TreeListView requires two delegates:
            // 1. CanExpandGetter - Can a particular model be expanded?
            // 2. ChildrenGetter  - Once the CanExpandGetter returns true, ChildrenGetter should return the list of children

            // CanExpandGetter is called very often! It must be very fast.
            _tlview.CanExpandGetter = x => ((RecipeCalculation)x).IsSection;

            // We just want to get the children of the given section.
            // This becomes a little complicated when we can't (for whatever reason). We need to report the error
            // to the user, but we can't just call MessageBox.Show() directly, since that would stall the UI thread
            // leaving the tree in a potentially undefined state (not good). We also don't want to keep trying to
            // get the contents of the given directory if the tree is refreshed. To get around the first problem,
            // we immediately return an empty list of children and use BeginInvoke to show the MessageBox at the
            // earliest opportunity. We get around the second problem by collapsing the branch again, so it's children
            // will not be fetched when the tree is refreshed. The user could still explicitly unroll it again --
            // that's their problem :)
            _tlview.ChildrenGetter = delegate (object x)
            {
                try
                {
                    return ((RecipeCalculation)x).GetChildren();
                }
                catch (Exception ex)
                {
                    _tlview.BeginInvoke((MethodInvoker)delegate()
                    {
                        _tlview.Collapse(x);
                        KryptonMessageBox.Show(ex.Message, @"Recipes Error", KryptonMessageBoxButtons.OK, false);
                    });
                    return new ArrayList();
                }
            };

            // Fill root elements with calculation sections (EXCEPT ingredients)
            var roots = new ArrayList();
            RecipeCalculation section;

            if (DUData.ProductionListMode)
            {
                // Add a section for production list
                section = new RecipeCalculation(DUData.ProductionListTitle)
                {
                    IsSection = true,
                    ParentId = calc.Id // Point to main item
                };
                roots.Add(section);
            }

            if (calc.Recipe != null)
            {
                // we exclude Ingredients for now
                for (var idx = 0; idx < (int)SummationType.INGREDIENTS; idx++)
                {
                    var s = ((SummationType)idx).ToString();
                    s = s[0].ToString().ToUpper() + s.Substring(1).ToLower();
                    section = new RecipeCalculation(s, calc.Get((SummationType)idx))
                    {
                        IsSection = true,
                        ParentId = calc.Id,
                        SumType  = (SummationType)idx
                    };
                    if (section.HasData)
                    {
                        roots.Add(section);
                    }
                }

                // Add a section for list of distinct schematic types
                if (calc.SumSchemClass?.Any() == true)
                {
                    section = new RecipeCalculation(DUData.SchematicsTitle)
                    {
                        IsSection = true,
                        ParentId = calc.Id
                    };
                    roots.Add(section);
                }
            }

            _tlview.Roots = roots;
        }
    }
}
