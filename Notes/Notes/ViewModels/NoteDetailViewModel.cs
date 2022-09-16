using System;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.AppCenter.Analytics;
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
        private Location _locaiton;
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
        )
        {
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

        private void OnSaveNoteCommand()
        {
            /*var location = await Geolocation.GetLocationAsync();
            Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}");*/

            if (_note != null)
            {
                _note.Title = Title;
                _note.Content = Content;
                _note.Type = (NoteType)NoteTypeSelected;
                _noteService.Update(_note);
                _note.Latitude = _locaiton.Latitude;
                _note.Longitude = _locaiton.Longitude;

                MessagingCenter.Send(this, Constants.MSGC_UPDATE_NOTE, _note);
            }
            else
            {
                _note = new Note()
                {
                    Title = Title,
                    Content = Content,
                    IdUsuario = _userService.GetLoggedUser().Id,
                    Type = (NoteType)NoteTypeSelected,
                    Latitude = _locaiton.Latitude,
                    Longitude = _locaiton.Longitude
                };
                _noteService.Create(_note);

                var properties = new Dictionary<string, string> {
                    {
                        "Type", NoteTypes[NoteTypeSelected]
                    },
                };
                Analytics.TrackEvent("NoteTypeAdded", properties);

                MessagingCenter.Send(this, Constants.MSGC_NEW_NOTE, _note);
            }
            _navigation.GoBackAsync();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
            Console.WriteLine("OnNavigatedFrom");
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            _locaiton = await Geolocation.GetLocationAsync();
            Console.WriteLine($"Latitude: {_locaiton.Latitude}, Longitude: {_locaiton.Longitude}");

            if (parameters.ContainsKey("note"))
            {
                _note = parameters.GetValue<Note>("note");
                TitlePage = Constants.NoteDetailPageTitle;
                Content = _note.Content;
                Title = _note.Title;
                NoteTypeSelected = (int)_note.Type;

                _locaiton = await Geolocation.GetLocationAsync();
                Console.WriteLine($"Latitude: {_locaiton.Latitude}, Longitude: {_locaiton.Longitude}");

            }
            else
            {
                TitlePage = Constants.NoteAddPageTitle;
            }
        }
    }
}