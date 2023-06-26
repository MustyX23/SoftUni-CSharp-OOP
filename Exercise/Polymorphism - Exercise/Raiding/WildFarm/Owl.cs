using System;
using System.Collections.Generic;
using System.Text;

namespace WildFarm
{
    public class Owl : Bird
    {
        public Owl(string name, double weight, double wingSize)
            : base(name, weight, wingSize)
        {
        }

        public override void Feed(Food food)
        {
            if (food.GetType().Name == "Meat")
            {
                Weight += food.Quantity * 0.25;
                FoodEaten += food.Quantity;
            }
            else
            {
                Console.WriteLine($"{GetType().Name} does not eat {food.GetType().Name}!");
            }
        }

        public override void SoundAbility()
        {
            Console.WriteLine("Hoot Hoot");
        }
    }
}
