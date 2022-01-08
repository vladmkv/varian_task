using System;
using System.Collections.Generic;
using System.Linq;
using Sushi.Model;
using Xunit;

namespace SushiTests
{
    public class OrderTest
    {
        [Fact]
        public void TestOrderTotal()
        {
            var factory = new PlateFactory();

            var bluePlate = factory.CreatePlate(PlateType.Blue);

            var order1 = new Order(Enumerable.Repeat(bluePlate, 5));
            Assert.Equal(4.75, order1.TotalPrice);

            var order2 = new Order(new List<Plate>());
            Assert.Equal(0, order2.TotalPrice);

            var plates3 = new List<Plate>
            {
                factory.CreatePlate(PlateType.Grey),
                factory.CreatePlate(PlateType.Green),
                factory.CreatePlate(PlateType.Yellow),
                factory.CreatePlate(PlateType.Red),
                factory.CreatePlate(PlateType.Blue)
            };

            var order3 = new Order(plates3);
            Assert.Equal(14.75, order3.TotalPrice);
        }
    }
}