using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Notes.Data.Constants;
using Notes.Data.Models;
using Notes.Pages;
using Notes.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class NotesViewModel : BindableBase, INavigationAware
    {
        public ICommand DeleteNoteCommand { get; private set; }
        public ICommand DetailCommand { get; private set; }
        public ICommand NewNoteCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand MultipleSelectionCommand { get; private set; }
        public ICommand LogOutCommand { get; private set; }
        public DelegateCommand DeleteAllCommand { get; private set; }
        public ICommand MapCommand { get; private set; }

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

        private ObservableCollection<object> _notesSelected;
        public ObservableCollection<object> NotesSelected
        {
            get => _notesSelected;
            set => SetProperty(ref _notesSelected, value);
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private readonly INavigationService _navigation;
        private readonly INoteService _noteService;
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authService;
        private readonly IPageDialogService _dialogService;
        private readonly ILoadingService _loadingService;
        private readonly long _userId;

        public NotesViewModel(
            INavigationService navigation,
            INoteService noteService,
            IUserService userService,
            IAuthenticationService authService,
            IPageDialogService dialogService,
            ILoadingService loadingService)
        {
            _navigation = navigation;
            _noteService = noteService;
            _userService = userService;
            _authService = authService;
            _dialogService = dialogService;
            _loadingService = loadingService;
            _userId = _userService.GetLoggedUser().Id;

            Title = Constants.NotePageTitle;


            var notes = _noteService.GetNotes(_userId);
            Notes = new ObservableCollection<Note>(notes);
            NotesSelected = new ObservableCollection<object>();


            RefreshCommand = new Command(async () => await OnRefreshCommand());
            DeleteNoteCommand = new Command<Note>(OnDeleteNoteCommand);
            DetailCommand = new Command<Note>(OnDetailCommand);
            NewNoteCommand = new Command(OnNewNoteCommand);
            MultipleSelectionCommand = new Command(OnMultipleSelectionCommand);
            DeleteAllCommand = new DelegateCommand(OnDeleteAllCommand, CanDeleteItems);
            LogOutCommand = new Command(OnLogOutCommand);
            MapCommand = new Command<Note>(OnMapCommand);

           
            MessagingCenter.Subscribe<NoteDetailViewModel, Note>(this, Constants.MSGC_NEW_NOTE, OnAddNoteCommand);
            MessagingCenter.Subscribe<NoteDetailViewModel, Note>(this, Constants.MSGC_UPDATE_NOTE, OnUpdateNoteCommand);
        }

        private bool CanDeleteItems()
        {
            return NotesSelected.Any();
        }

        private void OnMapCommand(Note note)
        {
            var parms = new NavigationParameters();
            parms.Add("note", note);
            _navigation.NavigateAsync($"{nameof(MapsViewModel)}", parms);

            var properties = new Dictionary<string, string> {
                {
                    "Note_Title", note.Title
                }
            };
            Analytics.TrackEvent("ViewMap", properties);
        }

        private async void OnNewNoteCommand(object obj)
        {
            _loadingService.ShowLoadingPage("loading...");
            //await Task.Delay(TimeSpan.FromSeconds(3));
            //_loadingService.HideLoadingPage();
            await _navigation.NavigateAsync($"{nameof(NoteDetailViewModel)}");
        }

        private async void OnDeleteAllCommand()
        {
            try
            {
                bool confirm = await _dialogService.DisplayAlertAsync(
                    Constants.DIALOG_QUESTION,
                    Constants.DIALOG_DELETE_NOTES,
                    Constants.YES,
                    Constants.NO
                );

                if (confirm)
                {
                    foreach (Note note in NotesSelected)
                    {
                        _noteService.Delete(note);
                        Notes.Remove(note);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                await _dialogService.DisplayAlertAsync(Constants.ERRMSG_DELETE_NOTE, Constants.ERRMSG_DELETE_NOTE_DESC, Constants.OK);
            }
        }

        private async void OnLogOutCommand(object obj)
        {
            try
            {
                bool confirm = await _dialogService.DisplayAlertAsync(
                    Constants.DIALOG_QUESTION,
                    Constants.DIALOG_AUTHENTICATION_LOGOUT,
                    Constants.YES,
                    Constants.NO
                );

                if (confirm)
                {
                    _authService.SignOut(_userId.ToString());
                    await _navigation.NavigateAsync($"/NavigationPage/{nameof(LoginViewModel)}");
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                await _dialogService.DisplayAlertAsync(Constants.ERRMSG_AUTHENTICATION_LOGOUT, Constants.ERRMSG_AUTHENTICATION_LOGOUT_DESC, Constants.OK);
            }
        }

        private void OnMultipleSelectionCommand(object obj)
        {

            Console.WriteLine(NotesSelected.Count);

            DeleteAllCommand?.RaiseCanExecuteChanged();
        }

        private void OnDetailCommand(Note note = null)
        {
            //var page = new NoteDetailPage(note, _noteService, _userService);
            //_navigation.PushAsync(page, true);
            var parms = new NavigationParameters();
            parms.Add("note", note);
            _navigation.NavigateAsync($"{nameof(NoteDetailViewModel)}", parms);
        }

        private async void OnDeleteNoteCommand(Note note)
        {
            try
            {
                bool confirm = await _dialogService.DisplayAlertAsync(
                    Constants.DIALOG_QUESTION,
                    Constants.DIALOG_DELETE_NOTE + note.Title,
                    Constants.YES,
                    Constants.NO
                );

                if (confirm)
                {
                    _noteService.Delete(note);
                    Notes.Remove(note);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                await _dialogService.DisplayAlertAsync(Constants.ERRMSG_DELETE_NOTE, Constants.ERRMSG_DELETE_NOTE_DESC, Constants.OK);
            }
            
        }

        private void OnAddNoteCommand(NoteDetailViewModel nodeDetail, Note note)
        {
            Notes.Add(note);
        }

        private void OnUpdateNoteCommand(NoteDetailViewModel nodeDetail, Note note)
        {
            var oldNote = Notes.First(n => n.Id == note.Id);
            int notePosition = Notes.IndexOf(oldNote);
            Notes[notePosition] = note;
        }

        async Task OnRefreshCommand()
        {
            IsRefreshing = true;
            await Task.Delay(TimeSpan.FromSeconds(2));
            var notes = _noteService.GetNotes(_userId);
            Notes = new ObservableCollection<Note>(notes);
            IsRefreshing = false;
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

