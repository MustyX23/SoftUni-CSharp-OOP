using System;
using System.Collections.Generic;
using System.Text;

namespace Animals
{
    public abstract class Animal
    {
        private string name;
        private int age;
        private string gender;
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Invalid input!");
                }
                else
                {
                    name = value;
                }
            }
        }
        public int Age
        {
            get { return age; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(nameof(this.age), "Age must be positive");
                }
                else
                {
                    this.age = value;
                }
            }
        }
        public string Gender
        {
            get
            {
                return gender;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Invalid input!");
                }
                else
                {
                    gender = value;
                }
            }
        }
        protected Animal(string name, int age, string gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }

        public abstract string ProduceSound();

    }
}
