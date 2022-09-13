using System;
using System.Windows.Input;
using Notes.Data.Constants;
using Notes.Services;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class LoginViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigation;
        private readonly IAuthenticationService _authService;
        private readonly IUserService _userService;
        private readonly IAppConfigurationService _appConfigurationService;
        //Func<string, string, string, Task> _displayAlert;

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
            IAppConfigurationService appConfigurationService)
        {
            _navigation = navigation;
            _userService = userService;
            _authService = authService;
            _appConfigurationService = appConfigurationService;
            //_displayAlert = displayAlert;

            Title = Constants.LoginPageTitle;
            SignUpCommand = new Command(OnSignUpCommand);
            LoginCommand = new Command(OnLoginCommand);

        }

        private void OnLoginCommand(object obj)
        {
            try
            {
                _authService.SignIn(UserName, Password);
                _navigation.NavigateAsync($"/NavigationPage/{nameof(NotesViewModel)}");
                //_navigation.NavigateAsync(new System.Uri("/NavigationPage/NotesViewModel", System.UriKind.Absolute));
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
                //_displayAlert(Constants.ERRMSG_AUTHENTICATION, e.Message, Constants.OK);
            }

            //_navigation.NavigateAsync($"NavigationPage/{nameof(NotesViewModel)}");
        }

        private void OnSignUpCommand(object obj)
        {
            _navigation.NavigateAsync($"{nameof(SignUpViewModel)}");
            //  NavigationPage/Login/signUp
            //_navigation.PushAsync(new SignUpPage(_userService, _authService, _appConfigurationService));
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

