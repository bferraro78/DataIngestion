using MediaData.Constants;
using Microsoft.Extensions.Logging;
using MediaData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaData.Services.DataReader.DataHandlers
{
    public class ArtistCollectionDataHandler : DataHandler
    {
        private readonly ILogger<ArtistCollectionDataHandler> _logger;

        public ArtistCollectionDataHandler(ILogger<ArtistCollectionDataHandler> logger)
        {
            _logger = logger;
        }

        public Task<IDictionary<string, List<ArtistCollection>>> Handle(string dataLocation)
        {
            return Task.Run(() => GetArtistCollections(dataLocation));
        }

        private Task<IDictionary<string, List<ArtistCollection>>> GetArtistCollections(string dataLocation)
        {
            string line;

            // Read the file and display it line by line.  
            string currentDir = Environment.CurrentDirectory + dataLocation;
            System.IO.StreamReader file =
                new(currentDir);
            IDictionary<string, List<ArtistCollection>> artistCollections = new Dictionary<string, List<ArtistCollection>>();

            while ((line = file.ReadLine()) != null)
            {
                if (!Equals(line.First(), SKIP_LINE)) // Skip Table MetaData
                {
                    try
                    {
                        line = line.Trim(UNICODE_ENTRY_NEWLINE);
                        string[] dataEntry = line.Split(UNICODE_ENTRY_SPLIT);
                        var artistCollectionEntry = new ArtistCollection
                        {
                            ExportDate = (string.IsNullOrEmpty(dataEntry[0])) ? null : dataEntry[0],
                            ArtistId = (string.IsNullOrEmpty(dataEntry[1])) ? null : dataEntry[1],
                            CollectionId = (string.IsNullOrEmpty(dataEntry[2])) ? null : dataEntry[2],
                            IsPrimaryArtist = string.Equals(dataEntry[3], "1"),
                            RoleId = (string.IsNullOrEmpty(dataEntry[4])) ? null : dataEntry[4]
                        };
                        if (artistCollections.ContainsKey(artistCollectionEntry.CollectionId))
                        {
                            artistCollections[artistCollectionEntry.CollectionId].Add(artistCollectionEntry);
                        }
                        else
                        {
                            artistCollections.Add(artistCollectionEntry.CollectionId, new List<ArtistCollection> { artistCollectionEntry });
                        }
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        _logger.LogError(ex, "Bad Data Line Format");
                        continue;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Unable to Read ArtistCollection Data");
                        continue;
                    }
                }
            }
            file.Close();
            return Task.FromResult(artistCollections);
        }
    }
}
