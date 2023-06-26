using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles
{
    public class Truck : Vehicle
    {
        public Truck(double fuelQuantity, double fuelConsumption)
            : base(fuelQuantity, fuelConsumption)
        {
            FuelConsumption += 1.6;
        }

        public override void Drive(double distance)
        {
            double liters = FuelConsumption * distance;

            if (FuelQuantity >= liters)
            {
                FuelQuantity -= liters;
                Console.WriteLine($"Truck travelled {distance} km");
            }
            else
            {
                Console.WriteLine("Truck needs refueling");
            }
        }

        public override void Refuel(double liters)
        {
            liters *= 0.95;
            FuelQuantity += liters;
        }
        public override string ToString()
        {
            return $"Truck: {FuelQuantity:f2}";
        }
    }
}
