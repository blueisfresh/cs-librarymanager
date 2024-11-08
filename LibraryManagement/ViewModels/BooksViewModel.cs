using LibraryManagementSystem.Data;
using System.Collections.ObjectModel;
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
        public ICommand SearchCommand { get; }

        private Book _selectedBook;
        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                // Raise OnPropertyChanged("SelectedBook") here if INotifyPropertyChanged is implemented
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

        public BooksViewModel()
        {
            SqlConnection _dbConnection = DatabaseConnection.Instance.GetConnection();
            _bookRepository = new BookRepository(Convert.ToString(_dbConnection));

            OpenAddBooksWindowCommand = new RelayCommand(OpenAddBooksWindow);
            OpenDeleteBookWindowCommand = new RelayCommand(OpenDeleteBookWindow);
            EditBookCommand = new RelayCommand(OpenEditBookWindow);
            SearchCommand = new RelayCommand(SearchBooks);

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
            var addBooksViewModel = new AddBooksViewModel(_bookRepository);

            addBooksWindow.DataContext = addBooksViewModel;
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
            var addBooksViewModel = new AddBooksViewModel(_bookRepository, SelectedBook); // Pass SelectedBook for editing

            addBooksWindow.DataContext = addBooksViewModel;
            addBooksWindow.ShowDialog();

            LoadBooks(); // Refresh books list after closing the edit window
        }


        private void SearchBooks()
        {
            var result = _bookRepository.SearchBooksByTitle(SearchTerm);
            Books.Clear();
            foreach (var book in result)
            {
                Books.Add(book);
            }
        }
    }
}
