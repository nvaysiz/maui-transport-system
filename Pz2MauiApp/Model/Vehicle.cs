namespace Pz2MauiApp.Model
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Number { get; set; } = "";
        public string Brand { get; set; } = "";
        public string Model { get; set; } = "";
        public string DriverName { get; set; } = "";
        public double FuelConsumption { get; set; }
        public double AmortizationPerKm { get; set; }
        public Fuel FuelType { get; set; }

        public override string ToString()
        {
            return $"{Brand} {Model} ({Number})";
        }
    }
}