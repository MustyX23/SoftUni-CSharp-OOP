using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles
{
    public class Car : Vehicle
    {

        public Car(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption, tankCapacity)
        {
            FuelConsumptionPerKm += 0.9;
        }

        public override void Drive(double distance)
        {
            double liters = FuelConsumptionPerKm * distance;
                       
            if (FuelQuantity < liters)
            {
                Console.WriteLine("Car needs refueling");
            }
            else
            {
                FuelQuantity -= liters;
                Console.WriteLine($"Car travelled {distance} km");
            }
            
        }

        public override void Refuel(double fuel)
        {
            if (fuel <= 0)
            {
                Console.WriteLine($"Fuel must be a positive number");
                return;
            }

            if (FuelQuantity + fuel <= TankCapacity)
            {
                FuelQuantity += fuel;
            }
            else
            {
                Console.WriteLine($"Cannot fit {fuel} fuel in the tank");
            }
        }
        public override string ToString()
        {
            return $"Car: {FuelQuantity:f2}";
        }
    }
}
