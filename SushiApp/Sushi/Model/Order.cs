using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sushi.Model
{
    public class Order
    {
        IEnumerable<Plate> Plates { get; set; } = new List<Plate>();

        public float TotalPrice { get; } = default;
    }
}
