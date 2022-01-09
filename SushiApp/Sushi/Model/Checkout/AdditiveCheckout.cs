using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sushi.Model.Checkout
{
    public class AdditiveCheckout : ICheckoutStrategy
    {
        public bool IsApplicable(Order order)
        {
            throw new NotImplementedException();
        }

        public double CalculatePrice(Order order)
        {
            return order.TotalPrice;
        }
    }
}
