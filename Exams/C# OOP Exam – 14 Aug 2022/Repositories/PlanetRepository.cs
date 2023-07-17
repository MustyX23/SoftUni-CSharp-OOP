using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private List<IPlanet> planets;

        public PlanetRepository()
        {
            planets = new List<IPlanet>();
        }
        public IReadOnlyCollection<IPlanet> Models => planets;

        public void AddItem(IPlanet model)
        {
            planets.Add(model);
        }

        public IPlanet FindByName(string name)
        {
            IPlanet planet = null;
            return planet = planets.FirstOrDefault(p => p.Name == name);
        }

        public bool RemoveItem(string name)
        {
            IPlanet planet = planets.FirstOrDefault(p => p.Name == name);
            if (planets.Contains(planet))
            {
                planets.Remove(planet);
                return true;
            }
            return false;   

        }
    }
}
