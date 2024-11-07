using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Models;
using System.Data.Common;
using System.Data;

namespace LibraryManagementSystem.Data
{
    public class BorrowRepository
    {
        private readonly SqlConnection _databaseConnection;

        public BorrowRepository(string connectionString)
        {
            _databaseConnection = DatabaseConnection.Instance.GetConnection();
        }

        public void BorrowBook(string studentLibraryCardNum, string bookBookNum, DateTime dueDate)
        {
            // Check if the student exists
            string checkStudentQuery = "SELECT COUNT(*) FROM TblStudent WHERE LibraryCardNum = @LibraryCardNum";
            using (var checkStudentCommand = new SqlCommand(checkStudentQuery, _databaseConnection))
            {
                checkStudentCommand.Parameters.AddWithValue("@LibraryCardNum", studentLibraryCardNum);
                EnsureConnectionOpen();
                int studentCount = (int)checkStudentCommand.ExecuteScalar();
                EnsureConnectionClosed();

                if (studentCount == 0)
                {
                    throw new InvalidOperationException("Der Student mit der angegebenen Bibliothekskartennummer existiert nicht.");
                }
            }

            // Check if the book exists
            string checkBookQuery = "SELECT COUNT(*) FROM TblBook WHERE BookNum = @BookNum";
            using (var checkBookCommand = new SqlCommand(checkBookQuery, _databaseConnection))
            {
                checkBookCommand.Parameters.AddWithValue("@BookNum", bookBookNum);
                EnsureConnectionOpen();
                int bookCount = (int)checkBookCommand.ExecuteScalar();
                EnsureConnectionClosed();

                if (bookCount == 0)
                {
                    throw new InvalidOperationException("Das Buch mit der angegebenen Buchnummer existiert nicht.");
                }
            }

            // Check if the book is already borrowed
            string checkBorrowQuery = "SELECT COUNT(*) FROM TblBorrow WHERE BookBookNum = @BookBookNum AND ReturnDate IS NULL";
            using (var checkBorrowCommand = new SqlCommand(checkBorrowQuery, _databaseConnection))
            {
                checkBorrowCommand.Parameters.AddWithValue("@BookBookNum", bookBookNum);
                EnsureConnectionOpen();
                int borrowCount = (int)checkBorrowCommand.ExecuteScalar();
                EnsureConnectionClosed();

                if (borrowCount > 0)
                {
                    throw new InvalidOperationException("Dieses Buch ist bereits ausgeliehen und kann nicht erneut ausgeliehen werden, bis es zurückgegeben wird.");
                }
            }

            // Insert borrow record if all checks pass
            string query = "INSERT INTO TblBorrow (StudentLibraryCardNum, BookBookNum, BorrowDate, DueDate) VALUES (@StudentLibraryCardNum, @BookBookNum, @BorrowDate, @DueDate)";
            using (var command = new SqlCommand(query, _databaseConnection))
            {
                command.Parameters.AddWithValue("@StudentLibraryCardNum", studentLibraryCardNum);
                command.Parameters.AddWithValue("@BookBookNum", bookBookNum);
                command.Parameters.AddWithValue("@BorrowDate", DateTime.Now);
                command.Parameters.AddWithValue("@DueDate", dueDate);

                EnsureConnectionOpen();
                command.ExecuteNonQuery();
                EnsureConnectionClosed();
            }
        }

        // Hilfsmethode zur Sicherstellung, dass die Verbindung nur bei Bedarf geöffnet/geschlossen wird
        private void EnsureConnectionOpen()
        {
            if (_databaseConnection.State == ConnectionState.Closed)
            {
                _databaseConnection.Open();
            }
        }

        private void EnsureConnectionClosed()
        {
            if (_databaseConnection.State == ConnectionState.Open)
            {
                _databaseConnection.Close();
            }
        }

        public List<KeyValuePair<string, int>> GetTopBorrowedBooks(int topCount)
        {
            var topBorrowedBooks = new List<KeyValuePair<string, int>>();
            string query = @"SELECT TOP (@TopCount) BookBookNum, COUNT(*) AS BorrowCount
                     FROM TblBorrow
                     GROUP BY BookBookNum
                     ORDER BY BorrowCount DESC";

            using (var command = new SqlCommand(query, _databaseConnection))
            {
                command.Parameters.AddWithValue("@TopCount", topCount);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var bookNum = reader["BookBookNum"].ToString();
                        var borrowCount = (int)reader["BorrowCount"];
                        topBorrowedBooks.Add(new KeyValuePair<string, int>(bookNum, borrowCount));
                    }
                }
            }

            return topBorrowedBooks;
        }


        public void ReturnBook(int borrowId)
        {
            // Define the "Anonym" student ID based on the database value
            string anonymStudentLibraryCardNum = "0"; // Matches the single zero in the database

            // SQL query to update the `ReturnDate` and set the `StudentLibraryCardNum` to "Anonym"
            string query = @"
        UPDATE TblBorrow 
        SET ReturnDate = @ReturnDate, 
            StudentLibraryCardNum = @AnonymStudentLibraryCardNum
        WHERE BorrowID = @BorrowID";

            using (var command = new SqlCommand(query, _databaseConnection))
            {
                // Set parameters
                command.Parameters.AddWithValue("@ReturnDate", DateTime.Now);
                command.Parameters.AddWithValue("@AnonymStudentLibraryCardNum", anonymStudentLibraryCardNum);
                command.Parameters.AddWithValue("@BorrowID", borrowId);

                // Execute the update
                command.ExecuteNonQuery();
            }
        }


        public List<Borrow> GetBorrowedBooksByStudent(int studentLibraryCardNum)
        {
            string query = "SELECT * FROM TblBorrow WHERE StudentLibraryCardNum = @StudentLibraryCardNum AND ReturnDate IS NULL";
            var borrowedBooks = new List<Borrow>();

            using (var command = new SqlCommand(query, _databaseConnection))
            {
                command.Parameters.AddWithValue("@StudentLibraryCardNum", studentLibraryCardNum);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        borrowedBooks.Add(new Borrow
                        {
                            BorrowID = Convert.ToInt32(reader["BorrowID"]),
                            StudentLibraryCardNum = Convert.ToInt32(reader["StudentLibraryCardNum"]),
                            BookBookNum = reader["BookBookNum"].ToString(),
                            BorrowDate = Convert.ToDateTime(reader["BorrowDate"]),
                            DueDate = Convert.ToDateTime(reader["DueDate"]),
                            ReturnDate = reader["ReturnDate"] == DBNull.Value ? null : (DateTime?)reader["ReturnDate"]

                        });
                    }
                }
            }

            return borrowedBooks;
        }

        public List<Borrow> GetAllBorrowedBooks()
        {
            string query = "SELECT * FROM TblBorrow WHERE ReturnDate IS NULL";
            var borrowedBooks = new List<Borrow>();

            using (var command = new SqlCommand(query, _databaseConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        borrowedBooks.Add(new Borrow
                        {
                            BorrowID = Convert.ToInt32(reader["BorrowID"]),
                            StudentLibraryCardNum = Convert.ToInt32(reader["StudentLibraryCardNum"]),
                            BookBookNum = reader["BookBookNum"].ToString(),
                            BorrowDate = Convert.ToDateTime(reader["BorrowDate"]),
                            DueDate = Convert.ToDateTime(reader["DueDate"]),
                            ReturnDate = reader["ReturnDate"] == DBNull.Value ? null : (DateTime?)reader["ReturnDate"]
                        });
                    }
                }
            }

            return borrowedBooks;
        }


        public List<Borrow> GetOverdueBooks()
        {
            string query = "SELECT * FROM TblBorrow WHERE DueDate < @Today AND ReturnDate IS NULL";
            var overdueBooks = new List<Borrow>();

            using (var command = new SqlCommand(query, _databaseConnection))
            {
                command.Parameters.AddWithValue("@Today", DateTime.Now);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        overdueBooks.Add(new Borrow
                        {
                            BorrowID = Convert.ToInt32(reader["BorrowID"]),
                            StudentLibraryCardNum = Convert.ToInt32(reader["StudentLibraryCardNum"]),
                            BookBookNum = reader["BookBookNum"].ToString(),
                            BorrowDate = Convert.ToDateTime(reader["BorrowDate"]),
                            DueDate = Convert.ToDateTime(reader["DueDate"]),
                            ReturnDate = reader["ReturnDate"] == DBNull.Value ? null : (DateTime?)reader["ReturnDate"]
                        });
                    }
                }
            }

            return overdueBooks;
        }

        public void ExtendDueDate(int borrowId, DateTime newDueDate)
        {
            string query = "UPDATE TblBorrow SET DueDate = @NewDueDate WHERE BorrowID = @BorrowID";
            using (var command = new SqlCommand(query, _databaseConnection))
            {
                command.Parameters.AddWithValue("@NewDueDate", newDueDate);
                command.Parameters.AddWithValue("@BorrowID", borrowId);
                command.ExecuteNonQuery();
            }
        }

        public bool IsBookAvailable(string bookNum)
        {
            string query = "SELECT COUNT(*) FROM TblBorrow WHERE BookBookNum = @BookBookNum AND ReturnDate IS NULL";
            using (var command = new SqlCommand(query, _databaseConnection))
            {
                command.Parameters.AddWithValue("@BookBookNum", bookNum);

                int borrowCount = (int)command.ExecuteScalar();
                return borrowCount == 0; // If count is 0, the book is available
            }
        }

        public List<Borrow> GetAllBorrowedBooksByStudent(int studentLibraryCardNum)
        {
            string query = "SELECT * FROM TblBorrow WHERE StudentLibraryCardNum = @StudentLibraryCardNum";
            var borrowedBooks = new List<Borrow>();

            using (var command = new SqlCommand(query, _databaseConnection))
            {
                command.Parameters.AddWithValue("@StudentLibraryCardNum", studentLibraryCardNum);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        borrowedBooks.Add(new Borrow
                        {
                            BorrowID = Convert.ToInt32(reader["BorrowID"]),
                            StudentLibraryCardNum = Convert.ToInt32(reader["StudentLibraryCardNum"]),
                            BookBookNum = reader["BookBookNum"].ToString(),
                            BorrowDate = Convert.ToDateTime(reader["BorrowDate"]),
                            DueDate = Convert.ToDateTime(reader["DueDate"]),
                            ReturnDate = reader["ReturnDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ReturnDate"])
                        });
                    }
                }
            }

            return borrowedBooks;
        }

        public Borrow GetBorrowRecordByBookNum(string bookNum)
        {
            Borrow borrowRecord = null;

            string query = "SELECT BorrowID, StudentLibraryCardNum, BookBookNum, BorrowDate, DueDate " +
                           "FROM TblBorrow " +
                           "WHERE BookBookNum = @BookNum AND ReturnDate IS NULL";

            using (var command = new SqlCommand(query, _databaseConnection))
            {
                command.Parameters.AddWithValue("@BookNum", bookNum);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        borrowRecord = new Borrow
                        {
                            BorrowID = reader.GetInt32(reader.GetOrdinal("BorrowID")),
                            StudentLibraryCardNum = reader.GetOrdinal("StudentLibraryCardNum"),
                            BookBookNum = reader.GetString(reader.GetOrdinal("BookBookNum")),
                            BorrowDate = reader.GetDateTime(reader.GetOrdinal("BorrowDate")),
                            DueDate = reader.GetDateTime(reader.GetOrdinal("DueDate"))
                            // ReturnDate is not set here since we only fetch records with NULL ReturnDate
                        };
                    }
                }
            }

            return borrowRecord;
        }



    }
}
