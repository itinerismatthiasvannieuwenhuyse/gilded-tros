using System.Collections.Generic;

namespace GildedTros.App
{
    public static class ItemSellInCalculator
    {
        private static readonly IList<string> s_legendaryItems = ["B-DAWG Keychain"];

        public static int Calculate(Item item)
        {
            return item.SellIn - (s_legendaryItems.Contains(item.Name) ? 0 : 1);
        }
    }
}
