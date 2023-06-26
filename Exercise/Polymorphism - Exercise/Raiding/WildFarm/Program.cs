using System;
using System.Collections.Generic;

namespace WildFarm
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Animal> animals = new List<Animal>();

            string input;

            while ((input = Console.ReadLine()) != "End")
            {
                Animal animal = null;

                string[] info = input.Split(' ');

                string type = info[0];
                string name = info[1];
                double weight = double.Parse(info[2]);

                if (type == "Hen")
                {
                    double wingSize = double.Parse(info[3]);
                    animal = new Hen(name, weight, wingSize);
                }
                else if (type == "Owl")
                {
                    double wingSize = double.Parse(info[3]);
                    animal = new Owl(name, weight, wingSize);
                }
                else if (type == "Mouse")
                {
                    string livingRegion = info[3];
                    animal = new Mouse(name, weight, livingRegion);
                }
                else if (type == "Cat")
                {
                    string livingRegion = info[3];
                    string breed = info[4];
                    animal = new Cat(name, weight, livingRegion, breed);
                }
                else if (type == "Dog")
                {
                    string livingRegion = info[3];
                    animal = new Dog(name, weight, livingRegion);
                }
                else if (type == "Tiger")
                {
                    string livingRegion = info[3];
                    string breed = info[4];
                    animal = new Tiger(name, weight, livingRegion, breed);
                }

                string[] foodInformation = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string foodType = foodInformation[0];
                int foodQuantity = int.Parse(foodInformation[1]);
                Food food = null;

                if (foodType == "Vegetable")
                {
                    food = new Vegetable(foodQuantity);
                }
                else if (foodType == "Fruit")
                {
                    food = new Fruit(foodQuantity);
                }
                else if (foodType == "Meat")
                {
                    food = new Meat(foodQuantity);
                }
                else if (foodType == "Seeds")
                {
                    food = new Seeds(foodQuantity);
                }

                animal.SoundAbility();
                animal.Feed(food);
                animals.Add(animal);
            }

            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
            }
        }
    }
}
