using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCalories
{
    public class Topping
    {
        private double weight;
        private string type;
        private double totalCals;

        public double Weight
        {
            set
            {
                if (value < 1 || value > 50)
                {
                    throw new InvalidOperationException($"{type} weight should be in the range [1..50].");
                }
                weight = value;
            }
        }
        public string Type
        {
            set
            {
                if (value.ToLower() == "meat" || value.ToLower() == "veggies" ||
                value.ToLower() == "cheese" || value.ToLower() == "sauce")
                {
                    type = value;
                }
                else
                {
                    throw new InvalidOperationException($"Cannot place {value} on top of your pizza.");
                }
            }
        }
        public double TotalCals()
        {

            totalCals = weight * 2;

            if (type == "meat")
            {
                totalCals *= 2;
            }
            else if (type == "veggies")
            {
                totalCals *= 0.8;
            }
            else if (type == "cheese")
            {
                totalCals *= 1.1;
            }
            else if (type == "sauce")
            {
                totalCals *= 0.9;
            }
            return totalCals;
        }
    }
}
