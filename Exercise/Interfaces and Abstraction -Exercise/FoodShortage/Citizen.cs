using BirthdayCelebrations;
using FoodShortage;
using System;
using System.Collections.Generic;
using System.Text;

namespace BorderControl
{
    public class Citizen : Human, IIdentifiable, IBirthable
    {

        public Citizen(string name, int age, string id, string birthDate)
            : base(name, age)
        {
            ID = id;
            BirthDate = birthDate;
            Food = 0;
            Type = "Citizen";
        }
        public string ID { get; set; }
        public string BirthDate { get; set; }
        public override int Food { get; set; }

        public override void BuyFood()
        {
            Food += 10;
        }
    }
}
