using System;
using System.Collections.Generic;
using System.Text;

namespace Person
{
    public class Child : Person
    {
        public Child(string name, int age)
            :base(name, age)
        {
            if (age > 15)
            {
                throw new ArgumentException("Child's age cannot be greater than 15.");
            }
        }
    }
}
