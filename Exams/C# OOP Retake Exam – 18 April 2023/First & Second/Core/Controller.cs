using EDriveRent.Core.Contracts;
using EDriveRent.Models.Contracts;
using EDriveRent.Models;
using EDriveRent.Repositories;
using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;

namespace EDriveRent.Core
{
    public class Controller : IController
    {
        private UserRepository users;
        private VehicleRepository vehicles;
        private RouteRepository routes;

        public Controller()
        {
            users = new UserRepository();
            vehicles = new VehicleRepository();
            routes = new RouteRepository();
        }

        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            IUser user = users.FindById(drivingLicenseNumber);
            if (user != null)
            {
                return $"{drivingLicenseNumber} is already registered in our platform.";
            }

            user = new User(firstName, lastName, drivingLicenseNumber);
            users.AddModel(user);

            return $"{firstName} {lastName} is registered successfully with DLN-{drivingLicenseNumber}";
        }

        public string UploadVehicle(string vehicleTypeName, string brand, string model, string licensePlateNumber)
        {

            if (vehicleTypeName != nameof(PassengerCar) && vehicleTypeName != nameof(CargoVan))
            {
                return $"{vehicleTypeName} is not accessible in our platform.";
            }

            IVehicle vehicle = vehicles.FindById(licensePlateNumber);

            if (vehicle != null)
            {
                return $"{licensePlateNumber} belongs to another vehicle.";
            }

            if (vehicleTypeName == nameof(PassengerCar))
            {
                vehicle = new PassengerCar(brand, model, licensePlateNumber);
            }
            else if (vehicleTypeName == nameof(CargoVan))
            {
                vehicle = new CargoVan(brand, model, licensePlateNumber);
            }

            vehicles.AddModel(vehicle);
            return $"{brand} {model} is uploaded successfully with LPN-{licensePlateNumber}";
        }

        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            // Find existing routes with the same start and end points
            IRoute route = routes.GetAll().FirstOrDefault(r =>
                r.StartPoint == startPoint && r.EndPoint == endPoint);

            if (route != null)
            {
                // If a route with the same start and end points exists, check if its length is greater than the given length
                if (route.Length == length)
                {
                    return $"{startPoint}/{endPoint} - {length} km is already added in our platform.";
                }
                if (route.Length < length)
                {
                    // If the existing route is longer, lock the new route
                    return $"{startPoint}/{endPoint} shorter route is already added in our platform.";
                }

                // If the new route is longer, lock the existing route
                route.LockRoute();
            }

            // If no existing route found, create a new route
            int routeId = routes.GetAll().Count + 1;
            route = new Route(startPoint, endPoint, length, routeId);
            routes.AddModel(route);
            return $"{startPoint}/{endPoint} - {length} km is unlocked in our platform.";
        }
        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {
            IUser user = users.FindById(drivingLicenseNumber);
            IVehicle vehicle = vehicles.FindById(licensePlateNumber);
            IRoute route = routes.FindById(routeId);


            if (user.IsBlocked)
            {
                return $"User {drivingLicenseNumber} is blocked in the platform! Trip is not allowed.";
            }

            if (vehicle.IsDamaged)
            {
                return $"Vehicle {licensePlateNumber} is damaged! Trip is not allowed.";
            }

            if (route.IsLocked)
            {
                return $"Route {routeId} is locked! Trip is not allowed.";
            }

            vehicle.Drive(route.Length);

            if (isAccidentHappened)
            {
                vehicle.ChangeStatus();
                user.DecreaseRating();
            }
            else
            {
                user.IncreaseRating();
            }

            return vehicle.ToString();
        }

        public string RepairVehicles(int count)
        {

            List<IVehicle> damagedVehicles = vehicles.GetAll().Where(v => v.IsDamaged)
                                          .OrderBy(v => v.Brand)
                                          .ThenBy(v => v.Model)
                                          .ToList();

            int vehiclesToRepairCount = Math.Min(count, damagedVehicles.Count);

            for (int i = 0; i < vehiclesToRepairCount; i++)
            {
                IVehicle vehicle = damagedVehicles[i];
                vehicle.ChangeStatus();
                vehicle.Recharge();
            }

            return $"{damagedVehicles.Count} vehicles are successfully repaired!";
        }

        public string UsersReport()
        {
            List<IUser> usersReport = users.GetAll().OrderByDescending(u => u.Rating).ThenBy(u => u.LastName).ThenBy(u => u.FirstName).ToList();
            StringBuilder reportBuilder = new StringBuilder();
            reportBuilder.AppendLine("*** E-Drive-Rent ***");

            foreach (IUser user in usersReport)
            {
                reportBuilder.AppendLine(user.ToString());
            }

            return reportBuilder.ToString().TrimEnd();
        }
    }
}