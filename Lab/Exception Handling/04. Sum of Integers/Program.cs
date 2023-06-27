using System;

namespace _04._Sum_of_Integers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(' ');
            int totalSum = 0;

            for (int i = 0; i < input.Length; i++)
            {
                try
                {
                    int currentElement = int.Parse(input[i]);
                    totalSum += currentElement;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"The element '{input[i]}' is in wrong format!");
                }
                catch (OverflowException)
                {
                    Console.WriteLine($"The element '{input[i]}' is out of range!");
                }
                finally
                {
                    Console.WriteLine($"Element '{input[i]}' processed - current sum: {totalSum}");
                }
            }

            Console.WriteLine($"The total sum of all integers is: {totalSum}");

        }
    }
}
