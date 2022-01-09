﻿namespace Sushi.Model.Checkout
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

        public ICheckoutStrategy SelectStrategy(DateTime dateTime)
        {
            var time = dateTime.TimeOfDay;

            if (MenuDays.Contains(dateTime.DayOfWeek) && time >= MenuStartTime && time < MenuEndTime)
            {
                return new SoupMenuCheckout();
            }

            return new AdditiveCheckout();
        }
    }
}
