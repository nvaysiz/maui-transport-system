using System;

namespace Pz2MauiApp.Model
{
    public class Trip
    {
        public int Id { get; set; }
        public Vehicle Vehicle { get; set; }
        public double Distance { get; set; }
        public DateTime TripDate { get; set; }
        public string CargoName { get; set; } = "";
        public string Route { get; set; } = "";
        public double FuelAmount { get; set; }
        public double TotalCost { get; set; }

        public override string ToString()
        {
            return $"{TripDate:dd.MM.yyyy} | {Route} | {Distance} км | {TotalCost} руб.";
        }
    }
}