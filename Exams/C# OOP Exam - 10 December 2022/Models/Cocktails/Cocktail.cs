using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Cocktails
{
    public abstract class Cocktail : ICocktail
    {
        private string name;
        private string size;
        private double price;

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                name = value;
            }
        }

        public string Size
        {
            get { return size; }
            private set { size = value; }
        }

        public double Price
        {
            get { return price; }
            private set
            {
                if (size == "Large")
                    price = value;
                else if (size == "Middle")
                    price = value * 2 / 3;
                else if (size == "Small")
                    price = value / 3;
            }
        }

        protected Cocktail(string cocktailName, string size, double price)
        {
            //!
            Name = cocktailName;
            Size = size;
            Price = price;
        }

        public override string ToString()
        {
            return $"{name} ({size}) - {price:F2} lv";
        }
    }

}
