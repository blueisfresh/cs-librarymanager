using LibraryManagementSystem.Data;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LibraryManagementSystem.Models;
using Microsoft.Data.SqlClient;
using LibraryManagement.Views;

namespace LibraryManagement.ViewModels
{
    public class StudentsViewModel
    {
        private readonly StudentRepository _studentRepository;

        public ObservableCollection<Student> AllStudents { get; set; }
        public ObservableCollection<Student> FilteredStudents { get; set; }

        public ICommand OpenAddStudentWindowCommand { get; }
        public ICommand EditStudentCommand { get; }
        public ICommand OpenDeleteStudentWindowCommand { get; }
        public ICommand SearchCommand { get; }

        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                // Raise OnPropertyChanged("SelectedStudent") here if INotifyPropertyChanged is implemented
            }
        }

        private string _searchTerm;
        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                // Raise OnPropertyChanged("SearchTerm") here if INotifyPropertyChanged is implemented
            }
        }

        public StudentsViewModel()
        {
            SqlConnection _dbConnection = DatabaseConnection.Instance.GetConnection();
            _studentRepository = new StudentRepository(Convert.ToString(_dbConnection));

            OpenAddStudentWindowCommand = new RelayCommand(OpenAddStudentWindow);
            OpenDeleteStudentWindowCommand = new RelayCommand(OpenDeleteStudentWindow);
            EditStudentCommand = new RelayCommand(OpenEditStudentWindow);
            SearchCommand = new RelayCommand(SearchStudents);

            LoadStudents();
        }

        private void LoadStudents()
        {
            var studentsList = _studentRepository.GetAllStudents();
            AllStudents = new ObservableCollection<Student>(studentsList);
            FilteredStudents = new ObservableCollection<Student>(AllStudents); // Initially, FilteredStudents has all students
        }

        private void SearchStudents()
        {
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                var filteredList = _studentRepository.SearchStudentsByLastName(SearchTerm);
                FilteredStudents.Clear();
                foreach (var student in filteredList)
                {
                    FilteredStudents.Add(student);
                }
            }
            else
            {
                // Reset FilteredStudents to show all students
                FilteredStudents.Clear();
                foreach (var student in AllStudents)
                {
                    FilteredStudents.Add(student);
                }
            }
        }

        private void OpenAddStudentWindow()
        {
            var addStudentWindow = new Views.AddStudentWindow();
            var addStudentViewModel = new AddStudentViewModel(_studentRepository);

            addStudentWindow.DataContext = addStudentViewModel;
            addStudentWindow.ShowDialog();

            LoadStudents(); // Refresh students list after closing AddStudentWindow, if necessary
        }

        private void OpenDeleteStudentWindow()
        {
            var deleteStudentWindow = new DeleteStudentWindow(_studentRepository);
            deleteStudentWindow.ShowDialog();
        }

        private void OpenEditStudentWindow()
        {
            if (SelectedStudent == null) return;

            var addStudentWindow = new Views.AddStudentWindow();
            var addStudentViewModel = new AddStudentViewModel(_studentRepository, SelectedStudent);

            addStudentWindow.DataContext = addStudentViewModel;
            addStudentWindow.ShowDialog();

            LoadStudents(); // Refresh students list after closing the edit window
        }
    }
}
