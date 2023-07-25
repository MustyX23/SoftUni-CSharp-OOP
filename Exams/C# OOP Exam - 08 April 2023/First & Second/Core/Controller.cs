using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private SupplementRepository supplements;
        private RobotRepository robots;

        public Controller()
        {
            supplements = new SupplementRepository();
            robots = new RobotRepository();
        }

        public string CreateRobot(string model, string typeName)
        {
            if (typeName != nameof(DomesticAssistant) && typeName != nameof(IndustrialAssistant))
            {
                return String.Format(OutputMessages.RobotCannotBeCreated, typeName);
            }

            IRobot robot;

            if (typeName == nameof(DomesticAssistant))
            {
                robot = new DomesticAssistant(model);
            }
            else
            {
                robot = new IndustrialAssistant(model);
            }
            robots.AddNew(robot);
            return String.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
        }
        public string CreateSupplement(string typeName)
        {
            if (typeName != nameof(SpecializedArm) && typeName != nameof(LaserRadar))
            {
                return String.Format(OutputMessages.SupplementCannotBeCreated, typeName);
            }
            ISupplement supplement;

            if (typeName == nameof(SpecializedArm))
            {
                supplement = new SpecializedArm();
            }
            else
            {
                supplement= new LaserRadar();
            }

            supplements.AddNew(supplement);
            return String.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
        }
        public string UpgradeRobot(string model, string supplementTypeName)
        {
            ISupplement supplement = supplements.Models().FirstOrDefault(s => s.GetType().Name == supplementTypeName);
            int interfaceValue = supplement.InterfaceStandard;

            List<IRobot> notUpgradedRobots = robots.Models()
            .Where(r => !r.InterfaceStandards.Contains(interfaceValue) && r.Model == model)
            .ToList();

            if (notUpgradedRobots.Count == 0)
            {
                return String.Format(OutputMessages.AllModelsUpgraded, model);
            }

            IRobot robot = notUpgradedRobots.First();
            robot.InstallSupplement(supplement);
            supplements.RemoveByName(supplementTypeName);
            return String.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName);
        }
        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            List<IRobot> selectedRobots = robots.Models()
            .Where(r => r.InterfaceStandards.Contains(intefaceStandard))
            .OrderByDescending(r => r.BatteryLevel)
            .ToList();

            if (selectedRobots.Count == 0)
            {
                return String.Format(OutputMessages.UnableToPerform, intefaceStandard);
            }

            int availablePower = selectedRobots.Sum(r => r.BatteryLevel);

            if (availablePower < totalPowerNeeded)
            {
                return String.Format(OutputMessages.MorePowerNeeded, serviceName, totalPowerNeeded - availablePower);
            }

            int usedRobotsCount = 0;
            foreach (var robot in selectedRobots)
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

            return String.Format(OutputMessages.PerformedSuccessfully, serviceName, usedRobotsCount);
        }
        public string RobotRecovery(string model, int minutes)
        {
            var robotsToFeed = robots.Models()
            .Where(r => r.Model == model && r.BatteryLevel < (r.BatteryCapacity * 0.5))
            .ToList();

            int fedCount = 0;
            foreach (var robot in robotsToFeed)
            {
                robot.Eating(minutes);
                fedCount++;
            }

            return $"Robots fed: {fedCount}";
        }
        public string Report()
        {
            var sortedRobots = robots.Models().OrderByDescending(r => r.BatteryLevel).ThenBy(r => r.BatteryCapacity);

            StringBuilder sb = new StringBuilder();
            foreach (var robot in sortedRobots)
            {
                sb.AppendLine(robot.ToString());
            }
            return sb.ToString().TrimEnd();
        }
        
    }
}