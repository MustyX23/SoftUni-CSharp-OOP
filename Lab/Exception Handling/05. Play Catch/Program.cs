using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Play_Catch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int exceptionCounter = 0;

            while (exceptionCounter < 3)
            {
                string[] manipulativeTokens = Console.ReadLine().Split(' ');

                try
                {
                    string action = manipulativeTokens[0];

                    if (action == "Replace")
                    {
                        //Replace {index} {element}
                        int index = int.Parse(manipulativeTokens[1]);
                        int elementToReplace = int.Parse(manipulativeTokens[2]);
                        array[index] = elementToReplace;
                    }
                    else if (action == "Print")
                    {
                        //Print {startIndex} {endIndex}
                        int startIndex = int.Parse(manipulativeTokens[1]);
                        int endIndex = int.Parse(manipulativeTokens[2]);

                        List<int> elementsToPrint = new List<int>();
                        for (int i = startIndex; i <= endIndex; i++)
                        {
                            elementsToPrint.Add(array[i]);
                        }
                        Console.WriteLine(String.Join(", ", elementsToPrint));
                    }
                    else if (action == "Show")
                    {
                        int index = int.Parse(manipulativeTokens[1]);
                        Console.WriteLine(array[index]);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("The index does not exist!");
                    exceptionCounter++;
                }
                catch (FormatException)
                {
                    Console.WriteLine("The variable is not in the correct format!");
                    exceptionCounter++;
                }               
            }

            Console.WriteLine(String.Join(", ", array));
        }
    }
}
