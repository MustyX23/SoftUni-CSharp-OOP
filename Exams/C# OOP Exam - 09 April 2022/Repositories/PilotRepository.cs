using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Repositories
{
    public class PilotRepository : IRepository<IPilot>
    {
        private List<IPilot> pilots;

        public PilotRepository()
        {
            pilots = new List<IPilot>();
        }
        public IReadOnlyCollection<IPilot> Models => pilots.AsReadOnly();

        public void Add(IPilot model)
        {
            pilots.Add(model);
        }
        public bool Remove(IPilot model)
        {
            IPilot pilot = pilots.FirstOrDefault(pilot => pilot == model);
            if (pilot != null)
            {
                pilots.Remove(pilot);
                return true;
            }
            return false;   
        }

        public IPilot FindByName(string name)
        {
            return pilots.FirstOrDefault(p => p.FullName == name);
        }

    }
}
