using NUnit.Framework;
using System.Collections.Generic;

namespace RobotFactory.Tests
{
    public class Tests
    {
        private Robot robot;        
        private Supplement supplement;
        private Factory factory;

        [SetUp]
        public void Setup()
        {
            robot = new Robot("R2D2", 1000.0, 1);
            supplement = new Supplement("Enhancer", 1);
            factory = new Factory("Robot Factory", 5);
        }

        [Test]
        public void Robot_ShouldHaveCorrectProperties()
        {
            // Arrange & Act
            string model = "R2D2";
            double price = 1000.0;
            int interfaceStandard = 1;

            // Assert
            Assert.That(robot.Model, Is.EqualTo(model));
            Assert.That(robot.Price, Is.EqualTo(price));
            Assert.That(robot.InterfaceStandard, Is.EqualTo(interfaceStandard));
            Assert.That(robot.Supplements, Is.Not.Null);
            Assert.That(robot.Supplements, Is.InstanceOf<List<Supplement>>());
            Assert.That(robot.Supplements, Is.Empty);
            Assert.AreEqual(robot.Supplements.Count, 0);
        }

        [Test]
        public void Robot_ToString_ShouldReturnCorrectStringRepresentation()
        {
            // Arrange
            string expectedString = "Robot model: R2D2 IS: 1, Price: 1000.00";

            // Act
            string result = robot.ToString();

            // Assert
            Assert.That(result, Is.EqualTo(expectedString));
        }

        [Test]
        public void Supplement_ShouldHaveCorrectProperties()
        {
            // Arrange & Act
            string name = "Enhancer";
            int interfaceStandard = 1;

            // Assert
            Assert.That(supplement.Name, Is.EqualTo(name));
            Assert.That(supplement.InterfaceStandard, Is.EqualTo(interfaceStandard));
        }

        [Test]
        public void Supplement_ToString_ShouldReturnCorrectStringRepresentation()
        {
            // Arrange
            string expectedString = "Supplement: Enhancer IS: 1";

            // Act
            string result = supplement.ToString();

            // Assert
            Assert.That(result, Is.EqualTo(expectedString));
        }
        [Test]
        public void ProduceRobot_ShouldProduceRobot_WhenCapacityAvailable()
        {
            // Arrange
            string model = "R2D2";
            double price = 1000.0;
            int interfaceStandard = 1;

            // Act
            string result = factory.ProduceRobot(model, price, interfaceStandard);

            // Assert
            Assert.That(result, Is.EqualTo($"Produced --> Robot model: {model} IS: {interfaceStandard}, Price: {price:f2}"));
            Assert.That(factory.Robots.Count, Is.EqualTo(1));
            Assert.That(factory.Robots[0].Model, Is.EqualTo(model));
        }

        [Test]
        public void ProduceRobot_ShouldReturnErrorMessage_WhenCapacityExceeded()
        {
            // Arrange
            string model = "R2D2";
            double price = 1000.0;
            int interfaceStandard = 1;

            // Act
            factory.ProduceRobot(model, price, interfaceStandard);
            factory.ProduceRobot(model, price, interfaceStandard);
            factory.ProduceRobot(model, price, interfaceStandard);
            factory.ProduceRobot(model, price, interfaceStandard);
            factory.ProduceRobot(model, price, interfaceStandard);
            string result = factory.ProduceRobot(model, price, interfaceStandard);

            // Assert
            Assert.That(result, Is.EqualTo("The factory is unable to produce more robots for this production day!"));
            Assert.That(factory.Robots.Count, Is.EqualTo(5));
        }

        [Test]
        public void ProduceSupplement_ShouldProduceSupplement_WhenCalled()
        {
            // Arrange
            string name = "Enhancer";
            int interfaceStandard = 1;

            // Act
            string result = factory.ProduceSupplement(name, interfaceStandard);

            // Assert
            Assert.That(result, Is.EqualTo($"Supplement: {name} IS: {interfaceStandard}"));
            Assert.That(factory.Supplements.Count, Is.EqualTo(1));
            Assert.That(factory.Supplements[0].Name, Is.EqualTo(name));
        }

        [Test]
        public void UpgradeRobot_ShouldUpgradeRobot_WhenNotAlreadyUpgraded()
        {
            // Arrange
            var robot = new Robot("R2D2", 1000.0, 1);
            var supplement = new Supplement("Enhancer", 1);

            // Act
            bool result = factory.UpgradeRobot(robot, supplement);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(robot.Supplements.Count, Is.EqualTo(1));
            Assert.That(robot.Supplements[0], Is.EqualTo(supplement));
        }

        [Test]
        public void UpgradeRobot_ShouldNotUpgradeRobot_WhenAlreadyUpgraded()
        {
            // Arrange
            var robot = new Robot("R2D2", 1000.0, 1);
            var supplement = new Supplement("Enhancer", 1);

            // Act
            factory.UpgradeRobot(robot, supplement);
            bool result = factory.UpgradeRobot(robot, supplement);

            // Assert
            Assert.That(result, Is.False);
            Assert.That(robot.Supplements.Count, Is.EqualTo(1));
        }

        [Test]
        public void SellRobot_ShouldSellRobotWithPriceLessThanOrEqualToGivenPrice()
        {
            // Arrange
            var robot1 = new Robot("R2D2", 1000.0, 1);
            var robot2 = new Robot("C3PO", 1500.0, 2);
            var robot3 = new Robot("BB-8", 800.0, 1);

            factory.Robots.Add(robot1);
            factory.Robots.Add(robot2);
            factory.Robots.Add(robot3);

            // Act
            var soldRobot = factory.SellRobot(1200.0);

            // Assert
            Assert.That(soldRobot, Is.EqualTo(robot1));
            Assert.That(factory.Robots.Count, Is.EqualTo(3));
        }

        [Test]
        public void SellRobot_ShouldReturnNull_WhenNoRobotAvailableWithGivenPrice()
        {
            // Arrange
            var robot1 = new Robot("R2D2", 1000.0, 1);
            var robot2 = new Robot("C3PO", 1500.0, 2);

            factory.Robots.Add(robot1);
            factory.Robots.Add(robot2);

            // Act
            var soldRobot = factory.SellRobot(800.0);

            // Assert
            Assert.That(soldRobot, Is.Null);
            Assert.That(factory.Robots.Count, Is.EqualTo(2));
        }
    }
}