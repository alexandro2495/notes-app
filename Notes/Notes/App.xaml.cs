using System;
using Notes.ViewModels;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Notes.Pages;
using Xamarin.Forms;

namespace Notes
{
    public partial class App : PrismApplication
    {
        public App() : this(null)
        {

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
            containerRegistry.RegisterForNavigation<NoteDetailPage, MainViewModel>(nameof(MainViewModel));
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync($"NavigationPage/{nameof(MainViewModel)}");
        }
    }
}

