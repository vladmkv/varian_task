using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sushi.Model;
using Sushi.Model.Checkout;
using Xunit;

namespace SushiTests
{
    public class PlatePriceDescendingComparerTest
    {
        [Fact]
        void TestCompare()
        {
            var factory = new PlateFactory();
            var greyPlate = factory.CreatePlate(PlateType.Grey);
            var greenPlate = factory.CreatePlate(PlateType.Green);

            var comparer = new PlatePriceDescendingComparer();

            Assert.Equal(0, comparer.Compare(greyPlate, greyPlate));

            // X < Y, in our terms grey plate must precede green one
            Assert.True(comparer.Compare(greyPlate, greenPlate) < 0);
        }
    }
}
