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
        public bool IsEditMode { get; private set; }

        public AddBooksViewModel(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            SaveBookCommand = new RelayCommand(SaveBook);
        }

        public AddBooksViewModel(BookRepository bookRepository, Book existingBook) : this(bookRepository)
        {
            NewBook = existingBook;
            IsEditMode = true; // Set to edit mode when an existing book is passed
        }

        private void SaveBook()
        {
            try
            {
                if (IsEditMode)
                {
                    _bookRepository.UpdateBook(NewBook);
                    MessageBox.Show("Book updated successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _bookRepository.AddBook(NewBook);
                    MessageBox.Show("New book has been added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (InvalidOperationException ex)
            {
                // Display an error message if a book with the same details already exists
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
