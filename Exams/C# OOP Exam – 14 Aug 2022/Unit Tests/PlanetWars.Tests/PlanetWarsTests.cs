using NUnit.Framework;
using System;
using System.Linq;

namespace PlanetWars.Tests
{
    public class Tests
    {        
        [TestFixture]
        public class PlanetWarsTests
        {
            private Weapon weapon;
            private Planet planet;

            [SetUp]
            public void Setup()
            {
                weapon = new Weapon("Avtomat", 50, 10);
                planet = new Planet("Earth", 500);
            }
            [TearDown]
            public void TearDown()
            {
                weapon = null;
                planet = null;
            }
            [Test]
            public void Weapon_Constructor_ValidParameters_ShouldSetPropertiesCorrectly()
            {
                
                // Assert
                Assert.AreEqual("Avtomat", weapon.Name);
                Assert.AreEqual(50, weapon.Price);
                Assert.AreEqual(10, weapon.DestructionLevel);
            }
            [Test]
            public void Weapon_Price_Cannot_Be_Negative()
            {
                Assert.Throws<ArgumentException>(() => new Weapon("Sniper", -1, 10));
            }
            [Test]
            public void Increase_Destruction_Level()
            {
                Assert.AreEqual(10, weapon.DestructionLevel);
                weapon.IncreaseDestructionLevel();
                Assert.AreEqual(11, weapon.DestructionLevel);

            }
            [Test]
            public void NuclearWeapon_IsNuclear()
            {
                Weapon nuclearWeapon = new Weapon("AtomnaBomba", 10, 999);
                Assert.IsTrue(weapon.IsNuclear);
            }
            [Test]
            public void NuclearWeapon_IsNotNuclear()
            {
                Weapon notNuclearWeapon = new Weapon("PompaDvucevka", 8, 3);
                Assert.IsFalse(notNuclearWeapon.IsNuclear);
            }
            [Test]
            public void Planet_Constructor_ValidParameters_ShouldSetPropertiesCorrectly()
            {
                // Arrange
                string planetName = "Earth";
                double planetBudget = 1000;

                // Act
                Planet planet = new Planet(planetName, planetBudget);

                // Assert
                Assert.AreEqual(planetName, planet.Name);
                Assert.AreEqual(planetBudget, planet.Budget);
                Assert.IsEmpty(planet.Weapons);
            }
            [Test]
            public void Planet_Constructor_InvalidName_ShouldThrowArgumentException()
            {
                // Arrange
                string planetName = "";

                // Act & Assert
                Assert.Throws<ArgumentException>(() => new Planet(planetName, 1000));
            }
            [Test]
            [TestCase(null)]
            [TestCase("")]
            public void Planet_Name_InvalidName_IsNullOrEmpty(string name)
            {               
                Assert.Throws<ArgumentException>(() => new Planet(name, 500));
            }
            [Test]
            public void Planet_Budget_Cannot_BeBellow_Zero()
            {
                Assert.Throws<ArgumentException>(() => new Planet("Earth", -1));
            }
            [Test]
            public void Planet_Read_Weapons()
            {
                planet.AddWeapon(weapon);
                Assert.AreEqual("Avtomat", weapon.Name);
                Assert.AreEqual(50, weapon.Price);
                Assert.AreEqual(10, weapon.DestructionLevel);
                Assert.AreEqual(1, planet.Weapons.Count);
            }
            [Test]
            public void Planet_MilitaryPowerRatio_Weapons()
            {
                planet.AddWeapon(weapon);
                planet.AddWeapon(new Weapon("Pompa", 50, 10));
                Assert.AreEqual(20, planet.MilitaryPowerRatio);
            }
            [Test]
            public void Planet_Profit_ShouldIncreaseBudget()
            {
                // Arrange
                double initialBudget = 1000;
                double profitAmount = 500;
                Planet planet = new Planet("Earth", initialBudget);

                // Act
                planet.Profit(profitAmount);

                // Assert
                double expectedBudget = initialBudget + profitAmount;
                Assert.AreEqual(expectedBudget, planet.Budget);
            }
            [Test]
            public void Planet_SpendFunds_InsufficientFunds_ShouldThrowInvalidOperationException()
            {
                // Arrange
                double initialBudget = 1000;
                double amountToSpend = 1500;
                Planet planet = new Planet("Earth", initialBudget);

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => planet.SpendFunds(amountToSpend));
            }

            [Test]
            public void Planet_SpendFunds_ShouldDecreaseBudget()
            {
                // Arrange
                double initialBudget = 1000;
                double amountToSpend = 500;
                Planet planet = new Planet("Earth", initialBudget);

                // Act
                planet.SpendFunds(amountToSpend);

                // Assert
                double expectedBudget = initialBudget - amountToSpend;
                Assert.AreEqual(expectedBudget, planet.Budget);
            }
            [Test]
            public void Planet_AddWeapon_WeaponDoesNotExist_ShouldAddWeapon()
            {
                // Arrange
                Weapon weapon = new Weapon("Photon Blaster", 50, 10);
                Planet planet = new Planet("Earth", 1000);

                // Act
                planet.AddWeapon(weapon);

                // Assert
                Assert.Contains(weapon, (System.Collections.ICollection)planet.Weapons);
            }

            [Test]
            public void Planet_AddWeapon_WeaponAlreadyExists_ShouldThrowInvalidOperationException()
            {
                // Arrange
                Weapon weapon = new Weapon("Photon Blaster", 50, 10);
                Planet planet = new Planet("Earth", 1000);
                planet.AddWeapon(weapon);

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => planet.AddWeapon(weapon));
            }
            [Test]
            public void Planet_RemoveWeapon_WeaponExists_ShouldRemoveWeapon()
            {
                // Arrange
                Weapon weapon1 = new Weapon("Photon Blaster", 50, 10);
                Weapon weapon2 = new Weapon("Laser Cannon", 80, 10);
                Planet planet = new Planet("Earth", 1000);
                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);

                // Act
                planet.RemoveWeapon(weapon1.Name);

                // Assert
                Assert.IsFalse(planet.Weapons.Contains(weapon1));
                Assert.IsTrue(planet.Weapons.Contains(weapon2));
            }
            [Test]
            public void Planet_RemoveWeapon_WeaponDoesntExists_Reurns_False()
            {
                // Arrange
                Weapon sniper = new Weapon("Snaiper", 100, 100);
                Planet planet = new Planet("Earth", 1000);
                planet.AddWeapon(weapon);

                // Act
                planet.RemoveWeapon("Snaiper");

                // Assert
                Assert.IsFalse(planet.Weapons.Contains(sniper));
                Assert.AreEqual(1, planet.Weapons.Count);                
            }
            [Test]
            public void Planet_UpgradeWeapon_WeaponDoesNotExist_ShouldThrowInvalidOperationException()
            {
                // Arrange
                Planet planet = new Planet("Earth", 1000);

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => planet.UpgradeWeapon("Photon Blaster"));
            }
            [Test]
            public void Planet_UpgradeWeapon_UpgradingWeapon()
            {
                // Arrange
                Planet planet = new Planet("Earth", 1000);
                planet.AddWeapon(weapon);
                Assert.AreEqual(10, weapon.DestructionLevel);
                planet.UpgradeWeapon(weapon.Name);
                Assert.AreEqual(11, weapon.DestructionLevel);
            }
            [Test]
            public void Planet_DestructOpponent_StrongerOpponent_ShouldThrowInvalidOperationException()
            {
                // Arrange
                Planet planet1 = new Planet("Earth", 1000);
                Planet planet2 = new Planet("Mars", 2000);
                Planet planet3 = new Planet("Uran", 300);
                planet1.AddWeapon(new Weapon("Photon Blaster", 50, 10));
                planet2.AddWeapon(weapon);
                planet3.AddWeapon(new Weapon("OgneHvurgachka", 3, 10));
                

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => planet1.DestructOpponent(planet2));
                Assert.Throws<InvalidOperationException>(() => planet3.DestructOpponent(planet1));
            }
            [Test]
            public void Planet_DestructOpponent_WeakerOpponent_ShouldReturnDestructionMessage()
            {
                // Arrange
                Planet planet1 = new Planet("Earth", 1000);
                Planet planet2 = new Planet("Mars", 500);
                planet1.AddWeapon(new Weapon("Photon Blaster", 50, 10));

                // Act
                string result = planet1.DestructOpponent(planet2);

                // Assert
                Assert.AreEqual($"{planet2.Name} is destructed!", result);
            }
        }
    }
}
