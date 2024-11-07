using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LibraryManagement.ViewModels
{
    public class BorrowedBooksViewModel : INotifyPropertyChanged
    {
        private readonly BorrowRepository _borrowRepository;
        public ObservableCollection<BorrowDisplay> BorrowedBooks { get; set; }

        public BorrowedBooksViewModel()
        {
            _borrowRepository = new BorrowRepository(DatabaseConnection.Instance.GetConnection().ToString());
            BorrowedBooks = new ObservableCollection<BorrowDisplay>(GetAllBorrowedBooks());
        }

        private List<BorrowDisplay> GetAllBorrowedBooks()
        {
            var borrowedBooksData = _borrowRepository.GetAllBorrowedBooks();
            var borrowedBooksDisplayList = new List<BorrowDisplay>();

            // Transform Borrow data to BorrowDisplay data for DataGrid binding
            foreach (var borrow in borrowedBooksData)
            {
                borrowedBooksDisplayList.Add(new BorrowDisplay
                {
                    Buchnummer = borrow.BookBookNum,
                    Titel = "Sample Title", // Replace with actual title lookup if available
                    AusgeliehenVon = borrow.StudentLibraryCardNum.ToString(),
                    Ausleihdatum = borrow.BorrowDate,
                    Fristdatum = borrow.DueDate
                });
            }
            return borrowedBooksDisplayList;
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Data structure for DataGrid display
    public class BorrowDisplay
    {
        public string Buchnummer { get; set; }
        public string Titel { get; set; }
        public string AusgeliehenVon { get; set; }
        public DateTime Ausleihdatum { get; set; }
        public DateTime Fristdatum { get; set; }
    }
}
