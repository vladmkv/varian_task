using System;
using System.Collections.Generic;
using Sushi.Model;
using Sushi.Model.Checkout;
using Xunit;

namespace SushiTests
{
    public class CheckoutStrategySelectorTest
    {
        [Fact]
        public void TestSelectStrategyByTime()
        {
            var selector = new CheckoutStrategySelector();

            var factory = new PlateFactory();

            var soupMenuOrder = new Order(new List<Plate>()
            {
                factory.CreatePlate(PlateType.Soup),
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Red)
            });

            var outsideMenuDataTime = new DateTime(2022, 1, 9, 12, 0, 0);
            Assert.IsType<AdditiveCheckout>(selector.SelectStrategy(soupMenuOrder, outsideMenuDataTime));

            var menuDateTime = new DateTime(2022, 1, 10, 12, 0, 0);
            Assert.IsType<SoupMenuCheckout>(selector.SelectStrategy(soupMenuOrder, menuDateTime));

            // Test time corner cases
            var cornerMenuStarts = new DateTime(2022, 1, 10, 11, 0, 0);
            Assert.IsType<SoupMenuCheckout>(selector.SelectStrategy(soupMenuOrder, cornerMenuStarts));

            var cornerMenuEnded = new DateTime(2022, 1, 10, 17, 0, 0);
            Assert.IsType<AdditiveCheckout>(selector.SelectStrategy(soupMenuOrder, cornerMenuEnded));
        }

        [Fact]
        public void TestSelectStrategyByApplicability()
        {
            var selector = new CheckoutStrategySelector();

            var factory = new PlateFactory();

            var soupMenuOrder = new Order(new List<Plate>()
            {
                factory.CreatePlate(PlateType.Soup),
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Red)
            });

            var fivePlateOrder = new Order(new List<Plate>()
            {
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Red)
            });

            var basicOrder = new Order(new List<Plate>()
            {
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Red)
            });

            var menuDateTime = new DateTime(2022, 1, 10, 12, 0, 0);

            Assert.IsType<SoupMenuCheckout>(selector.SelectStrategy(soupMenuOrder, menuDateTime));
            Assert.IsType<FivePlateMenuCheckout>(selector.SelectStrategy(fivePlateOrder, menuDateTime));
            Assert.IsType<AdditiveCheckout>(selector.SelectStrategy(basicOrder, menuDateTime));

            // Without order time basic price calculation should occur
            Assert.IsType<AdditiveCheckout>(selector.SelectStrategy(soupMenuOrder));
            Assert.IsType<AdditiveCheckout>(selector.SelectStrategy(fivePlateOrder));
            Assert.IsType<AdditiveCheckout>(selector.SelectStrategy(basicOrder));
        }
    }
}