using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles
{
    public abstract class Vehicle
    {
        private double fuelQuantity;
        private double fuelConsumption;

        public Vehicle(double fuelQuantity, double fuelConsumption)
        {
            FuelQuantity = fuelQuantity;
            FuelConsumption = fuelConsumption;
            FuelConsumptionPerKm = fuelConsumption;
        }
        public double FuelConsumptionPerKm { get; set; }
        public double FuelQuantity
        {
            get { return fuelQuantity; }
            set { fuelQuantity = value; } 
        }
        public double FuelConsumption
        {
            get { return fuelConsumption; }
            set { fuelConsumption = value; }
        }

        public abstract void Drive(double distance);
        public abstract void Refuel(double liters);
    }
}
