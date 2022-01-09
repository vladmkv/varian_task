using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sushi.Model.Checkout
{
    public class PlatePriceDescendingComparer : IComparer<Plate>
    {
        public int Compare(Plate? x, Plate? y)
        {
            var priceX = x?.Price ?? 0;
            var priceY = y?.Price ?? 0;

            return priceY.CompareTo(priceX);
        }
    }
}
