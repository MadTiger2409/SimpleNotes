using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Simple_Notes.Business_Logic
{
    class FileManager
    {
        private StorageFolder AppFolder { get; set; }
        private StorageFile File { get; set; }
        private string FullFileName { get; set; }

        public FileManager(string fullFileName)
        {
            FullFileName = fullFileName;
            AppFolder = ApplicationData.Current.LocalFolder;
        }

        public async Task<IEnumerable<string>> ReadCSVLines()
        {
            IList<string> csvLines = new List<string>();

            try
            {
                File = await AppFolder.GetFileAsync(FullFileName);
                csvLines = await FileIO.ReadLinesAsync(File);

                return await Task.FromResult(csvLines.AsEnumerable());
            }
            catch (Exception)
            {
                await AppFolder.CreateFileAsync(FullFileName);

                return await Task.FromResult(csvLines.AsEnumerable());
            }
        }

        public async Task SaveCSVLines(IEnumerable<string> csvLines)
        {
            try
            {
                File = await AppFolder.GetFileAsync(FullFileName);
                await FileIO.WriteLinesAsync(File, csvLines);
            }
            catch (Exception)
            {
                await AppFolder.CreateFileAsync(FullFileName);
            }
        }
    }
}
