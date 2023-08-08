namespace Book.Tests
{
    using System;

    using NUnit.Framework;
    using static System.Net.Mime.MediaTypeNames;

    [TestFixture]
    public class Tests
    {
        private Book book;
        [SetUp]
        public void SetUp()
        {
            string bookName = "Trite prasenca";
            string author = "Bratq Grim";
            book = new Book(bookName, author);
        }

        [TearDown]
        public void TearDown()
        {
            book = null;
        }

        [Test]
        public void Book_Constructor_Sets_Correctly()
        {
            Assert.AreEqual("Trite prasenca", book.BookName);
            Assert.AreEqual("Bratq Grim", book.Author);
            Assert.AreEqual(0, book.FootnoteCount);
        }
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void Book_Constructor_Name_IsNullOrEmpty_ThrowsArgumentException(string name)
        {
            Assert.Throws<ArgumentException>(() => new Book(name, "Bratq Grim"));
        }
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void Book_Constructor_Author_IsNullOrEmpty_ThrowsArgumentException(string author)
        {
            Assert.Throws<ArgumentException>(() => new Book("Trite Praschovci", author));
        }
        [Test]
        [TestCase(1, "Gruh")]
        public void Book_Adds_FootNote_Correctly(int footNoteNumber, string text)
        {
            book.AddFootnote(footNoteNumber, text);
            Assert.AreEqual(1, book.FootnoteCount);
        }
        [Test]
        [TestCase(1, "Gruh")]
        public void Book_AddMethod_Throws_InvalidOperationException(int footNoteNumber, string text)
        {
            book.AddFootnote(footNoteNumber, text);
            Assert.Throws<InvalidOperationException>(() => book.AddFootnote(footNoteNumber, "Wink"));
        }
        [Test]
        [TestCase(1)]
        public void Book_FindFootnote_Throws_InvalidOperationException(int footNoteNumber)
        {
            book.AddFootnote(footNoteNumber, "Gruh");
            Assert.Throws<InvalidOperationException>(() => book.FindFootnote(2));
        }
        [Test]
        [TestCase(1, "Gruh")]
        public void Book_FindsFootnote_Correctly(int footNoteNumber, string text)
        {
            book.AddFootnote(footNoteNumber, "Gruh");
            string result = book.FindFootnote(footNoteNumber);
            string expectedText = $"Footnote #{footNoteNumber}: {text}";
            Assert.AreEqual(expectedText, result);
        }
        [Test]
        [TestCase(1, "Wink")]
        public void Book_AltersFootnote_Correctly(int footNoteNumber, string newText)
        {
            book.AddFootnote(footNoteNumber, "Gruh");
            book.AlterFootnote(footNoteNumber, newText);

            string result = book.FindFootnote(footNoteNumber);
            string expected = $"Footnote #{footNoteNumber}: {newText}";
            Assert.AreEqual(expected, result);
        }
        [Test]
        [TestCase(1, "Wink")]
        public void Book_AlterFootnoteMethod_Throws_InvalidOperationException(int footNoteNumber, string newText)
        {
            book.AddFootnote(footNoteNumber, "Gruh");


            Assert.Throws<InvalidOperationException>(() => book.AlterFootnote(2, newText));
        }
    }
}