using System;
using System.Collections.Generic;
using System.Text;

namespace Raiding
{
    public class Warrior : BaseHero
    {
        public Warrior(string name)
            : base(name)
        {
            Power = 100;
        }

        public override void CastAbility()
        {
            Console.WriteLine($"Warrior - {Name} hit for {Power} damage");
        }
    }
}
