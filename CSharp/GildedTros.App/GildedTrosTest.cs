using System;
using System.Collections.Generic;
using Xunit;

namespace GildedTros.App
{
    public class GildedTrosTest
    {
        private const int STARTINGQUALITY = 25;
        private const int MINITERATIONS = 2;
        private const int MAXITERATIONS = 31;

        [Fact]
        public void UpdateQuality_NormalItem()
        {
            IList<Item> Items = [new() { Name = "foo", SellIn = 0, Quality = STARTINGQUALITY }];
            GildedTros app = new(Items);
            int iterations = new Random().Next(MINITERATIONS, MAXITERATIONS);
            int counter = 0;

            while (counter < iterations)
            {
                app.UpdateQuality();
                counter++;
            }



            Assert.Equal("foo", Items[0].Name);
            Assert.Equal(-1*iterations, Items[0].SellIn);
            Assert.Equal(Math.Max(0, STARTINGQUALITY - iterations), Items[0].Quality);
        }

        [Fact]
        public void UpdateQuality_WineItem()
        {
            IList<Item> Items = [new() { Name = "Good Wine", SellIn = 0, Quality = STARTINGQUALITY }];
            GildedTros app = new(Items);
            int iterations = new Random().Next(MINITERATIONS, MAXITERATIONS);
            int counter = 0;

            while (counter < iterations)
            {
                app.UpdateQuality();
                counter++;
            }

            Assert.Equal("Good Wine", Items[0].Name);
            Assert.Equal(-1 * iterations, Items[0].SellIn);
            Assert.Equal(Math.Min(50, 25 + iterations), Items[0].Quality);
        }

        [Fact]
        public void UpdateQuality_LegendaryItem()
        {
            IList<Item> Items = [new() { Name = "B-DAWG Keychain", SellIn = 0, Quality = STARTINGQUALITY }];
            GildedTros app = new(Items);

            int iterations = new Random().Next(MINITERATIONS, MAXITERATIONS);
            int counter = 0;

            while (counter < iterations)
            {
                app.UpdateQuality();
                counter++;
            }

            Assert.Equal("B-DAWG Keychain", Items[0].Name);
            Assert.Equal(0, Items[0].SellIn);
            Assert.Equal(80, Items[0].Quality);
        }

        [Fact]
        public void UpdateQuality_SmellyItem()
        {
            IList<Item> Items = [new() { Name = "Duplicate Code", SellIn = 0, Quality = STARTINGQUALITY }];
            GildedTros app = new(Items);

            int iterations = new Random().Next(MINITERATIONS, MAXITERATIONS);
            int counter = 0;

            while (counter < iterations)
            {
                app.UpdateQuality();
                counter++;
            }

            Assert.Equal("Duplicate Code", Items[0].Name);
            Assert.Equal(-1 * iterations, Items[0].SellIn);
            Assert.Equal(Math.Max(0, STARTINGQUALITY - (2 * iterations)), Items[0].Quality);
        }

        [Fact]
        public void UpdateQuality_BackstagePassItem()
        {
            IList<Item> Items = [new() { Name = "Backstage passes for Re:factor", SellIn = 15, Quality = STARTINGQUALITY }];
            GildedTros app = new(Items);

            int iterations = new Random().Next(MINITERATIONS, MAXITERATIONS);
            int counter = 0;

            while (counter < iterations)
            {
                app.UpdateQuality();
                counter++;
            }

            // Formula to calculate the expected quality after a variable amount of iterations
            int expectedQuality = iterations < 5 ? Math.Min(50, STARTINGQUALITY + iterations) : 
                iterations < 10 ? Math.Min(50, STARTINGQUALITY + (2 * iterations) - 4) :
                iterations < 15 ? Math.Min(50, STARTINGQUALITY + (3 * iterations) - 13) :
                0;

            Assert.Equal("Backstage passes for Re:factor", Items[0].Name);
            Assert.Equal(15 - iterations, Items[0].SellIn);
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Fact]
        public void UpdateQuality_AnyItem_QualityHigherThanZero()
        {
            Random random = new();
            IList<Item> Items = [
                new() { Name = "foo", SellIn = random.Next(1, 31), Quality = STARTINGQUALITY },
                new() { Name = "Good Wine", SellIn = random.Next(1, 31), Quality = STARTINGQUALITY },
                new() { Name = "B-DAWG Keychain", SellIn = random.Next(1, 31), Quality = STARTINGQUALITY },
                new() { Name = "Duplicate Code", SellIn = 0, Quality = STARTINGQUALITY },
                new() { Name = "Backstage passes for Re:factor", SellIn = 10, Quality = STARTINGQUALITY }];

            GildedTros app = new(Items);

            int iterations = new Random().Next(MINITERATIONS, MAXITERATIONS);
            int counter = 0;

            while (counter < iterations)
            {
                app.UpdateQuality();
                counter++;
            }

            foreach (Item item in Items)
                Assert.True(item.Quality >= 0);
        }
    }
}