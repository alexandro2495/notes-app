using System;
using System.Linq;
using Prism.Common;
using Prism.Services.Dialogs;
using Xamarin.Forms;

namespace Notes.Services.Implementations
{
    public class DialogCustomService : IDialogCustomService
    {
        private IApplicationProvider _applicationProvider { get; }
        private IDialogService _dialogService { get; }
        public DialogCustomService(IApplicationProvider applicationProvider, IDialogService dialogService)
        {
            _applicationProvider = applicationProvider;
            _dialogService = dialogService;
        }

        public void CloseCustomDialog()
        {
            ContentPage modal = GetCurrentContentPage();
            modal.Navigation.PopModalAsync(true);
        }

        public void ShowCustomDialog(string dialog)
        {
            _dialogService.ShowDialog(dialog);
        }

        public void ShowCustomDialog(string dialog, IDialogParameters parameter)
        {
            _dialogService.ShowDialog(dialog, parameter);
        }

        public void ShowCustomDialog(string dialog, IDialogParameters parameter, Action<IDialogResult> callback)
        {
            _dialogService.ShowDialog(dialog,parameter,callback);
        }

        public void ShowCustomDialog(string dialog, Action<IDialogResult> callback)
        {
            _dialogService.ShowDialog(dialog, callback);
        }

    
        private ContentPage GetCurrentContentPage()
        {
            var cp = GetCurrentPage();
            var mp = TryGetModalPage(cp);
            return mp ?? cp;
        }

        private ContentPage TryGetModalPage(ContentPage cp)
        {
            var mp = cp.Navigation.ModalStack.LastOrDefault();
            if (mp != null)
            {
                return GetCurrentPage(mp);
            }

            return null;
        }

        private ContentPage GetCurrentPage(Page page = null)
        {
            switch (page)
            {
                case ContentPage cp:
                    return cp;
                case TabbedPage tp:
                    return GetCurrentPage(tp.CurrentPage);
                case NavigationPage np:
                    return GetCurrentPage(np.CurrentPage);
                case CarouselPage carouselPage:
                    return GetCurrentPage(carouselPage.CurrentPage);
                case FlyoutPage flyout:
                    flyout.IsPresented = false;
                    return GetCurrentPage(flyout.Detail);
                case Shell shell:
                    return GetCurrentPage((shell.CurrentItem.CurrentItem as IShellSectionController).PresentedPage);
                default:
                    // If we get some random Page Type
                    if (page != null)
                    {
                        Xamarin.Forms.Internals.Log.Warning("Warning", $"An Unknown Page type {page.GetType()} was found walk walking the Navigation Stack. This is not supported by the DialogService");
                        return null;
                    }

                    var mainPage = _applicationProvider.MainPage;
                    if (mainPage is null)
                    {
                        return null;
                    }

                    return GetCurrentPage(mainPage);
            }
        }
    }
}

