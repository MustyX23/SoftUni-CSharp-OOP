namespace UniversityLibrary
{
    using System;
    public class StartUp
    {
        static void Main(string[] args)
        {
            TextBook textBook = new TextBook("Dune", "Ivan", "Fantasy");
            Console.WriteLine(textBook.InventoryNumber);
            Console.WriteLine(textBook.Holder);
        }
    }
}
