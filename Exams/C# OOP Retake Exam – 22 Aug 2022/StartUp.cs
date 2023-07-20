namespace BookingApp
{
    using BookingApp.Core;
    using BookingApp.Core.Contracts;
    using BookingApp.Models.Bookings;
    using BookingApp.Models.Rooms;
    using BookingApp.Models.Rooms.Contracts;
    using System;
    public class StartUp
    {
        public static void Main()
        {

            IEngine engine = new Engine();
            engine.Run();

        }
    }
}
