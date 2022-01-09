namespace Sushi.Model.Checkout
{
    public class CheckoutStrategySelector
    {
        private static readonly List<DayOfWeek> MenuDays = new()
        {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday
        };

        // Menu starts at 11:00
        private static readonly TimeSpan MenuStartTime = new TimeSpan(11, 0, 0);

        // Menu ends at 17:00
        private static readonly TimeSpan MenuEndTime = new TimeSpan(17, 0, 0);

        private readonly AdditiveCheckout additiveCheckout = new AdditiveCheckout();

        private readonly SoupMenuCheckout soupMenuCheckout = new SoupMenuCheckout();

        private readonly FivePlateMenuCheckout fivePlateMenuCheckout = new FivePlateMenuCheckout();

        public ICheckoutStrategy SelectStrategy(Order order, DateTime? optionalDateTime = null)
        {
            if (optionalDateTime != null)
            {
                var dateTime = (DateTime) optionalDateTime;
                var time = dateTime.TimeOfDay;

                if (MenuDays.Contains(dateTime.DayOfWeek) && time >= MenuStartTime && time < MenuEndTime)
                {
                    if (soupMenuCheckout.IsApplicable(order))
                    {
                        return soupMenuCheckout;
                    }
                    if (fivePlateMenuCheckout.IsApplicable(order))
                    {
                        return fivePlateMenuCheckout;
                    }
                }
            }

            return additiveCheckout;
        }
    }
}
