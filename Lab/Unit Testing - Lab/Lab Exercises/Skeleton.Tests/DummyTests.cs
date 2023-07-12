using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        [Test]
        public void DummyLoosesHealthIfAttacked()
        {
            Axe baltiq = new Axe(10, 10);
            Dummy dummy = new Dummy(11, 10);

            dummy.TakeAttack(1);

            Assert.AreEqual(10, dummy.Health, "Dummy is not taking damage.");
        }
        [Test]
        public void DeadDummyThrowsAnExceptionIfAttacked()
        {
            Axe baltiq = new Axe(10, 10);
            Dummy dummy = new Dummy(0, 10);

            Assert.Throws<InvalidOperationException>(() => dummy.TakeAttack(1), "Dummy is taking damage while dead.");
        }
        [Test]
        public void DeadDummyThrowsCanGiveXp()
        {
            Axe baltiq = new Axe(10, 10);
            Dummy dummy = new Dummy(0, 10);

            int xp = dummy.GiveExperience();
            Assert.AreEqual(10, xp, $"Experience is not equal to the expected!");
        }
        [Test]
        public void DeadDummyThrowsCannotGiveXp()
        {
            Axe baltiq = new Axe(10, 10);
            Dummy dummy = new Dummy(1, 10);

            Assert.Throws<InvalidOperationException>(() => dummy.GiveExperience(), "Dummy is dead, it must give XP.");
        }
    }
}