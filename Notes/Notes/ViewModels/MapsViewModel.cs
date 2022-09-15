using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Notes.Data.Constants;
using Notes.Data.Models;
using Notes.Services;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Notes.ViewModels
{
    public class MapsViewModel : BindableBase, INavigationAware
    {
        private Note _note = null;
        public Map _map { get; private set; }
        private readonly INoteService _noteService;
        private readonly IUserService _userService;
        //readonly ObservableCollection<Position> _positions;

        //public IEnumerable positions => _positions;

        public MapsViewModel(INoteService noteService,IUserService userService)
        {
            _map = new Map();
            _noteService = noteService;
            _userService = userService;
           
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
           // throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
            if (parameters.ContainsKey("note"))
            {
                _note = parameters.GetValue<Note>("note");
                var noteselectedpos = new Position(_note.Latitude, _note.Longitude);
                long userId = _userService.GetLoggedUser().Id;
                List<Note> notes = _noteService.GetNotes(userId);
                foreach (Note note in notes)
                {
                    var position = new Position(note.Latitude, note.Longitude);
                    var myPin = new Pin()
                    {
                        Position = position,
                        Label = note.Title,
                        
                    };
                    
                    _map.Pins.Add(myPin);
                }
               
                MapSpan mapSpan = MapSpan.FromCenterAndRadius(noteselectedpos, Distance.FromKilometers(0.5));
                _map.MoveToRegion(mapSpan);
            }
        }
    }
}

