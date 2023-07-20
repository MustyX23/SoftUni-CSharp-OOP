using FrontDeskApp;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BookigApp.Tests
{
    public class Tests
    {
        private Room room;
        private Hotel hotel;
        [SetUp]
        public void Setup()
        {
            room = new Room(2, 50);
            hotel = new Hotel("Royal Place", 5);
        }
        [TearDown]
        public void TearDown()
        {
            room = null;
            hotel = null;
        }

        [Test]
        public void Room_Valid_Constructor()
        {
            // Arrange
            int bedCapacity = 2;
            double pricePerNight = 50;


            // Assert
            Assert.AreEqual(bedCapacity, room.BedCapacity);
            Assert.AreEqual(pricePerNight, room.PricePerNight);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-2)]
        public void Room_Zero_Or_Negative_BedCapacity_ThrowsArgumentException(int bedCapacity)
        {
            // Arrange
            double pricePerNight = 100;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Room(bedCapacity, pricePerNight));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-2)]
        public void Room_Zero_Or_Negative_PricePerNight_ThrowsArgumentException(double pricePerNight)
        {
            // Arrange
            int bedCapacity = 2;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Room(bedCapacity, pricePerNight));
        }

        [Test]
        public void Hotel_Valid_Constructor()
        {
            Assert.AreEqual("Royal Place", hotel.FullName);
            Assert.AreEqual(5, hotel.Category);
            Assert.AreEqual(0, hotel.Bookings.Count);
            Assert.AreEqual(0, hotel.Rooms.Count);
        }
        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void Hotel_FullName_Throws_IsNullOrWhiteSpaceException(string fullName)
        {
            Assert.Throws<ArgumentNullException>(() => new Hotel(fullName, 5));
        }
        [Test]
        [TestCase(0)]
        [TestCase(6)]
        public void Hotel_Category_Throws_ValueBiggerOrLess_Exception(int category)
        {
            Assert.Throws<ArgumentException>(() => new Hotel("Royal Place", category));
        }
        [Test]
        public void AddRoom_ValidRoom_Success()
        {
            // Arrange
            string hotelName = "Test Hotel";
            int category = 4;
            Hotel hotel = new Hotel(hotelName, category);
            Room room = new Room(2, 150);

            // Act
            hotel.AddRoom(room);

            // Assert
            CollectionAssert.Contains((ICollection<Room>)hotel.Rooms, room);
            Assert.AreEqual(hotel.Rooms.Count, 1);
        }

        [Test]
        public void BookRoom_ValidBooking_Success()
        {
            // Arrange
            Room room1 = new Room(2, 150);
            Room room2 = new Room(4, 200);
            hotel.AddRoom(room1);
            hotel.AddRoom(room2);

            int adults = 2;
            int children = 1;
            int residenceDuration = 3;
            double budget = 700;

            // Act
            hotel.BookRoom(adults, children, residenceDuration, budget);

            // Assert
            Assert.AreEqual(1, hotel.Bookings.Count);
            Assert.AreEqual(600, hotel.Turnover); // 3 nights * 150 price per night for room1
        }
        [Test]
        [TestCase(0, 1, 3, 500)]
        [TestCase(-1, 1, 3, 500)]
        [TestCase(2, -1, 2, 300)]
        [TestCase(2, 1, 0, 300)]
        public void BookRoom_InvalidParameters_ThrowsArgumentException(int adults, int children, int residenceDuration, double budget)
        {
            // Arrange
            string hotelName = "Test Hotel";
            int category = 4;
            Hotel hotel = new Hotel(hotelName, category);
            Room room1 = new Room(2, 150);
            Room room2 = new Room(4, 200);
            hotel.AddRoom(room1);
            hotel.AddRoom(room2);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(adults, children, residenceDuration, budget));
        }
    }
}