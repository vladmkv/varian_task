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
