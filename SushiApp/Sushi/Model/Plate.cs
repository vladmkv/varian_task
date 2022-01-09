namespace Sushi.Model
{
    public class Plate
    {
        public PlateType Type { get; }

        public double Price { get; }

        public Plate(PlateType type, double price)
        {
            Type = type;
            Price = price;
        }
    }
}
