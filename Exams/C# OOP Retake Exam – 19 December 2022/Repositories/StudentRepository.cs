using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class StudentRepository : IRepository<IStudent>
    {
        private List<IStudent> students;

        public StudentRepository()
        {
            students = new List<IStudent>();
        }
        public IReadOnlyCollection<IStudent> Models => students;

        public void AddModel(IStudent model)
        {
            students.Add(model);
        }

        public IStudent FindById(int id)
        {
            IStudent student = null;
            return student = students.FirstOrDefault(s => s.Id == id);
        }

        public IStudent FindByName(string name)
        {
            string[] splittedName = name.Split(' ');
            string firstName = splittedName[0];
            string lastName = splittedName[1];
            IStudent student = null;
            return student = students.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName);
        }
    }
}
