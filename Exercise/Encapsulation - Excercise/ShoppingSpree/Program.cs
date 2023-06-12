using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingSpree
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>();
            List<Product> products = new List<Product>();


            try
            {
                string[] nameMoneyPairs = Console.ReadLine()
                    .Split(";", StringSplitOptions.RemoveEmptyEntries);

                foreach (var nameMoneyPair in nameMoneyPairs)
                {
                    string[] nameMoney = nameMoneyPair
                        .Split("=", StringSplitOptions.RemoveEmptyEntries);

                    Person person = new Person(nameMoney[0], decimal.Parse(nameMoney[1]));

                    people.Add(person);
                }

                string[] productCostPairs = Console.ReadLine()
                    .Split(";", StringSplitOptions.RemoveEmptyEntries);

                foreach (var productCostPair in productCostPairs)
                {
                    string[] productCost = productCostPair
                        .Split("=", StringSplitOptions.RemoveEmptyEntries);

                    Product product = new Product(productCost[0], decimal.Parse(productCost[1]));

                    products.Add(product);
                }
                string input;

                while ((input = Console.ReadLine()) != "END")
                {
                    string[] personProductPair = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    string personName = personProductPair[0];
                    string productName = personProductPair[1];

                    Person person = people.FirstOrDefault(person => person.Name == personName);
                    Product product = products.FirstOrDefault(product => product.Name == productName);

                    if (person != null && product != null)
                    {
                        Console.WriteLine(person.Add(product));
                    }
                }

                Console.WriteLine(String.Join(Environment.NewLine, people));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }          
        }
    }
}
