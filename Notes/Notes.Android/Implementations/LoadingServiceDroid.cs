using System;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Notes.Pages;
using Notes.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Notes.Droid.Implementations
{
    public class LoadingServiceDroid : ILoadingService
    {
        private Android.Views.View _nativeView;

        private Dialog _dialog;

        private bool _isInitialized;

        public LoadingServiceDroid()
        {
           
        }

        public void HideLoadingPage()
        {
            // Hide the page
            _dialog.Hide();
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

                //var renderer = loadingIndicatorContent.GetOrCreateRenderer();
                loadingIndicatorContent.BackgroundColor = Color.Red;

                var renderer = Platform.CreateRendererWithContext(loadingIndicatorContent, Xamarin.Essentials.Platform.CurrentActivity);

                _nativeView = renderer.View;

                _dialog = new Dialog(Xamarin.Essentials.Platform.CurrentActivity);
                _dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
                _dialog.SetCancelable(false);

                _dialog.SetContentView(_nativeView);
                _dialog.SetFeatureDrawableAlpha(1, 0);

                Window window = _dialog.Window;
                window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                window.ClearFlags(WindowManagerFlags.DimBehind);
                window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Aqua));

                _isInitialized = true;
            }
        }

        public void ShowLoadingPage(string text = "Loading...")
        {
            // check if the user has set the page or not
            if (!_isInitialized)
                InitLoading(new LoadingPage(text)); // set the default page

            // showing the native loading page
            _dialog.Show();
        }
    }
}
