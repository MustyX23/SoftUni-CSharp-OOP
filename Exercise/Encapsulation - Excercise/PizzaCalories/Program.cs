using System;

namespace PizzaCalories
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            //Dough White Chewy 100
            try
            {
                while (true)
                {
                    string[] input = Console.ReadLine().Split(' ');
                    string flourType = input[1];
                    string bakingTechnique = input[2];
                    double grams = double.Parse(input[3]);

                    Dough dough = new Dough(flourType, bakingTechnique, grams);
                    Console.WriteLine(dough.Calories);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            
        }
    }
}
