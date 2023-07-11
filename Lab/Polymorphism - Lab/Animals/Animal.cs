using System;
using System.Collections.Generic;
using System.Text;

namespace Animals
{
    public abstract class Animal
    {
        //•	name: string
        private string name;
        //•	favouriteFood: string
        private string favouriteFood;

        protected Animal(string name, string favouriteFood)
        {
            Name = name;
            FavouriteFood = favouriteFood;
        }

        public string Name 
        {
            get { return name; }
            set { name = value; } 
        }
        public string FavouriteFood
        {
            get { return favouriteFood; }
            set { favouriteFood = value; }
        }
        public virtual string ExplainSelf()
        {
            return $"I am {Name} and my fovourite food is {FavouriteFood}";
        }
    }
}
