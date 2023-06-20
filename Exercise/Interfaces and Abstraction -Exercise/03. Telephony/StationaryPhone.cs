using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telephony
{
    public class StationaryPhone : ICall
    {
        public void Call(string phoneNumber)
        {
            if (!phoneNumber.All(p => Char.IsDigit(p)))
            {
                Console.WriteLine("Invalid number!");
                return;
            }
            if (phoneNumber.Length == 7)
            {
                Console.WriteLine($"Dialing... {phoneNumber}");
            }
        }
    }
}
