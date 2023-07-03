using RobotService.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotService.Models
{
    public abstract class Robot : IRobot
    {
        private readonly List<int> interfaceStandards;

        private string model;
        private int batteryCapacity;

        public string Model 
        {
            get => model;
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Model cannot be null or empty.");
                }
                model = value;
            }
        }
        public int BatteryCapacity 
        {
            get => batteryCapacity;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Battery capacity cannot drop below zero.");
                }
                batteryCapacity = value;
            }
        }
        public int BatteryLevel { get; private set; }
        public int ConvertionCapacityIndex { get; private set; }
        public IReadOnlyCollection<int> InterfaceStandards => interfaceStandards;

        protected Robot(string model, int batteryCapacity, int convertionCapacityIndex)
        {
            
            Model = model;
            BatteryCapacity = batteryCapacity;
            BatteryLevel = batteryCapacity;
            ConvertionCapacityIndex = convertionCapacityIndex;
            interfaceStandards = new List<int>();
        }

        public void Eating(int minutes)
        {
            int energyProduced = ConvertionCapacityIndex * minutes;
            BatteryLevel = Math.Min(BatteryLevel + energyProduced, BatteryCapacity);
        }

        public bool ExecuteService(int consumedEnergy)
        {
            if (BatteryLevel >= consumedEnergy)
            {
                BatteryLevel -= consumedEnergy;
                return true;
            }

            return false;
        }

        public void InstallSupplement(ISupplement supplement)
        {
            interfaceStandards.Add(supplement.InterfaceStandard);
            BatteryCapacity -= supplement.BatteryUsage;
            BatteryLevel -= supplement.BatteryUsage;
        }

        public override string ToString()
        {
            string supplements = InterfaceStandards.Any() ? string.Join(" ", InterfaceStandards) : "none";

            return $"{GetType().Name} {Model}:\n" +
                   $"-- Maximum battery capacity: {BatteryCapacity}\n" +
                   $"-- Current battery level: {BatteryLevel}\n" +
                   $"-- Supplements installed: {supplements}";
        }
    }
}
