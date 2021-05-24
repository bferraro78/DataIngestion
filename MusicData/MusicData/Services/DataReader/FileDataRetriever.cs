using MusicData.Models;
using MusicData.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicData.Services.DataReader
{

    public class FileDataRetriever : IMusicDataProxy
    {
        const char UNICODE_ENTRY_SPLIT = '\u0001';
        const char UNICODE_ENTRY_NEWLINE = '\u0002';
        const char SKIP_LINE = '#';

        // TODO - unit test - exception reguarding bad file format?

        public MusicDataResponse GetMusicData()
        {
            var response = new MusicDataResponse();
            response.Artists = GetArtist();
            response.ArtistCollections = GetArtistCollections();
            response.Collections = GetCollection();
            response.CollectionMatches = GetMatchCollections();

            return response;
        }

        public List<Artist> GetArtist()
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
            System.Console.WriteLine("There were {0} lines.", counter);
            return artists;
        }

        public List<ArtistCollection> GetArtistCollections()
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.  
            string currentDir = Environment.CurrentDirectory + "\\Data\\artist_collection";
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
            System.Console.WriteLine("There were {0} lines.", counter);
            return artistCollections;
        }

        public List<CollectionMatch> GetMatchCollections()
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
            System.Console.WriteLine("There were {0} lines.", counter);
            return collectionMatches;
        }

        public List<Collection> GetCollection()
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
            System.Console.WriteLine("There were {0} lines.", counter);
            return collections;

        }

    }
}
