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

        }

        public NoteModel SelectedNote { get; set; }
        public IEnumerable<NoteModel> NotesCollection { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
