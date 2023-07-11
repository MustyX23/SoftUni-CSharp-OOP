using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class UniversityRepository : IRepository<IUniversity>
    {
        private List<IUniversity> universities; 
        public IReadOnlyCollection<IUniversity> Models => universities;

        public UniversityRepository()
        {
            this.universities = new List<IUniversity>();
        }
        public void AddModel(IUniversity model)
        {
            this.universities.Add(model);
        }

        public IUniversity FindById(int id)
        {
            return this.universities.FirstOrDefault(u => u.Id == id);
        }

        public IUniversity FindByName(string name)
        {
            return this.universities.FirstOrDefault(u => u.Name == name);
        }
    }
}
