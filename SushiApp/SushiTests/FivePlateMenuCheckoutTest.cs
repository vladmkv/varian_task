using System.Collections.Generic;
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
        private readonly Order orderWithSoup;

        private readonly Order fivePlateMenuOrder;
        private readonly double fivePlateMenuOrderExpectedPrice;

        private readonly Order cheapPlateFirstOrder;
        private readonly double cheapPlateFirstOrderExpectedPrice;

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

            orderWithSoup = new Order(new List<Plate>()
            {
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Soup)
            });

            fivePlateMenuOrder = new Order(new List<Plate>()
            {
                factory.CreatePlate(PlateType.Blue),
                factory.CreatePlate(PlateType.Grey),
                factory.CreatePlate(PlateType.Grey),
                factory.CreatePlate(PlateType.Grey),
                factory.CreatePlate(PlateType.Grey)
            });

            fivePlateMenuOrderExpectedPrice = MENU_EXPECTED_PRICE;

            // Test that we indeed optimize the order by including most expensive
            // plates into menu part.
            cheapPlateFirstOrder = new Order(new List<Plate>()
            {
                factory.CreatePlate(PlateType.Blue),
                factory.CreatePlate(PlateType.Blue),
                factory.CreatePlate(PlateType.Blue),
                factory.CreatePlate(PlateType.Blue),
                factory.CreatePlate(PlateType.Grey),
                factory.CreatePlate(PlateType.Grey),
            });

            cheapPlateFirstOrderExpectedPrice = MENU_EXPECTED_PRICE + BLUE_PLATE_EXPECTED_PRICE;
        }

        [Fact]
        public void TestIsMenuApplicable()
        {
            var menuCheckout = new FivePlateMenuCheckout();

            Assert.True(menuCheckout.IsApplicable(orderWithRedPlate));
            Assert.True(menuCheckout.IsApplicable(orderWithBluePlate));

            Assert.False(menuCheckout.IsApplicable(orderWithFourPlates));
            Assert.False(menuCheckout.IsApplicable(orderMissingRedOrBlue));
            Assert.False(menuCheckout.IsApplicable(orderWithSoup));
        }

        [Fact]
        public void TestCalculatePrice()
        {
            var menuCheckout = new FivePlateMenuCheckout();

            Assert.Equal(fivePlateMenuOrderExpectedPrice,
                menuCheckout.CalculatePrice(fivePlateMenuOrder));

            Assert.Equal(cheapPlateFirstOrderExpectedPrice,
                menuCheckout.CalculatePrice(cheapPlateFirstOrder));
        }
    }
}