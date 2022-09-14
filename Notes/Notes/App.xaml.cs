using System;
using Notes.ViewModels;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Notes.Pages;
using Xamarin.Forms;
using Notes.Services;
using Notes.Services.Implementations.SqliteImp;

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
            //pages
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>(nameof(LoginViewModel));
            containerRegistry.RegisterForNavigation<ProfilePage, SignUpViewModel>(nameof(SignUpViewModel));

            containerRegistry.RegisterForNavigation<NotesPage, NotesViewModel>(nameof(NotesViewModel));
            containerRegistry.RegisterForNavigation<NoteDetailPage, NoteDetailViewModel>(nameof(NoteDetailViewModel));
            containerRegistry.RegisterForNavigation<MapsPage, MapsViewModel>(nameof(MapsViewModel));
            //services
            containerRegistry.Register<INoteService, NoteSQLiteService>();
            containerRegistry.Register<IUserService, UserSQLiteService>();
            containerRegistry.Register<IAuthenticationService, AuthSQLiteService>();
            containerRegistry.Register<IAppConfigurationService, AppConfigSQLiteService>();

        }

        protected override void OnInitialized()
        {
            InitializeComponent();
            IUserService _userService = Container.Resolve<IUserService>();
            var user = _userService.GetLoggedUser();
            if (user != null && user != default)
            {
                NavigationService.NavigateAsync($"NavigationPage/{nameof(NotesViewModel)}");
            }
            else
            {
                NavigationService.NavigateAsync($"NavigationPage/{nameof(LoginViewModel)}");
            }
            
        }
    }
}

