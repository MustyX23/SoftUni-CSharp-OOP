using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Repositories
{
    public class RaceRepository : IRepository<IRace>
    {
        private List<IRace> races;

        public RaceRepository()
        {
            races = new List<IRace>();
        }
        public IReadOnlyCollection<IRace> Models => races.AsReadOnly();

        public void Add(IRace model)
        {
            races.Add(model);
        }
        public bool Remove(IRace model)
        {
            IRace race = races.FirstOrDefault(race => race == model);
            if (race != null)
            {
                races.Remove(race);
                return true;
            }
            return false;
        }

        public IRace FindByName(string name)
        {
            return races.FirstOrDefault(race => race.RaceName == name);
        }

    }
}
