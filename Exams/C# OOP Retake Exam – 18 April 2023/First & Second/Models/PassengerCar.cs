using System;
using System.Collections.Generic;
using System.Text;

namespace EDriveRent.Models
{
    public class PassengerCar : Vehicle
    {
        private const double MaxMileagePassengerCar = 450;
        public PassengerCar(string brand, string model, string licensePlateNumber)
            : base(brand, model, MaxMileagePassengerCar, licensePlateNumber)
        {
        }
    }
}
