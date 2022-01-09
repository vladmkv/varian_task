using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sushi.Model.Checkout
{
    internal class PlateComparer : IComparer<Plate>
    {
        public int Compare(Plate? x, Plate? y)
        {
            var priceX = x?.Price ?? 0;
            var priceY = y?.Price ?? 0;

            return priceX.CompareTo(priceY);
        }
    }
}
