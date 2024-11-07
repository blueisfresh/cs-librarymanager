using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Data
{
    public class BookRepository
    {
        private readonly SqlConnection _databaseConnection;

        public BookRepository(string connectionString)
        {
            _databaseConnection = DatabaseConnection.Instance.GetConnection();
        }

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

        public void AddBook(Book book)
        {
            try
            {
                // Datenvalidierung
                ValidateBookData(book);

                EnsureConnectionOpen();

                // Check if the book already exists
                string checkQuery = "SELECT COUNT(*) FROM TblBook WHERE BookNum = @BookNum";
                using (var checkCommand = new SqlCommand(checkQuery, _databaseConnection))
                {
                    checkCommand.Parameters.AddWithValue("@BookNum", book.BookNum);
                    int count = (int)checkCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        throw new InvalidOperationException("Das Buch mit dieser Nummer existiert bereits.");
                    }
                }

                // Insert book if it doesn't already exist
                string insertQuery = "INSERT INTO TblBook (BookNum, Title, Author, Publisher, ISBN, PublicationPlace, PublicationDate) " +
                                     "VALUES (@BookNum, @Title, @Author, @Publisher, @ISBN, @PublicationPlace, @PublicationDate)";
                using (var command = new SqlCommand(insertQuery, _databaseConnection))
                {
                    command.Parameters.AddWithValue("@BookNum", book.BookNum);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@Publisher", book.Publisher);
                    command.Parameters.AddWithValue("@ISBN", book.ISBN);
                    command.Parameters.AddWithValue("@PublicationPlace", book.PublicationPlace);
                    command.Parameters.AddWithValue("@PublicationDate", book.PublicationDate);

                    command.ExecuteNonQuery();
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Datenvalidierungsfehler: " + ex.Message);
                throw;
            }
            finally
            {
                EnsureConnectionClosed();
            }
        }

        // Methode zur Datenvalidierung des Buches
        private void ValidateBookData(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.BookNum))
            {
                throw new ArgumentException("Die Buchnummer darf nicht leer sein.");
            }

            if (string.IsNullOrWhiteSpace(book.Title))
            {
                throw new ArgumentException("Der Titel darf nicht leer sein.");
            }

            if (string.IsNullOrWhiteSpace(book.Author))
            {
                throw new ArgumentException("Der Autor darf nicht leer sein.");
            }

            if (string.IsNullOrWhiteSpace(book.Publisher))
            {
                throw new ArgumentException("Der Verlag darf nicht leer sein.");
            }

            if (string.IsNullOrWhiteSpace(book.ISBN) || book.ISBN.Length != 13)
            {
                throw new ArgumentException("Die ISBN muss 13 Zeichen lang sein.");
            }

            if (string.IsNullOrWhiteSpace(book.PublicationPlace))
            {
                throw new ArgumentException("Der Veröffentlichungsort darf nicht leer sein.");
            }

            if (book.PublicationDate == DateTime.MinValue)
            {
                throw new ArgumentException("Das Veröffentlichungsdatum ist ungültig.");
            }
        }



        public void UpdateBook(Book book)
        {
            try
            {
                EnsureConnectionOpen();

                string query = "UPDATE TblBook SET Title = @Title, Author = @Author, Publisher = @Publisher, ISBN = @ISBN, PublicationPlace = @PublicationPlace, PublicationDate = @PublicationDate WHERE BookNum = @BookNum";
                using (var command = new SqlCommand(query, _databaseConnection))
                {
                    command.Parameters.AddWithValue("@BookNum", book.BookNum);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@Publisher", book.Publisher);
                    command.Parameters.AddWithValue("@ISBN", book.ISBN);
                    command.Parameters.AddWithValue("@PublicationPlace", book.PublicationPlace);
                    command.Parameters.AddWithValue("@PublicationDate", book.PublicationDate);

                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                EnsureConnectionClosed();
            }
        }

        public void DeleteBook(string bookNum)
        {
            try
            {
                EnsureConnectionOpen();

                using (var command = new SqlCommand("DELETE FROM TblBook WHERE BookNum = @BookNum", _databaseConnection))
                {
                    command.Parameters.AddWithValue("@BookNum", bookNum);
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                EnsureConnectionClosed();
            }
        }

        public Book GetBookByNum(string bookNum)
        {
            try
            {
                EnsureConnectionOpen();

                using (var command = new SqlCommand("SELECT * FROM TblBook WHERE BookNum = @BookNum", _databaseConnection))
                {
                    command.Parameters.AddWithValue("@BookNum", bookNum);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Book
                            {
                                BookNum = reader["BookNum"].ToString(),
                                Title = reader["Title"].ToString(),
                                Author = reader["Author"].ToString(),
                                Publisher = reader["Publisher"].ToString(),
                                ISBN = reader["ISBN"].ToString(),
                                PublicationPlace = reader["PublicationPlace"].ToString(),
                                PublicationDate = Convert.ToDateTime(reader["PublicationDate"])
                            };
                        }
                    }
                }
            }
            finally
            {
                EnsureConnectionClosed();
            }

            return null; // Book not found
        }

        public List<Book> GetAllBooks()
        {
            var books = new List<Book>();

            try
            {
                EnsureConnectionOpen();

                using (var command = new SqlCommand("SELECT * FROM TblBook", _databaseConnection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new Book
                        {
                            BookNum = reader["BookNum"].ToString(),
                            Title = reader["Title"].ToString(),
                            Author = reader["Author"].ToString(),
                            Publisher = reader["Publisher"].ToString(),
                            ISBN = reader["ISBN"].ToString(),
                            PublicationPlace = reader["PublicationPlace"].ToString(),
                            PublicationDate = Convert.ToDateTime(reader["PublicationDate"])
                        });
                    }
                }
            }
            finally
            {
                EnsureConnectionClosed();
            }

            return books;
        }

        public List<Book> SearchBooksByTitle(string title)
        {
            var books = new List<Book>();
            string query = @"SELECT * FROM TblBook WHERE Title LIKE @Title";

            try
            {
                EnsureConnectionOpen();

                using (var command = new SqlCommand(query, _databaseConnection))
                {
                    command.Parameters.AddWithValue("@Title", "%" + title + "%");

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            books.Add(new Book
                            {
                                BookNum = reader["BookNum"].ToString(),
                                Title = reader["Title"].ToString(),
                                Author = reader["Author"].ToString(),
                                Publisher = reader["Publisher"].ToString(),
                                ISBN = reader["ISBN"].ToString(),
                                PublicationPlace = reader["PublicationPlace"].ToString(),
                                PublicationDate = (DateTime)reader["PublicationDate"]
                            });
                        }
                    }
                }
            }
            finally
            {
                EnsureConnectionClosed();
            }

            return books;
        }
    }
}
