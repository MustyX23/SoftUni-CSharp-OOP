using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityCompetition.Models.Contracts;

namespace UniversityCompetition.Models
{
    public class University : IUniversity
    {
        private int universityId;
        private string universityName;       
        private string category;
        private int capacity;
        private List<int> requiredSubjects;

        public University(int universityId, string name, string category, int capacity, List<int> requiredSubjects)
        {
            Id = universityId;
            Name = name;
            Category = category;
            Capacity = capacity;
            this.requiredSubjects = requiredSubjects;
        }

        public int Id
        {
            get { return universityId; }
            private set { universityId = value; }
        }
        public string Name
        {
            get { return universityName; }
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or whitespace!");
                }
                universityName = value;
            }
        }
        public string Category
        {
            get { return category; }
            private set
            {
                if (value != "Technical" && value != "Economical" && value != "Humanity")
                {
                    throw new ArgumentException($"University category {value} is not allowed in the application!");
                }
                category = value;
            }
        }
        public int Capacity 
        { 
            get { return capacity; }
            private set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("University capacity cannot be a negative value!");
                }
                capacity = value;
            }
        }
        public IReadOnlyCollection<int> RequiredSubjects => requiredSubjects;
    }
}
