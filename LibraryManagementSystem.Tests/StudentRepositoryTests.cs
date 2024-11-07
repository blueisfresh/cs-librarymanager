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
    public class StudentRepositoryTests
    {
        private StudentRepository _studentRepository;
        private SqlConnection _dbConnection;

        [SetUp]
        public void Setup()
        {
            _dbConnection = DatabaseConnection.Instance.GetConnection();
            _studentRepository = new StudentRepository(_dbConnection.ConnectionString);
        }

        [TearDown]
        public void Cleanup()
        {
            _dbConnection.Close(); // Close the connection
            _dbConnection.Dispose(); // Dispose of the connection
        }

        [Test]
        public void AddStudent_ShouldAddStudentToDatabase()
        {
            // Arrange
            var student = new Student
            {
                LibraryCardNum = 789012, // New unique LibraryCardNum
                FirstName = "Robert",
                LastName = "Johnson"
            };

            // Act
            _studentRepository.AddStudent(student);

            // Assert
            var addedStudent = _studentRepository.GetStudentByLibraryCard("789012");
            Assert.IsNotNull(addedStudent);
            Assert.AreEqual(student.FirstName, addedStudent.FirstName);
            Assert.AreEqual(student.LastName, addedStudent.LastName);
        }

        [Test]
        public void GetStudentByLibraryCard_ShouldReturnCorrectStudent()
        {
            // Arrange
            var student = new Student
            {
                LibraryCardNum = 100001,
                FirstName = "David",
                LastName = "Green"
            };
            _studentRepository.AddStudent(student);

            // Act
            var retrievedStudent = _studentRepository.GetStudentByLibraryCard("100001");

            // Assert
            Assert.IsNotNull(retrievedStudent);
            Assert.AreEqual(student.FirstName, retrievedStudent.FirstName);
            Assert.AreEqual(student.LastName, retrievedStudent.LastName);
        }

        [Test]
        public void UpdateStudent_ShouldModifyStudentDetails()
        {
            // Arrange
            var student = new Student
            {
                LibraryCardNum = 100002,
                FirstName = "Samuel",
                LastName = "White"
            };
            _studentRepository.AddStudent(student);

            // Modify student
            student.FirstName = "Samuel Updated";
            student.LastName = "White";

            // Act
            _studentRepository.UpdateStudent(student);

            // Assert
            var updatedStudent = _studentRepository.GetStudentByLibraryCard("100002");
            Assert.IsNotNull(updatedStudent);
            Assert.AreEqual("Samuel Updated", updatedStudent.FirstName);
            Assert.AreEqual("White", updatedStudent.LastName);
        }

        [Test]
        public void DeleteStudent_ShouldRemoveStudentFromDatabase()
        {
            // Arrange
            var student = new Student
            {
                LibraryCardNum = 100003,
                FirstName = "Grace",
                LastName = "Bell"
            };
            _studentRepository.AddStudent(student);

            // Act
            _studentRepository.DeleteStudent("100003");

            // Assert
            var deletedStudent = _studentRepository.GetStudentByLibraryCard("100003");
            Assert.IsNull(deletedStudent);
        }

        [Test]
        public void GetAllStudents_ShouldReturnAllStudents()
        {
            // Arrange
            var student1 = new Student
            {
                LibraryCardNum = 100004,
                FirstName = "Ella",
                LastName = "Scott"
            };
            var student2 = new Student
            {
                LibraryCardNum = 100005,
                FirstName = "Mason",
                LastName = "Gray"
            };
            _studentRepository.AddStudent(student1);
            _studentRepository.AddStudent(student2);

            // Act
            var allStudents = _studentRepository.GetAllStudents();

            // Assert
            Assert.IsTrue(allStudents.Count >= 2);
            Assert.IsTrue(allStudents.Exists(s => s.LibraryCardNum == 100004));
            Assert.IsTrue(allStudents.Exists(s => s.LibraryCardNum == 100005));
        }

        [Test]
        public void GetStudentByLastName_ShouldReturnCorrectStudents()
        {
            // Arrange
            var student1 = new Student
            {
                LibraryCardNum = 100006,
                FirstName = "Henry",
                LastName = "Taylor"
            };
            var student2 = new Student
            {
                LibraryCardNum = 100007,
                FirstName = "Olivia",
                LastName = "Taylor"
            };
            _studentRepository.AddStudent(student1);
            _studentRepository.AddStudent(student2);

            // Act
            var studentsWithLastName = _studentRepository.GetStudentByLastName("Taylor");

            // Assert
            Assert.AreEqual(2, studentsWithLastName.Count);
            Assert.IsTrue(studentsWithLastName.Exists(s => s.FirstName == "Henry"));
            Assert.IsTrue(studentsWithLastName.Exists(s => s.FirstName == "Olivia"));
        }

    }
}
