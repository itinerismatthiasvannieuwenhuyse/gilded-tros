using System.Collections.Generic;

namespace GildedTros.App
{
    public class GildedTros(IList<Item> Items)
    {
        public IList<Item> Items = Items;

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                SetSellIn(item);
                SetQuality(item);
            }
        }

        protected static void SetQuality(Item item)
        {
            item.Quality = ItemQualityCalculator.CalculateQuality(item);
        }

        protected static void SetSellIn(Item item) 
        {
            item.SellIn = ItemSellInCalculator.CalculateSellIn(item);
        }
    }
}
