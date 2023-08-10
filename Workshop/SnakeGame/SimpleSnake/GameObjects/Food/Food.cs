using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleSnake.GameObjects
{
    public abstract class Food : Point
    {
        private readonly char foodSymbol;
        private readonly Random random;
        private readonly Wall wall;

        protected Food(Wall wall, char foodSymbol, int points)
            : base(0, 0)
        {
            FoodPoints = points;
            this.wall = wall;
            this.foodSymbol = foodSymbol;
            random = new Random();
        }

        public int FoodPoints { get; private set; }

        public bool isFoodPoint(Point snakeHead)
            => LeftX == snakeHead.LeftX && TopY == snakeHead.TopY;


        public void SetRandomPosition(Queue<Point> snake)
        {
            LeftX = random.Next(2, wall.LeftX - 2);
            TopY = random.Next(2, wall.TopY - 2);

            bool isPartOfSnake = snake.Any(s => s.TopY == TopY && s.LeftX == LeftX);

            while (isPartOfSnake)
            {
                LeftX = random.Next(2, wall.LeftX - 2);
                TopY = random.Next(2, wall.TopY - 2);

                isPartOfSnake = snake.Any(s => s.TopY == TopY && s.LeftX == LeftX);
            }
            Console.BackgroundColor = ConsoleColor.Red;
            Draw(foodSymbol);
            Console.BackgroundColor = ConsoleColor.White;
        }
    }
}
