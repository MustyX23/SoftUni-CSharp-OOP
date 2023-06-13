using System;
using System.Collections.Generic;
using System.Text;

namespace PersonsInfo
{
    public class Person
    {
        private string firstName;
        private string lastName;
        private int age;
        private decimal salary;

        public Person(string firstName, string lastName, int age, decimal salary)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Salary = salary;
        }

        public string FirstName
        {
            get { return firstName; }
            private set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            private set { lastName = value; }
        }
        public int Age
        {
            get { return age; }
            private set { age = value; }
        }
        public decimal Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        public override string ToString()
        {
            return $"{firstName} {lastName} receives {Salary:f2} leva.";
        }
        public void IncreaseSalary(decimal percentage)
        {
            decimal increase = Salary * percentage / 100;
            if (age < 30)
            {
                increase /= 2; 
            }

            Salary += increase;
        }
    }
}
