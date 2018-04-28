using Simple_Notes.Business_Logic.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Notes.Business_Logic.Models
{
    class NoteModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public NoteModel(string title = "New title", string description = "New description")
        {
            Title = title;
            Description = description;
        }
    }
}
