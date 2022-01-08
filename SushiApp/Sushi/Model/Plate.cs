using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sushi.Model
{
    public class Plate
    {
        public PlateType Type { get; }

        public double Price { get; }

        public Plate(PlateType type, double price)
        {
            Type = type;
            Price = price;
        }
    }
}
