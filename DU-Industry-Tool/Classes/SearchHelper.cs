using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AutocompleteMenuNS;

// ReSharper disable ConvertToAutoProperty

namespace DU_Helpers
{
    public class RecipeAutocompleteItem : AutocompleteItem
    {
        public RecipeAutocompleteItem(string text) : base(text) { }

        public override CompareResult Compare(string fragmentText)
        {
            var searchResults = SearchHelper.SearchItems(fragmentText);
            return !searchResults.Contains(Text)
                ? CompareResult.Hidden
                : (searchResults.Count == 1 ? CompareResult.VisibleAndSelected : CompareResult.Visible);
        }
    }

    public static class SearchHelper
    {
        private static List<string> _searchableItems;
        private static int _maximumSearchLength = 50; // Default maximum search length
        private static int _minimumSearchLength = 3; // Default minimum search length
        private static bool _noResultsIfEmpty = false;
        private static string _noResultsText = "No results found";

        public static int MaxResults { get; set; } = 50;

        public static List<string> SearchableItems
        {
            get => _searchableItems ?? (_searchableItems = new List<string>());
            set => _searchableItems = value;
        }

        public static int MaximumSearchLength
        {
            get => _maximumSearchLength;
            set => _maximumSearchLength = value;
        }

        public static int MinimumSearchLength
        {
            get => _minimumSearchLength;
            set => _minimumSearchLength = value;
        }

        public static bool NoResultsIfEmpty
        {
            get => _noResultsIfEmpty;
            set => _noResultsIfEmpty = value;
        }

        public static string NoResultsText
        {
            get => _noResultsText;
            set => _noResultsText = value;
        }

        public static List<string> SearchItems(string searchText, bool useRegex = false)
        {
            // Truncate searchText if its length exceeds the specified maximum length
            searchText = searchText?.Substring(0, Math.Min(searchText.Length, MaximumSearchLength));

            // Return empty list if searchText is shorter than the specified minimum
            if (string.IsNullOrEmpty(searchText) || searchText.Length < MinimumSearchLength)
            {
                return NoResultsIfEmpty ? new List<string> { NoResultsText } : new List<string>();
            }

            List<string> matchingItems;

            try
            {
                if (useRegex)
                {
                    var regexPattern = WildcardToRegex(searchText);
                    var regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
                    matchingItems = SearchableItems.Where(item => regex.IsMatch(item)).ToList();
                }
                else
                {
                    matchingItems = SearchableItems.Where(item => WildcardMatch(item, searchText)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during search: {ex.Message}");
                matchingItems = new List<string> { ex.Message };
            }

            if (matchingItems.Any())
            {
                matchingItems = matchingItems.OrderBy(s => s, StringComparer.OrdinalIgnoreCase)
                                .Take(MaxResults)
                                .ToList();
                return matchingItems;
            }
            return NoResultsIfEmpty ? new List<string> { NoResultsText } : new List<string>();
        }

        public static string GetRegexPattern(string pattern)
        {
            if (pattern.IndexOfAny(new[] { '*', '?' }) == -1)
            {
                return @"\b\w+\b";
            }

            // Escape special characters in the pattern and replace wildcards
            var escapedPattern = Regex.Escape(pattern)
                .Replace("\\*", ".*")
                .Replace("\\?", ".");

            var regexPattern = ".*" + escapedPattern + ".*";
            return regexPattern;
        }

        private static bool WildcardMatch(string text, string pattern)
        {
            if (pattern.IndexOfAny(new[] { '*', '?' }) == -1)
            {
                // If no wildcards are present, perform a simple substring check
                return text.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0;
            }

            // Escape special characters in the pattern and replace wildcards
            var escapedPattern = Regex.Escape(pattern)
                .Replace("\\*", ".*")
                .Replace("\\?", ".");

            // wildcard testcase: "*ic*ic*xl*"
            var regexPattern = ".*" + escapedPattern + ".*";

            // Use regex for pattern matching
            var regex = new Regex(regexPattern, RegexOptions.IgnoreCase);

            return regex.IsMatch(text);
        }

        private static string WildcardToRegex(string pattern)
        {
            return "^" + Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".") + "$";
        }
    }
}