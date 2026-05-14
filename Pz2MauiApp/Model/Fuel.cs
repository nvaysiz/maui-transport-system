using System;
using System.Collections.Generic;
using System.Text;

namespace Pz2MauiApp.Model
{
    public class Fuel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public double PricePerLiter { get; set; }

        public override string ToString()
        {
            return $"{Name} - {PricePerLiter} руб./л";
        }
    }
}
