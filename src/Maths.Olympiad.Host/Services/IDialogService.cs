using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths.Olympiad.Host.Services
{
    public interface IDialogService
    {
        void ShowDialog(object viewModel);
    }

    public class DialogService : IDialogService
    {
        public event EventHandler<GenericEventArgs<object>> DisplayDialog; 
        public void ShowDialog(object viewModel)
        {
            DisplayDialog?.Invoke(this, new GenericEventArgs<object>(viewModel));
        }
    }

    public class GenericEventArgs<T> : EventArgs
    {
        public T Data { get; }

        public GenericEventArgs(T data)
        {
            Data = data;
        }
    }
}
