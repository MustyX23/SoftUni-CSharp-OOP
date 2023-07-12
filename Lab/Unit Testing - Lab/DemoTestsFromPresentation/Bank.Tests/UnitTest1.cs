using NUnit.Framework;

namespace Bank.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AccountInitializeWithPositiveValue()
        {
            BankAccount bankAccount = new BankAccount(2000m);
            Assert.That(bankAccount.Amount, Is.EqualTo(2000m));
        }
    }
}