using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDriveRent.Repositories
{
    public class RouteRepository : IRepository<IRoute>
    {
        private List<IRoute> routes;
        public RouteRepository()
        {
            routes = new List<IRoute>();
        }
        public void AddModel(IRoute model)
        {
            routes.Add(model);
        }
        public bool RemoveById(string identifier)
        {
            IRoute route = routes.FirstOrDefault(r => r.RouteId == int.Parse(identifier));
            if (route != null)
            {
                routes.Remove(route);
                return true;
            }
            return false;
        }
        public IRoute FindById(string identifier)
        {
            return routes.FirstOrDefault(r => r.RouteId == int.Parse(identifier));
        }

        public IReadOnlyCollection<IRoute> GetAll()
        {
            return routes.AsReadOnly();
        }
        
    }
}
