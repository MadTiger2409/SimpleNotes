using Simple_Notes.Business_Logic.Models;
using Simple_Notes.Business_Logic.Parsers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Simple_Notes.Business_Logic.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<NoteModel> NotesCollection { get; set; }
        public FileManager FileManager { get; set; }
        private NoteModel _selectedNote;

        public MainViewModel()
        {
            FileManager = new FileManager("notes.csv");
            NotesCollection = GetNotesAsync().GetAwaiter().GetResult();
            //NotesCollection = new List<NoteModel>() { new NoteModel(), new NoteModel() };

            SelectedNote = NotesCollection[0];
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedNote)));
            }
        } 

        private async Task<List<NoteModel>> GetNotesAsync()
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
            var csvLines = await NoteCSVParser.ToCSVLinesAsync(NotesCollection);
            await FileManager.SaveCSVLinesAsync(csvLines);
        }
    }
}
