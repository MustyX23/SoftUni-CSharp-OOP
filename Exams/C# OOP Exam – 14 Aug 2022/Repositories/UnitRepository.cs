using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class UnitRepository : IRepository<IMilitaryUnit>
    {
        private List<IMilitaryUnit> units;

        public UnitRepository()
        {
            units = new List<IMilitaryUnit>();
        }
        public IReadOnlyCollection<IMilitaryUnit> Models
        {
            get { return units; }
        }

        public void AddItem(IMilitaryUnit model)
        {
            units.Add(model);
        }

        public IMilitaryUnit FindByName(string name)
        {
            IMilitaryUnit militaryUnit = null;

            return militaryUnit = units.FirstOrDefault(u => u.GetType().Name == name);
        }

        public bool RemoveItem(string name)
        {
            IMilitaryUnit militaryUnit = units.FirstOrDefault(u => u.GetType().Name == name);

            if (units.Contains(militaryUnit))
            {
                units.Remove(militaryUnit);
                return true;
            }
            return false;
        }
    }
}
