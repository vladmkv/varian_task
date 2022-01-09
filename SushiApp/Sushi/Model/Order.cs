using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sushi.Model
{
    public class Order
    {
        public IEnumerable<Plate> Plates { get; set; }

        // Implements User Story 1
        public double TotalPrice => Plates.Select(plate => plate.Price).Sum();

        public Order(IEnumerable<Plate> plates)
        {
            Plates = plates;
        }
    }
}
