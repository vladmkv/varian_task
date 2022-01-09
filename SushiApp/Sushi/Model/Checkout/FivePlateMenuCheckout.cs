using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sushi.Model.Checkout
{
    public class FivePlateMenuCheckout : ICheckoutStrategy
    {
        private const double MENU_PRICE = 8.5;

        public bool IsApplicable(Order order)
        {
            return (order.Plates.Any(plate => plate.Type == PlateType.Red) ||
                    order.Plates.Any(plate => plate.Type == PlateType.Blue)) &&
                   order.Plates.All(plate => plate.Type != PlateType.Soup) &&
                   order.Plates.Count() >= 5;
        }

        public double CalculatePrice(Order order)
        {
            var total = MENU_PRICE;

            var plates = order.Plates.ToList();

            // Remove Red plate or Blue plate otherwise
            var plateToRemove = plates.FirstOrDefault(plate => plate.Type == PlateType.Red) ??
                                plates.FirstOrDefault(plate => plate.Type == PlateType.Blue);

            if (plateToRemove == null)
            {
                throw new InvalidOperationException("Cannot apply checkout");
            }

            plates.Remove(plateToRemove);
            plates.Sort(new PlatePriceDescendingComparer());
            plates.RemoveRange(0, 4);

            // Add remaining plates prices
            total += plates.Select(plate => plate.Price).Sum();

            return total;
        }
    }
}
