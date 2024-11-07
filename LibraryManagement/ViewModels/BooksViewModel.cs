using LibraryManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LibraryManagementSystem.Models;
using Microsoft.Data.SqlClient;
using LibraryManagement.Views;

namespace LibraryManagement.ViewModels
{
    public class BooksViewModel
    {
        private readonly BookRepository _bookRepository;
        public ObservableCollection<Book> Books { get; set; }
        public ICommand OpenAddBooksWindowCommand { get; }
        public ICommand EditBookCommand { get; }
        public ICommand OpenDeleteBookWindowCommand { get; }

        private Book _selectedBook;
        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                // Hier können Sie OnPropertyChanged("SelectedBook") aufrufen, wenn Sie INotifyPropertyChanged implementieren
            }
        }


        public BooksViewModel()
        {
            SqlConnection _dbConnection = DatabaseConnection.Instance.GetConnection();
            _bookRepository = new BookRepository(Convert.ToString(_dbConnection));
            OpenAddBooksWindowCommand = new RelayCommand(OpenAddBooksWindow);
            OpenDeleteBookWindowCommand = new RelayCommand(OpenDeleteBookWindow);
            EditBookCommand = new RelayCommand(OpenEditBookWindow);

            LoadBooks();
        }

        private void LoadBooks()
        {
            var booksList = _bookRepository.GetAllBooks();
            Books = new ObservableCollection<Book>(booksList);
        }

        private void OpenAddBooksWindow()
        {
            var addBooksWindow = new Views.AddBooksWindow();

            // Pass the existing _bookRepository to AddBooksViewModel
            var addBooksViewModel = new AddBooksViewModel(_bookRepository);

            addBooksWindow.DataContext = addBooksViewModel;  // Set DataContext to AddBooksViewModel

            addBooksWindow.ShowDialog();

            LoadBooks();  // Refresh books list after closing AddBooksWindow, if necessary
        }

        private void OpenDeleteBookWindow()
        {
            var deleteBookWindow = new DeleteBookWindow(_bookRepository);
            deleteBookWindow.ShowDialog();
        }

        private void OpenEditBookWindow()
        {
            if (SelectedBook == null) return;

            var addBooksWindow = new Views.AddBooksWindow();

            // Pass the existing _bookRepository to AddBooksViewModel
            var addBooksViewModel = new AddBooksViewModel(_bookRepository)
            {
                NewBook = SelectedBook // Das Fenster mit dem ausgewählten Buch befüllen
            };

            addBooksWindow.DataContext = addBooksViewModel;
            addBooksWindow.ShowDialog();

            // Nach dem Schließen des Fensters die Liste aktualisieren
            LoadBooks();
        }


    }
}
