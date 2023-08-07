using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms.Contracts;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Models.Gyms
{
    public abstract class Gym : IGym
    {
        private string name;
        private List<IEquipment> equipments;
        private List<IAthlete> athletes;

        public Gym(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            equipments = new List<IEquipment>();
            athletes = new List<IAthlete>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGymName);
                }
                name = value;
            }
        }

        public int Capacity { get; private set; }

        public double EquipmentWeight => this.Equipment.Sum(e => e.Weight);

        public ICollection<IEquipment> Equipment => equipments;

        public ICollection<IAthlete> Athletes => athletes.Distinct().ToList();

        public void AddAthlete(IAthlete athlete)
        {
            if (athletes.Count >= Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughSize);
            }
            athletes.Add(athlete);
        }
        public bool RemoveAthlete(IAthlete athlete)
        {
            if (athletes.Contains(athlete))
            {
                athletes.Remove(athlete);
                return true;
            }
            return false;
        }

        public void AddEquipment(IEquipment equipment)
        {
            equipments.Add(equipment);
        }

        public void Exercise()
        {
            foreach (var athlete in athletes)
            {
                athlete.Exercise();
            }
        }

        public string GymInfo()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"{this.Name} is a {this.GetType().Name}:");
            result.Append("Athletes: ");
            if (athletes.Count > 0)
            {
                result.AppendLine(String.Join(", ", athletes.Select(a => a.FullName)));
            }
            else
            {
                result.AppendLine("No athletes");
            }
            result.AppendLine($"Equipment total count: {this.equipments.Count}");
            result.AppendLine($"Equipment total weight: {this.EquipmentWeight:F2} grams");

            return result.ToString().TrimEnd();
        }        
    }
}
