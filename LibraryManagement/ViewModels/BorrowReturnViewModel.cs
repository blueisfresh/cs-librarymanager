using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace LibraryManagement.ViewModels
{
    public class BorrowReturnViewModel : INotifyPropertyChanged
    {
        private readonly BorrowRepository _borrowRepository;
        private bool _isBorrowing;
        private bool _isReturning;
        private string _bookNumber;
        private string _studentLibraryCardNum;
        private DateTime? _dueDate;

        public event PropertyChangedEventHandler PropertyChanged;

        public BorrowReturnViewModel()
        {
            SqlConnection _dbConnection = DatabaseConnection.Instance.GetConnection();
            _borrowRepository = new BorrowRepository(_dbConnection.ToString());
            SelectedBooks = new ObservableCollection<Book>();
            AddBookCommand = new RelayCommand(AddBookToSelectedList);
            ConfirmActionCommand = new RelayCommand(ExecuteBorrowOrReturn);
        }

        public ObservableCollection<Book> SelectedBooks { get; set; }

        public bool IsBorrowing
        {
            get => _isBorrowing;
            set
            {
                _isBorrowing = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsBorrowingVisible));
            }
        }

        public bool IsReturning
        {
            get => _isReturning;
            set
            {
                _isReturning = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsBorrowingVisible));
            }
        }

        public string BookNumber
        {
            get => _bookNumber;
            set
            {
                _bookNumber = value;
                OnPropertyChanged();
            }
        }

        public string StudentLibraryCardNum
        {
            get => _studentLibraryCardNum;
            set
            {
                _studentLibraryCardNum = value;
                OnPropertyChanged();
            }
        }

        public DateTime? DueDate
        {
            get => _dueDate;
            set
            {
                _dueDate = value;
                OnPropertyChanged();
            }
        }

        public Visibility IsBorrowingVisible => IsBorrowing ? Visibility.Visible : Visibility.Collapsed;

        public ICommand AddBookCommand { get; }
        public ICommand ConfirmActionCommand { get; }

        private void AddBookToSelectedList()
        {
            if (!string.IsNullOrEmpty(BookNumber))
            {
                var book = GetBookByNumber(BookNumber);
                if (book != null)
                {
                    SelectedBooks.Add(book);
                    BookNumber = string.Empty;
                }
                else
                {
                    MessageBox.Show("Book not found.");
                }
            }
        }

        private void ExecuteBorrowOrReturn()
        {
            if (IsBorrowing)
            {
                foreach (var book in SelectedBooks)
                {
                    // Assuming you have the required parameters for borrowing.
                    BorrowBook(book);
                }
                MessageBox.Show("Books borrowed successfully.");
            }
            else if (IsReturning)
            {
                foreach (var book in SelectedBooks)
                {
                    // Retrieve the corresponding Borrow record (you'll need to implement this in your repository).
                    var borrowRecord = _borrowRepository.GetBorrowRecordByBookNum(book.BookNum);

                    if (borrowRecord != null)
                    {
                        ReturnBook(borrowRecord);
                    }
                    else
                    {
                        MessageBox.Show($"No borrow record found for {book.Title}.");
                    }
                }
                MessageBox.Show("Books returned successfully.");
            }

            // Clear the selected books and reset due date after each operation
            SelectedBooks.Clear();
            DueDate = null;
        }


        private Book GetBookByNumber(string bookNumber)
        {
            return new Book { BookNum = bookNumber, Title = "Sample Book" };
        }

        private void BorrowBook(Book book)
        {
            if (DueDate == null)
            {
                MessageBox.Show("Please select a due date for borrowing.");
                return;
            }

            if (string.IsNullOrEmpty(StudentLibraryCardNum))
            {
                MessageBox.Show("Please enter the Student Library Card Number.");
                return;
            }

            _borrowRepository.BorrowBook(StudentLibraryCardNum, book.BookNum, DueDate.Value);
            MessageBox.Show($"Borrowed {book.Title} until {DueDate.Value.ToShortDateString()}.");
        }

        private void ReturnBook(Borrow borrow)
        {
            // Check if the book has a BorrowID associated with it
            if (borrow.BorrowID != null)
            {
                _borrowRepository.ReturnBook(borrow.BorrowID);

                MessageBox.Show($"Succesfull");
            }
            else
            {
                MessageBox.Show($"Cannot return - no BorrowID found.");
            }
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
