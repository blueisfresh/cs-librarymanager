using System.Windows;
using System.Windows.Input;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagement.ViewModels
{
    public class AddStudentViewModel
    {
        private readonly StudentRepository _studentRepository;
        public ICommand SaveStudentCommand { get; set; }
        public Student NewStudent { get; set; }
        public bool IsEditMode { get; private set; }

        // Constructor for adding a new student
        public AddStudentViewModel(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
            NewStudent = new Student(); // Initialize a new student object
            SaveStudentCommand = new RelayCommand(SaveStudent);
        }

        // Constructor for editing an existing student
        public AddStudentViewModel(StudentRepository studentRepository, Student existingStudent) : this(studentRepository)
        {
            NewStudent = existingStudent;
            IsEditMode = true; // Set to edit mode when an existing student is passed
        }

        private void SaveStudent()
        {
            try
            {
                if (IsEditMode)
                {
                    _studentRepository.UpdateStudent(NewStudent);
                    MessageBox.Show("Student updated successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _studentRepository.AddStudent(NewStudent);
                    MessageBox.Show("New student has been added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (InvalidOperationException ex)
            {
                // Display an error message if a student with the same LibraryCardNum already exists
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Handle any other unexpected exceptions
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
