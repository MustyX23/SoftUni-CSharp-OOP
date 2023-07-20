using BookingApp.Core.Contracts;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Bookings;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Hotels;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Models.Rooms;
using BookingApp.Repositories.Contracts;
using System.Text;
using System;
using System.Linq;
using BookingApp.Repositories;

namespace BookingApp.Core
{
    public class Controller : IController
    {
        private readonly HotelRepository hotels;

        public Controller()
        {
            hotels = new HotelRepository();
        }

        public string AddHotel(string hotelName, int category)
        {
            IHotel hotel = hotels.All().FirstOrDefault(hotel => hotel.FullName == hotelName);
            if (hotel != null)
            {
                return $"Hotel {hotelName} is already registered in our platform.";
            }

            hotel = new Hotel(hotelName, category);
            hotels.AddNew(hotel);

            return $"{category} stars hotel {hotelName} is registered in our platform and expects room availability to be uploaded.";
        }
        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            IHotel hotel = hotels.All().FirstOrDefault(h => h.FullName == hotelName);
            if (hotel == null)
            {
                return $"Profile {hotelName} doesn’t exist!";
            }

            IRoom room = hotel.Rooms.All().FirstOrDefault(r => r.GetType().Name == roomTypeName);
            if (room != null)
            {
                return "Room type is already created!";
            }

            switch (roomTypeName)
            {
                case "DoubleBed":
                    room = new DoubleBed();
                    break;
                case "Studio":
                    room = new Studio();
                    break;
                case "Apartment":
                    room = new Apartment();
                    break;
                default:
                    throw new ArgumentException("Incorrect room type!");
            }

            hotel.Rooms.AddNew(room);
            return $"Successfully added {roomTypeName} room type in {hotelName} hotel!";
        }
        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            IHotel hotel = hotels.All().FirstOrDefault(h => h.FullName == hotelName);
            if (hotel == null)
            {
                return $"Profile {hotelName} doesn’t exist!";
            }

            if (roomTypeName != nameof(Apartment) && roomTypeName != nameof(DoubleBed)
                && roomTypeName != nameof(Studio))
            {
                throw new ArgumentException("Incorrect room type!");                
            }

            IRoom room = hotel.Rooms.All().FirstOrDefault(r => r.GetType().Name == roomTypeName);
            if (room == null)
            {
                return "Room type is not created yet!";
            }

            if (room.PricePerNight > 0)
            {
                throw new InvalidOperationException("Price is already set!");
            }

            room.SetPrice(price);
            return $"Price of {roomTypeName} room type in {hotelName} hotel is set!";
        }

        public string BookAvailableRoom(int adultsCount, int childrenCount, int duration, int category)
        {
            var availableHotels = hotels.All()
                .OrderBy(hotel => hotel.FullName)
                .Where(hotel => hotel.Rooms.All().Any(room => room.PricePerNight > 0))
                .ToList();

            var filteredHotels = availableHotels
                .Where(hotel => hotel.Category == category)
                .ToList();

            if (!filteredHotels.Any())
            {
                return $"{category} star hotel is not available in our platform.";
            }

            foreach (var hotel in filteredHotels)
            {
                var availableRooms = hotel.Rooms.All()
                    .Where(room => room.PricePerNight > 0)
                    .OrderBy(room => room.BedCapacity)
                    .ToList();

                var selectedRoom = availableRooms.FirstOrDefault(room => room.BedCapacity >= adultsCount + childrenCount);
                if (selectedRoom != null)
                {
                    int bookingNumber = hotel.Bookings.All().Count() + 1;
                    var booking = new Booking(selectedRoom, duration, adultsCount, childrenCount, bookingNumber);
                    hotel.Bookings.AddNew(booking);
                    return $"Booking number {bookingNumber} for {hotel.FullName} hotel is successful!";
                }
            }

            return "We cannot offer an appropriate room for your request.";
        }

        public string HotelReport(string hotelName)
        {
            IHotel hotel = hotels.All().FirstOrDefault(h => h.FullName == hotelName);
            if (hotel == null)
            {
                return $"Profile {hotelName} doesn’t exist!";
            }

            var sb = new StringBuilder();
            sb.AppendLine($"Hotel name: {hotel.FullName}");
            sb.AppendLine($"--{hotel.Category} star hotel");
            sb.AppendLine($"--Turnover: {hotel.Turnover:F2} $");
            sb.AppendLine("Bookings:");

            var bookings = hotel.Bookings.All();
            if (!bookings.Any())
            {
                sb.AppendLine("none");
            }
            else
            {
                foreach (var booking in bookings)
                {
                    sb.AppendLine(booking.BookingSummary());
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}