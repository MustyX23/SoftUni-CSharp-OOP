using NUnit.Framework;
using System;

namespace Gyms.Tests
{
    [TestFixture]
    public class GymsTests
    {
        private Athlete athlete;
        private Gym gym;

        [SetUp]
        public void SetUp()
        {
            athlete = new Athlete("Messi");
            gym = new Gym("Rich", 10);
        }

        [TearDown]
        public void TearDown()
        {
            athlete = null;
        }

        [Test]
        public void Athlete_Constructor_Works()
        {
            Assert.AreEqual("Messi", athlete.FullName);
            Assert.IsFalse(athlete.IsInjured);
        }
        [Test]
        public void Gym_Constructor_Works()
        {
            Assert.AreEqual("Rich", gym.Name);
            Assert.AreEqual(10, gym.Capacity);
            Assert.AreEqual(0, gym.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void Gym_Invalid_Name_ArgumentNullException(string name)
        {
            Assert.Throws<ArgumentNullException>(() => new Gym(name, 10));
        }
        [Test]
        [TestCase(-1)]
        public void Gym_Invalid_Capacity_ArgumentException(int capacity)
        {
            Assert.Throws<ArgumentException>(() => new Gym("Messi", capacity));
        }

        [Test]
        public void Gym_Adds_Correctly()
        {
            gym.AddAthlete(athlete);
            Assert.AreEqual(1, gym.Count);
        }
        [Test]
        public void Gym_Adds_InvalidOperationException_Capacity_Full()
        {
            gym.AddAthlete(athlete);
            gym.AddAthlete(new Athlete("Toncho"));
            gym.AddAthlete(new Athlete("Vanko"));
            gym.AddAthlete(new Athlete("Draganko"));
            gym.AddAthlete(new Athlete("Ivancho"));
            gym.AddAthlete(new Athlete("Popolcho"));
            gym.AddAthlete(new Athlete("Grisho"));
            gym.AddAthlete(new Athlete("Pesho"));
            gym.AddAthlete(new Athlete("Ibn Ebn al Kamil"));
            gym.AddAthlete(new Athlete("Pipercho"));
            Assert.Throws<InvalidOperationException>(() => gym.AddAthlete(new Athlete("Djamal")));
        }
        [Test]
        public void Gym_Removes_Correctly()
        {
            gym.AddAthlete(athlete);
            gym.AddAthlete(new Athlete("Djamal"));
            gym.RemoveAthlete("Messi");

            Assert.AreEqual(gym.Count, 1);
        }
        [Test]
        public void Gym_DoesNotRemove_Correctly()
        {
            gym.AddAthlete(athlete);
            gym.AddAthlete(new Athlete("Djamal"));
            Assert.Throws<InvalidOperationException>(() => gym.RemoveAthlete("Ivan"));            
        }
        [Test]
        public void Gym_Injures_Correctly()
        {
            gym.AddAthlete(athlete);
            gym.InjureAthlete("Messi");
            Assert.IsTrue(athlete.IsInjured);
        }
        [Test]
        public void Gym_InjureAthlete_ShouldSetIsInjuredToTrue_WhenAthleteExists()
        {
            // Arrange
            Gym gym = new Gym("TestGym", 3);
            Athlete athlete1 = new Athlete("John");
            gym.AddAthlete(athlete1);

            // Act
            Athlete injuredAthlete = gym.InjureAthlete("John");

            // Assert
            Assert.IsTrue(injuredAthlete.IsInjured);
        }
        [Test]
        public void Gym_DoesNotInjure_Correctly()
        {
            gym.AddAthlete(athlete);
            Assert.Throws<InvalidOperationException>(() => gym.InjureAthlete("Ivan"));
        }
        [Test]
        public void Gym_Reports_Correctly_NotInjured()
        {            
            gym.AddAthlete(athlete);
            gym.AddAthlete(new Athlete("Ronaldo"));
            gym.AddAthlete(new Athlete("Berbatov"));

            // Act
            string report = gym.Report();

            // Assert
            string expectedReport = "Active athletes at Rich: Messi, Ronaldo, Berbatov";
            Assert.AreEqual(expectedReport, report);
        }
        [Test]
        public void Gym_Reports_Correctly_Injured()
        {
            gym.AddAthlete(athlete);
            gym.AddAthlete(new Athlete("Ronaldo"));
            gym.AddAthlete(new Athlete("Berbatov"));

            gym.InjureAthlete("Messi");
            gym.InjureAthlete("Ronaldo");
            gym.InjureAthlete("Berbatov");

            // Act
            string report = gym.Report();

            // Assert
            string expectedReport = "Active athletes at Rich: ";
            Assert.AreEqual(expectedReport, report);
        }

    }
}
