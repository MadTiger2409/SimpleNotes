using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Notes.Business_Logic.Models;

namespace Simple_Notes.Business_Logic.Parsers
{
    static class NoteCSVParser
    {
        public static async Task<List<NoteModel>> ToNote(List<string> csvLines)
        {
            var notes = new List<NoteModel>();

            if (csvLines != null)
            {
                foreach (var line in csvLines)
                {
                    var splitLine = line.Split(';');

                    notes.Add(new NoteModel(splitLine[0], splitLine[1]));
                }
            }

            return await Task.FromResult(notes);
        }

        public static async Task<List<string>> ToCSVLines(List<NoteModel> notes)
        {
            var csvLines = new List<string>();

            if (notes != null)
            {
                foreach (var note in notes)
                {
                    var csvLine = $"{note.Title.ToString()};{note.Description.ToString()}";

                    csvLines.Add(csvLine);
                }
            }

            return await Task.FromResult(csvLines);
        }
    }
}
