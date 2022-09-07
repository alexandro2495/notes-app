using System;
using Prism.Mvvm;
using Prism.Navigation;

namespace Notes.ViewModels
{
    public class MainViewModel : BindableBase,  INavigationAware
    {
        private readonly INavigationService _navigationService;

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Title = "Hey there is a binding over here!!";
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            Console.WriteLine("OnNavigatedFrom - MainViewModel");
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Console.WriteLine("OnNavigatedTo - MainViewModel");
        }
    }
}

