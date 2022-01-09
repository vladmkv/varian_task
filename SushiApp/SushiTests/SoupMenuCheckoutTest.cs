using System;
using System.Collections.Generic;
using System.Linq;
using Sushi.Model;
using Sushi.Model.Checkout;
using Xunit;

namespace SushiTests
{
    public class SoupMenuCheckoutTest
    {
        private const double MENU_EXPECTED_PRICE = 8.5;
        private const double BLUE_PLATE_EXPECTED_PRICE = 0.95;

        private readonly Order soupMenuOrder1;

        private readonly Order soupMenuCheapPlateFirstOrder;
        private readonly double soupMenuCheapPlateFirstOrderExpectedPrice;

        private readonly Order orderMissingSoup;

        private readonly Order orderMissingPlate;

        public SoupMenuCheckoutTest()
        {
            var factory = new PlateFactory();

            soupMenuOrder1 = new Order(new List<Plate>()
            {
                factory.CreatePlate(PlateType.Soup),
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Red),
            });

            // Test that we indeed optimize the order by including most expensive
            // plates into menu part.
            soupMenuCheapPlateFirstOrder = new Order(new List<Plate>()
            {
                factory.CreatePlate(PlateType.Soup),
                factory.CreatePlate(PlateType.Blue),
                factory.CreatePlate(PlateType.Blue),
                factory.CreatePlate(PlateType.Blue),
                factory.CreatePlate(PlateType.Blue),
                factory.CreatePlate(PlateType.Grey),
                factory.CreatePlate(PlateType.Grey),
            });

            soupMenuCheapPlateFirstOrderExpectedPrice = MENU_EXPECTED_PRICE + 2 * BLUE_PLATE_EXPECTED_PRICE;

            orderMissingSoup = new Order(new List<Plate>()
            {
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Red),
            });

            orderMissingPlate = new Order(new List<Plate>()
            {
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Soup),
            });
        }

        [Fact]
        public void TestIsMenuApplicable()
        {
            var menuCheckout = new SoupMenuCheckout();

            Assert.True(menuCheckout.IsApplicable(soupMenuOrder1));
            Assert.False(menuCheckout.IsApplicable(orderMissingSoup));
            Assert.False(menuCheckout.IsApplicable(orderMissingPlate));
        }

        [Fact]
        public void TestCalculatePrice()
        {
            var menuCheckout = new SoupMenuCheckout();

            Assert.Equal(MENU_EXPECTED_PRICE, menuCheckout.CalculatePrice(soupMenuOrder1));
            Assert.Equal(soupMenuCheapPlateFirstOrderExpectedPrice,
                menuCheckout.CalculatePrice(soupMenuCheapPlateFirstOrder));

        }
    }
}