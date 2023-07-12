using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Sum_IF_1Plus2_Equals3()
        {
            int sum = 1 + 2;
            Assert.That(sum, Is.EqualTo(3), "Go learn Math!");
            //Assert.Pass();
        }
    }
}