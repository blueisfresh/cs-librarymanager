﻿using LibraryManagementSystem.Data;
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
        public ObservableCollection<Student> Students { get; set; }

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

            LoadStudents();
        }

        private void LoadStudents()
        {
            var studentsList = _studentRepository.GetAllStudents();
            Students = new ObservableCollection<Student>(studentsList);
        }

        private void OpenAddStudentWindow()
        {
            var addStudentWindow = new Views.AddStudentWindow();
            var addStudentViewModel = new AddStudentViewModel(_studentRepository);

            addStudentWindow.DataContext = addStudentViewModel;
            addStudentWindow.ShowDialog();

            LoadStudents();  // Refresh students list after closing AddStudentWindow, if necessary
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
            var addStudentViewModel = new AddStudentViewModel(_studentRepository, SelectedStudent); // Pass SelectedStudent for editing

            addStudentWindow.DataContext = addStudentViewModel;
            addStudentWindow.ShowDialog();

            LoadStudents(); // Refresh students list after closing the edit window
        }

    }
}
