using System;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace Notes.Dialogs
{
    public class LoaderDialogViewModel: BindableBase, IDialogAware
    {
        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
        public LoaderDialogViewModel()
        {
        }

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;
        

        public void OnDialogClosed()
        {
          
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>("message");
        }
    }
}

