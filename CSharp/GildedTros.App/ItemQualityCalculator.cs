using System;
using System.Collections.Generic;

namespace GildedTros.App
{
    public static class ItemQualityCalculator
    {
        private static readonly IList<string> s_backstagePassItems = ["Backstage passes for Re:factor", "Backstage passes for HAXX"];
        private static readonly IList<string> s_legendaryItems = ["B-DAWG Keychain"];
        private static readonly IList<string> s_wineItems = ["Good Wine"];
        private static readonly IList<string> s_smellyItems = ["Duplicate Code", "Long Methods", "Ugly Variable Names"];

        private const int MINIMUMQUALITY = 0;
        private const int MAXIMUMQUALITY = 50;
        private const int LEGENDARYQUALITY = 80;

        public static int Calculate(Item item)
        {
            bool itemIsExpired = item.SellIn < 0;
            int resultingQuality;

            if (s_backstagePassItems.Contains(item.Name))
                resultingQuality = CalculateQualityBackstagePassItem(item, itemIsExpired);
            else if (s_legendaryItems.Contains(item.Name))
                resultingQuality = CalculateQualityLegendaryItem(item, itemIsExpired);
            else if (s_wineItems.Contains(item.Name))
                resultingQuality = CalculateQualityWineItem(item, itemIsExpired);
            else if (s_smellyItems.Contains(item.Name))
                resultingQuality = CalculateQualitySmellyItem(item, itemIsExpired);
            else
                resultingQuality = CalculateQualityNormalItem(item, itemIsExpired);

            return LimitQuality(resultingQuality, s_legendaryItems.Contains(item.Name));
        }

        private static int CalculateQualityBackstagePassItem(Item item, bool expired)
        {
            int resultingQuality;

            if (!expired)
                resultingQuality = item.Quality + (item.SellIn <= 5 ? 3 : item.SellIn <= 10 ? 2 : 1);
            else
                resultingQuality = MINIMUMQUALITY;

            return resultingQuality;
        }

        private static int CalculateQualityLegendaryItem(Item item, bool expired)
        {
            return LEGENDARYQUALITY;
        }

        private static int CalculateQualityWineItem(Item item, bool expired)
        {
            return item.Quality + (expired ? 2 : 1);
        }

        private static int CalculateQualitySmellyItem(Item item, bool expired)
        {
            return item.Quality - (expired ? 4 : 2);
        }

        private static int CalculateQualityNormalItem(Item item, bool expired)
        {
            return item.Quality - (expired ? 2 : 1);
        }

        private static int LimitQuality(int originalQuality, bool isLegendary)
        {
            return Math.Min(isLegendary ? LEGENDARYQUALITY : MAXIMUMQUALITY, Math.Max(originalQuality, MINIMUMQUALITY));
        }

    }
}
