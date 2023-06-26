using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles
{
    public class Car : Vehicle
    {

        public Car(double fuelQuantity, double fuelConsumption)
            : base(fuelQuantity, fuelConsumption)
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
                TotalKilometres += distance;
                FuelQuantity -= liters;
                Console.WriteLine($"Car travelled {distance} km");
            }
            
        }

        public override void Refuel(double liters)
        {
            FuelQuantity += liters;
        }
        public override string ToString()
        {
            return $"Car: {FuelQuantity:f2}";
        }
    }
}
