using System;
using Xamarin.Forms;

namespace Notes.Services
{
    public interface ILoadingService
    {
        void InitLoading(ContentPage loadingIndicatorContent = null);

        void ShowLoadingPage(string text = "Loading...");

        void HideLoadingPage();
    }
}
