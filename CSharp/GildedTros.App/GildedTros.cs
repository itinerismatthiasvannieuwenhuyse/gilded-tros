using System;
using System.Collections.Generic;

namespace GildedTros.App
{
    public class GildedTros(IList<Item> Items)
    {
        public IList<Item> Items = Items;

        private static readonly List<string> s_backstagePassItems = ["Backstage passes for Re:factor", "Backstage passes for HAXX"];
        private static readonly List<string> s_legendaryItems = ["B-DAWG Keychain"];
        private static readonly List<string> s_wineItems = ["Good Wine"];
        private static readonly List<string> s_smellyItems = ["Duplicate Code", "Long Methods", "Ugly Variable Names"];

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
            if (s_backstagePassItems.Contains(item.Name))
            {
                if (item.SellIn > 0)
                    item.Quality += item.SellIn <= 5 ? 3 : item.SellIn <= 10 ? 2 : 1;
                else
                    item.Quality = MINIMUMQUALITY;
            }
            else if (s_legendaryItems.Contains(item.Name))
            {
                item.Quality = LEGENDARYQUALITY;
            }
            else if (s_wineItems.Contains(item.Name))
            {
                item.Quality += item.SellIn < 0 ? 2 : 1;
            }
            else if (s_smellyItems.Contains(item.Name))
            {
                item.Quality -= item.SellIn < 0 ? 4 : 2;
            }
            else
            {
                item.Quality -= item.SellIn < 0 ? 2 : 1;
            }

            item.Quality = LimitQuality(item.Quality, s_legendaryItems.Contains(item.Name));
        }

        private static int LimitQuality(int originalQuality, bool isLegendary) => Math.Min(isLegendary ? LEGENDARYQUALITY : MAXIMUMQUALITY, Math.Max(originalQuality, MINIMUMQUALITY));

        private static void SetSellIn(Item item) 
        {
            if (!s_legendaryItems.Contains(item.Name))
                item.SellIn -= 1;
        }
    }
}
