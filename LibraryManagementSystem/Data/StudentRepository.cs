using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Data
{
    public class StudentRepository
    {
        private readonly SqlConnection _databaseConnection;

        public StudentRepository(string connectionString)
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

        public void AddStudent(Student student)
        {
            try
            {
                EnsureConnectionOpen();

                // Check if the student already exists
                string checkQuery = "SELECT COUNT(*) FROM TblStudent WHERE LibraryCardNum = @LibraryCardNum";
                using (var checkCommand = new SqlCommand(checkQuery, _databaseConnection))
                {
                    checkCommand.Parameters.AddWithValue("@LibraryCardNum", student.LibraryCardNum);
                    int count = (int)checkCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        throw new InvalidOperationException("Ein Student mit dieser Bibliothekskartennummer existiert bereits.");
                    }
                }

                // Insert new student
                string query = "INSERT INTO TblStudent (LibraryCardNum, FirstName, LastName) VALUES (@LibraryCardNum, @FirstName, @LastName)";
                using (var command = new SqlCommand(query, _databaseConnection))
                {
                    command.Parameters.AddWithValue("@LibraryCardNum", student.LibraryCardNum);
                    command.Parameters.AddWithValue("@FirstName", student.FirstName);
                    command.Parameters.AddWithValue("@LastName", student.LastName);

                    command.ExecuteNonQuery();
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                EnsureConnectionClosed();
            }
        }



        public Student GetStudentByLibraryCard(string libraryCard)
        {
            try
            {
                EnsureConnectionOpen();

                string query = "SELECT * FROM TblStudent WHERE LibraryCardNum = @LibraryCardNum";
                using (var command = new SqlCommand(query, _databaseConnection))
                {
                    command.Parameters.AddWithValue("@LibraryCardNum", libraryCard);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Student
                            {
                                LibraryCardNum = Convert.ToInt32(reader["LibraryCardNum"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString()
                            };
                        }
                    }
                }
            }
            finally
            {
                EnsureConnectionClosed();
            }

            return null; // Student not found
        }

        public void UpdateStudent(Student student)
        {
            try
            {
                EnsureConnectionOpen();

                string query = "UPDATE TblStudent SET FirstName = @FirstName, LastName = @LastName WHERE LibraryCardNum = @LibraryCardNum";
                using (var command = new SqlCommand(query, _databaseConnection))
                {
                    command.Parameters.AddWithValue("@FirstName", student.FirstName);
                    command.Parameters.AddWithValue("@LastName", student.LastName);
                    command.Parameters.AddWithValue("@LibraryCardNum", student.LibraryCardNum);

                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                EnsureConnectionClosed();
            }
        }

        public void DeleteStudent(string libraryCard)
        {
            try
            {
                EnsureConnectionOpen();

                string query = "DELETE FROM TblStudent WHERE LibraryCardNum = @LibraryCardNum";
                using (var command = new SqlCommand(query, _databaseConnection))
                {
                    command.Parameters.AddWithValue("@LibraryCardNum", libraryCard);
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                EnsureConnectionClosed();
            }
        }

        public List<Student> GetAllStudents()
        {
            var students = new List<Student>();

            try
            {
                EnsureConnectionOpen();

                // Updated SQL query to exclude students with LibraryCardNum = 0
                string query = "SELECT * FROM TblStudent WHERE LibraryCardNum <> 0";
                using (var command = new SqlCommand(query, _databaseConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                LibraryCardNum = Convert.ToInt32(reader["LibraryCardNum"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString()
                            });
                        }
                    }
                }
            }
            finally
            {
                EnsureConnectionClosed();
            }

            return students;
        }


        public List<Student> GetStudentByLastName(string lastName)
        {
            var students = new List<Student>();

            try
            {
                EnsureConnectionOpen();

                string query = "SELECT * FROM TblStudent WHERE LastName = @LastName";
                using (var command = new SqlCommand(query, _databaseConnection))
                {
                    command.Parameters.AddWithValue("@LastName", lastName);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                LibraryCardNum = Convert.ToInt32(reader["LibraryCardNum"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString()
                            });
                        }
                    }
                }
            }
            finally
            {
                EnsureConnectionClosed();
            }

            return students;
        }

        public List<Student> SearchStudentsByLastName(string lastName)
        {
            var students = new List<Student>();
            string query = @"SELECT * FROM TblStudent WHERE LastName LIKE @LastName";

            try
            {
                EnsureConnectionOpen();

                using (var command = new SqlCommand(query, _databaseConnection))
                {
                    command.Parameters.AddWithValue("@LastName", "%" + lastName + "%");

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                LibraryCardNum = Convert.ToInt32(reader["LibraryCardNum"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString()
                            });
                        }
                    }
                }
            }
            finally
            {
                EnsureConnectionClosed();
            }

            return students;
        }

        public Student GetStudentByLibraryCardNum(string libraryCardNum)
        {
            string query = "SELECT * FROM TblStudent WHERE LibraryCardNum = @LibraryCardNum";

            try
            {
                EnsureConnectionOpen();

                using (var command = new SqlCommand(query, _databaseConnection))
                {
                    command.Parameters.AddWithValue("@LibraryCardNum", libraryCardNum);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Student
                            {
                                LibraryCardNum = Convert.ToInt32(reader["LibraryCardNum"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString()
                            };
                        }
                    }
                }
            }
            finally
            {
                EnsureConnectionClosed();
            }

            return null; // Student not found
        }

    }
}
