using System;
using System.Collections.Generic;
using System.Text;

namespace Shapes
{
    public class Circle : Shape
    {
        private double radius;

        public Circle(string name, double radius)
            : base(name)
        {
            Radius = radius;
        }

        public double Radius { get; private set; }

        public override double CalculateArea()
        {
            return 3.14 * Radius * Radius;
        }

        public override double CalculatePerimeter()
        {
            return 2 * 3.14 * Radius;
        }
        public override string Draw()
        {
            return base.Draw();
        }
    }
}
