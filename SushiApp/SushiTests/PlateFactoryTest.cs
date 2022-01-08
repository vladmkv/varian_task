using System;
using Sushi.Model;
using Xunit;

namespace SushiTests
{
    public class PlateFactoryTest
    {
        [Fact]
        public void TestCreatePlate()
        {
            var factory = new PlateFactory();

            var invalidPlateType = (PlateType) 100;
            Assert.Throws<InvalidOperationException>(() => factory.CreatePlate(invalidPlateType));

            Assert.Equal(0.95, factory.CreatePlate(PlateType.Blue).Price);
        }
    }
}