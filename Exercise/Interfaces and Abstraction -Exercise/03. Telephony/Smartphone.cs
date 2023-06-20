using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telephony
{
    public class Smartphone : ICall, IBrowse
    {
        public void Call(string phoneNumber)
        {
            if (!phoneNumber.All(p => Char.IsDigit(p)))
            {
                Console.WriteLine("Invalid number!");
            }
            else if (phoneNumber.Length == 10)
            {
                Console.WriteLine($"Calling... {phoneNumber}");
            }
        }
        public void Browse(string url)
        {
            if (!url.All(u => Char.IsDigit(u)))
            {
                Console.WriteLine("Invalid URL!");
                return;
            }

            Console.WriteLine($"Browsing: {url}!");
        }

    }
}
