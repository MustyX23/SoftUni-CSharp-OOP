﻿using PlanetWars.Core;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using System;
using System.Collections.Generic;

namespace PlanetWars
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var engine = new Engine();
            engine.Run();

        }
    }
}
