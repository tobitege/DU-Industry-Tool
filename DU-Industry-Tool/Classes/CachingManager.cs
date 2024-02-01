using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace DU_Industry_Tool
{
    public enum ItemTypeEnum
    {
        Part,
        Product
    }

    public class CacheKey
    {
        public int Quantity { get; set; }
        private ItemTypeEnum ItemType { get; set; }

        public CacheKey(string key, int quantity, ItemTypeEnum itemType)
        {
            this.Quantity = quantity;
            this.ItemType = itemType;
            this.Key = key;
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CacheKey cKey))
            {
                return false;
            }

            return Quantity == cKey.Quantity && ItemType == cKey.ItemType && Key == cKey.Key;
        }

        public string Key { get; }
    }

    public class CachingManager
    {
        private const int cacheSizeLimit = 2000;
        private const int ItemTypeCount = 2;

        private string cacheFolder;
        private int cachedItemCount = 0; // Track the number of cached items
        private readonly Dictionary<CacheKey, decimal>[] cachedCosts;

        public CachingManager()
        {
            cacheFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"AppData\Roaming\DU-Industry-Tool");
            cachedCosts = new Dictionary<CacheKey, decimal>[ItemTypeCount];
            foreach (var itemType in Enum.GetValues(typeof(ItemTypeEnum)))
            {
                cachedCosts[(int)itemType] = new Dictionary<CacheKey, decimal>();
            }
        }

        public decimal CalculateCost(SchematicRecipe recipe, int quantity, ItemTypeEnum itemType)
        {
            var cacheKey = GetCacheKey(recipe, quantity, itemType);

            // Check if the cost is already cached
            if (cachedCosts[(int)itemType].TryGetValue(cacheKey, out decimal cachedCost))
            {
                return cachedCost;
            }

            // Calculate the cost if not cached
            decimal cost = CalculateUncachedCost(recipe.Key, quantity, itemType);
            cachedCosts[(int)itemType][cacheKey] = cost;

            // Increment the cached item count
            cachedItemCount++;

            // Save the cache if necessary
            if (cachedItemCount >= cacheSizeLimit)
            {
                SaveCache();
            }

            return cost;
        }

        public void RefreshCache()
        {
            foreach (var itemType in Enum.GetValues(typeof(ItemTypeEnum)))
            {
                foreach (var cacheKeyValuePair in cachedCosts[(int)itemType])
                {
                    CacheKey cacheKey = cacheKeyValuePair.Key;
                    decimal cachedCost = cacheKeyValuePair.Value;

                    var uncachedCost = CalculateUncachedCost(cacheKey.Key, cacheKey.Quantity, (ItemTypeEnum)itemType);

                    if (uncachedCost != cachedCost)
                    {
                        cachedCosts[(int)itemType][cacheKey] = uncachedCost;
                    }
                }
            }
        }

        private decimal CalculateUncachedCost(string key, int quantity, ItemTypeEnum itemType)
        {
            // Reset and initialize the calculator for each calculation
            Calculator.ResetRecipeName();
            Calculator.Initialize();

            // Set the product quantity and call CalculateRecipe from the calculator
            Calculator.ProductQuantity = quantity;
            Calculator.CalculateRecipe(key, quantity, silent: true);

            // Retrieve the calculated cost from the calculator
            var calc = Calculator.Get(key, Guid.Empty);
            return calc.Retail;
        }

        private CacheKey GetCacheKey(SchematicRecipe recipe, int quantity, ItemTypeEnum itemType)
        {
            return new CacheKey(recipe.Key, quantity, itemType);
        }

        public void SetCacheFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            cacheFolder = folderPath;
        }

        private void LoadCache()
        {
            if (!File.Exists(Path.Combine(cacheFolder, "cachedCosts.json")))
            {
                return; // No cached costs file exists
            }

            try
            {
                string jsonString = File.ReadAllText(Path.Combine(cacheFolder, "cachedCosts.json"));
                var loadedData = JsonConvert.DeserializeObject<Dictionary<CacheKey, decimal>[]>(jsonString);

                if (loadedData != null)
                {
                    foreach (var itemType in loadedData)
                    {
                        var hash = (int)itemType.GetHashCode();
                        var existingDictionary = cachedCosts[hash];
                        if (!existingDictionary.ContainsKey(itemType.Keys.First()))
                        {
                            cachedCosts[hash] = itemType;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load cache: {ex.Message}");
            }
        }

        private void SaveCache()
        {
            if (cachedItemCount == 0)
            {
                return; // No need to save an empty cache
            }

            var jsonString = JsonConvert.SerializeObject(cachedCosts);
            File.WriteAllText(Path.Combine(cacheFolder, "cachedCosts.json"), jsonString);

            // Reset the cached item count
            cachedItemCount = 0;
        }

    }

}