using System;

namespace CustomStack
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            StackOfStrings stack = new StackOfStrings();
            stack.Push("Apple");
            stack.Push("Banana");
            stack.Push("Orange");

            bool isEmpty = stack.IsEmpty();
            Console.WriteLine("Is stack empty? " + isEmpty);

            stack.AddRange(new[] { "Grapes", "Mango" });

            string topItem = stack.Peek();
            Console.WriteLine("Top item in stack: " + topItem);
        }
    }
}
