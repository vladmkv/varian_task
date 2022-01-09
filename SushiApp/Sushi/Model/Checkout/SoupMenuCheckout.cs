namespace Sushi.Model.Checkout
{
    public class SoupMenuCheckout : ICheckoutStrategy
    {
        private const double MENU_PRICE = 8.5;

        public bool IsApplicable(Order order)
        {
            return order.Plates.Any(plate => plate.Type == PlateType.Soup) &&
                   order.Plates.Count(plate => plate.Type != PlateType.Soup) >= 4;
        }

        public double CalculatePrice(Order order)
        {
            var total = MENU_PRICE;

            var plates = order.Plates.ToList();

            // Remove soup menu items: a soup and four costliest plates.
            plates.Remove(plates.First(plate => plate.Type == PlateType.Soup));
            plates.Sort(new PlatePriceDescendingComparer());
            plates.RemoveRange(0, 4);

            // Add remaining plates prices
            total += plates.Select(plate => plate.Price).Sum();

            return total;
        }
    }
}
