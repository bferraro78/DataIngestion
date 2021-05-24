using MusicData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicData.Services.DataReader.DataHandlers
{
    public class CollectionDataHandler: DataHandler
    {
        public override void Handle(ref ArtistDataModel model)
        {
            int counter = 0;
            string line;


            // Read the file and display it line by line.  
            string currentDir = Environment.CurrentDirectory + "\\Data\\Collection";
            System.IO.StreamReader file =
                new System.IO.StreamReader(currentDir);
            List<Collection> collections = new List<Collection>();

            while ((line = file.ReadLine()) != null)
            {
                if (!string.Equals(line.First(), SKIP_LINE)) // Skip Table MetaData
                {
                    try
                    {
                        line = line.Trim(UNICODE_ENTRY_NEWLINE);
                        string[] dataEntry = line.Split(UNICODE_ENTRY_SPLIT);
                        var collectionEntry = new Collection();

                        collectionEntry.ExportDate = (string.IsNullOrEmpty(dataEntry[0])) ? null : dataEntry[0];
                        collectionEntry.CollectionId = (string.IsNullOrEmpty(dataEntry[1])) ? null : dataEntry[1];
                        collectionEntry.Name = (string.IsNullOrEmpty(dataEntry[2])) ? null : dataEntry[2];
                        collectionEntry.TitleVersions = (string.IsNullOrEmpty(dataEntry[3])) ? null : dataEntry[3];
                        collectionEntry.SearchTerms = (string.IsNullOrEmpty(dataEntry[4])) ? null : dataEntry[4];
                        collectionEntry.ParentalAdvisory = (string.IsNullOrEmpty(dataEntry[5])) ? null : dataEntry[5];
                        collectionEntry.ArtistDisplayName = (string.IsNullOrEmpty(dataEntry[6])) ? null : dataEntry[6];
                        collectionEntry.ViewUrl = (string.IsNullOrEmpty(dataEntry[7])) ? null : dataEntry[7];
                        collectionEntry.ArtworkUrl = (string.IsNullOrEmpty(dataEntry[8])) ? null : dataEntry[8];
                        collectionEntry.OriginalReleaseDate = (string.IsNullOrEmpty(dataEntry[9])) ? null : dataEntry[9];
                        collectionEntry.ITunesReleaseDate = (string.IsNullOrEmpty(dataEntry[10])) ? null : dataEntry[10];
                        collectionEntry.LabelStudio = (string.IsNullOrEmpty(dataEntry[11])) ? null : dataEntry[11];
                        collectionEntry.ContentProviderName = (string.IsNullOrEmpty(dataEntry[12])) ? null : dataEntry[12];
                        collectionEntry.Copyright = (string.IsNullOrEmpty(dataEntry[13])) ? null : dataEntry[13];
                        collectionEntry.PLine = (string.IsNullOrEmpty(dataEntry[14])) ? null : dataEntry[14];
                        collectionEntry.MediaTypeId = (string.IsNullOrEmpty(dataEntry[15])) ? null : dataEntry[15];
                        collectionEntry.IsCompilation = string.Equals(dataEntry[16], "1");
                        collectionEntry.CollectionTypeId = (string.IsNullOrEmpty(dataEntry[17])) ? null : dataEntry[17];

                        collections.Add(collectionEntry);

                        counter++;
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        continue;
                    }
                }
            }
            file.Close();
            model.Collections = collections;
        }
    }
}
