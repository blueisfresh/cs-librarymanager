using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.Data.SqlClient;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace LibraryManagementSystem.Tests
{
    [TestClass]
    public class BookRepositoryTests
    {
        private BookRepository _bookRepository;
        private SqlConnection _dbConnection;
        
        [SetUp]
        public void Setup()
        {
            SqlConnection _dbConnection = DatabaseConnection.Instance.GetConnection();
            BookRepository _bookRepository = new BookRepository(Convert.ToString(_dbConnection));
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Cleanup added books
            _bookRepository.DeleteBook("00021-2024");
            _bookRepository.DeleteBook("00022-2024");
            _bookRepository.DeleteBook("00023-2024");

            DatabaseConnection.Instance.CloseConnection();
        }


        [Test]
        public void AddBook_ShouldAddBookToDatabase()
        {
            SqlConnection _dbConnection = DatabaseConnection.Instance.GetConnection();
            BookRepository _bookRepository = new BookRepository(Convert.ToString(_dbConnection));

            // Arrange
            var newBook = new Book
            {
                BookNum = "00021-2024",
                Title = "New Test Book",
                Author = "Test Author",
                Publisher = "Test Publisher",
                ISBN = "1234567890123",
                PublicationPlace = "Test City",
                PublicationDate = DateTime.Now
            };

            // Act
            _bookRepository.AddBook(newBook);

            // Assert
            var addedBook = _bookRepository.GetBookByNum("00021-2024");
            Assert.IsNotNull(addedBook);
            Assert.AreEqual(newBook.Title, addedBook.Title);
        }

        [Test]
        public void GetBookByNum_ShouldReturnCorrectBook()
        {
            SqlConnection _dbConnection = DatabaseConnection.Instance.GetConnection();
            BookRepository _bookRepository = new BookRepository(Convert.ToString(_dbConnection));

            // Arrange
            var expectedBook = new Book
            {
                BookNum = "00022-2024",
                Title = "Another Test Book",
                Author = "Another Author",
                Publisher = "Another Publisher",
                ISBN = "9876543210987",
                PublicationPlace = "Another City",
                PublicationDate = DateTime.Now
            };
            _bookRepository.AddBook(expectedBook);

            // Act
            var selectedBook = _bookRepository.GetBookByNum("00022-2024");

            // Assert
            Assert.IsNotNull(selectedBook);
            Assert.AreEqual(expectedBook.Title, selectedBook.Title);
            Assert.AreEqual(expectedBook.Author, selectedBook.Author);
        }
        [Test]
        public void UpdateBook_ShouldModifyBookInDatabase()
        {
            SqlConnection _dbConnection = DatabaseConnection.Instance.GetConnection();
            BookRepository _bookRepository = new BookRepository(Convert.ToString(_dbConnection));

            // Arrange
            var book = new Book
            {
                BookNum = "00023-2024",
                Title = "Original Title",
                Author = "Original Author",
                Publisher = "Original Publisher",
                ISBN = "1234567890123",
                PublicationPlace = "Original City",
                PublicationDate = DateTime.Now
            };
            _bookRepository.AddBook(book);

            // Modify book details
            book.Title = "Updated Title";
            book.Author = "Updated Author";

            // Act
            _bookRepository.UpdateBook(book);

            // Assert
            var updatedBook = _bookRepository.GetBookByNum("00023-2024");
            Assert.IsNotNull(updatedBook);
            Assert.AreEqual("Updated Title", updatedBook.Title);
            Assert.AreEqual("Updated Author", updatedBook.Author);
        }
        [Test]
        public void GetAllBooks_ShouldReturnAllBooks()
        {
            SqlConnection _dbConnection = DatabaseConnection.Instance.GetConnection();
            BookRepository _bookRepository = new BookRepository(Convert.ToString(_dbConnection));

            // Arrange
            var book1 = new Book
            {
                BookNum = "00024-2024",
                Title = "Book One",
                Author = "Author One",
                Publisher = "Publisher One",
                ISBN = "1111111111111",
                PublicationPlace = "Place One",
                PublicationDate = DateTime.Now
            };
            var book2 = new Book
            {
                BookNum = "00025-2024",
                Title = "Book Two",
                Author = "Author Two",
                Publisher = "Publisher Two",
                ISBN = "2222222222222",
                PublicationPlace = "Place Two",
                PublicationDate = DateTime.Now
            };
            _bookRepository.AddBook(book1);
            _bookRepository.AddBook(book2);

            // Act
            var allBooks = _bookRepository.GetAllBooks();

            // Assert
            Assert.IsTrue(allBooks.Count >= 2);
            Assert.IsTrue(allBooks.Exists(b => b.BookNum == "00024-2024"));
            Assert.IsTrue(allBooks.Exists(b => b.BookNum == "00025-2024"));
        }
    }
}
