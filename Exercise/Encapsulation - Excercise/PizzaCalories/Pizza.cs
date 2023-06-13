using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCalories
{
    class Pizza
    {
        private string name;
        private Dough dough;
        private List<Topping> toppings;
        private double totalKCal;

        public string Name
        {
            get => this.name;
            set
            {
                if (value != null && value.Length >= 1 && value.Length <= 15)
                {
                    this.name = value;
                }
                else
                {
                    throw new InvalidOperationException("Pizza name should be between 1 and 15 symbols.");
                }
            }
        }

        public Dough Dough
        {
            set { this.dough = value; }
        }

        public Pizza()
        {
            this.dough = new Dough();
            this.toppings = new List<Topping>();
        }

        public void AddTopping(Topping topping)
        {
            this.toppings.Add(topping);
        }

        public int ToppingsCount()
        {
            return this.toppings.Count;
        }

        private double GetTotalKCal()
        {
            this.totalKCal += this.dough.CalculateKCals();

            foreach (var topping in this.toppings)
            {
                this.totalKCal += topping.TotalCals();
            }

            return this.totalKCal;
        }

        public override string ToString()
        {
            return $"{this.name} - {GetTotalKCal():f2} Calories.";
        }
    }
}
