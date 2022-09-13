using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Notes.Data.Constants;
using Notes.Data.Models;
using Notes.Pages;
using Notes.Services;
using Prism.Mvvm;
using Prism.Navigation;
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
        public ICommand DeleteAllCommand { get; private set; }

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
        //private Func<string, string, string, Task> _displayAlert;
        private readonly long _userId;

        public NotesViewModel(
            INavigationService navigation,
            INoteService noteService,
            IUserService userService,
            IAuthenticationService authService)
        {
            _navigation = navigation;
            _noteService = noteService;
            _userService = userService;
            _authService = authService;
            //_displayAlert = displayAlert;
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
            DeleteAllCommand = new Command(OnDeleteAllCommand);
            LogOutCommand = new Command(OnLogOutCommand);

            MessagingCenter.Subscribe<NoteDetailViewModel, Note>(this, Constants.MSGC_NEW_NOTE, OnAddNoteCommand);
            MessagingCenter.Subscribe<NoteDetailViewModel, Note>(this, Constants.MSGC_UPDATE_NOTE, OnUpdateNoteCommand);
        }

        private void OnNewNoteCommand(object obj)
        {
            _navigation.NavigateAsync($"{nameof(NoteDetailViewModel)}");
        }

        private void OnDeleteAllCommand(object obj)
        {
            foreach (Note note in NotesSelected)
            {
                _noteService.Delete(note);
                Notes.Remove(note);
            }
        }

        private void OnLogOutCommand(object obj)
        {
            try
            {
                _authService.SignOut(_userId.ToString());
                _navigation.NavigateAsync($"/NavigationPage/{nameof(LoginViewModel)}");
            }
            catch (Exception e)
            {
                //_displayAlert(Constants.ERRMSG_AUTHENTICATION_LOGOUT, e.Message, Constants.OK);
            }

        }

        private void OnMultipleSelectionCommand(object obj)
        {
            Console.WriteLine(NotesSelected.Count);
        }

        private void OnDetailCommand(Note note = null)
        {
            //var page = new NoteDetailPage(note, _noteService, _userService);
            //_navigation.PushAsync(page, true);
            var parms = new NavigationParameters();
            parms.Add("note", note);
            _navigation.NavigateAsync($"{nameof(NoteDetailViewModel)}", parms);
        }

        private void OnDeleteNoteCommand(Note note)
        {
            _noteService.Delete(note);
            Notes.Remove(note);
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

