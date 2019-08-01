using System;
using System.Windows.Input;

namespace DataConventer
{
    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Action _action;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> action)
        {
            execute = action;
        }

        public RelayCommand(Action action)
        {
            _action = action;
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            try
            {
                this.execute(parameter);
            }
            catch
            {
                this._action();
            }
        }
    }
}
