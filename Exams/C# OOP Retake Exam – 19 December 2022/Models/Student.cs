using System;
using System.Collections.Generic;
using System.Text;
using UniversityCompetition.Models.Contracts;

namespace UniversityCompetition.Models
{
    public class Student : IStudent
    {
        private int studentId;
        private string firstName;
        private string lastName;

        private List<int> coveredExams; // za sega e taka

        public int Id
        {
            get { return studentId; }
            private set
            {
                studentId = value;
            }
        }
        public string FirstName 
        {
            get { return firstName; }
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or whitespace!");
                }
                firstName = value; 
            }
        }
        public string LastName
        {
            get { return lastName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or whitespace!");
                }
                lastName = value;
            }
        }
        public IUniversity University { get; private set; }

        

        public Student(int studentId, string firstName, string lastName)
        {
            Id = studentId;
            FirstName = firstName;
            LastName = lastName;
            coveredExams = new List<int>();
        }

        public IReadOnlyCollection<int> CoveredExams => coveredExams.AsReadOnly();
        public void CoverExam(ISubject subject)
        {
            coveredExams.Add(subject.Id);
        }

        public void JoinUniversity(IUniversity university)
        {
            University = university;
        }
    }
}
