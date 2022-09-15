using System;
using System.Collections.Generic;
using System.Windows.Input;
using Notes.Data.Constants;
using Notes.Data.Models;
using Notes.Services;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class NoteDetailViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigation;
        private INoteService _noteService;
        private IUserService _userService;
        

        public ICommand SaveNoteCommand { get; private set; }

        private Note _note = null;

        private string _titlePage;
        public string TitlePage
        {
            get => _titlePage;
            set => SetProperty(ref _titlePage, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _content;
        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        private int _noteTypeSelected;
        public int NoteTypeSelected
        {
            get => _noteTypeSelected;
            set => SetProperty(ref _noteTypeSelected, value);
        }

        private List<string> _noteTypes;
        public List<string> NoteTypes
        {
            get => _noteTypes;
            set => SetProperty(ref _noteTypes, value);
        }

        public NoteDetailViewModel(
            INavigationService navigation,
            INoteService noteService,
            IUserService userService
        ){
            _navigation = navigation;
            _noteService = noteService;
            _userService = userService;

            NoteTypes = new List<string>() {
                "None",
                "Picture",
                "Music",
                "Text"
            };

            SaveNoteCommand = new Command(OnSaveNoteCommand);
        }

        private async void OnSaveNoteCommand()
        {
            var location = await Geolocation.GetLocationAsync();
            Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}");
           
            if (_note != null)
            {
                _note.Title = Title;
                _note.Content = Content;
                _note.Type = (NoteType)NoteTypeSelected;
                _noteService.Update(_note);
                _note.Latitude = location.Latitude;
                _note.Longitude = location.Longitude;
                
                MessagingCenter.Send(this, Constants.MSGC_UPDATE_NOTE, _note);
                _navigation.GoBackAsync();
            }
            else
            {
                _note = new Note()
                {
                    Title = Title,
                    Content = Content,
                    IdUsuario = _userService.GetLoggedUser().Id,
                    Type = (NoteType)NoteTypeSelected,
                    Latitude = location.Latitude,
                    Longitude = location.Longitude
                };
                _noteService.Create(_note);
                

                MessagingCenter.Send(this, Constants.MSGC_NEW_NOTE, _note);
                _navigation.GoBackAsync();
            }

        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
            Console.WriteLine("OnNavigatedFrom");
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
            Console.WriteLine("OnNavigatedTo");
            if (parameters.ContainsKey("note"))
            {
                _note = parameters.GetValue<Note>("note");
                TitlePage = Constants.NoteDetailPageTitle;
                Content = _note.Content;
                Title = _note.Title;
                NoteTypeSelected = (int)_note.Type;
                
            } else
            {
                TitlePage = Constants.NoteAddPageTitle;
            }
        }
    }
}

