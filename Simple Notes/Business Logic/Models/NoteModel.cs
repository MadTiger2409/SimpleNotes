using Simple_Notes.Business_Logic.Parsers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Notes.Business_Logic.Models
{
    class NoteModel : INotifyPropertyChanged
    {
        private string _title;
        private string _description;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Title
        {
            get { return _title; }
            set
            {
                if (value == _title)
                {
                    return;
                }

                _title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (value == _description)
                {
                    return;
                }

                _description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
            }
        }

        public NoteModel(string title = "New title", string description = "New description")
        {
            Title = title;
            Description = description;
        }

        public override string ToString()
        {
            return $"{_title}, {_description}";
        }
    }
}
