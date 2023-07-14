namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;
        [SetUp]
        public void Setup()
        {
            arena = new Arena();
        }
        [TearDown]
        public void TearDown()
        {
            arena = null;
        }
        [Test]
        public void ArenaShouldBeVoidOnCreate()
        {
            arena = new Arena();
            Assert.AreEqual(0, arena.Count);
        }
        [Test]
        public void EnrollShallAddWarrior() 
        {
            arena.Enroll(new Warrior("Po", 5, 12));
            Assert.AreEqual(1, arena.Count);
        }
        [Test]
        public void EnrollShallThrowExIfNameIsFound()
        {
            arena.Enroll(new Warrior("Po", 5, 12));
            Assert.Throws<InvalidOperationException>(() => arena.Enroll(new Warrior("Po", 5, 12)));
        }
        [Test]     
        public void FightShallThrowIfDefenderIsMissing()
        {
            arena.Enroll(new Warrior("Po", 5, 12));
            Assert.Throws<InvalidOperationException>(() => arena.Fight("Po", "Thai Lung"));
        }
        [Test]
        public void FightShallThrowIfAttackerIsMissing()
        {
            arena.Enroll(new Warrior("Sasuke", 5, 12));
            Assert.Throws<InvalidOperationException>(() => arena.Fight("Itachi", "Sasuke"));
        }
        [Test]
        public void TestFight()
        {
            Warrior attacker = new Warrior("Sasuke", 15, 35);
            arena.Enroll(attacker);

            Warrior defender = new Warrior("Itachi", 15, 45);
            arena.Enroll(defender);

            arena.Fight(attacker.Name, defender.Name);

            Assert.AreEqual(20, attacker.HP);
            Assert.AreEqual(30, defender.HP);
        }
    }
}
