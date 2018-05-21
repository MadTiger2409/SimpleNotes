using Simple_Notes.Business_Logic.Commands;
using Simple_Notes.Business_Logic.Models;
using Simple_Notes.Business_Logic.Parsers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;

namespace Simple_Notes.Business_Logic.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        #region Members
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<NoteModel> _notesCollection;
        private NoteModel _selectedNote;
        private bool _isTextBoxEnabled;

        #endregion

        public MainViewModel()
        {
            AddCommand = new RelayCommand(async () => await AddNoteAsync());
            SaveCommand = new RelayCommand(async () => await SaveNotesAsync());
            RemoveCommand = new RelayCommand(async () => await RemoveNoteAsync(), CanRemoveNote);
            FileManager = new FileManager("notes.csv");

            if (DesignMode.DesignModeEnabled == true)
            {
                NotesCollection = new ObservableCollection<NoteModel>() { new NoteModel("A"), new NoteModel("B"), new NoteModel("C") };
            }
            else
            {
                NotesCollection = GetNotesAsync().GetAwaiter().GetResult();
            }

            SelectedNote = NotesCollection.FirstOrDefault();
        }

        #region Properties

        public FileManager FileManager { get; set; }
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand RemoveCommand { get; private set; }

        public ObservableCollection<NoteModel> NotesCollection
        {
            get { return _notesCollection; }
            set
            {
                _notesCollection = value;
                RemoveCommand.RaiseCanExecuteChanged();
            }
        }

        public NoteModel SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                if (value == _selectedNote)
                {
                    return;
                }

                _selectedNote = value;
                RemoveCommand.RaiseCanExecuteChanged();
                IsTextBoxEnabled = CanRemoveNote();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedNote)));
            }
        }

        public bool IsTextBoxEnabled
        {
            get { return _isTextBoxEnabled; }
            set
            {
                _isTextBoxEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsTextBoxEnabled)));
            }
        }

        #endregion

        #region Methods

        private async Task<ObservableCollection<NoteModel>> GetNotesAsync()
            => await NoteCSVParser.ToNotesAsync(await FileManager.ReadCSVLinesAsync());

        public async Task SaveNotesAsync()
        {
            var csvLines = await NoteCSVParser.ToCSVLinesAsync(_notesCollection);
            await FileManager.SaveCSVLinesAsync(csvLines);
        }

        public async Task RemoveNoteAsync()
        {
            if (SelectedNote == null || NotesCollection.Count <= 0)
            {
                return;
            }

            await Task.FromResult(NotesCollection.Remove(SelectedNote));
            SelectedNote = NotesCollection.LastOrDefault();
        }

        public async Task AddNoteAsync()
        {
            NotesCollection.Add(new NoteModel());
            await Task.FromResult(SelectedNote = NotesCollection.LastOrDefault());
        }

        public bool CanRemoveNote()
        {
            return SelectedNote != null;
        }

        #endregion
    }
}
