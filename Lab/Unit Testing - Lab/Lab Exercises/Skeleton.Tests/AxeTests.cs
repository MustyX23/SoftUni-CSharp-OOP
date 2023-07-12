using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        [Test]
        public void AxeLoosesDurabilityAfterAttack()
        {
            Axe axe = new Axe(10, 10);
            Dummy dummy = new Dummy(10, 10);

            axe.Attack(dummy);

            Assert.AreEqual(9, axe.DurabilityPoints, "Axe durability doesn't change after attack.");
        }
        [Test]
        public void TestIfTryingAttackWithABrokenAxe()
        {
            Axe axe = new Axe (10, 0);
            Dummy dummy = new Dummy(10, 10);

           
            Assert.Throws<InvalidOperationException>(() => axe.Attack(dummy), "Axe durability must be zero or less!");
        }
    }
}