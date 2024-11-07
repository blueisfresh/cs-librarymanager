using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Models;
using System.Data.Common;

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
            // Check if the book is already borrowed
            string checkQuery = "SELECT COUNT(*) FROM TblBorrow WHERE BookBookNum = @BookBookNum AND ReturnDate IS NULL";
            using (var checkCommand = new SqlCommand(checkQuery, _databaseConnection))
            {
                checkCommand.Parameters.AddWithValue("@BookBookNum", bookBookNum);
                int borrowCount = (int)checkCommand.ExecuteScalar();

                if (borrowCount > 0)
                {
                    throw new InvalidOperationException("This book is already borrowed and cannot be borrowed again until it is returned.");
                }
            }

            string query = "INSERT INTO TblBorrow (StudentLibraryCardNum, BookBookNum, BorrowDate, DueDate) VALUES (@StudentLibraryCardNum, @BookBookNum, @BorrowDate, @DueDate)";
            using (var command = new SqlCommand(query, _databaseConnection))
            {
                command.Parameters.AddWithValue("@StudentLibraryCardNum", studentLibraryCardNum);
                command.Parameters.AddWithValue("@BookBookNum", bookBookNum);
                command.Parameters.AddWithValue("@BorrowDate", DateTime.Now);
                command.Parameters.AddWithValue("@DueDate", dueDate);
                command.ExecuteNonQuery();
            }
        }

        public void ReturnBook(int borrowId)
        {
            string query = "UPDATE TblBorrow SET ReturnDate = @ReturnDate WHERE BorrowID = @BorrowID";
            using (var command = new SqlCommand(query, _databaseConnection))
            {
                command.Parameters.AddWithValue("@ReturnDate", DateTime.Now);
                command.Parameters.AddWithValue("@BorrowID", borrowId);
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
