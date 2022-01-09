using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sushi.Model
{
    public interface ICheckoutStrategy
    {
        double CalculatePrice(Order order);
    }
}
