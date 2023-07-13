namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Database _database;

        [SetUp]
        public void Setup()
        {
            _database = new Database();
        }

        [TearDown]
        public void TearDown()
        {
            _database = null;
        }

        [Test]
        public void AddMethodTest()
        {
            _database.Add(new Person(1, "Dimitrichko"));
            Person result = _database.FindById(1);

            Assert.AreEqual(1, _database.Count);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Dimitrichko", result.UserName);

        }

        [Test]
        public void ShouldThrowIfMoreThanMaximumLength()
        {
            Person[] people = CreateFullArray();
            _database = new Database(people);

            Assert.Throws<InvalidOperationException>(() => _database.Add(new Person(1, "Kaloqn")));
        }

        private Person[] CreateFullArray()
        {
            Person[] people = new Person[16];

            for (int i = 0; i < people.Length; i++)
            {
                people[i] = new Person(i, i.ToString());
            }
            return people;
        }
        [Test]
        public void ThrowsIfAlreadyUsersWithGivenUsername()
        {
            _database.Add(new Person(1, "Ivan"));       
            Person concretePerson = _database.FindByUsername("Ivan");

            Assert.Throws<InvalidOperationException>(() => _database.Add(concretePerson) ,"Doesn't throw");
        }
        [Test]
        public void ThrowsIfAlreadyUsersWithGivenId()
        {
            _database.Add(new Person(12, "Kroki"));
            Person concretePerson = _database.FindById(12);

            Assert.Throws<InvalidOperationException>(() => _database.Add(concretePerson), "Doesn't throw exception!");

        }
        [Test]
        public void CreateDatabaseWith2Elements()
        {
            _database = new Database(new Person(1, "Qndzu"), new Person(2, "Huanhu"));
            Person first = _database.FindById(1);
            Person second = _database.FindById(2);

            Assert.AreEqual("Qndzu", first.UserName);
            Assert.AreEqual("Huanhu", second.UserName);
        }

        [Test]
        public void RemoveFromEmptyDatabaseShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(() => _database.Remove());
        }

        [Test]
        public void RemoveFromDatabase()
        {
            _database = new Database(new Person(1, "Qndzu"), new Person(2, "Huanhu"));
            _database.Remove();

            Person firstPerson = _database.FindById(1);

            Assert.AreEqual(1, _database.Count);
            Assert.AreEqual("Qndzu", firstPerson.UserName);
            Assert.Throws<InvalidOperationException>(() => _database.FindByUsername("Huanhu"));
        }

        [Test]
        public void FindByUserNameShouldThrowIfUserNameNullOrEmpty()
        {
            _database = new Database();
            Assert.Throws<ArgumentNullException>(() => _database.FindByUsername(null));
        }
        [Test]
        public void FindByUserNameShouldThrowIfUserNameDoesntExist()
        {
            _database = new Database();
            Assert.Throws<InvalidOperationException>(() => _database.FindByUsername("Mickey Mouse"));
        }

        [Test]
        public void FindByUserNameReturnsCorrectUser()
        {
            _database = new Database(new Person(1, "Kroki"), new Person(2, "Peri"));
            Person kroki = _database.FindByUsername("Kroki");
            Person peri = _database.FindByUsername("Peri");

            Assert.AreEqual("Kroki", kroki.UserName);
            Assert.AreEqual("Peri", peri.UserName);
        }
        [Test]
        public void FindByIdShouldThrowIfIdisLessThanZero()
        {
            _database = new Database();
            Assert.Throws<ArgumentOutOfRangeException>(() => _database.FindById(-2));
        }
        [Test]
        public void FindByIdShouldThrowIfIdDoesntExist()
        {
            _database = new Database(new Person(1, "Checheneca"));
            Assert.Throws<InvalidOperationException>(() => _database.FindById(2));
        }
        [Test]
        public void FindByIdReturnsCorrectUser()
        {
            _database = new Database(new Person(1, "Checheneca"));
            Person person = _database.FindById(1);

            Assert.AreEqual("Checheneca", person.UserName);
        }
    }
}