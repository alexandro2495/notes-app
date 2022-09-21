using System;
using Prism.Services.Dialogs;

namespace Notes.Services
{
    public interface IDialogCustomService
    {
        void ShowCustomDialog(string dialog);

        void ShowCustomDialog(string dialog, IDialogParameters parameter);

        void ShowCustomDialog(string dialog, IDialogParameters parameter,Action<IDialogResult> callback);

        void ShowCustomDialog(string dialog, Action<IDialogResult> callback);

        void CloseCustomDialog();
    }
}

