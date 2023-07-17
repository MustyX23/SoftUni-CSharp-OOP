using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private PlanetRepository planets;

        public Controller()
        {
            planets = new PlanetRepository();
        }
        public string AddUnit(string unitTypeName, string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);
            if (planet == default)
            {
                throw new InvalidOperationException($"Planet {planetName} does not exist!");
            }

            if (planet.Army.Any(u => u.GetType().Name == unitTypeName))
            {
                throw new InvalidOperationException($"{unitTypeName} already added to the Army of {planetName}!");
            }

            MilitaryUnit unit = null;

            if (unitTypeName == "AnonymousImpactUnit")
            {
                unit = new AnonymousImpactUnit();
                planet.Spend(unit.Cost);
                planet.AddUnit(unit);                
                return $"{unitTypeName} added successfully to the Army of {planetName}!";
            }
            else if (unitTypeName == "SpaceForces")
            {
                unit = new SpaceForces();
                planet.Spend(unit.Cost);
                planet.AddUnit(unit);                
                return $"{unitTypeName} added successfully to the Army of {planetName}!";
            }
            else if (unitTypeName == "StormTroopers")
            {
                unit = new StormTroopers();
                planet.Spend(unit.Cost);
                planet.AddUnit(unit);                
                return $"{unitTypeName} added successfully to the Army of {planetName}!";
            }
            else
            {
                throw new InvalidOperationException($"{unitTypeName} still not available!");
            }
            
        }
        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            IPlanet planet = planets.FindByName(planetName);
            if (planet == default)
            {
                throw new InvalidOperationException($"Planet {planetName} does not exist!");
            }
            if (planet.Weapons.Any(u => u.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException($"{weaponTypeName} already added to the Weapons of {planetName}!");
            }
            if (weaponTypeName != "BioChemicalWeapon" &&
                weaponTypeName != "NuclearWeapon" &&
                    weaponTypeName != "SpaceMissiles")
            {
                throw new InvalidOperationException($"{weaponTypeName} still not available!");
            }
            IWeapon weapon = null;
            if (weaponTypeName == "BioChemicalWeapon")
            {
                weapon = new BioChemicalWeapon(destructionLevel);
            }
            else if (weaponTypeName == "NuclearWeapon")
            {
                weapon = new NuclearWeapon(destructionLevel);
            }
            else if (weaponTypeName == "SpaceMissiles")
            {
                weapon = new SpaceMissiles(destructionLevel);
            }
            planet.Spend(weapon.Price);
            planet.AddWeapon(weapon);
            double power = planet.MilitaryPower;
            return $"{planetName} purchased {weaponTypeName}!";
        }
        public string CreatePlanet(string name, double budget)
        {
            Planet planet = new Planet(name, budget);
            if (planets.FindByName(name) != default)
            {
                return $"Planet {planet.Name} is already added!";
            }
            planets.AddItem(planet);
            return $"Successfully added Planet: {planet.Name}";
        }
        public string ForcesReport()
        {
            
            StringBuilder report = new StringBuilder();
            report.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");

            foreach (Planet planet in planets.Models.OrderByDescending(p => p.MilitaryPower)
                .ThenBy(p => p.Name))
            {
                report.AppendLine(planet.PlanetInfo());
            }

            return report.ToString().TrimEnd();
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            Planet firstPlanet = (Planet)planets.FindByName(planetOne);
            Planet secondPlanet = (Planet)planets.FindByName(planetTwo);

            bool firstPlanetHasNuclearWeapon = firstPlanet.Weapons.Any(w=>w.GetType().Name == "NuclearWeapon");
            bool secondPlanetHasNuclearWeapon = secondPlanet.Weapons.Any(w => w.GetType().Name == "NuclearWeapon");

            if (firstPlanet.MilitaryPower > secondPlanet.MilitaryPower)
            {
                PerformCombatResults(firstPlanet, secondPlanet);
                return $"{firstPlanet.Name} destructed {secondPlanet.Name}!";
            }
            else if (firstPlanet.MilitaryPower < secondPlanet.MilitaryPower)
            {
                PerformCombatResults(firstPlanet, secondPlanet);
                return $"{secondPlanet.Name} destructed {firstPlanet.Name}!";
            }
            else
            {
                if (firstPlanetHasNuclearWeapon && !secondPlanetHasNuclearWeapon)
                {
                    PerformCombatResults(firstPlanet, secondPlanet);
                    return $"{firstPlanet.Name} destructed {secondPlanet.Name}!";
                }
                else if (!firstPlanetHasNuclearWeapon && secondPlanetHasNuclearWeapon)
                {
                    PerformCombatResults(secondPlanet, firstPlanet);
                    return $"{secondPlanet.Name} destructed {firstPlanet.Name}!";
                }
                else
                {
                    double halfBudgetFirstPlanet = firstPlanet.Budget / 2;
                    double halfBudgetSecondPlanet = secondPlanet.Budget / 2;

                    firstPlanet.Spend(halfBudgetFirstPlanet);
                    secondPlanet.Spend(halfBudgetSecondPlanet);
                    
                    return "The only winners from the war are the ones who supply the bullets and the bandages!";
                }
            }
            
        }
        public string SpecializeForces(string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);
            if (planet == default)
            {
                throw new InvalidOperationException($"Planet {planetName} does not exist!");
            }
            if (planet.Army.Count == 0)
            {
                throw new InvalidOperationException($"No units available for upgrade!");
            }
            planet.Spend(1.25);
            planet.TrainArmy();
            return $"{planetName} has upgraded its forces!";
        }
        private void PerformCombatResults(Planet winningPlanet, Planet losingPlanet)
        {
            double winningBudget = winningPlanet.Budget;
            double losingBudget = losingPlanet.Budget;

            double sumOfLoosingPlanetUnits = losingPlanet.Army.Sum(u => u.Cost);
            double sumOfLoosingPlanetWeapons = losingPlanet.Weapons.Sum(w => w.Price);

            winningPlanet.Spend(winningBudget / 2);
            winningPlanet.Profit(losingBudget / 2);
            winningPlanet.Profit(sumOfLoosingPlanetUnits + sumOfLoosingPlanetWeapons);

            planets.RemoveItem(losingPlanet.Name);
        }
    }
}
