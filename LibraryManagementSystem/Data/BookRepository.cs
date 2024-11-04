using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Data
{
    public class BookRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public BookRepository(string connectionString)
        {
            _databaseConnection = DatabaseConnection.GetInstance(connectionString);
        }

        public void AddBook(Book book)
        {
            using (var connection = _databaseConnection.GetConnection())
            using (var command = new SqlCommand("INSERT INTO TblBook (BookNum, Title, Author, Publisher, ISBN, PublicationPlace, PublicationDate) VALUES (@BookNum, @Title, @Author, @Publisher, @ISBN, @PublicationPlace, @PublicationDate)", connection))
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

        public void UpdateBook(Book book)
        {
            using (var connection = _databaseConnection.GetConnection())
            using (var command = new SqlCommand("UPDATE TblBook SET Title = @Title, Author = @Author, Publisher = @Publisher, ISBN = @ISBN, PublicationPlace = @PublicationPlace, PublicationDate = @PublicationDate WHERE BookNum = @BookNum", connection))
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

        public void DeleteBook(string bookNum)
        {
            using (var connection = _databaseConnection.GetConnection())
            using (var command = new SqlCommand("DELETE FROM TblBook WHERE BookNum = @BookNum", connection))
            {
                command.Parameters.AddWithValue("@BookNum", bookNum);
                command.ExecuteNonQuery();
            }
        }

        public Book GetBookByNum(string bookNum)
        {
            using (var connection = _databaseConnection.GetConnection())
            using (var command = new SqlCommand("SELECT * FROM TblBook WHERE BookNum = @BookNum", connection))
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
            return null; // Book not found
        }

        public List<Book> GetAllBooks()
        {
            var books = new List<Book>();

            using (var connection = _databaseConnection.GetConnection())
            using (var command = new SqlCommand("SELECT * FROM TblBook", connection))
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

            return books;
        }
    }
}
