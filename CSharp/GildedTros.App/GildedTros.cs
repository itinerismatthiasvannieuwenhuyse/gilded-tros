using System;
using System.Collections.Generic;

namespace GildedTros.App
{
    public class GildedTros(IList<Item> Items)
    {
        public IList<Item> Items = Items;

        private static readonly List<string> _backstagePassItems = ["Backstage passes for Re:factor", "Backstage passes for HAXX"];
        private static readonly List<string> _legendaryItems = ["B-DAWG Keychain"];
        private static readonly List<string> _wineItems = ["Good Wine"];
        private static readonly List<string> _smellyItems = ["Duplicate Code", "Long Methods", "Ugly Variable Names"];

        private const int MINIMUMQUALITY = 0;
        private const int MAXIMUMQUALITY = 50;
        private const int LEGENDARYQUALITY = 80;

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                SetSellIn(item);
                SetQuality(item);
            }
        }

        private static void SetQuality(Item item)
        {
            if (_backstagePassItems.Contains(item.Name))
            {
                if (item.SellIn > 0)
                    item.Quality += item.SellIn <= 5 ? 3 : item.SellIn <= 10 ? 2 : 1;
                else
                    item.Quality = MINIMUMQUALITY;
            }
            else if (_legendaryItems.Contains(item.Name))
            {
                item.Quality = LEGENDARYQUALITY;
            }
            else if (_wineItems.Contains(item.Name))
            {
                item.Quality += item.SellIn < 0 ? 2 : 1;
            }
            else if (_smellyItems.Contains(item.Name))
            {
                item.Quality -= item.SellIn < 0 ? 4 : 2;
            }
            else
            {
                item.Quality -= item.SellIn < 0 ? 2 : 1;
            }

            item.Quality = LimitQuality(item.Quality, _legendaryItems.Contains(item.Name));
        }

        private static int LimitQuality(int originalQuality, bool isLegendary) => Math.Min(isLegendary ? LEGENDARYQUALITY : MAXIMUMQUALITY, Math.Max(originalQuality, MINIMUMQUALITY));

        private static void SetSellIn(Item item) 
        {
            if (!_legendaryItems.Contains(item.Name))
                item.SellIn -= 1;
        }
    }
}
