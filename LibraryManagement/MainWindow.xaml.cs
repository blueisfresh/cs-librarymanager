using System.Windows;
using LibraryManagement.ViewModels;

namespace LibraryManagement
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(MainFrame); // Pass the Frame for navigation
        }
    }
}
