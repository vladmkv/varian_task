namespace Sushi.Model.Checkout
{
    public interface ICheckoutStrategy
    {
        public bool IsApplicable(Order order);

        double CalculatePrice(Order order);
    }
}
