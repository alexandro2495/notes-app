using System;
using Prism.Mvvm;
using Prism.Navigation;

namespace Notes.ViewModels
{
    public class MapsViewModel : BindableBase, INavigationAware
    {
        public MapsViewModel()
        {
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
           // throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }
    }
}

