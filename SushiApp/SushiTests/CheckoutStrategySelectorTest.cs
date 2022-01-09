using System;
using Sushi.Model;
using Sushi.Model.Checkout;
using Xunit;

namespace SushiTests
{
    public class CheckoutStrategySelectorTest
    {
        [Fact]
        public void TestSelectStrategy()
        {
            var selector = new CheckoutStrategySelector();

            var outsideMenuDataTime = new DateTime(2022, 1, 9, 12, 0, 0);
            Assert.IsType<AdditiveCheckout>(selector.SelectStrategy(outsideMenuDataTime));

            var menuDateTime = new DateTime(2022, 1, 10, 12, 0, 0);
            Assert.IsType<SoupMenuCheckout>(selector.SelectStrategy(menuDateTime));

            // Test time corner cases
            var cornerMenuStarts = new DateTime(2022, 1, 10, 11, 0, 0);
            Assert.IsType<SoupMenuCheckout>(selector.SelectStrategy(cornerMenuStarts));

            var cornerMenuEnded = new DateTime(2022, 1, 10, 17, 0, 0);
            Assert.IsType<AdditiveCheckout>(selector.SelectStrategy(cornerMenuEnded));
        }
    }
}