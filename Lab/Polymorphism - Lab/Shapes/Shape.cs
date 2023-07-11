using System;
using System.Collections.Generic;
using System.Text;

namespace Shapes
{
    public abstract class Shape
    {
        protected Shape(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        //•	Abstract methods:
        //o CalculatePerimeter(): double
        public abstract double CalculatePerimeter();
        //o   CalculateArea() : double
        public abstract double CalculateArea();
        //•	Virtual methods:
        //o Draw(): string

        //	The method should get the name of class type as string, and should return a message in the format: $"Drawing {classType.Name}"
        public virtual string Draw() 
        {
            return $"Drawing {Name}";
        }

    }
}
