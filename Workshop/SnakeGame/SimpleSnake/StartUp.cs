namespace SimpleSnake
{
    using SimpleSnake.Core;
    using SimpleSnake.GameObjects;
    using System;
    using Utilities;

    public class StartUp
    {
        public static void Main()
        {
            ConsoleWindow.CustomizeConsole();
            Wall wall = new Wall(100, 50);
            Snake snake = new Snake(wall);

            Engine engine = new Engine(wall, snake);
            engine.Run();

        }
    }
}




