using SoftUniDI_Framework.DemoTest.HeroContracts;
using SoftUniDI_Framework.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniDI_Framework.DemoTest.HeroModels
{
    public class Swordsmanship : IWarrior
    {
        private readonly IWeapon weapon;

        [Inject]
        public Swordsmanship(IWeapon weapon, string name)
        {
            weapon = new Sword();
            Name = name;
        }
        public string Name { get; private set; }

        public void Attack()
        {
            Console.Write("The Swordsmanship attacks with ");
            weapon.Use();
        }
    }
}
