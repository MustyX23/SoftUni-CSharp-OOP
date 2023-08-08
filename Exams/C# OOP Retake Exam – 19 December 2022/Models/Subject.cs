using System;
using System.Collections.Generic;
using System.Text;
using UniversityCompetition.Models.Contracts;

namespace UniversityCompetition.Models
{
    public abstract class Subject : ISubject
    {
        private int subjectId;
        private string subjectName;
        private double subjectRate;

        protected Subject(int subjectId, string subjectName, double subjectRate)
        {
            Id = subjectId;
            Name = subjectName;
            Rate = subjectRate;
        }

        public int Id
        {
            get { return subjectId; }
            private set { subjectId = value; }
        }
        public string Name
        {
            get { return subjectName; }
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or whitespace!");
                }
                subjectName = value; 
            }
        }
        public double Rate 
        {
            get { return subjectRate; }
            private set
            {
                subjectRate = value;
            }
        }
    }
}
