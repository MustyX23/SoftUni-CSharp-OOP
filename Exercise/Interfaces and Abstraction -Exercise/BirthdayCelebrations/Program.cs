using BorderControl;
using System;
using System.Collections.Generic;

namespace BirthdayCelebrations
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<IBirthable> citizensAndPets = new List<IBirthable>();
            string input;

            while ((input = Console.ReadLine()) != "End")
            {

                string[] commandArgs = input.Split(' ');

                string name = commandArgs[1];

                if (commandArgs.Length == 5)
                {
                    //Citizen                    
                    int age = int.Parse(commandArgs[2]);
                    string id = commandArgs[3];
                    string dateOfBirth = commandArgs[4];
                    citizensAndPets.Add(new Citizen(name, age, id, dateOfBirth));
                }
                else if (commandArgs.Length == 3 && commandArgs[0] == "Pet")
                {
                    //Pet
                    string date = commandArgs[2];
                    citizensAndPets.Add(new Pet(name, date));
                }
                else if (commandArgs.Length == 3 && commandArgs[0] == "Robot")
                {
                    continue;
                }
            }

            string yearToSearchUpon = Console.ReadLine();

            foreach (var type in citizensAndPets)
            {
                string birthYear = type.BirthDate.Substring(type.BirthDate.Length - 4);

                if (yearToSearchUpon == birthYear)
                {
                    Console.WriteLine(type.BirthDate);
                }
            }
        }

    }
}
