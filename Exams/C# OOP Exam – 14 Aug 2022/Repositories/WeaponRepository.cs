using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private HashSet<IWeapon> weapons;

        public WeaponRepository()
        {
            weapons = new HashSet<IWeapon>();
        }
        public IReadOnlyCollection<IWeapon> Models => weapons;

        public void AddItem(IWeapon model)
        {
            weapons.Add(model);
        }

        public IWeapon FindByName(string name)
        {
            IWeapon weapon = null;
            return weapon = weapons.FirstOrDefault(W => W.GetType().Name == name);
        }

        public bool RemoveItem(string name)
        {
            IWeapon weapon = weapons.FirstOrDefault(W => W.GetType().Name == name);
            if (weapons.Contains(weapon))
            {
                weapons.Remove(weapon);
                return true;
            }
            return false;
            
        }
    }
}
