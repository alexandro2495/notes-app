using System;
using Notes.ViewModels;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Notes.Pages;
using Xamarin.Forms;
using Notes.Services;
using Notes.Services.Implementations.SqliteImp;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Notes.Dialogs;
using Notes.Services.Implementations;
using System.Collections.Generic;
using Xamarin.Essentials;
using Notes.Data.Constants;
using Notes.Themes;

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
            containerRegistry.Register<IDialogCustomService, DialogCustomService>();
            containerRegistry.Register<IAnalyticService, AppCenterAnalyticService>();
            containerRegistry.Register<ICrashReposrtService, AppCenterCrashReportService>();

            //Dialogs
            containerRegistry.RegisterDialog<LoaderDialog, LoaderDialogViewModel>(nameof(LoaderDialogViewModel));
        }

        protected override void OnInitialized()
        {
            try
            {
                InitializeComponent();
                //theme app changes
                InitializeAppTheme(Application.Current.RequestedTheme);
                Application.Current.RequestedThemeChanged += OnThemeChanged;

                //init App Center
                AppCenter.Start("android=d7eb73c3-5ba2-446e-a671-a9265dec2e22;" +
                      "ios={Your iOS App secret here};",
                      typeof(Analytics), typeof(Crashes));


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

            } catch(Exception e)
            {
                Crashes.TrackError(e);
            }


        }

        private void OnThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            InitializeAppTheme(e.RequestedTheme);
        }


        private void InitializeAppTheme(OSAppTheme? oSAppTheme= null)
        {
            if(oSAppTheme== null)
            {
                if (Preferences.Get(Constants.DarkModeKey, false))
                {
                    oSAppTheme = OSAppTheme.Dark;
                }
                else
                {
                    oSAppTheme = OSAppTheme.Light;
                }
            }

            ICollection<ResourceDictionary> mergedDictionaries =
            Application.Current.Resources.MergedDictionaries;

            if(mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
            }
            if(oSAppTheme == OSAppTheme.Dark)
            {
                mergedDictionaries.Add(new DarkTheme());
            }
            else
            {
                mergedDictionaries.Add(new LightTheme());
            }
        }
    }
}

