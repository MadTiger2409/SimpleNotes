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

namespace Simple_Notes.Business_Logic.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        #region Members
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<NoteModel> _notesCollection { get; set; }
        public FileManager FileManager { get; set; }
        private NoteModel _selectedNote;

        #endregion

        public MainViewModel()
        {
            AddCommand = new RelayCommand(async () => await AddNoteAsync());
            SaveCommand = new RelayCommand(async () => await SaveNotesAsync());
            RemoveCommand = new RelayCommand(async () => await RemoveNoteAsync(), CanRemoveNote);

            FileManager = new FileManager("notes.csv");
            NotesCollection = GetNotesAsync().GetAwaiter().GetResult();
            //_notesCollection = new ObservableCollection<NoteModel>() { new NoteModel("A"), new NoteModel("B"), new NoteModel("C") };
            SelectedNote = _notesCollection[0];
        }

        #region Properties

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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedNote)));
            }
        }

        #endregion

        #region Methods

        private async Task<ObservableCollection<NoteModel>> GetNotesAsync()
        {
            var notes = await NoteCSVParser.ToNotesAsync(await FileManager.ReadCSVLinesAsync());

            if (notes.Count < 1)
            {
                notes.Add(new NoteModel());
            }

            return notes;
        }

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
            SelectedNote = NotesCollection.FirstOrDefault();
        }

        public async Task AddNoteAsync()
        {
            NotesCollection.Add(new NoteModel());
            await Task.FromResult(SelectedNote = NotesCollection.Last());
        }

        public bool CanRemoveNote()
        {
            return SelectedNote != null;
        }

        #endregion
    }
}
