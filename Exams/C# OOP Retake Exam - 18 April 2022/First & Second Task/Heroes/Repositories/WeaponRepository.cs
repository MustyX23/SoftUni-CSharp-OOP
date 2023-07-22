using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private readonly List<IWeapon> weapons;

        public WeaponRepository()
        {
            weapons = new List<IWeapon>();
        }

        public void Add(IWeapon model)
        {
            if (!weapons.Any(w => w.Name == model.Name))
            {
                weapons.Add(model);
            }
        }

        public bool Remove(IWeapon model)
        {
            return weapons.Remove(model);
        }

        public IWeapon FindByName(string name)
        {
            return weapons.FirstOrDefault(w => w.Name == name);
        }
        public IReadOnlyCollection<IWeapon> Models => weapons.AsReadOnly();
    }
}
