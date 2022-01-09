using System;
using System.Collections.Generic;
using System.Linq;
using Sushi.Model;
using Sushi.Model.Checkout;
using Xunit;

namespace SushiTests
{
    public class FivePlateMenuCheckoutTest
    {
        private const double MENU_EXPECTED_PRICE = 8.5;
        private const double BLUE_PLATE_EXPECTED_PRICE = 0.95;

        private readonly Order orderWithRedPlate;
        private readonly Order orderWithBluePlate;

        private readonly Order orderMissingRedOrBlue;
        private readonly Order orderWithFourPlates;

        private readonly Order soupMenuOrder2;
        private readonly double soupMenuOrder2ExpectedPrice;

        private readonly Order soupMenuOrder3;
        private readonly double soupMenuOrder3ExpectedPrice;

        private readonly Order soupMenuCheapPlateFirstOrder;
        private readonly double soupMenuCheapPlateFirstOrderExpectedPrice;

        private readonly Order orderMissingSoup;

        private readonly Order orderMissingPlate;

        public FivePlateMenuCheckoutTest()
        {
            var factory = new PlateFactory();

            orderWithRedPlate = new Order(new List<Plate>()
            {
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Red),
            });

            orderWithBluePlate = new Order(new List<Plate>()
            {
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Blue),
            });

            orderMissingRedOrBlue = new Order(new List<Plate>()
            {
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Grey),
            });

            orderWithFourPlates = new Order(new List<Plate>()
            {
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Green)
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

            // Some provided tests
            soupMenuOrder2 = new Order(new List<Plate>()
            {
                factory.CreatePlate(PlateType.Soup),
                factory.CreatePlate(PlateType.Grey),
                factory.CreatePlate(PlateType.Grey),
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Blue),
                factory.CreatePlate(PlateType.Blue)
            });

            soupMenuOrder2ExpectedPrice = 10.4;

            soupMenuOrder3 = new Order(new List<Plate>()
            {
                factory.CreatePlate(PlateType.Soup),
                factory.CreatePlate(PlateType.Grey),
                factory.CreatePlate(PlateType.Grey),
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Red)
            });

            soupMenuOrder3ExpectedPrice = 16.35;

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
            var menuCheckout = new FivePlateMenuCheckout();

            Assert.True(menuCheckout.IsApplicable(orderWithRedPlate));
            Assert.True(menuCheckout.IsApplicable(orderWithBluePlate));

            Assert.False(menuCheckout.IsApplicable(orderWithFourPlates));
            Assert.False(menuCheckout.IsApplicable(orderMissingRedOrBlue));
        }

        public void TestCalculatePrice()
        {
            var menuCheckout = new SoupMenuCheckout();

            //Assert.Equal(MENU_EXPECTED_PRICE, menuCheckout.CalculatePrice(soupMenuOrder1));

            Assert.Equal(soupMenuCheapPlateFirstOrderExpectedPrice,
                menuCheckout.CalculatePrice(soupMenuCheapPlateFirstOrder));

            Assert.Equal(soupMenuOrder2ExpectedPrice,
                menuCheckout.CalculatePrice(soupMenuOrder2));

            Assert.Equal(soupMenuOrder3ExpectedPrice,
                menuCheckout.CalculatePrice(soupMenuOrder3));


        }
    }
}