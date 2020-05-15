using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DashCamGPSView.Tools
{
    public class RelayCommand : ICommand
    {
        private Action<object> _exec;
        private Func<object, bool> _canExec;

        public RelayCommand(Action<object> exec, Func<object, bool> canExec = null)
        {
            _exec = exec;
            _canExec = canExec;
        }

        public void Execute(object parameter)
        {
            _exec(parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (_canExec == null)
                return true;
            return _canExec(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
