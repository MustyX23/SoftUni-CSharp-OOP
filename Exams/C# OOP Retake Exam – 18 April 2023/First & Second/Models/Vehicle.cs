using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace EDriveRent.Models
{
    public abstract class Vehicle : IVehicle
    {
        private string brand;
        private string model;
        private string licensePlateNumber;

        protected Vehicle(string brand, string model, double maxMileage, string licensePlateNumber)
        {
            Brand = brand;
            Model = model;
            MaxMileage = maxMileage;
            LicensePlateNumber = licensePlateNumber;
            BatteryLevel = 100;
            IsDamaged = false;
        }

        public string Brand 
        {
            get => brand;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.BrandNull);
                }
                brand = value;
            }
        }

        public string Model 
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ModelNull);
                }
                model = value;
            }
        }

        public double MaxMileage { get; private set; }

        public string LicensePlateNumber 
        {
            get => licensePlateNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.LicenceNumberRequired);
                }
                licensePlateNumber = value;
            }
        }

        public int BatteryLevel { get; private set; }
        public bool IsDamaged { get; private set; }
        public void Drive(double mileage)
        {
            double percentage = mileage / MaxMileage * 100;
            if (this is CargoVan)
            {
                percentage += 5;
            }                
            BatteryLevel -= (int)Math.Round(percentage);

            if (BatteryLevel < 0)
            {
                BatteryLevel = 0;
            }
                
        }
        public void Recharge()
        {
            BatteryLevel = 100;
        }
        public void ChangeStatus()
        {
            if (IsDamaged == true)
            {
                IsDamaged = false;
            }
            else
            {
                IsDamaged = true;
            }
        }

        public override string ToString()
        {
            string status = IsDamaged ? "damaged" : "OK";
            return $"{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}% Status: {status}";
        }

    }
}
