using System;

namespace CustomRandomList
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            RandomList strings = new RandomList() { "banana", "apple", "grape"};
            
            Console.WriteLine(strings.RandomString());
        }
    }
}
