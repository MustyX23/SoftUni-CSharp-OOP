using SoftUniDI_Framework.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniDI_Framework.Models
{
    public class DependencyInjector
    {
        public static Injector CreateInjector(IModule module)
        {
            module.Configure();
            return new Injector(module);
        }
    }
}
