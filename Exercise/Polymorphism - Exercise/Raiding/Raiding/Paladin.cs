using System;
using System.Collections.Generic;
using System.Text;

namespace Raiding
{
    public class Paladin : BaseHero
    {
        public Paladin(string name)
            : base(name)
        {
            Power = 100;
        }

        public override void CastAbility()
        {
            Console.WriteLine($"Paladin - {Name} healed for {Power}");
        }
    }
}
