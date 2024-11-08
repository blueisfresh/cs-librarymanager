using LibraryManagement.Views;
using LibraryManagement;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;

public class BorrowReturnViewModel : INotifyPropertyChanged
{
    private readonly BorrowRepository _borrowRepository;
    private readonly BookRepository _bookRepository; // Added BookRepository
    private readonly StudentRepository _studentRepository;
    private bool _isBorrowing;
    private bool _isReturning;
    private string _bookNumber;
    private string _studentLibraryCardNum;
    private DateTime? _dueDate;

    public Visibility IsReturningVisible => IsReturning ? Visibility.Collapsed : Visibility.Visible;

    public event PropertyChangedEventHandler PropertyChanged;

    public BorrowReturnViewModel()
    {
        SqlConnection _dbConnection = DatabaseConnection.Instance.GetConnection();
        _borrowRepository = new BorrowRepository(_dbConnection.ToString());
        _bookRepository = new BookRepository(_dbConnection.ToString()); // Initialize BookRepository
        _studentRepository = new StudentRepository(_dbConnection.ToString());
        SelectedBooks = new ObservableCollection<Book>();
        AddBookCommand = new RelayCommand(AddBookToSelectedList);
        ConfirmActionCommand = new RelayCommand(ExecuteBorrowOrReturn);
        OpenAddBookWindowCommand = new RelayCommand(OpenAddBookWindow);
        OpenAddStudentWindowCommand = new RelayCommand(OpenAddStudentWindow);
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
            OnPropertyChanged(nameof(IsReturningVisible));
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
    public ICommand OpenAddBookWindowCommand { get; }
    public ICommand OpenAddStudentWindowCommand { get; }

    private void AddBookToSelectedList()
    {
        if (!string.IsNullOrEmpty(BookNumber))
        {
            var book = _bookRepository.GetBookByNum(BookNumber); // Use GetBookByNum from BookRepository
            if (book != null)
            {
                SelectedBooks.Add(book);
                BookNumber = string.Empty;
            }
            else
            {
                var result = MessageBox.Show("Book not found. Would you like to add this book?", "Book Not Found", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    OpenAddBookWindow();
                }
            }
        }
    }

    private void ExecuteBorrowOrReturn()
    {
        // Validate student existence only when "Bestätigen" is pressed
        var student = _studentRepository.GetStudentByLibraryCardNum(StudentLibraryCardNum);
        if (student == null)
        {
            var result = MessageBox.Show("Student not found. Would you like to add this student?", "Student Not Found", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var addStudentWindow = new AddStudentWindow();
                addStudentWindow.ShowDialog();
            }
            return;
        }

        if (IsBorrowing)
        {
            foreach (var book in SelectedBooks)
            {
                BorrowBook(book);
            }
            MessageBox.Show("Books borrowed successfully.");
        }
        else if (IsReturning)
        {
            foreach (var book in SelectedBooks)
            {
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

        SelectedBooks.Clear();
        DueDate = null;
    }

    private void BorrowBook(Book book)
    {
        if (string.IsNullOrEmpty(StudentLibraryCardNum))
        {
            MessageBox.Show("Please enter the Student Library Card Number.");
            return;
        }

        _borrowRepository.BorrowBook(StudentLibraryCardNum, book.BookNum, DateTime.Now.AddMonths(3));
        MessageBox.Show($"Borrowed {book.Title} until {DateTime.Now.AddMonths(3):d}.");
    }

    private void ReturnBook(Borrow borrow)
    {
        if (borrow.BorrowID != null)
        {
            _borrowRepository.ReturnBook(borrow.BorrowID);
            MessageBox.Show("Successfully returned the book.");
        }
        else
        {
            MessageBox.Show("Cannot return - no BorrowID found.");
        }
    }

    private void OpenAddBookWindow()
    {
        var addBookWindow = new AddBooksWindow();
        addBookWindow.ShowDialog();
    }

    private void OpenAddStudentWindow()
    {
        var addStudentWindow = new AddStudentWindow();
        addStudentWindow.ShowDialog();
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
