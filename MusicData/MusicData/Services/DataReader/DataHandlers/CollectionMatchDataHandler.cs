using MusicData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicData.Services.DataReader.DataHandlers
{
    public class CollectionMatchDataHandler : DataHandler
    {
        public override void Handle(ref ArtistDataModel model)
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.  
            string currentDir = Environment.CurrentDirectory + "\\Data\\collection_match";
            System.IO.StreamReader file =
                new System.IO.StreamReader(currentDir);
            List<CollectionMatch> collectionMatches = new List<CollectionMatch>();

            while ((line = file.ReadLine()) != null)
            {
                if (!string.Equals(line.First(), SKIP_LINE)) // Skip Table MetaData
                {
                    try
                    {
                        line = line.Trim(UNICODE_ENTRY_NEWLINE);
                        string[] dataEntry = line.Split(UNICODE_ENTRY_SPLIT);
                        var collectionMatchEntry = new CollectionMatch();

                        collectionMatchEntry.ExportDate = (string.IsNullOrEmpty(dataEntry[0])) ? null : dataEntry[0];
                        collectionMatchEntry.CollectionId = (string.IsNullOrEmpty(dataEntry[1])) ? null : dataEntry[1];
                        collectionMatchEntry.Upc = (string.IsNullOrEmpty(dataEntry[2])) ? null : dataEntry[2];
                        collectionMatchEntry.Grid = (string.IsNullOrEmpty(dataEntry[3])) ? null : dataEntry[3];
                        collectionMatchEntry.AmgAlbumId = (string.IsNullOrEmpty(dataEntry[4])) ? null : dataEntry[4];

                        collectionMatches.Add(collectionMatchEntry);
                        counter++;
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        continue;
                    }
                }
            }
            file.Close();
            model.CollectionMatches = collectionMatches;
        }
    }
}
