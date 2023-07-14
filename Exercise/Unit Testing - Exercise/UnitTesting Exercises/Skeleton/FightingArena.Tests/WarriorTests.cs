namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;
    using System.Xml.Linq;

    [TestFixture]
    public class WarriorTests
    {
        private Warrior warrior;
        [SetUp]
        public void Setup()
        {
            warrior = new Warrior("Naruto", 50, 100);
        }
        [TearDown]
        public void TearDown()
        {
            warrior = null;
        }
        [Test]
        public void WarriorConstructor_ShouldSetPropertiesCorrectly()
        {
            // Assert
            Assert.AreEqual("Naruto", warrior.Name);
            Assert.AreEqual(50, warrior.Damage);
            Assert.AreEqual(100, warrior.HP);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void WarriorConstructor_InvalidName_ShouldThrowException(string name)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Warrior(name, 50, 100));
        }
        [Test]
        [TestCase(0)]
        [TestCase(-2)]
        public void WarriorConstructor_InvalidDamage_ShouldThrowException(int damage)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Warrior("Naruto", damage, 100));
        }
        [Test]
        [TestCase(-2)]
        public void WarriorConstructor_InvalidHP_ShouldThrowException(int hp)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Warrior("Naruto", 50, hp));
        }
        public void WarriorAttack_InsufficientHP_ShouldThrowException()
        {
            // Arrange
            Warrior warrior1 = new Warrior("Warrior1", 50, 10);
            Warrior warrior2 = new Warrior("Warrior2", 30, 100);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => warrior1.Attack(warrior2));
        }

        [Test]
        public void WarriorAttack_AttackerHPBelowThreshold_ShouldThrowException()
        {
            // Arrange
            Warrior attacker = new Warrior("Sasuke", 50, 10);
            Warrior deffender = new Warrior("Itachi", 30, 40);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => attacker.Attack(deffender));
        }

        public void WarriorAttack_DeffenderHPBelowThreshold_ShouldThrowException()
        {
            // Arrange
            Warrior attacker = new Warrior("Sasuke", 50, 100);
            Warrior deffender = new Warrior("Sakura", 30, 10);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => attacker.Attack(deffender));
        }

        [Test]
        public void WarriorAttack_EnemyStrongerThanHP_ShouldThrowException()
        {
            // Arrange
            Warrior attacker = new Warrior("Chocho", 50, 50);
            Warrior deffender = new Warrior("Kakashi", 70, 100);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => attacker.Attack(deffender));
        }

        [Test]
        public void WarriorAttack_EnemyWeakerThanHP_ShouldReduceHP()
        {
            // Arrange
            Warrior attacker = new Warrior("Sasuke", 50, 100);
            Warrior deffender = new Warrior("Orochimaru", 30, 100);

            // Act
            attacker.Attack(deffender);

            // Assert
            Assert.AreEqual(70, attacker.HP);
            Assert.AreEqual(50, deffender.HP);
        }
        [Test]
        public void WarriorAttack_EnemyWeakerHP_ThanAttackerDamage()
        {
            // Arrange
            Warrior attacker = new Warrior("Sasuke", 100, 100);
            Warrior deffender = new Warrior("Orochimaru", 30, 50);

            // Act
            attacker.Attack(deffender);

            //Assert
            Assert.AreEqual(0, deffender.HP);
            Assert.AreEqual(70, attacker.HP);
        }
        [Test]
        public void WarriorAttack_Enemy()
        {
            //Arrange
            Warrior deffender = new Warrior("Gara", 30, 60);

            // Act
            warrior.Attack(deffender);

            //Assert
            Assert.AreEqual(70, warrior.HP);
            Assert.AreEqual(10, deffender.HP);
        }
    }
}