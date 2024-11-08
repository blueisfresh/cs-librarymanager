using LibraryManagementSystem.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LibraryManagementSystem.Data;
using Microsoft.Data.SqlClient;

namespace LibraryManagement.ViewModels
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        private readonly BorrowRepository _borrowRepository;

        public List<KeyValuePair<string, int>> MostBorrowedBooks { get; set; }

        public StatisticsViewModel()
        {
            SqlConnection _dbConnection = DatabaseConnection.Instance.GetConnection();
            _borrowRepository = new BorrowRepository(_dbConnection.ToString());
            LoadMostBorrowedBooks();
        }

        private void LoadMostBorrowedBooks()
        {
            MostBorrowedBooks = _borrowRepository.GetTopBorrowedBooks(10);
            OnPropertyChanged(nameof(MostBorrowedBooks));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
