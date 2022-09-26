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
using SQLite;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class SignUpViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigation;
        private readonly IUserService _userService;
        private readonly IAppConfigurationService _appConfiguration;
        private readonly IPageDialogService _dialogService;
        private readonly IAnalyticService _analyticService;
        private readonly ICrashReposrtService _crashReposrtService;

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
            IPageDialogService dialogService,
            IAnalyticService analyticService,
            ICrashReposrtService crashReposrtService)
        {
            _navigation = navigation;
            _userService = userService;
            _dialogService = dialogService;
            _appConfiguration = appConfiguration;
            _crashReposrtService = crashReposrtService;
            _analyticService = analyticService;

            Title = Constants.SignUpPageTitle;

            SingUpCommand = new Command(OnSignUp);
        }

        public AppConfiguration ValidateCreateAppConfiguration()
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

                return config;
            }catch(Exception e)
            {
                _crashReposrtService.TrackError(e);
                return null;
            }
        }

        public User ValidateCreateUser(long confId)
        {
            try
            {
                User user = new User()
                {
                    Name = Name,
                    LastName = LastName,
                    UserName = UserName,
                    Password = Password,
                    Email = Email,
                    IdAppConfiguration = confId
                };

                _userService.Save(user);

                return user;
            }
            catch (SQLiteException e)
            {
                if (e.Message == "UNIQUE constraint failed: User.UserName")
                {
                    _dialogService.DisplayAlertAsync(
                        Constants.ERRMSG_AUTHENTICATION_SIGN_UP,
                        Constants.ERRMSG_AUTHENTICATION_SIGN_UP_DUPLICATE_USERNAME,
                        Constants.OK);
                } else
                {
                    return null;
                }
                return new User();
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public void OnSignUp()
        {
            try
            {

                var config = ValidateCreateAppConfiguration();

                if (config == null)
                {
                    throw new Exception(Constants.ERRMSG_SINGUP_APPCONFIG_DESC);
                }

                var user = ValidateCreateUser(config.Id);

                if (user == null)
                {
                    throw new Exception(Constants.ERRMSG_SINGUP_USER_DESC);
                }

                if (config != null && user != null)
                {
                    var properties = new Dictionary<string, string> {
                        {
                            "Name", Name + LastName
                        },
                        {
                            "Email", Email
                        },
                    };

                    _analyticService.CreatedUserEvent(properties);

                    var parms = new NavigationParameters();
                    parms.Add("user", user);

                    _navigation.GoBackAsync(parms);
                }
                
            }
            catch (Exception e)
            {
                _dialogService.DisplayAlertAsync(
                    Constants.ERRMSG_AUTHENTICATION_SIGN_UP,
                    Constants.ERRMSG_AUTHENTICATION_SIGN_UP_DESC,
                    Constants.OK);
                _crashReposrtService.TrackError(e);
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

