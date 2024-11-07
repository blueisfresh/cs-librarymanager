using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagement.ViewModels
{
    public class BooksViewModel
    {
        public ICommand OpenAddBooksWindowCommand { get; }

        public BooksViewModel()
        {
            OpenAddBooksWindowCommand = new RelayCommand(OpenAddBooksWindow);
        }

        private void OpenAddBooksWindow()
        {
            // Open the AddBooksWindow as a dialog
            var addBooksWindow = new Views.AddBooksWindow();
            addBooksWindow.ShowDialog();
        }
    }
}
