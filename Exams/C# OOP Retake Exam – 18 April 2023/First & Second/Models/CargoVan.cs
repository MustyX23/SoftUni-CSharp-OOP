using System;
using System.Collections.Generic;
using System.Text;

namespace EDriveRent.Models
{
    public class CargoVan : Vehicle
    {
        private const double MaxMileageCargoVan = 180;
        public CargoVan(string brand, string model, string licensePlateNumber) 
            : base(brand, model, MaxMileageCargoVan, licensePlateNumber)
        {
        }
    }
}
