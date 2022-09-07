using System;
<<<<<<< HEAD
using Notes.ViewModels;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
=======
using Notes.Pages;
>>>>>>> 88f4301 (create login page, added icons, and renderers)
using Xamarin.Forms;

namespace Notes
{
    public partial class App : PrismApplication
    {
        public App() : this(null)
        {

<<<<<<< HEAD
=======
            MainPage = new NavigationPage(new LoginPage());
>>>>>>> 88f4301 (create login page, added icons, and renderers)
        }

        public App(IPlatformInitializer initializer)
            : this(initializer, true)
        {

        }

        public App(IPlatformInitializer initializer, bool setFormsDependencyResolver)
            : base(initializer, setFormsDependencyResolver)
        {
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>(nameof(MainViewModel));
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync($"NavigationPage/{nameof(MainViewModel)}");
        }
    }
}

