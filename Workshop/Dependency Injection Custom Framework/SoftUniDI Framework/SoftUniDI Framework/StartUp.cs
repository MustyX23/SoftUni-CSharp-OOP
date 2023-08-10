using Microsoft.Extensions.DependencyInjection;
using SoftUniDI_Framework.DemoTest;
using SoftUniDI_Framework.DemoTest.HeroContracts;
using SoftUniDI_Framework.Models;
using System;

namespace SoftUniDI_Framework
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello Dependency Injector Framework :)");

            var module = new DemoModule();

            var injector = DependencyInjector.CreateInjector(module);

            var swordsMan = injector.Inject<IWarrior>();
            

            // Output: The swordsMan attacks with Swinging the sword!
        }

    }
}
