using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSnake.GameObjects
{
    public class FoodHash : Food
    {
        private const char Hash = '#';
        private const int Points = 3;

        public FoodHash(Wall wall)
            : base(wall, Hash, Points)
        {
        }
    }
}
