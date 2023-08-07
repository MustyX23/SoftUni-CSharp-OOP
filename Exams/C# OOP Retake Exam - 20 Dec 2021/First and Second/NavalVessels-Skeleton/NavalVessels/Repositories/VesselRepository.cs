using NavalVessels.Models.Contracts;
using NavalVessels.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace NavalVessels.Repositories
{
    public class VesselRepository : IRepository<IVessel>
    {
        private readonly ICollection<IVessel> vessels;

        public VesselRepository()
        {
            vessels = new HashSet<IVessel>();
        }
        public IReadOnlyCollection<IVessel> Models => (IReadOnlyCollection<IVessel>)vessels;

        public void Add(IVessel model)
        {
            vessels.Add(model);
        }

        public IVessel FindByName(string name)
        {
            return vessels.FirstOrDefault(v => v.Name == name);
        }

        public bool Remove(IVessel model)
        {
            IVessel vessel = FindByName(model.Name);
            return vessels.Remove(vessel);
        }
    }
}
