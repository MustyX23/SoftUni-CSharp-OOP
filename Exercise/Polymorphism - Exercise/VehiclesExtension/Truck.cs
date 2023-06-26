using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles
{
    public class Truck : Vehicle
    {
        public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption, tankCapacity)
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

        public override void Refuel(double fuel)
        {
            if (fuel <= 0)
            {
                Console.WriteLine($"Fuel must be a positive number");
                return;
            }

            if (this.FuelQuantity + fuel <= TankCapacity)
            {
                fuel *= 0.95;
                this.FuelQuantity += fuel;
            }
            else
            {
                Console.WriteLine($"Cannot fit {fuel} fuel in the tank");
            }
        }
        public override string ToString()
        {
            return $"Truck: {FuelQuantity:f2}";
        }
    }
}
