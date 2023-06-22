using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShortage
{
    public interface IBuyer
    {
        int Food { get; }
        string Name { get; }
        string Type { get; }

        void BuyFood();
    }
}
