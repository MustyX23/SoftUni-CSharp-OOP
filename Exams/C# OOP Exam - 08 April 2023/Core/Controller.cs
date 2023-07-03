using RobotService.Core.Contracts;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RobotService.Models;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private SupplementRepository supplements;
        private RobotRepository robots;

        public Controller()
        {
            this.supplements = new SupplementRepository();
            this.robots = new RobotRepository();
        }

        public string CreateRobot(string model, string typeName)
        {
            // Check if the given typeName is a valid Robot's child class
            if (typeName != "DomesticAssistant" && typeName != "IndustrialAssistant")
            {
                return $"Robot type {typeName} cannot be created.";
            }

            // Create an IRobot from the valid child type
            IRobot robot;
            if (typeName == "DomesticAssistant")
            {
                robot = new DomesticAssistant(model);
            }
            else
            {
                robot = new IndustrialAssistant(model);
            }

            // Add the robot to the RobotRepository
            robots.AddNew(robot);
            return $"{typeName} {model} is created and added to the RobotRepository.";
        }
        public string CreateSupplement(string typeName)
        {
            // Check if the given typeName is a valid Supplement's child class
            if (typeName != "SpecializedArm" && typeName != "LaserRadar")
            {
                return $"{typeName} is not compatible with our robots.";
            }

            // Create an ISupplement from the valid child type
            ISupplement supplement;
            if (typeName == "SpecializedArm")
            {
                supplement = new SpecializedArm();
            }
            else
            {
                supplement = new LaserRadar();
            }

            // Add the supplement to the SupplementRepository
            supplements.AddNew(supplement);

            return $"{typeName} is created and added to the SupplementRepository.";
        }
        public string PerformService(string serviceName, int interfaceStandard, int totalPowerNeeded)
        {
            // Select robots that support the given interfaceStandard from the RobotRepository
            List<IRobot> selectedRobots = robots.Models().Where(r => r.InterfaceStandards.Contains(interfaceStandard)).ToList();

            // Check if none of the robots support the given interfaceStandard
            if (selectedRobots.Count == 0)
            {
                return $"Unable to perform service, {interfaceStandard} not supported!";
            }

            // Order selected robots by BatteryLevel descending
            selectedRobots = selectedRobots.OrderByDescending(r => r.BatteryLevel).ToList();

            // Find sum of BatteryLevel of selected robots
            int availablePower = selectedRobots.Sum(r => r.BatteryLevel);

            // Check if there is enough power to perform service
            if (availablePower < totalPowerNeeded)
            {
                return $"{serviceName} cannot be executed! {totalPowerNeeded - availablePower} more power needed.";
            }

            // Perform service using selected robots
            int usedRobotsCount = 0;
            foreach (IRobot robot in selectedRobots)
            {
                if (robot.BatteryLevel >= totalPowerNeeded)
                {
                    robot.ExecuteService(totalPowerNeeded);
                    usedRobotsCount++;
                    break;
                }
                else
                {
                    totalPowerNeeded -= robot.BatteryLevel;
                    robot.ExecuteService(robot.BatteryLevel);
                    usedRobotsCount++;
                }
            }

            return $"{serviceName} is performed successfully with {usedRobotsCount} robots.";
        }



        public string Report()
        {
            // Order robots by BatteryLevel descending, then by BatteryCapacity ascending
            List<IRobot> orderedRobots = robots.Models().OrderByDescending(r => r.BatteryLevel).ThenBy(r => r.BatteryCapacity).ToList();

            // Build report string
            StringBuilder report = new StringBuilder();
            foreach (IRobot robot in orderedRobots)
            {
                report.AppendLine(robot.ToString());
            }

            return report.ToString();
        }
        public string RobotRecovery(string model, int minutes)
        {
            // Select robots of the given model with BatteryLevel under 50% from the RobotRepository
            List<IRobot> selectedRobots = robots.Models().Where(r => r.Model == model && r.BatteryLevel < r.BatteryCapacity / 2).ToList();

            // Feed selected robots for the given number of minutes
            foreach (IRobot robot in selectedRobots)
            {
                robot.Eating(minutes);
            }

            return $"Robots fed: {selectedRobots.Count}";
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            // Find the first ISupplement with the given supplementTypeName in the SupplementRepository
            ISupplement supplement = supplements.Models().FirstOrDefault(s => s.GetType().Name == supplementTypeName);

            // Get the interface value of the supplement
            var selectedModels = this.robots.Models().Where(r => r.Model == model);


            // Find robots that do not support the interface value and are of the given model
            var stillNotUpgraded = selectedModels.Where(r => r.InterfaceStandards.All(s => s != supplement.InterfaceStandard));
            var robotForUpgrade = stillNotUpgraded.FirstOrDefault();
            // Check if all robots of the given model are already upgraded

            if (robotForUpgrade == null)
            {
                return $"All {model} are already upgraded!";
            }

            // Upgrade the first not upgraded robot
            robotForUpgrade.InstallSupplement(supplement);

            // Remove the supplement from the SupplementRepository
            supplements.RemoveByName(supplementTypeName);

            return $"{model} is upgraded with {supplementTypeName}.";
        }

        public void End()
        {
            // Perform any necessary cleanup or finalization tasks here

            // Exit the program
            Environment.Exit(0);
        }



    }
}
