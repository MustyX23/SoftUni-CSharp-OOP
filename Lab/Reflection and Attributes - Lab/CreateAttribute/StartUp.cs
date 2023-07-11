using System;

namespace AuthorProblem
{
    [Author("Viktor")]
    public class StartUp
    {
        [Author("George")]
        static void Main(string[] args)
        {
            
        }
    }
    public class AuthorAttribute : Attribute
    {
        public AuthorAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
