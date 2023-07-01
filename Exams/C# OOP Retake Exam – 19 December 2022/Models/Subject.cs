using System;
using UniversityCompetition.Models.Contracts;

namespace UniversityCompetition.Models
{
    public abstract class Subject : ISubject
    {
        private int nextId = 1;
        private string name;

        protected Subject(string name, double rate)
        {
            Id = nextId++;
            Name = name;
            Rate = rate;
        }

        public int Id { get; private set; }
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or whitespace!");
                }
                name = value;
            }
        }
        public double Rate { get; private set; }


    }
}
