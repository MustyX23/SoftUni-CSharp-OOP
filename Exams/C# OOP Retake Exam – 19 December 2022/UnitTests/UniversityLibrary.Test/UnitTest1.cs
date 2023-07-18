namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    using System.Text;

    public class Tests
    {
        private TextBook textBook;
        private UniversityLibrary universityLibrary;
        [SetUp]
        public void Setup()
        {
            textBook = new TextBook("Dune", "Ivan", "Fantasy");
            universityLibrary = new UniversityLibrary();
        }
        [TearDown]
        public void TearDown()
        {
            textBook = null;    
            universityLibrary = null;
        }

        [Test]
        public void TextBook_Constructor_Works()
        {
            Assert.AreEqual("Dune", textBook.Title);
            Assert.AreEqual("Ivan", textBook.Author);
            Assert.AreEqual("Fantasy", textBook.Category);
            Assert.AreEqual(0, textBook.InventoryNumber);
            Assert.AreEqual(null, textBook.Holder);
        }
        [Test]
        public void ToString_ReturnsCorrectStringRepresentation_WhenTitleIsEmpty()
        {
            // Arrange
            string title = "Introduction to Programming";
            string author = "John Smith";
            string category = "Computer Science";
            int inventoryNumber = 12345;

            TextBook textbook = new TextBook(title, author, category);
            textbook.InventoryNumber = inventoryNumber;

            StringBuilder expected = new StringBuilder();
            expected.AppendLine($"Book: {title} - {inventoryNumber}");
            expected.AppendLine($"Category: {category}");
            expected.AppendLine($"Author: {author}");

            // Act
            string actual = textbook.ToString();

            // Assert
            Assert.AreEqual(expected.ToString().TrimEnd(), actual);
        }
        [Test]
        public void UniversityLibrary_Catalogue_Returns_TextBooks()
        {
            universityLibrary.AddTextBookToLibrary(textBook);
            universityLibrary.AddTextBookToLibrary(new TextBook("Introduction to Programming", "John Smith", "Computer Science"));

            Assert.IsTrue(universityLibrary.Catalogue.Contains(textBook));                      
        }
        [Test]
        public void AddTextBookToLibrary_AddsTextBookToCatalogue()
        {
            // Arrange
            TextBook textBook = new TextBook("Introduction to Programming", "John Smith", "Computer Science");
            UniversityLibrary universityLibrary = new UniversityLibrary();

            // Act
            string result = universityLibrary.AddTextBookToLibrary(textBook);

            // Assert
            Assert.AreEqual(1, universityLibrary.Catalogue.Count);
            Assert.IsTrue(universityLibrary.Catalogue.Contains(textBook));
            Assert.AreEqual(textBook.ToString(), result);
        }
        [Test]
        public void LoanTextBook_UpdatesHolderName_WhenBookIsAvailable()
        {
            // Arrange
            TextBook textBook = new TextBook("Introduction to Programming", "John Smith", "Computer Science");
            UniversityLibrary universityLibrary = new UniversityLibrary();
            universityLibrary.AddTextBookToLibrary(textBook);

            int inventoryNumber = 1;
            string studentName = "Alice";

            // Act
            string result = universityLibrary.LoanTextBook(inventoryNumber, studentName);

            // Assert
            Assert.AreEqual(studentName, textBook.Holder);
            Assert.AreEqual($"{textBook.Title} loaned to {studentName}.", result);
        }
        [Test]
        public void LoanTextBook_ReturnsErrorMessage_WhenBookIsAlreadyLoanedToSameStudent()
        {
            // Arrange
            TextBook textBook = new TextBook("Introduction to Programming", "John Smith", "Computer Science");
            UniversityLibrary universityLibrary = new UniversityLibrary();
            universityLibrary.AddTextBookToLibrary(textBook);

            int inventoryNumber = 1;
            string studentName = "Alice";

            universityLibrary.LoanTextBook(inventoryNumber, studentName);

            // Act
            string result = universityLibrary.LoanTextBook(inventoryNumber, studentName);

            // Assert
            Assert.AreEqual($"{studentName} still hasn't returned {textBook.Title}!", result);
        }
        [Test]
        public void ReturnTextBook_ClearsHolderName_WhenBookIsReturned()
        {
            // Arrange
            TextBook textBook = new TextBook("Introduction to Programming", "John Smith", "Computer Science");
            UniversityLibrary universityLibrary = new UniversityLibrary();
            universityLibrary.AddTextBookToLibrary(textBook);

            int inventoryNumber = 1;
            string studentName = "Alice";
            universityLibrary.LoanTextBook(inventoryNumber, studentName);

            // Act
            string result = universityLibrary.ReturnTextBook(inventoryNumber);

            // Assert
            Assert.AreEqual(string.Empty, textBook.Holder);
            Assert.AreEqual($"{textBook.Title} is returned to the library.", result);
        }
    }
}