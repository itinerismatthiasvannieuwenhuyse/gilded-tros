using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GildedTros.App
{
    public class GildedTrosTest
    {
        private const int STARTINGQUALITY = 25;
        private const int STARTINGSELLIN = 15;
        private const int MINITERATIONS = 2;
        private const int MAXITERATIONS = 31;

        [Fact]
        public void UpdateQuality_NormalItem()
        {
            IList<Item> Items = [new() { Name = "foo", SellIn = STARTINGSELLIN, Quality = STARTINGQUALITY }];
            GildedTros app = new(Items);
            int iterations = new Random().Next(MINITERATIONS, MAXITERATIONS);
            int counter = 0;

            while (counter < iterations)
            {
                app.UpdateQuality();
                counter++;
            }

            int expectedQuality = STARTINGQUALITY - iterations;

            if (iterations > STARTINGSELLIN)
                expectedQuality -= iterations - STARTINGSELLIN;

            Assert.Equal("foo", Items[0].Name);
            Assert.Equal(STARTINGSELLIN - iterations, Items[0].SellIn);
            Assert.Equal(Math.Max(0, expectedQuality), Items[0].Quality);
        }

        [Fact]
        public void UpdateQuality_WineItem()
        {
            IList<Item> Items = [new() { Name = "Good Wine", SellIn = STARTINGSELLIN, Quality = STARTINGQUALITY }];
            GildedTros app = new(Items);
            int iterations = new Random().Next(MINITERATIONS, MAXITERATIONS);
            int counter = 0;

            while (counter < iterations)
            {
                app.UpdateQuality();
                counter++;
            }

            int expectedQuality = STARTINGQUALITY + iterations;

            if (iterations > STARTINGSELLIN)
                expectedQuality += iterations - STARTINGSELLIN;

            Assert.Equal("Good Wine", Items[0].Name);
            Assert.Equal(STARTINGSELLIN - iterations, Items[0].SellIn);
            Assert.Equal(Math.Min(50, expectedQuality), Items[0].Quality);
        }

        [Fact]
        public void UpdateQuality_LegendaryItem()
        {
            IList<Item> Items = [new() { Name = "B-DAWG Keychain", SellIn = STARTINGSELLIN, Quality = STARTINGQUALITY }];
            GildedTros app = new(Items);

            int iterations = new Random().Next(MINITERATIONS, MAXITERATIONS);
            int counter = 0;

            while (counter < iterations)
            {
                app.UpdateQuality();
                counter++;
            }

            Assert.Equal("B-DAWG Keychain", Items[0].Name);
            Assert.Equal(STARTINGSELLIN, Items[0].SellIn);
            Assert.Equal(80, Items[0].Quality);
        }

        [Fact]
        public void UpdateQuality_SmellyItem()
        {
            IList<Item> Items = [new() { Name = "Duplicate Code", SellIn = STARTINGSELLIN, Quality = STARTINGQUALITY }];
            GildedTros app = new(Items);

            int iterations = new Random().Next(MINITERATIONS, MAXITERATIONS);
            int counter = 0;

            while (counter < iterations)
            {
                app.UpdateQuality();
                counter++;
            }

            int expectedQuality = STARTINGQUALITY - (2 * iterations);

            if (iterations > STARTINGSELLIN)
                expectedQuality -= 2 * (iterations - STARTINGSELLIN) ;

            Assert.Equal("Duplicate Code", Items[0].Name);
            Assert.Equal(STARTINGSELLIN - iterations, Items[0].SellIn);
            Assert.Equal(Math.Max(0, expectedQuality), Items[0].Quality);
        }

        [Fact]
        public void UpdateQuality_BackstagePassItem()
        {
            IList<Item> Items = [new() { Name = "Backstage passes for Re:factor", SellIn = STARTINGSELLIN, Quality = STARTINGQUALITY }];
            GildedTros app = new(Items);

            int iterations = new Random().Next(MINITERATIONS, MAXITERATIONS);
            int counter = 0;

            while (counter < iterations)
            {
                app.UpdateQuality();
                counter++;
            }

            int remainder = STARTINGSELLIN - iterations;

            // Formula to calculate the expected quality after a variable amount of iterations
            int expectedQuality = remainder > 10 ? STARTINGQUALITY + iterations : 
                remainder > 5 ? STARTINGQUALITY + (2 * iterations) - 4 :
                remainder > 0 ? STARTINGQUALITY + (3 * iterations) - 13 :
                0;

            Assert.Equal("Backstage passes for Re:factor", Items[0].Name);
            Assert.Equal(STARTINGSELLIN - iterations, Items[0].SellIn);
            Assert.Equal(Math.Min(50, expectedQuality), Items[0].Quality);
        }

        [Fact]
        public void UpdateQuality_AnyItem_QualityHigherThanZero()
        {
            Random random = new();
            IList<Item> Items = [
                new() { Name = "foo", SellIn = STARTINGSELLIN, Quality = STARTINGQUALITY },
                new() { Name = "Good Wine", SellIn = STARTINGSELLIN, Quality = STARTINGQUALITY },
                new() { Name = "B-DAWG Keychain", SellIn = STARTINGSELLIN, Quality = STARTINGQUALITY },
                new() { Name = "Duplicate Code", SellIn = STARTINGSELLIN, Quality = STARTINGQUALITY },
                new() { Name = "Backstage passes for Re:factor", SellIn = STARTINGSELLIN, Quality = STARTINGQUALITY }];

            GildedTros app = new(Items);

            int iterations = new Random().Next(MINITERATIONS, MAXITERATIONS);
            int counter = 0;

            while (counter < iterations)
            {
                app.UpdateQuality();
                counter++;
            }

            Assert.True(!Items.Any(i => i.Quality < 0));
        }
    }
}