using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DU_Helpers
{
    public static class KryptonPaletteColorFinder
    {
        public static void FindColorAttributes(XDocument kryptonPaletteXml, Action<XElement> colorElementFound)
        {
            var propertiesElement = kryptonPaletteXml?.Root?.Element("Properties");
            if (propertiesElement == null) return;
            var visitedElements = new HashSet<XElement>(); // Track visited elements
            FindColorAttributesRecursive(propertiesElement, colorElementFound, visitedElements);
        }

        private static void FindColorAttributesRecursive(XElement element, Action<XElement> colorElementFound, HashSet<XElement> visitedElements)
        {
            if (!visitedElements.Add(element))
            {
                return; // Skip already visited elements
            }

            // Mark element as visited
            // Check for "Type" attribute with value "Color":
            var typeAttribute = element.Attribute("Type");
            if (typeAttribute is { Value: "Color" })
            {
                colorElementFound(element); // Fire event for found color element
            }

            // Check children, skipping any already visited:
            foreach (var childElement in element.Elements().Where(child => !visitedElements.Contains(child)))
            {
                FindColorAttributesRecursive(childElement, colorElementFound, visitedElements);
            }

            // Check siblings, skipping any already visited:
            var nextSibling = element.ElementsAfterSelf().FirstOrDefault(sibling => !visitedElements.Contains(sibling));
            if (nextSibling != null)
            {
                FindColorAttributesRecursive(nextSibling, colorElementFound, visitedElements);
            }
        }
    }
}