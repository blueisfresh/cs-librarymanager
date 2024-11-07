using System;
using System.Windows.Input;

namespace LibraryManagement
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _executeWithParam;
        private readonly Action _execute;
        private readonly Predicate<object> _canExecute;

        // Constructor for commands with parameters
        public RelayCommand(Action<object> executeWithParam, Predicate<object> canExecute = null)
        {
            _executeWithParam = executeWithParam ?? throw new ArgumentNullException(nameof(executeWithParam));
            _canExecute = canExecute;
        }

        // Constructor for commands without parameters
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute == null ? (Predicate<object>)null : _ => canExecute();
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (_execute != null)
                _execute();               // Execute parameterless command
            else
                _executeWithParam(parameter); // Execute command with parameter
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
