using Heroes.Core.Contracts;
using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Models.Map;
using Heroes.Models.Weapons;
using Heroes.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private readonly HeroRepository heroes;
        private readonly WeaponRepository weapons;
        private readonly IMap map;

        public Controller()
        {
            heroes = new HeroRepository();
            weapons = new WeaponRepository();
            map = new Map();
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            IHero hero = null;
            if (type == nameof(Knight))
            {
                hero = new Knight(name, health, armour);
            }
            else if (type == nameof(Barbarian))
            {
                hero = new Barbarian(name, health, armour);
            }
            else
            {
                throw new InvalidOperationException("Invalid hero type.");
            }

            heroes.Add(hero);

            return $"Successfully added {type} {name} to the collection.";
        }
        public string CreateWeapon(string type, string name, int durability)
        {
            IWeapon weapon = null;

            switch (type)
            {
                case "Claymore":
                    weapon = new Claymore(name, durability);
                    break;
                case "Mace":
                    weapon = new Mace(name, durability);
                    break;
                default:
                    throw new InvalidOperationException("Invalid weapon type.");
            }

            weapons.Add(weapon);

            return $"A {type.ToLower()} {name} is added to the collection.";
        }

        public string AddWeaponToHero(string weaponName, string heroName)
        {
            var weapon = weapons.FindByName(weaponName);
            if (weapon == null)
            {
                throw new InvalidOperationException($"Weapon {weaponName} does not exist.");
            }

            var hero = heroes.FindByName(heroName);
            if (hero == null)
            {
                throw new InvalidOperationException($"Hero {heroName} does not exist.");
            }
            if (hero.Weapon != weapon && hero.Weapon != null)
            {
                throw new InvalidOperationException($"Hero {heroName} is well-armed.");
            }
            hero.AddWeapon(weapon);
            weapons.Remove(weapon);

            return $"Hero {heroName} can participate in battle using a {weapon.GetType().Name.ToLower()}.";
        }

        public string StartBattle()
        {
            return map.Fight((ICollection<IHero>)heroes.Models);
        }

        public string HeroReport()
        {
            var sortedHeroes = heroes.Models
            .OrderBy(h => h.GetType().Name)
            .ThenByDescending(h => h.Health)
            .ThenBy(h => h.Name)
            .ToList();

            var reportBuilder = new StringBuilder();

            foreach (var hero in sortedHeroes)
            {
                reportBuilder.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                reportBuilder.AppendLine($"--Health: {hero.Health}");
                reportBuilder.AppendLine($"--Armour: {hero.Armour}");
                reportBuilder.AppendLine($"--Weapon: {(hero.Weapon != null ? hero.Weapon.Name : "Unarmed")}");
            }

            return reportBuilder.ToString().TrimEnd();
        }
    }
}