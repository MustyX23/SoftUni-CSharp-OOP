using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShortage
{
    public abstract class Human : IBuyer
    {
        public Human(string name, int age)
        {
            Name = name;
            Age = age;
            Type = string.Empty;
        }

        public string Name { get; set; }
        public int Age { get; }
        public string Type { get; set; }

        public abstract int Food { get; set; }
        
        public abstract void BuyFood();
        
    }
}
