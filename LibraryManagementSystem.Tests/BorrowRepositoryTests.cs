using System;
using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.Data.SqlClient;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Tests
{
    [TestFixture]
    public class BorrowRepositoryTests
    {
        private BorrowRepository _borrowRepository;

        // Prevent to borrow books that are borrowed

        [SetUp]
        public void Setup()
        {
            SqlConnection _dbConnection = DatabaseConnection.Instance.GetConnection();
            _borrowRepository = new BorrowRepository(_dbConnection.ToString());
        }

        [TearDown]
        public void Cleanup()
        {
            DatabaseConnection.Instance.CloseConnection();
        }


        [Test]
        public void BorrowBook_ShouldAddBorrowEntryToDatabase()
        {
            // Arrange
            string studentLibraryCardNum = "123456";
            string bookBookNum = "00030-2024";
            DateTime dueDate = DateTime.Now.AddDays(7);

            // Act
            _borrowRepository.BorrowBook(studentLibraryCardNum, bookBookNum, dueDate);

            // Assert
            var borrowedBooks = _borrowRepository.GetBorrowedBooksByStudent(Convert.ToInt32(studentLibraryCardNum));
            Assert.IsNotEmpty(borrowedBooks);
            Assert.IsTrue(borrowedBooks.Exists(b => b.BookBookNum == bookBookNum));
        }

        [Test]
        public void ReturnBook_ShouldSetReturnDate()
        {
            // Arrange
            string studentLibraryCardNum = "123456";
            string bookBookNum = "0000-2024";
            DateTime dueDate = DateTime.Now.AddDays(7);

            // Borrow the book to create a borrow entry
            _borrowRepository.BorrowBook(studentLibraryCardNum, bookBookNum, dueDate);

            // Retrieve the BorrowID of the entry just created (should have ReturnDate as NULL)
            var unreturnedBooks = _borrowRepository.GetBorrowedBooksByStudent(Convert.ToInt32(studentLibraryCardNum));
            int borrowId = unreturnedBooks.FirstOrDefault(b => b.BookBookNum == bookBookNum)?.BorrowID ?? -1;

            // Check if we successfully retrieved a borrow entry
            Assert.AreNotEqual(-1, borrowId, "Failed to retrieve borrow entry with a null ReturnDate.");

            // Act: Set the ReturnDate by returning the book
            _borrowRepository.ReturnBook(borrowId);

            // Assert: Verify that the ReturnDate is now set
            var allBorrowedBooks = _borrowRepository.GetAllBorrowedBooksByStudent(Convert.ToInt32(studentLibraryCardNum));
            var returnedBook = allBorrowedBooks.FirstOrDefault(b => b.BorrowID == borrowId);

            Assert.IsNotNull(returnedBook, "Returned book entry was not found.");
            Assert.IsTrue(returnedBook.ReturnDate.HasValue, "ReturnDate should not be null after returning the book.");
        }

        [Test]
        public void GetBorrowedBooksByStudent_ShouldReturnOnlyUnreturnedBooks()
        {
            // Arrange
            string studentLibraryCardNum = "123456";
            string bookBookNum1 = "00001-2024";
            string bookBookNum2 = "00002-2024";
            DateTime dueDate = DateTime.Now.AddDays(7);

            _borrowRepository.BorrowBook(studentLibraryCardNum, bookBookNum1, dueDate);
            _borrowRepository.BorrowBook(studentLibraryCardNum, bookBookNum2, dueDate);

            var borrowedBooks = _borrowRepository.GetBorrowedBooksByStudent(Convert.ToInt32(studentLibraryCardNum));
            int borrowId = borrowedBooks[0].BorrowID;
            _borrowRepository.ReturnBook(borrowId);

            // Act
            var unreturnedBooks = _borrowRepository.GetBorrowedBooksByStudent(Convert.ToInt32(studentLibraryCardNum));

            // Assert
            Assert.AreEqual(1, unreturnedBooks.Count);
            Assert.AreEqual(bookBookNum2, unreturnedBooks[0].BookBookNum);
        }

        [Test]
        public void GetOverdueBooks_ShouldReturnOnlyOverdueBooks()
        {
            // Arrange
            string studentLibraryCardNum = "123456";
            string bookBookNum = "00003-2024";
            DateTime overdueDate = DateTime.Now.AddDays(-1); // Set due date to past to make it overdue

            _borrowRepository.BorrowBook(studentLibraryCardNum, bookBookNum, overdueDate);

            // Act
            var overdueBooks = _borrowRepository.GetOverdueBooks();

            // Assert
            Assert.IsNotEmpty(overdueBooks);
            Assert.IsTrue(overdueBooks.Exists(b => b.BookBookNum == bookBookNum));
        }

        [Test]
        public void ExtendDueDate_ShouldUpdateDueDate()
        {
            // Arrange
            string studentLibraryCardNum = "123456";
            string bookBookNum = "00008-2024";
            DateTime initialDueDate = DateTime.Now.AddDays(7).AddMilliseconds(-DateTime.Now.Millisecond); // Remove milliseconds
            DateTime newDueDate = DateTime.Now.AddDays(14).AddMilliseconds(-DateTime.Now.Millisecond);

            _borrowRepository.BorrowBook(studentLibraryCardNum, bookBookNum, initialDueDate);
            var borrowedBooks = _borrowRepository.GetBorrowedBooksByStudent(Convert.ToInt32(studentLibraryCardNum));
            int borrowId = borrowedBooks[0].BorrowID;

            // Act
            _borrowRepository.ExtendDueDate(borrowId, newDueDate);

            // Assert
            borrowedBooks = _borrowRepository.GetBorrowedBooksByStudent(Convert.ToInt32(studentLibraryCardNum));
            Assert.AreEqual(newDueDate, borrowedBooks[0].DueDate, "The DueDate should be updated accurately.");
        }


        [Test]
        public void IsBookAvailable_ShouldReturnFalseForBorrowedBook()
        {
            // Arrange
            string studentLibraryCardNum = "123456";
            string bookBookNum = "00005-2024";
            DateTime dueDate = DateTime.Now.AddDays(7);

            _borrowRepository.BorrowBook(studentLibraryCardNum, bookBookNum, dueDate);

            // Act
            bool isAvailable = _borrowRepository.IsBookAvailable(bookBookNum);

            // Assert
            Assert.IsFalse(isAvailable);
        }

        [Test]
        public void IsBookAvailable_ShouldReturnTrueForAvailableBook()
        {
            // Arrange
            string bookBookNum = "00006-2024";

            // Act
            bool isAvailable = _borrowRepository.IsBookAvailable(bookBookNum);

            // Assert
            Assert.IsTrue(isAvailable);
        }

        [Test]
        public void BorrowBook_ShouldThrowException_WhenBookAlreadyBorrowed()
        {
            // Arrange
            string studentLibraryCardNum = "123456";
            string bookBookNum = "00007-2024";
            DateTime dueDate = DateTime.Now.AddDays(7);

            // First borrow - this should succeed
            _borrowRepository.BorrowBook(studentLibraryCardNum, bookBookNum, dueDate);

            // Act & Assert - trying to borrow the same book again should throw an exception
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                _borrowRepository.BorrowBook(studentLibraryCardNum, bookBookNum, dueDate);
            });
            Assert.AreEqual("This book is already borrowed and cannot be borrowed again until it is returned.", ex.Message);
        }


    }
}
