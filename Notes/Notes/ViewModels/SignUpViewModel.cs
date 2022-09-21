using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Notes.Data.Constants;
using Notes.Data.Models;
using Notes.Services;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class SignUpViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigation;
        private readonly IUserService _userService;
        private readonly IAppConfigurationService _appConfiguration;
        private readonly IPageDialogService _dialogService;

        public ICommand SingUpCommand { get; private set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public SignUpViewModel(
            INavigationService navigation,
            IUserService userService,
            IAppConfigurationService appConfiguration,
            IPageDialogService dialogService)
        {
            _navigation = navigation;
            _userService = userService;
            _dialogService = dialogService;
            _appConfiguration = appConfiguration;

            Title = Constants.SignUpPageTitle;

            SingUpCommand = new Command(OnSignUp);
        }

        private void OnSignUp()
        {
            try
            {
                AppConfiguration config = new AppConfiguration()
                {
                    PrimaryColor = Constants.PrimaryColor,
                    DarkColor = Constants.DarkColor,
                    AccentColor = Constants.AccentColor,
                    PageBackgroundColor = Constants.PageBackgroundColor,
                    FontFamily = Constants.FontFamily,
                    Language = Constants.Language
                };

                _appConfiguration.Create(config);

                User user = new User()
                {
                    Name = Name,
                    LastName = LastName,
                    UserName = UserName,
                    Password = Password,
                    Email = Email,
                    IdAppConfiguration = config.Id
                };

                var properties = new Dictionary<string, string> {
                    {
                        "Name", Name + LastName
                    },
                    {
                        "Email", Email
                    },
                };
                Analytics.TrackEvent("CreatedUser", properties);


                _userService.Save(user);
                var parms = new NavigationParameters();
                parms.Add("user", user);
                _navigation.GoBackAsync(parms);
            }
            catch (Exception e)
            {
                _dialogService.DisplayAlertAsync(Constants.ERRMSG_AUTHENTICATION_SIGN_UP, e.Message, Constants.OK);
                Crashes.TrackError(e);
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }
    }
}

