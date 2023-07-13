namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {
        [TearDown]
        public void TearDown()
        {
            Database database = new Database();
            database = null;
        }
        [Test]
        public void ArraysCapacityIsExactly16Integers()
        {
            Database dataBase = new Database(1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16);

            Assert.AreEqual(16, dataBase.Count);
        }
        [Test]
        public void AddingElementAtTheNextFreeCell()
        {
            int[] sixteenIntegers = new int[16];

            for (int i = 0; i < sixteenIntegers.Length; i++)
            {
                sixteenIntegers[i] = i;
            }

            Database dataBase = new Database(sixteenIntegers);

            Assert.Throws<InvalidOperationException>(() => dataBase.Add(5));
        }
        [Test]
        public void RemovingAnElementFromTheLastIndex()
        {
            Database database = new Database(5, 15);
            database.Remove();
            var result = database.Fetch();

            Assert.AreEqual(1, database.Count);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(5, result[0]);
        }
        [Test]
        public void RemovingAnElementFromAnEmptyDataBase()
        {
            Database database = new Database();

            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }
        [Test]
        public void FetchingElementsReturnsArray()
        {
            Database database = new Database(1, 2, 3);
            int[] fetchedDataBaseElements = database.Fetch();

            int[] expextedElements = {1, 2 ,3};
            Assert.AreEqual(expextedElements, fetchedDataBaseElements);
        }
        [Test]
        public void CreateDatabaseWith10Elements()
        {
            Database database = new Database(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            Assert.AreEqual(10, database.Count);
        }
    }
}
