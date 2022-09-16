using System;
using Notes.Pages;
using Notes.Services;
using UIKit;
using Xamarin.Forms;

namespace Notes.iOS.Implementations
{
    public class LoadingServiceIOS : ILoadingService
    {
        private UIView _nativeView;

        private bool _isInitialized;

        public LoadingServiceIOS()
        {
        }

        public void HideLoadingPage()
        {
            // Hide the page
            _nativeView.RemoveFromSuperview();
        }

        public void InitLoading(ContentPage loadingIndicatorContent = null)
        {
            // check if the page parameter is available
            if (loadingIndicatorContent != null)
            {
                // build the loading page with native base
                loadingIndicatorContent.Parent = Xamarin.Forms.Application.Current.MainPage;

                loadingIndicatorContent.Layout(new Rectangle(0, 0,
                    Xamarin.Forms.Application.Current.MainPage.Width,
                    Xamarin.Forms.Application.Current.MainPage.Height));

                var renderer = loadingIndicatorContent.CreateViewController();

                renderer.View.Alpha = .5f;
                _nativeView = renderer.View;

                _isInitialized = true;
            }
        }

        public void ShowLoadingPage(string text = "Loading...")
        {
            // check if the user has set the page or not
            if (!_isInitialized)
                InitLoading(new LoadingPage(text)); // set the default page

            // showing the native loading page
            UIApplication.SharedApplication.KeyWindow.AddSubview(_nativeView);
        }
    }
}
