namespace Animals
{
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<Animal> animals = new List<Animal>();

            string stopCommand;

            while ((stopCommand = Console.ReadLine()) != "Beast!")
            {
                try
                {
                    string animal = stopCommand;

                    string[] charachteristics = Console.ReadLine().Split(' ');

                    string name = charachteristics[0];
                    int age = int.Parse(charachteristics[1]);

                    if (animal == "Cat")
                    {
                        string gender = charachteristics[2];
                        Cat cat = new Cat(name, age, gender);
                        animals.Add(cat);
                    }
                    else if (animal == "Kitten")
                    {
                        Kitten kitten = new Kitten(name, age);
                        animals.Add(kitten);
                    }
                    else if (animal == "Tomcat")
                    {
                        Tomcat tomcat = new Tomcat(name, age);
                        animals.Add(tomcat);
                    }
                    else if (animal == "Dog")
                    {
                        string gender = charachteristics[2];
                        Dog dog = new Dog(name, age, gender);
                        animals.Add(dog);
                    }
                    else if (animal == "Frog")
                    {
                        string gender = charachteristics[2];
                        Frog frog = new Frog(name, age, gender);
                        animals.Add(frog);
                    }

                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input!");
                }                              
            }

            foreach (var animal in animals)
            {
                Console.WriteLine(animal.GetType().Name);
                Console.WriteLine($"{animal.Name} {animal.Age} {animal.Gender}");
                animal.ProduceSound();
            }
                       
        }
    }
}
