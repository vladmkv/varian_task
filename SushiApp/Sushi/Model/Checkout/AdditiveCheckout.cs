namespace Sushi.Model.Checkout
{
    public class AdditiveCheckout : ICheckoutStrategy
    {
        public bool IsApplicable(Order order) => true;

        public double CalculatePrice(Order order)
        {
            // Delegate to Order
            return order.TotalPrice;
        }
    }
}
