using System;
using System.Collections.Generic;
using System.Text;
using Vehicles;

namespace VehiclesExtencion
{
    public class Bus : Vehicle, IBus
    {
        //public string Type { get; private set; }

        public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption, tankCapacity)
        {
        }

        public override void Drive(double distance)
        {
            double liters = FuelConsumptionPerKm * distance;

            if (FuelQuantity < liters)
            {
                Console.WriteLine("Bus needs refueling");
            }
            else
            {
                FuelQuantity -= liters;
                Console.WriteLine($"Bus travelled {distance} km");
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

        public void DriveBus(double distance)
        {
            FuelConsumptionPerKm += 1.4;
            Drive(distance);
            FuelConsumptionPerKm -= 1.4;
        }

        public override string ToString()
        {
            return $"Bus: {FuelQuantity:f2}";
        }
    }
}
