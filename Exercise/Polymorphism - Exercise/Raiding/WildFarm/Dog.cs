using System;
using System.Collections.Generic;
using System.Text;

namespace WildFarm
{
    public class Dog : Mammal
    {
        public Dog(string name, double weight, string livingRegion)
            : base(name, weight, livingRegion)
        {
        }

        public override void Feed(Food food)
        {
            if (food.GetType().Name == "Meat")
            {
                Weight += food.Quantity * 0.40;
                FoodEaten += food.Quantity;
            }
            else
            {
                Console.WriteLine($"{GetType().Name} does not eat {food.GetType().Name}!");
            }
        }

        public override void SoundAbility()
        {
            Console.WriteLine("Woof!");
        }
    }
}
