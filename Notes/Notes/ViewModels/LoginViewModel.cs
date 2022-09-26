using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AppCenter.Crashes;
using Notes.Data.Constants;
using Notes.Data.Models;
using Notes.Services;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class LoginViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigation;
        private readonly IAuthenticationService _authService;
        private readonly IUserService _userService;
        private readonly IAppConfigurationService _appConfigurationService;
        private readonly IPageDialogService _dialogService;
        private readonly ICrashReposrtService _crashReposrtService;

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _username;
        public string UserName
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand SignUpCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }

        public LoginViewModel(
            INavigationService navigation,
            IUserService userService,
            IAuthenticationService authService,
            IAppConfigurationService appConfigurationService,
            IPageDialogService dialogService,
            ICrashReposrtService crashReposrtService
            )
        {
            _navigation = navigation;
            _userService = userService;
            _authService = authService;
            _appConfigurationService = appConfigurationService;
            _dialogService = dialogService;
            _crashReposrtService = crashReposrtService;

            Title = Constants.LoginPageTitle;
            SignUpCommand = new Command(OnSignUpCommand);
            LoginCommand = new Command(async () => await OnLoginCommand());

        }

        public bool ValidateAuthentication()
        {
            return _authService.SignIn(UserName, Password);
        }


        public async Task OnLoginCommand()
        {
            try
            {
                if (ValidateAuthentication())
                {
                    await _navigation.NavigateAsync($"/NavigationPage/{nameof(NotesViewModel)}");
                }
                else
                {
                    await _dialogService.DisplayAlertAsync(
                        Constants.ERRMSG_AUTHENTICATION_SIGN_IN,
                        Constants.ERRMSG_AUTHENTICATION_SIGN_IN_DESC,
                        Constants.OK);
                }
                
            }
            catch (Exception ex)
            {
                _crashReposrtService.TrackError(ex);
                await _dialogService.DisplayAlertAsync(Constants.ERRMSG_AUTHENTICATION_SIGN_IN, ex.Message, Constants.OK);
            }
        }

        private void OnSignUpCommand()
        {
            _navigation.NavigateAsync($"{nameof(SignUpViewModel)}");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("user"))
            {
                var _username = parameters.GetValue<User>("user");
                UserName = _username.UserName;
                Password = _username.Password;
            }
        }
    }
}

