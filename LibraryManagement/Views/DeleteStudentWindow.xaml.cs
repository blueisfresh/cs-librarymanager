using LibraryManagementSystem.Data;
using System.Windows;

namespace LibraryManagement.Views
{
    public partial class DeleteStudentWindow : Window
    {
        private readonly StudentRepository _studentRepository;

        public DeleteStudentWindow(StudentRepository studentRepository)
        {
            InitializeComponent();
            _studentRepository = studentRepository;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the LibraryCardNum from the TextBox
            var libraryCardNum = LibraryCardNumTextBox.Text;

            // Confirm that the LibraryCardNum is provided
            if (string.IsNullOrEmpty(libraryCardNum))
            {
                MessageBox.Show("Please enter a Library Card Number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Try deleting the student
            try
            {
                _studentRepository.DeleteStudent(libraryCardNum);
                MessageBox.Show("Student deleted successfully.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
