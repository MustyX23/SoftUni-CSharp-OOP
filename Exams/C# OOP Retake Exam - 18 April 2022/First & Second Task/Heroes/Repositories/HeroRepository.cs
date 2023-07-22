using Heroes.Models.Contracts;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Repositories
{
    public class HeroRepository : IRepository<IHero>
    {
        private readonly List<IHero> heroes;
        public HeroRepository()
        {
            heroes = new List<IHero>();
        }
             
        public void Add(IHero model)
        {
            if (!heroes.Any(h => h.Name == model.Name))
            {
                heroes.Add(model);
            }
        }

        public bool Remove(IHero model)
        {
            return heroes.Remove(model);
        }

        public IHero FindByName(string name)
        {
            return heroes.FirstOrDefault(h => h.Name == name);
        }

        public IReadOnlyCollection<IHero> Models => heroes.AsReadOnly();

    }
}
