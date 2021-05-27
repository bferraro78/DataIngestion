using MediaData.Constants;
using Microsoft.Extensions.Logging;
using MediaData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaData.Services.DataReader.DataHandlers
{
    public class CollectionMatchDataHandler : DataHandler
    {
        private readonly ILogger<CollectionMatchDataHandler> _logger;

        public CollectionMatchDataHandler(ILogger<CollectionMatchDataHandler> logger)
        {
            _logger = logger;
        }

        public Task<IDictionary<string, List<CollectionMatch>>> Handle(string dataLocation)
        {
            return Task.Run(() => GetCollectionsMatches(dataLocation));
        }

        private Task<IDictionary<string, List<CollectionMatch>>> GetCollectionsMatches(string dataLocation)
        {
            string line;

            // Read the file and display it line by line.  
            string currentDir = Environment.CurrentDirectory + dataLocation;
            System.IO.StreamReader file =
                new(currentDir);
            IDictionary<string, List<CollectionMatch>> collectionMatches = new Dictionary<string, List<CollectionMatch>>();

            while ((line = file.ReadLine()) != null)
            {
                if (!Equals(line.First(), SKIP_LINE)) // Skip Table MetaData
                {
                    try
                    {
                        line = line.Trim(UNICODE_ENTRY_NEWLINE);
                        string[] dataEntry = line.Split(UNICODE_ENTRY_SPLIT);
                        var collectionMatchEntry = new CollectionMatch
                        {
                            ExportDate = (string.IsNullOrEmpty(dataEntry[0])) ? null : dataEntry[0],
                            CollectionId = (string.IsNullOrEmpty(dataEntry[1])) ? null : dataEntry[1],
                            Upc = (string.IsNullOrEmpty(dataEntry[2])) ? null : dataEntry[2],
                            Grid = (string.IsNullOrEmpty(dataEntry[3])) ? null : dataEntry[3],
                            AmgAlbumId = (string.IsNullOrEmpty(dataEntry[4])) ? null : dataEntry[4]
                        };

                        if (collectionMatches.ContainsKey(collectionMatchEntry.CollectionId))
                        {
                            collectionMatches[collectionMatchEntry.CollectionId].Add(collectionMatchEntry);
                        }
                        else
                        {
                            collectionMatches.Add(collectionMatchEntry.CollectionId, new List<CollectionMatch> { collectionMatchEntry });
                        }
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        _logger.LogError(ex, "Bad Data Line Format");
                        continue;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Unable to Read CollectionMatch Data");
                        continue;
                    }
                }
            }
            file.Close();
            return Task.FromResult(collectionMatches);
        }
    }
}
