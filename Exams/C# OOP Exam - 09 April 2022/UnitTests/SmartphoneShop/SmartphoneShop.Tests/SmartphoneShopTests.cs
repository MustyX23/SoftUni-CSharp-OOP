using NUnit.Framework;
using System;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        private Shop shop;
        [SetUp]
        public void Setup()
        {
            shop = new Shop(10);
        }
        [TearDown]
        public void TearDown()
        {
            shop = null;
        }
        [Test]
        public void Smartphone_Constructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            string modelName = "iPhone 13";
            int maximumBatteryCharge = 100;

            // Act
            Smartphone smartphone = new Smartphone(modelName, maximumBatteryCharge);

            // Assert
            Assert.AreEqual(modelName, smartphone.ModelName);
            Assert.AreEqual(maximumBatteryCharge, smartphone.MaximumBatteryCharge);
            Assert.AreEqual(maximumBatteryCharge, smartphone.CurrentBateryCharge);
        }
        [Test]
        public void Shop_Add_ShouldAddPhoneCorrectly()
        {
            // Arrange
            Shop shop = new Shop(10);
            Smartphone phone = new Smartphone("Samsung Galaxy S21", 90);

            // Act
            shop.Add(phone);

            // Assert
            Assert.AreEqual(1, shop.Count);
            Assert.AreEqual(10, shop.Capacity);
        }

        [Test]
        public void Shop_Add_ShouldThrowException_WhenPhoneAlreadyExists()
        {
            // Arrange
            Shop shop = new Shop(10);
            Smartphone phone1 = new Smartphone("iPhone 12", 95);
            Smartphone phone2 = new Smartphone("iPhone 12", 90);

            // Act
            shop.Add(phone1);

            // Assert
            Assert.Throws<InvalidOperationException>(() => shop.Add(phone2));
        }

        [Test]
        public void Shop_Add_ShouldThrowException_WhenShopIsFull()
        {
            // Arrange
            Shop shop = new Shop(1);
            Smartphone phone1 = new Smartphone("Samsung Galaxy S21", 90);
            Smartphone phone2 = new Smartphone("iPhone 13", 95);

            // Act
            shop.Add(phone1);

            // Assert
            Assert.Throws<InvalidOperationException>(() => shop.Add(phone2));
        }

        [Test]
        public void Shop_Remove_ShouldRemovePhoneCorrectly()
        {
            // Arrange
            Shop shop = new Shop(10);
            Smartphone phone = new Smartphone("iPhone 12", 95);
            shop.Add(phone);

            // Act
            shop.Remove("iPhone 12");

            // Assert
            Assert.AreEqual(0, shop.Count);
        }

        [Test]
        public void Shop_Remove_ShouldThrowException_WhenPhoneDoesNotExist()
        {
            // Arrange
            Shop shop = new Shop(10);
            Smartphone phone = new Smartphone("iPhone 12", 95);
            shop.Add(phone);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => shop.Remove("Samsung Galaxy S21"));
        }

        [Test]
        public void Shop_TestPhone_ShouldReduceBatteryCorrectly()
        {
            // Arrange
            Shop shop = new Shop(10);
            Smartphone phone = new Smartphone("iPhone 12", 95);
            shop.Add(phone);

            // Act
            shop.TestPhone("iPhone 12", 10);

            // Assert
            Assert.AreEqual(85, phone.CurrentBateryCharge);
        }

        [Test]
        public void Shop_TestPhone_ShouldThrowException_WhenPhoneDoesNotExist()
        {
            // Arrange
            Shop shop = new Shop(10);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => shop.TestPhone("Samsung Galaxy S21", 10));
        }

        [Test]
        public void Shop_TestPhone_ShouldThrowException_WhenBatteryIsLow()
        {
            // Arrange
            Shop shop = new Shop(10);
            Smartphone phone = new Smartphone("iPhone 12", 95);
            shop.Add(phone);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => shop.TestPhone("iPhone 12", 100));
        }

        [Test]
        public void Shop_ChargePhone_ShouldChargeBatteryCorrectly()
        {
            // Arrange
            Shop shop = new Shop(10);
            Smartphone phone = new Smartphone("iPhone 12", 95);
            shop.Add(phone);

            // Act
            shop.TestPhone("iPhone 12", 10); // Decrease the battery charge
            shop.ChargePhone("iPhone 12"); // Charge the battery

            // Assert
            Assert.AreEqual(95, phone.CurrentBateryCharge);
        }

        [Test]
        public void Shop_ChargePhone_ShouldThrowException_WhenPhoneDoesNotExist()
        {
            // Arrange
            Shop shop = new Shop(10);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => shop.ChargePhone("Samsung Galaxy S21"));
        }

        [Test]
        public void Shop_Capacity_ShallThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Shop(-1));            
        }
    }
}