using System.Windows.Input;

namespace MusicApp.Chat.MVVM.Core
{
    internal class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        // This event is raised whenever the ability of the command to execute has changed
        public event EventHandler? CanExecuteChanged
        {
            // Add an event to the current version.
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public RelayCommand(Action<object> execute, Func<object, bool>? canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        // Determines if you can execute a command in the current state of the program
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
