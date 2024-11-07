using System.Windows;
using System.Windows.Input;

namespace LibraryManagement.ViewModels
{
    public class StudentsViewModel
    {
        public ICommand OpenAddStudentsWindowCommand { get; }

        public StudentsViewModel()
        {
            OpenAddStudentsWindowCommand = new RelayCommand(OpenAddStudentsWindow);
        }

        private void OpenAddStudentsWindow()
        {
            // Open the AddStudentsWindow as a dialog
            var addStudentsWindow = new Views.AddStudentWindow();
            addStudentsWindow.ShowDialog();
        }
    }
}
