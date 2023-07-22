using NUnit.Framework;
using System;

namespace RepairShop.Tests
{
    public class Tests
    {
        [TestFixture]
        public class RepairsShopTests
        {
            private Car car;
            private Garage garage;
            [SetUp]
            public void Setup()
            {
                car = new Car("BMW", 99);
                garage = new Garage("Los Santos", 5);
            }
            [Test]
            public void Car_Constructor()
            {
                Assert.AreEqual("BMW", car.CarModel);
                Assert.AreEqual(99, car.NumberOfIssues);
                Assert.IsFalse(0 == car.NumberOfIssues);
            }
            [Test]
            public void Garage_WhenInitialized_ShouldHaveCorrectNameAndMechanicsAvailable()
            {
                // Arrange
                string name = "Test Garage";
                int mechanicsAvailable = 3;

                // Act
                Garage garage = new Garage(name, mechanicsAvailable);

                // Assert
                Assert.AreEqual(name, garage.Name);
                Assert.AreEqual(mechanicsAvailable, garage.MechanicsAvailable);
            }

            [Test]
            public void Garage_WhenNameIsNullOrEmpty_ShouldThrowArgumentNullException()
            {
                // Arrange
                string name = null;
                int mechanicsAvailable = 3;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => new Garage(name, mechanicsAvailable));
            }

            [Test]
            public void Garage_WhenMechanicsAvailableIsZeroOrNegative_ShouldThrowArgumentException()
            {
                // Arrange
                string name = "Test Garage";
                int mechanicsAvailable = 0;

                // Act & Assert
                Assert.Throws<ArgumentException>(() => new Garage(name, mechanicsAvailable));
            }

            [Test]
            public void AddCar_WhenAddingCar_ShouldIncreaseCarsInGarage()
            {
                // Arrange
                Garage garage = new Garage("Test Garage", 3);

                // Act
                garage.AddCar(car);

                // Assert
                Assert.AreEqual(1, garage.CarsInGarage);
            }

            [Test]
            public void AddCar_WhenGarageIsFull_ShouldThrowInvalidOperationException()
            {
                // Arrange
                Garage garage = new Garage("Test Garage", 1);
                Car car2 = new Car("Car2", 5);

                // Act
                garage.AddCar(car);

                // Assert
                Assert.Throws<InvalidOperationException>(() => garage.AddCar(car2));
            }

            [Test]
            public void FixCar_WhenCarExists_ShouldSetNumberOfIssuesToZero()
            {
                // Arrange
                Garage garage = new Garage("Test Garage", 3);
                garage.AddCar(car);

                // Act
                var fixedCar = garage.FixCar("BMW");

                // Assert
                Assert.AreEqual(0, fixedCar.NumberOfIssues);
            }

            [Test]
            public void FixCar_WhenCarDoesNotExist_ShouldThrowInvalidOperationException()
            {
                // Arrange
                Garage garage = new Garage("Test Garage", 3);

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => garage.FixCar("Car1"));
            }

            [Test]
            public void RemoveFixedCar_WhenNoFixedCars_ShouldThrowInvalidOperationException()
            {
                // Arrange
                Garage garage = new Garage("Test Garage", 3);
                garage.AddCar(car);

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => garage.RemoveFixedCar());
            }

            [Test]
            public void RemoveFixedCar_WhenFixedCarsExist_ShouldRemoveFixedCarsAndReturnCount()
            {
                // Arrange
                Garage garage = new Garage("Test Garage", 3);
                Car car1 = new Car("Audi", 0);
                Car car2 = car;
                Car car3 = new Car("Toyota", 0);
                garage.AddCar(car1);
                garage.AddCar(car2);
                garage.AddCar(car3);

                // Act
                int removedCarsCount = garage.RemoveFixedCar();

                // Assert
                Assert.AreEqual(2, removedCarsCount);
                Assert.AreEqual(1, garage.CarsInGarage);
                Assert.IsTrue(garage.CarsInGarage == 1);
            }

            [Test]
            public void Report_WhenNoCarsNotFixed_ShouldReturnCorrectReport()
            {
                // Arrange
                Garage garage = new Garage("Test Garage", 3);
                Car car1 = new Car("Audi", 0);
                Car car2 = new Car("Toyota", 0);
                garage.AddCar(car1);
                garage.AddCar(car2);

                // Act
                string report = garage.Report();

                // Assert
                Assert.AreEqual("There are 0 which are not fixed: .", report);
            }

            [Test]
            public void Report_WhenCarsNotFixedExist_ShouldReturnCorrectReport()
            {
                // Arrange
                Garage garage = new Garage("Test Garage", 3);
                Car car1 = new Car("Toyota", 0);
                Car car2 = car;
                Car car3 = new Car("Citroen", 5);
                garage.AddCar(car1);
                garage.AddCar(car2);
                garage.AddCar(car3);

                // Act
                string report = garage.Report();

                // Assert
                Assert.AreEqual("There are 2 which are not fixed: BMW, Citroen.", report);
            }
        }
    }
}