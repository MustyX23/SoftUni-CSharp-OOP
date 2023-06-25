using System;
using System.Collections.Generic;
using System.Text;

namespace WildFarm
{
    public class Mouse : Mammal
    {
        public Mouse(string name, double weight, string livingRegion)
            : base(name, weight, livingRegion)
        {
        }

        public override void Feed(Food food)
        {
            if (food.GetType().Name == "Vegetable" || food.GetType().Name == "Fruit")
            {
                Weight += food.Quantity * 0.10;
                FoodEaten += food.Quantity;
            }
        }

        public override void SoundAbility()
        {
            Console.WriteLine("Squeak");
        }
    }
}
