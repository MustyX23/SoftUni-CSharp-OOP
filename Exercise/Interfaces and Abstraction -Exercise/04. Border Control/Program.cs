using System;
using System.Collections.Generic;

namespace BorderControl
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<IIdentifiable> inhabited = new List<IIdentifiable>();

            string input;

            while ((input = Console.ReadLine())!= "End")
            {
                string[] commandInfo = input.Split(" ");

                if (commandInfo.Length == 3)
                {
                    string name = commandInfo[0];
                    int age = int.Parse(commandInfo[1]);
                    string id = commandInfo[2];

                    inhabited.Add(new Citizen(name, age, id));
                }
                else
                {
                    string model = commandInfo[0];
                    string id = commandInfo[1];
                    inhabited.Add(new Robot(model, id));
                }
            }

            string lastDigitsOfFakeiIds = Console.ReadLine();

            foreach (var inhabit in inhabited)
            {
                if (inhabit.ID.EndsWith(lastDigitsOfFakeiIds))
                {
                    Console.WriteLine(inhabit.ID);
                }
            }

        }
    }
}
