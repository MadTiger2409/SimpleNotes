using Simple_Notes.Business_Logic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Notes.Business_Logic.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            NotesCollection = new List<NoteModel>();

            for (int i = 0; i < 5; i++)
            {
                NotesCollection.Add(new NoteModel($"Note number {i}", $"Description of the {i}. note."));
            }
        }
        public List<NoteModel> NotesCollection { get; set; }

        private NoteModel _selectedNote;

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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedNote)));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
