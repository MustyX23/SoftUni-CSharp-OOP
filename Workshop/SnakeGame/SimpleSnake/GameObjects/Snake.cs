using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleSnake.GameObjects
{
    public class Snake 
    {
        private const char SnakeSymbol = '\u25CF';

        private readonly Queue<Point> snake;
        private readonly Food[] food;
        private readonly Wall wall;

        private int nextLeftX;
        private int nextTopY;
        private int foodIndex;

        public int GetRandomPosition => new Random().Next(0, this.food.Length);

        public Snake(Wall wall)
        {
            snake = new Queue<Point>();
            this.food = new Food[3];
            this.wall = wall;
            this.GetFood();
            this.CreateSnake();
            foodIndex = GetRandomPosition;
            this.food[foodIndex].SetRandomPosition(snake);
        }

        public bool IsMoving(Point direction)
        {
            Point snakeHead = snake.Last();
            GetNextDirection(direction, snakeHead);

            bool isPartOfSnake = snake.Any(s => s.LeftX == this.nextLeftX && s.TopY == this.nextTopY);

            var newHead = new Point(this.nextLeftX, this.nextTopY);

            if (isPartOfSnake)
            {
                return false;
            }

            bool isWall = this.wall.IsPointOfWall(newHead);

            if (isWall)
            {
                return false;
            }

            snake.Enqueue(newHead);
            newHead.Draw(SnakeSymbol);

            if (this.food[foodIndex].isFoodPoint(newHead))
            {
                this.Eat(direction, newHead);
            }

            Point tail = this.snake.Dequeue();
            tail.Draw(' ');
            return true;
        }

        private void Eat(Point direction, Point newHead)
        {
            var foodPoints = this.food[foodIndex].FoodPoints;

            for (int i = 0; i < foodPoints; i++)
            {
                this.snake.Enqueue(new Point(nextLeftX, nextTopY));
                GetNextDirection(direction, newHead);
            }
            this.wall.AddPoints(snake);
            this.wall.PlayerInfo();

            this.foodIndex = this.GetRandomPosition;
            this.food[foodIndex].SetRandomPosition(snake);
        }

        private void GetNextDirection(Point direction, Point snakeHead)
        {
            this.nextLeftX = snakeHead.LeftX + direction.LeftX;
            this.nextTopY = snakeHead.TopY + direction.TopY;
        }
        private void GetFood()
        {
            this.food[0] = new FoodHash(wall);
            this.food[1] = new FoodDollar(wall);
            this.food[2] = new FoodAsterisk(wall);
        }

        private void CreateSnake()
        {
            for (int leftX = 3; leftX <= 9; leftX++)
            {
                snake.Enqueue(new Point(leftX, 3));
            }
        }
    }
}
