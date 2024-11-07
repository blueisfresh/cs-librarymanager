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

        public void AddStudent(Student student)
        {
            string query = "INSERT INTO TblStudent (LibraryCardNum, FirstName, LastName) VALUES (@LibraryCardNum, @FirstName, @LastName)";
            using (var command = new SqlCommand(query, _databaseConnection))
            {
                command.Parameters.AddWithValue("@LibraryCardNum", student.LibraryCardNum);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.ExecuteNonQuery();
            }
        }

        public Student GetStudentByLibraryCard(string libraryCard)
        {
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
            return null; // Student not found
        }

        public void UpdateStudent(Student student)
        {
            string query = "UPDATE TblStudent SET FirstName = @FirstName, LastName = @LastName WHERE LibraryCardNum = @LibraryCardNum";
            using (var command = new SqlCommand(query, _databaseConnection))
            {
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@LibraryCardNum", student.LibraryCardNum);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteStudent(string libraryCard)
        {
            string query = "DELETE FROM TblStudent WHERE LibraryCardNum = @LibraryCardNum";
            using (var command = new SqlCommand(query, _databaseConnection))
            {
                command.Parameters.AddWithValue("@LibraryCardNum", libraryCard);
                command.ExecuteNonQuery();
            }
        }

        public List<Student> GetAllStudents()
        {
            string query = "SELECT * FROM TblStudent";
            var students = new List<Student>();

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

            return students;
        }

        public List<Student> GetStudentByLastName(string lastName)
        {
            string query = "SELECT * FROM TblStudent WHERE LastName = @LastName";
            var students = new List<Student>();

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

            return students;
        }


    }

}
