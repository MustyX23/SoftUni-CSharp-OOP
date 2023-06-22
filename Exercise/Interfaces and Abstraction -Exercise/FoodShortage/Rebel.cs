using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShortage
{
    public class Rebel : Human
    {
        public Rebel(string name, int age, string group)
            : base(name, age)
        {
            Group = group;
            Food = 0;
            Type = "Rebel";
        }

        public string Group { get; set; }
        public override int Food { get; set; }

        public override void BuyFood()
        {
            Food += 5;
        }
    }
}
