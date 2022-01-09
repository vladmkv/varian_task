namespace Sushi.Model
{
    public class PlateFactory
    {
        private static readonly IEnumerable<Plate> AvailablePlates = new Plate[]
        {
            new(PlateType.Grey, 4.95),
            new(PlateType.Green, 3.95),
            new(PlateType.Yellow, 2.95),
            new(PlateType.Red, 1.95),
            new(PlateType.Blue, 0.95),
            new(PlateType.Soup, 2.5),
        };

        public Plate CreatePlate(PlateType type)
        {
            var plate = AvailablePlates.FirstOrDefault(plate => plate.Type == type);

            if (plate == null)
                throw new InvalidOperationException("Invalid plate type: " + type);

            return plate;
        }
    }
}
