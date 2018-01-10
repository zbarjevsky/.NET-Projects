using System;
using System.Diagnostics.Contracts;
using System.Windows.Input;

namespace ClipboardManager.DesktopUtil
{
    internal class DelegateCommand : ICommand
    {
        private Action<object> _execute;
        private Predicate<object> _canExecute;

        public DelegateCommand(Action<object> onExecuteMethod, Predicate<object> onCanExecuteMethod = null)
        {
            Contract.Requires(onExecuteMethod != null, "Execute method should be specified");

            _execute = onExecuteMethod;
            _canExecute = onCanExecuteMethod ?? ((arg) => true);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
