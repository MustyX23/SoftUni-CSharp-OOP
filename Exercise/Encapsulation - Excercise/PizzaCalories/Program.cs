using System;

namespace PizzaCalories
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Pizza pizza = new Pizza();

            string input;

            while ((input = Console.ReadLine()) != "END")
            {
                string[] pizzaInfo = input.Split(" ");

                string item = pizzaInfo[0];

                if (item == "Pizza")
                {
                    try
                    {
                        pizza.Name = item;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (item == "Dough")
                {
                    Dough dough = new Dough();
                    
                    try
                    {
                        dough.Type = pizzaInfo[1];
                        dough.BakingTechnique = pizzaInfo[2];
                        dough.Weight = int.Parse(pizzaInfo[3]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    pizza.Dough = dough;
                }
                else if (item == "Topping")
                {
                    Topping topping = new Topping();

                    try
                    {
                        topping.Type = pizzaInfo[1];
                        topping.Weight = int.Parse(pizzaInfo[2]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    pizza.AddTopping(topping);
                }
            }

            if (pizza.ToppingsCount() > 10)
            {
                Console.WriteLine("Number of toppings should be in range [0..10].");
                return;
            }

            Console.WriteLine(pizza);
        }
    }
}
