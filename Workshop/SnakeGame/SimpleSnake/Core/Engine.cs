using SimpleSnake.Enums;
using SimpleSnake.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SimpleSnake.Core
{
    public class Engine
    {
        private readonly Point[] pointDirections;
        private Direction direction;

        private readonly Wall wall;
        private readonly Snake snake;
        private float sleepTime;

        public Engine(Wall wall, Snake snake)
        {
            this.pointDirections = new Point[4];

            this.pointDirections[0] = new Point(1, 0); //Right
            this.pointDirections[1] = new Point(-1, 0); //Left

            this.pointDirections[2] = new Point(0, 1); //Up
            this.pointDirections[3] = new Point(0, -1); //Down

            this.wall = wall;
            this.snake = snake;
            this.sleepTime = 100;
        }

        public void Run()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    GetNextDirection();
                }

                bool IsMoving = this.snake.IsMoving(this.pointDirections[(int)direction]);

                if (!IsMoving)
                {
                    AskUserForRestart();
                }
                sleepTime -= 0.01f;

                Thread.Sleep((int)sleepTime);
            }
        }

        private void AskUserForRestart()
        {
            Console.SetCursorPosition(3, 3);
            Console.WriteLine("Would you like to continue? y/n");

            string input = Console.ReadLine();

            if (input == "y")
            {
                Console.Clear();
                StartUp.Main();
            }
            else
            {
                Console.Write("Game Over :(");
            }
        }

        private void GetNextDirection()
        {
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.LeftArrow)
            {
                if (this.direction != Direction.Right)
                {
                    this.direction = Direction.Left;
                }
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                if (this.direction != Direction.Left)
                {
                    this.direction = Direction.Right;
                }
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                if (this.direction != Direction.Down)
                {
                    this.direction = Direction.Up;
                }
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                if (this.direction != Direction.Up)
                {
                    this.direction = Direction.Down;
                }
            }

            Console.CursorVisible = false;
        }
    }
}
