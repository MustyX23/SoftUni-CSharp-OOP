using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSnake.GameObjects
{
    public class FoodAsterisk : Food
    {
        private const char Asterisk = '*';
        private const int Points = 1;

        public FoodAsterisk(Wall wall)
            : base(wall, Asterisk, Points)
        {
        }
    }
}
