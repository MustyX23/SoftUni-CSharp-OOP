using System;
using UniversityCompetition.Models.Contracts;

namespace UniversityCompetition.Models
{
    public abstract class Subject : ISubject
    {
        private int id;
        private string name;
        private double subjectRate;

        protected Subject(int id, string name, double subjectRate)
        {
            Id = id;
            Name = name;
            Rate = subjectRate;
        }

        public int Id 
        {
            get => id;
            private set => id = value;
        }
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
        public double Rate 
        {
            get => subjectRate;
            private set => subjectRate = value;
        }


    }
}
