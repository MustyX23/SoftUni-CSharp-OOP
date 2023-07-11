using System;

namespace Shapes
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Shape rectangle = new Rectangle("Rectangle", 23, 13);
            Console.WriteLine(rectangle.Draw());

            Shape circle = new Circle("Circle", 13);
            Console.WriteLine(circle.Draw());
        }
    }
}
