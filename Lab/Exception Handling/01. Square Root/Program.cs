using System;

namespace _01._Square_Root
{
    internal class Program
    {
        static void Main(string[] args)
        {
			try
			{
				double number = double.Parse(Console.ReadLine());

                if (number < 0)
                {
                    throw new ArgumentException("Invalid number.");
                }

                double rootedNumber = Math.Sqrt(number);
				Console.WriteLine(rootedNumber);
				
			}
			catch (ArgumentException ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				Console.WriteLine("Goodbye.");
			}
        }
    }
}
