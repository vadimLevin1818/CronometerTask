using System.Windows.Input;

namespace CronoTask.UI.Common
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object?>? _execute;
        private readonly Predicate<object?> _canExecute;
        private event EventHandler? CanExecuteChangedInternal;

        public RelayCommand(Action<object?>? execute):this(execute, DefaultCanExecute)
        {
        }

        public RelayCommand(Action<object?>? execute, Predicate<object?> canExecute)
        {
            ArgumentNullException.ThrowIfNull(execute);

            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChangedInternal -= value;
            }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute != null && _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute?.Invoke(parameter);
        }

        public void OnCanExecuteChanged()
        {
            var handler = CanExecuteChangedInternal;
            handler?.Invoke(this, EventArgs.Empty);
        }

        public static bool DefaultCanExecute(object? parameter) => true;
    }
}
