using SoftUniDI_Framework.DemoTest.HeroContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniDI_Framework.DemoTest.HeroModels
{
    public class Sword : IWeapon
    {
        public void Use()
        {
            Console.WriteLine("⚔︎ Swish! ▬▬ι═══════ﺤ Swish! ⚔︎");
        }
    }
}
