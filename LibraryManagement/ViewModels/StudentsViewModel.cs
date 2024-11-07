using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LibraryManagement.ViewModels
{
    public class StudentsViewModel
    {
        private readonly StudentRepository _studentRepository;
        public ObservableCollection<Student> Students { get; set; }
        public ICommand OpenAddStudentsWindowCommand { get; }
        public ICommand EditStudentCommand { get; }
        public ICommand OpenDeleteStudentWindowCommand { get; }

        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                // Raise PropertyChanged if using INotifyPropertyChanged
            }
        }

        public StudentsViewModel()
        {
            SqlConnection _dbConnection = DatabaseConnection.Instance.GetConnection();
            _studentRepository = new StudentRepository(_dbConnection.ToString());
            Students = new ObservableCollection<Student>();

            OpenAddStudentsWindowCommand = new RelayCommand(OpenAddStudentsWindow);

            LoadStudents(); // Load students when the ViewModel is initialized
        }

        private void LoadStudents()
        {
            var studentsList = _studentRepository.GetAllStudents();
            Students = new ObservableCollection<Student>(studentsList);
        }

        private void OpenAddStudentsWindow()
        {
            var addStudentsWindow = new Views.AddStudentWindow();
            addStudentsWindow.ShowDialog();

            LoadStudents(); // Refresh the list after adding a new student
        }
    }
}
