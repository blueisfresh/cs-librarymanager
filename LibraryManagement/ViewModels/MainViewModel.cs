using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManagement.ViewModels
{
    public class MainViewModel
    {
        private Frame _mainFrame;

        public MainViewModel(Frame mainFrame)
        {
            _mainFrame = mainFrame;
            NavigateCommand = new RelayCommand(Navigate);
        }

        public ICommand NavigateCommand { get; }

        private void Navigate(object parameter)
        {
            string pageName = parameter as string;
            if (string.IsNullOrEmpty(pageName))
                return;

            Uri pageUri = new Uri($"/Views/{pageName}.xaml", UriKind.Relative);
            _mainFrame.Navigate(pageUri);
        }
    }
}
