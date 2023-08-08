using EDriveRent.Core.Contracts;
using EDriveRent.Models.Contracts;
using EDriveRent.Models;
using EDriveRent.Repositories;
using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;
using EDriveRent.Utilities.Messages;

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
                return string.Format(OutputMessages.UserWithSameLicenseAlreadyAdded, drivingLicenseNumber);
            }

            user = new User(firstName, lastName, drivingLicenseNumber);
            users.AddModel(user);

            return string.Format(OutputMessages.UserSuccessfullyAdded, firstName, lastName, drivingLicenseNumber);
        }

        public string UploadVehicle(string vehicleTypeName, string brand, string model, string licensePlateNumber)
        {

            if (vehicleTypeName != nameof(PassengerCar) && vehicleTypeName != nameof(CargoVan))
            {
                return string.Format(OutputMessages.VehicleTypeNotAccessible, vehicleTypeName);
            }

            IVehicle vehicle = vehicles.FindById(licensePlateNumber);

            if (vehicle != null)
            {
                return string.Format(OutputMessages.LicensePlateExists, licensePlateNumber);
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
            return string.Format(OutputMessages.VehicleAddedSuccessfully, brand, model, licensePlateNumber);
        }

        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            // Find existing routes with the same start and end points
            IRoute existingRoute = routes.GetAll().FirstOrDefault(r =>
                r.StartPoint == startPoint && r.EndPoint == endPoint);

            if (existingRoute != null)
            {
                // If a existingRoute with the same start and end points exists, check if its length is greater than the given length
                if (existingRoute.Length == length)
                {
                    return string.Format(OutputMessages.RouteExisting, startPoint, endPoint, length);
                }
                if (existingRoute.Length < length)
                {
                    // If the existing existingRoute is longer, lock the new existingRoute
                    return string.Format(OutputMessages.RouteIsTooLong, startPoint, endPoint);
                }

                // If the new existingRoute is longer, lock the existing existingRoute
                existingRoute.LockRoute();
            }

            // If no existing existingRoute found, create a new existingRoute
            int routeId = routes.GetAll().Count + 1;
            IRoute newRoute = new Route(startPoint, endPoint, length, routeId);
            routes.AddModel(newRoute);
            return string.Format(OutputMessages.NewRouteAdded, startPoint, endPoint, length);
        }
        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {
            IUser user = users.FindById(drivingLicenseNumber);
            IVehicle vehicle = vehicles.FindById(licensePlateNumber);
            IRoute route = routes.FindById(routeId);


            if (user.IsBlocked)
            {
                return string.Format(OutputMessages.UserBlocked, drivingLicenseNumber);
            }

            if (vehicle.IsDamaged)
            {
                return string.Format(OutputMessages.VehicleDamaged, licensePlateNumber);
            }

            if (route.IsLocked)
            {
                return string.Format(OutputMessages.RouteLocked, routeId);
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

            return string.Format(OutputMessages.RepairedVehicles, vehiclesToRepairCount);
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