namespace Gyms
{
    using System;

    class StartUp
    {
        static void Main(string[] args)
        {
            Gym gym = new Gym("Rich", 3);
            Athlete athlete = new Athlete("Messi");

            gym.AddAthlete(athlete);

            gym.InjureAthlete("Messi");

            Console.WriteLine(gym.Report());
        }
    }
}
