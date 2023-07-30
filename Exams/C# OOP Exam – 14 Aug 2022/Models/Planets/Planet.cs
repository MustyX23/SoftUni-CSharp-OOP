using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        private IRepository<IMilitaryUnit> units;
        private IRepository<IWeapon> weapons;

        private string name;
        private double budget;
        private double militaryPower;

        public Planet(string name, double budget)
        {
            Name = name;
            Budget = budget;
            units = new UnitRepository();
            weapons = new WeaponRepository();
        }

        public string Name
        {
            get { return name; }
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Planet name cannot be null or empty.");
                }
                name = value; 
            }
        }
        public double Budget
        {
            get { return budget; }
            private set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("Budget's amount cannot be negative.");
                }
                budget = value; 
            }
        }

        public double MilitaryPower
        {
            get
            {
                militaryPower = CalculateMilitaryPower();
                return militaryPower;
            }
            private set
            {
                militaryPower = Math.Round(value, 3);
            }
        }

        private double CalculateMilitaryPower()
        {
            double totalAmount = units.Models.Sum(u => u.EnduranceLevel) + 
                weapons.Models.Sum(w => w.DestructionLevel);
           

            if (units.Models.Any(u => u.GetType().Name == "AnonymousImpactUnit"))
            {
                totalAmount *= 1.3;
            }

            if (weapons.Models.Any(w => w.GetType().Name == "NuclearWeapon"))
            {
                totalAmount *= 1.45;
            }

            return totalAmount;
            
        }

        public IReadOnlyCollection<IMilitaryUnit> Army => units.Models;

        public IReadOnlyCollection<IWeapon> Weapons => weapons.Models;

        public void AddUnit(IMilitaryUnit unit)
        {
            units.AddItem(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            weapons.AddItem(weapon);
        }

        public string PlanetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Planet: {this.Name}");
            sb.AppendLine($"--Budget: {this.Budget} billion QUID");
            sb.Append($"--Forces: ");

            if (this.Army.Count == 0)
            {
                sb.AppendLine("No units");
            }
            else
            {
                var units = new Queue<string>();

                foreach (var item in this.Army)
                {
                    units.Enqueue(item.GetType().Name);
                }

                sb.AppendLine(string.Join(", ", units));
            }

            sb.Append($"--Combat equipment: ");

            if (this.Weapons.Count == 0)
            {
                sb.AppendLine("No weapons");
            }
            else
            {
                var equipment = new Queue<string>();

                foreach (var item in this.Weapons)
                {
                    equipment.Enqueue(item.GetType().Name);
                }

                sb.AppendLine(string.Join(", ", equipment));
            }
            sb.AppendLine($"--Military Power: {this.MilitaryPower}");

            return sb.ToString().Trim();
        }

        public void Profit(double amount)
        {
            Budget += amount;
        }

        public void Spend(double amount)
        {
            if (Budget < amount)
            {
                throw new InvalidOperationException("Budget too low!");
            }
            Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var unit in units.Models)
            {
                unit.IncreaseEndurance();
            }
        }
    }
}
