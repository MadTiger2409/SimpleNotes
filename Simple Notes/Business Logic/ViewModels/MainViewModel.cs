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

        public NoteModel SelectedNote { get; set; }
        public List<NoteModel> NotesCollection { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
