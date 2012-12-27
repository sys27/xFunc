using System;
using System.Windows;
using System.Windows.Input;

namespace xFunc.App.Command
{

    public class RelayCommand : ICommand
    {

        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute) : this(execute, null) { }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        [System.Diagnostics.DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }
        
    }

    public class RelayCommand<T> : ICommand
    {

        private readonly Action<T> execute;
        private readonly Predicate<T> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<T> execute) : this(execute, null) { }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public void Execute(T parameter)
        {
            execute(parameter);
        }

        public bool CanExecute(T parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        void ICommand.Execute(object parameter)
        {
            Execute((T)parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

    }

}