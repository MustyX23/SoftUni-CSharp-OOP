using NUnit.Framework;

namespace VendingRetail.Tests
{
    public class Tests
    {
        private CoffeeMat coffeeMat;
        [SetUp]
        public void Setup()
        {
            this.coffeeMat = new CoffeeMat(1000, 3);
        }

        [Test]
        public void WaterCapacity()
        {
            Assert.AreEqual(1000, this.coffeeMat.WaterCapacity);
        }

        [Test]
        public void ButtonsCount()
        {
            Assert.AreEqual(3, this.coffeeMat.ButtonsCount);
        }

        [Test]
        public void FillWaterTank()
        {
            string result = this.coffeeMat.FillWaterTank();
            Assert.AreEqual("Water tank is filled with 1000ml", result);
            result = this.coffeeMat.FillWaterTank();
            Assert.AreEqual("Water tank is already full!", result);
        }

        [Test]
        public void AddDrink()
        {
            bool result = this.coffeeMat.AddDrink("Espresso", 1.5);
            Assert.IsTrue(result);
            result = this.coffeeMat.AddDrink("Cappuccino", 2.5);
            Assert.IsTrue(result);
            result = this.coffeeMat.AddDrink("Latte", 3.5);
            Assert.IsTrue(result);
            result = this.coffeeMat.AddDrink("Mocha", 4.5);
            Assert.IsFalse(result);
            result = this.coffeeMat.AddDrink("Espresso", 1.5);
            Assert.IsFalse(result);
        }

        [Test]
        public void BuyDrink()
        {
            string result = this.coffeeMat.BuyDrink("Espresso");
            Assert.AreEqual("CoffeeMat is out of water!", result);

            this.coffeeMat.FillWaterTank();
            result = this.coffeeMat.BuyDrink("Espresso");
            Assert.AreEqual("Espresso is not available!", result);

            this.coffeeMat.AddDrink("Espresso", 1.5);
            result = this.coffeeMat.BuyDrink("Espresso");
            Assert.AreEqual("Your bill is 1.50$", result);

            double income = this.coffeeMat.CollectIncome();
            Assert.AreEqual(1.5, income);

            income = this.coffeeMat.CollectIncome();
            Assert.AreEqual(0, income);
        }
        [Test]
        public void Income()
        {
            Assert.AreEqual(0, this.coffeeMat.Income);
            this.coffeeMat.FillWaterTank();
            this.coffeeMat.AddDrink("Espresso", 1.5);
            this.coffeeMat.BuyDrink("Espresso");
            Assert.AreEqual(1.5, this.coffeeMat.Income);
        }

        [Test]
        public void CollectIncome()
        {
            this.coffeeMat.FillWaterTank();
            this.coffeeMat.AddDrink("Espresso", 1.5);
            this.coffeeMat.BuyDrink("Espresso");
            double collectedIncome = this.coffeeMat.CollectIncome();
            Assert.AreEqual(1.5, collectedIncome);
            Assert.AreEqual(0, this.coffeeMat.Income);
        }
        [Test]
        public void AddDrinkWithSameName()
        {
            bool result = this.coffeeMat.AddDrink("Espresso", 1.5);
            Assert.IsTrue(result);
            result = this.coffeeMat.AddDrink("Espresso", 2.5);
            Assert.IsFalse(result);
        }

        [Test]
        public void BuyDrinkWithInsufficientWater()
        {
            this.coffeeMat.FillWaterTank();
            this.coffeeMat.AddDrink("Espresso", 1.5);
            for (int i = 0; i < 12; i++)
            {
                this.coffeeMat.BuyDrink("Espresso");
            }
            string result = this.coffeeMat.BuyDrink("Espresso");
            Assert.AreEqual("CoffeeMat is out of water!", result);
        }
    }
}