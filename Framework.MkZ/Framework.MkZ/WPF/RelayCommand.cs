﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MkZ.WPF
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        #region C'tor

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            BoundControls = new List<WeakReference>();
            _canExecute = canExecute;
            _execute = execute;
        }

        private List<WeakReference> BoundControls;

        private void RaiseCanExecuteChanged()
        {
            var act = new Action(() =>
            {
                if (BoundControls != null && BoundControls.Count > 0)
                {
                    List<WeakReference> garbagedObjects = new List<WeakReference>();
                    BoundControls.ForEach(ce =>
                    {
                        object target = ce.Target;
                        if (target != null)
                            ((EventHandler)(target)).Invoke(null, EventArgs.Empty);
                        else
                        {
                            garbagedObjects.Add(ce);
                        }
                    });

                    garbagedObjects.ForEach(x => BoundControls.Remove(x));
                    garbagedObjects.Clear();
                }

            });
            WPFUtils.ExecuteOnUIThread(act);
        }

        public void RefreshBoundControls()
        {
            RaiseCanExecuteChanged();
        }


        #endregion

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                BoundControls.Add(new WeakReference(value));
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                BoundControls.Remove(BoundControls.Find(r => ((EventHandler)r.Target) == value));
            }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

    }
}
