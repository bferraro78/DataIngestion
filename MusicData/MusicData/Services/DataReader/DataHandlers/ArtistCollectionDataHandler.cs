using MusicData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicData.Services.DataReader.DataHandlers
{
    public class ArtistCollectionDataHandler : DataHandler
    {
        public override void Handle(ref ArtistDataModel model)
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.  
            string currentDir = Environment.CurrentDirectory+"\\Data\\artist_collection";
            System.IO.StreamReader file =
                new System.IO.StreamReader(currentDir);
            List<ArtistCollection> artistCollections = new List<ArtistCollection>();

            while ((line = file.ReadLine()) != null)
            {
                if (!string.Equals(line.First(), SKIP_LINE)) // Skip Table MetaData
                {
                    try
                    {
                        line = line.Trim(UNICODE_ENTRY_NEWLINE);
                        string[] dataEntry = line.Split(UNICODE_ENTRY_SPLIT);
                        var artistCollectionEntry = new ArtistCollection();

                        artistCollectionEntry.ExportDate = (string.IsNullOrEmpty(dataEntry[0])) ? null : dataEntry[0];
                        artistCollectionEntry.ArtistId = (string.IsNullOrEmpty(dataEntry[1])) ? null : dataEntry[1];
                        artistCollectionEntry.CollectionId = (string.IsNullOrEmpty(dataEntry[2])) ? null : dataEntry[2];
                        artistCollectionEntry.IsPrimaryArtist = string.Equals(dataEntry[3], "1");

                        if (string.Equals(dataEntry[3], "1"))
                        {
                            var f = 5;
                        }

                        artistCollectionEntry.RoleId = (string.IsNullOrEmpty(dataEntry[4])) ? null : dataEntry[4];

                        artistCollections.Add(artistCollectionEntry);
                        counter++;
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        continue;
                    }
                }
            }
            file.Close();
            model.ArtistCollections = artistCollections;
        }
    }
}
