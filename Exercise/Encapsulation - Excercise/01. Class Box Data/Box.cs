using System;
using System.Collections.Generic;
using System.Text;

namespace ClassBoxData
{
    public class Box
    {
        //Length - double, should not be zero or negative number
        private double length;
        // Width - double, should not be zero or negative number
        private double width;
        // Height - double, should not be zero or negative number
        private double height;

        public Box(double length, double width, double height)
        {
            Length = length;
            Width = width;
            Height = height;
        }

        public double Length
        {
            get
            {
                return length;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"Length cannot be zero or negative.");
                }
                length = value;
            }
        }
        public double Width
        {
            get
            {
                return width;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"Width cannot be zero or negative.");
                }
                width = value;
            }
        }
        public double Height
        {
            get
            {
                return height;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"Height cannot be zero or negative.");
                }
                height = value;
            }
        }

        public double SurfaceArea()
        {
            return (2 * (this.length * this.width)) + (2 * (this.length * this.height)) + (2 * (this.width * this.height));
        }
        public double LateralSurfaceArea()
        {
            return (2 * (this.length * this.height)) + (2 * (this.width * this.height));
        }
        public double Volume()
        {
            return this.length * this.width * this.height;
        }
    }
}
