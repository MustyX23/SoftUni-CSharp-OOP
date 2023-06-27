using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles
{
    public abstract class Vehicle
    {
        private double fuelQuantity;
        private double fuelConsumption;
        private double tankCapacity;

        public Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity)
        {
            TankCapacity = tankCapacity;
            FuelQuantity = fuelQuantity;
            FuelConsumption = fuelConsumption;
            FuelConsumptionPerKm = fuelConsumption;            
        }
        public double FuelConsumptionPerKm { get; set; }
        public double TankCapacity
        {
            get { return tankCapacity; }
            set
            {
                tankCapacity = value;
            }
        }
        public double FuelQuantity
        {
            get { return fuelQuantity; }
            set
            {
                if (value > TankCapacity)
                {
                    value = 0;
                }
                fuelQuantity = value; 
            } 
        }
        public double FuelConsumption
        {
            get { return fuelConsumption; }
            set { fuelConsumption = value; }
        }
        
        public abstract void Drive(double distance);
        public virtual void Refuel(double liters) 
        {
            if (liters <= 0)
            {
                Console.WriteLine("Fuel must be a positive number");
            }
            else
            {
                FuelQuantity += liters;
            }
        }
    }
}
