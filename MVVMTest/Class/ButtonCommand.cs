using System;
using System.Windows.Input;

namespace DataConventer
{
    class ButtonCommand : ICommand
    {
        private Action _action;
        public ButtonCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public event EventHandler CanExecuteChanged;
    }
}
