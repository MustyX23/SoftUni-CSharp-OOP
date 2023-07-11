namespace UniversityCompetition
{
    using UniversityCompetition.Core;
    using UniversityCompetition.Core.Contracts;
    using UniversityCompetition.Models;

    public class StartUp
    {
        static void Main(string[]args)
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}
