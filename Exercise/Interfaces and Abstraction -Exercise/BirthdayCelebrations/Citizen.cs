using BirthdayCelebrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace BorderControl
{
    public class Citizen : IIdentifiable, IBirthable
    {
        public Citizen(string name, int age, string iD, string birthDate)
        {
            Name = name;
            Age = age;
            ID = iD;
            BirthDate = birthDate;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string ID { get; set; }
        public string BirthDate { get; set; }
    }
}
