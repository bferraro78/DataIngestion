using Microsoft.Extensions.Configuration;
using MusicData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicData.Services.DataReader.DataHandlers
{
    public class ArtistDataHandler : DataHandler
    {
        public override void Handle(ref ArtistDataModel model)
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.  
            string currentDir = Environment.CurrentDirectory + "\\Data\\Artist";
            System.IO.StreamReader file =
                new System.IO.StreamReader(currentDir);
            List<Artist> artists = new List<Artist>();

            while ((line = file.ReadLine()) != null)
            {
                if (!string.Equals(line.First(), SKIP_LINE)) // Skip Table MetaData
                {
                    try
                    {
                        line = line.Trim(UNICODE_ENTRY_NEWLINE);
                        string[] dataEntry = line.Split(UNICODE_ENTRY_SPLIT);
                        var artistEntry = new Artist();

                        artistEntry.ExportDate = (string.IsNullOrEmpty(dataEntry[0])) ? null : dataEntry[0];
                        artistEntry.ArtistId = (string.IsNullOrEmpty(dataEntry[1])) ? null : dataEntry[1];
                        artistEntry.Name = (string.IsNullOrEmpty(dataEntry[2])) ? null : dataEntry[2];
                        artistEntry.IsActualArtist = string.Equals(dataEntry[3], "1");
                        artistEntry.ViewUrl = (string.IsNullOrEmpty(dataEntry[4])) ? null : dataEntry[4];
                        artistEntry.ArtistTypeId = (string.IsNullOrEmpty(dataEntry[5])) ? null : dataEntry[5];

                        artists.Add(artistEntry);

                        counter++;
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        continue;
                    }
                }
            }
            file.Close();
            model.Artists = artists;
        }
    }
}
