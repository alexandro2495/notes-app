using System;
using System.Collections.ObjectModel;
using Notes.Data.Models;
using Notes.Services;
using Prism.Mvvm;
using Prism.Navigation;

namespace Notes.ViewModels
{
    public class MainViewModel : BindableBase,  INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly INoteService _noteService;

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private ObservableCollection<Note> _notes;
        public ObservableCollection<Note> Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        public MainViewModel(INavigationService navigationService, INoteService noteService, IUserService userService)
        {
            _navigationService = navigationService;
            _noteService = noteService;
            Title = "Hey there is a binding over here!!";

            Notes = new ObservableCollection<Note>();
        }

        public void GetNotesByUser() {
            Notes = new ObservableCollection<Note>(_noteService.GetNotes(1));
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

