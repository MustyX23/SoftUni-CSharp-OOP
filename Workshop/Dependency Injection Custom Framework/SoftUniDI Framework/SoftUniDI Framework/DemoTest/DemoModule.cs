using SoftUniDI_Framework.DemoTest.HeroContracts;
using SoftUniDI_Framework.DemoTest.HeroModels;
using SoftUniDI_Framework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniDI_Framework.DemoTest
{
    public class DemoModule : AbstractModule
    {
        public override void Configure()
        {
            CreateMapping<IWeapon, Sword>();
            CreateMapping<IWarrior, Swordsmanship>();
        }
    }
}
