using System.Windows;
using LibraryManagementSystem.Data;

namespace LibraryManagement.Views
{
    public partial class DeleteBookWindow : Window
    {
        private readonly BookRepository _bookRepository;

        public DeleteBookWindow(BookRepository bookRepository)
        {
            InitializeComponent();
            _bookRepository = bookRepository;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string bookNum = BookNumTextBox.Text;

            if (!string.IsNullOrEmpty(bookNum))
            {
                _bookRepository.DeleteBook(bookNum);
                MessageBox.Show("Book deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid Book Number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
