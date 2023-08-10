using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSnake.GameObjects
{
    public class Wall : Point
    {
        private const char WallSymbol = '■';
        private int playerPoints;

        public Wall(int leftX, int topY)
            : base(leftX, topY)
        {
            this.InitializeBorders();
            this.PlayerInfo();
        }

        public bool IsPointOfWall(Point snakeHead)
            => snakeHead.LeftX == 0 || snakeHead.TopY == 0
            || snakeHead.LeftX == this.LeftX
            || snakeHead.TopY == this.TopY;

        private void InitializeBorders()
        {
            SetHorizontalPosition(0);

            SetVerticalBorder(0);

            SetVerticalBorder(LeftX - 1);

            Console.SetCursorPosition(0, TopY);

            SetHorizontalPosition(TopY);
        }

        private void SetHorizontalPosition(int y)
        {
            Console.SetCursorPosition(0, y);

            for (int i = 0; i < LeftX; i++)
            {                
                Console.Write(WallSymbol);
            }
        }

        private void SetVerticalBorder(int x)
        {
            for (int i = 1; i < TopY; i++)
            {
                Console.SetCursorPosition(x, i);
                Console.Write(WallSymbol);
            }
        }
        public void AddPoints(Queue<Point> snakeElements)
        {
            this.playerPoints = snakeElements.Count - 6;
        }

        public void PlayerInfo()
        {
            Console.SetCursorPosition(this.LeftX + 3, 0);
            Console.Write($"Player points: {this.playerPoints}");

            Console.SetCursorPosition(this.LeftX + 3, 1);
            Console.Write($"Player level: {this.playerPoints / 10}");
        }
    }
}
