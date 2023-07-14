namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        private Car car;
        [SetUp]
        public void Setup()
        {
            car = new Car("BMW", "M5", 20, 100);
        }

        [TearDown]
        public void TearDown()
        {
            car = null;
        }

        [Test]
        public void CreateCar()
        {
            car = new Car("BMW", "M5", 20, 100);

            Assert.AreEqual("BMW", car.Make);
            Assert.AreEqual("M5", car.Model);
            Assert.AreEqual(20, car.FuelConsumption);
            Assert.AreEqual(100, car.FuelCapacity);
            Assert.AreEqual(0, car.FuelAmount);
        }

        [Test]
        public void CreateCarFailsIfMakeIsNull()
        {
            Assert.Throws<ArgumentException>(() => new Car(null, "M5", 20, 100));
        }
        [Test]
        public void CreateCarFailsIfMakeIsEmpty()
        {
            Assert.Throws<ArgumentException>(() => new Car(string.Empty, "M5", 20, 100));
        }

        [Test]
        public void CreateCarFailsIfModelIsNull()
        {
            Assert.Throws<ArgumentException>(() => new Car("BMW", null, 20, 100));
        }
        [Test]
        public void CreateCarFailsIfModelIsEmpty()
        {
            Assert.Throws<ArgumentException>(() => new Car("BMW", string.Empty, 20, 100));
        }

        [TestCase(0)]
        [TestCase(-2)]
        [Test]
        public void CreateCarFailsIfFuelConsumptionIsLessOrEqualToZero(double fuelConsumption)
        {
            Assert.Throws<ArgumentException>(() => new Car("BMW", "M5", fuelConsumption, 100));
        }

        [TestCase(0)]
        [TestCase(-2)]
        [Test]
        public void CreateCarFailsIfFuelCapacityIsLessOrEqualToZero(double fuelCapacity)
        {
            Assert.Throws<ArgumentException>(() => new Car("BMW", "M5", 20, fuelCapacity));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-2)]
        public void RefuelShouldThrowIfLessOrEqalToZero(double fuelToRefuel)
        {
            Assert.Throws<ArgumentException>(() => car.Refuel(fuelToRefuel));
        }

        [Test]
        public void RefuelShouldBeEqualToFuelAmount()
        {
            car.Refuel(42);
            Assert.AreEqual(42, car.FuelAmount);
        }
        [Test]
        public void RefuelShouldBeEqualToFuelCapacity()
        {
            car.Refuel(120);
            Assert.AreEqual(100, car.FuelCapacity);
        }

        [Test]
        public void DriveShallThrowNotEnoughFuel()
        {
            Assert.Throws<InvalidOperationException>(() => car.Drive(1)); 
        }
        [Test] 
        public void DriveShallLeaveFuel()
        {
            car.Refuel(25);
            car.Drive(100);
            Assert.AreEqual(5, car.FuelAmount);
        }
    }
}