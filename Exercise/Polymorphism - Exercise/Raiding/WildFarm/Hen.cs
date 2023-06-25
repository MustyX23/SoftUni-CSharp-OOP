using System;
using System.Collections.Generic;
using System.Text;

namespace WildFarm
{
    public class Hen : Bird
    {
        public Hen(string name, double weight, double wingSize)
            : base(name, weight, wingSize)
        {
        }

        public override void Feed(Food food)
        {
            Weight += food.Quantity * 0.35;
            FoodEaten += food.Quantity;
        }

        public override void SoundAbility()
        {
            Console.WriteLine("Cluck");
        }
    }
}
