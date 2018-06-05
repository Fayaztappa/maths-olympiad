using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Maths.Olympiad.Host.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged , IDisposable 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Dispose()
        {
            
        }
    }

    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Predicate<object> _canExecute;

        public DelegateCommand(Action<object> action, Predicate<object> canExecute = null)
        {
            _action = action;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute != null ? _canExecute(parameter) : true;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }
}
