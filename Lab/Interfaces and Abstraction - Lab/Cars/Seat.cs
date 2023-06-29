using System;
using System.Collections.Generic;
using System.Text;

namespace Cars
{
    public class Seat : Car
    {
        public Seat(string model, string color)
            :base()
        {
            Model = model;
            Color = color;
        }

        public override string ToString()
        {
            return $"{Color} Seat {Model}\n{Start()}\n{Stop()}";
        }
    }
}
