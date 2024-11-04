using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Tests
{
    [TestClass]
    public class BookRepositoryTests
    {
        private BookRepository _bookRepository;
        private DatabaseConnection _dbConnection;

        [TestInitialize]
        public void Setup()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagementDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            _dbConnection = DatabaseConnection.GetInstance(connectionString);
            _bookRepository = new BookRepository(Convert.ToString(_dbConnection));
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dbConnection.CloseConnection();
        }

        [TestMethod]
        public void AddBook_ShouldAddBookToDatabase()
        {
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
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(addedBook, "The book should exist in the database.");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(newBook.Title, addedBook.Title);
        }
    }
}
