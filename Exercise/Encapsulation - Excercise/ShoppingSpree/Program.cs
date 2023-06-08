using System;

namespace ShoppingSpree
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string input;

            while ((input = Console.ReadLine()) != "END")
            {
                //Peter=11;George=4
                string[] people = input.Split(';');
                string name = people[0];
                string age = people[1];
            }
        }
    }
}
