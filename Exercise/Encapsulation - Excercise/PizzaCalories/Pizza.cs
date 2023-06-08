using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCalories
{
    public class Pizza
    {
        private string name;
        private string dough;
        private string toppings;

        public Pizza(string name, string dough, string toppings)
        {
            Name = name;
            Dough = dough;
            Toppings = toppings;
        }

        public string Name
        {
            get => name;
            private set => name = value;
        }
        public string Dough
        {
            get => dough;
            set => dough = value;
        }
        public string Toppings
        {
            get => toppings;
            set => toppings = value;
        }
    }
}
