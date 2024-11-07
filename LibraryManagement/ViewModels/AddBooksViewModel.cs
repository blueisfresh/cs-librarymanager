using System.Windows;
using System.Windows.Input;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagement.ViewModels
{
    public class AddBooksViewModel
    {
        private readonly BookRepository _bookRepository;
        public ICommand SaveBookCommand { get; set; }

        public Book NewBook { get; set; }

        public AddBooksViewModel(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            NewBook = new Book(); // Initialize a new book object
            SaveBookCommand = new RelayCommand(SaveBook);
        }

        private void SaveBook()
        {
            // Save the new book to the database
            _bookRepository.AddBook(NewBook);

            MessageBox.Show($"New Book has been added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
   