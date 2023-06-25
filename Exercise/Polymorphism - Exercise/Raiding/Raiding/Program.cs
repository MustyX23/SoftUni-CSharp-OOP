using System;
using System.Collections.Generic;
using System.Threading;

namespace Raiding
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<BaseHero> heroPowers = new List<BaseHero>();
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string heroName = Console.ReadLine();
                string heroType = Console.ReadLine();
                if (heroType == "Druid")
                {
                    Druid druid = new Druid(heroName);
                    heroPowers.Add(druid);
                }
                else if (heroType == "Paladin")
                {
                    Paladin paladin = new Paladin(heroName);
                    heroPowers.Add(paladin);
                }
                else if (heroType == "Rogue")
                {
                    Rogue rogue = new Rogue(heroName);
                    heroPowers.Add(rogue);
                }
                else if (heroType == "Warrior")
                {
                    Warrior warrior = new Warrior(heroName);
                    heroPowers.Add(warrior);
                }
                else
                {
                    Console.WriteLine("Invalid hero!");
                }
                
            }
            int bossPower = int.Parse(Console.ReadLine());
            int totalHeroPower = 0;

            foreach (var hero in heroPowers)
            {
                hero.CastAbility();
                totalHeroPower += hero.Power; 
            }            

            if (totalHeroPower > bossPower)
            {
                Console.WriteLine("Victory!");
            }
            else
            {
                Console.WriteLine("Defeat...");
            }
        }
    }
}
