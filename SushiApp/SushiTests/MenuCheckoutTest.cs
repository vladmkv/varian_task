using System;
using System.Collections.Generic;
using System.Linq;
using Sushi.Model;
using Sushi.Model.Checkout;
using Xunit;

namespace SushiTests
{
    public class MenuCheckoutTest
    {
        private const double MENU_EXPECTED_PRICE = 8.5;

        private readonly Order soupMenuOrder1;

        private readonly Order orderMissingSoup;

        private readonly Order orderMissingPlate;

        public MenuCheckoutTest()
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
            var menuCheckout = new MenuCheckout();

            Assert.True(menuCheckout.IsSoupMenuApplicable(soupMenuOrder1));
            Assert.False(menuCheckout.IsSoupMenuApplicable(orderMissingSoup));
            Assert.False(menuCheckout.IsSoupMenuApplicable(orderMissingPlate));
        }
    }
}