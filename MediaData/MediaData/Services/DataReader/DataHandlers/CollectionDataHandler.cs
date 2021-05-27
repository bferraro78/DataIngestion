using MediaData.Constants;
using Microsoft.Extensions.Logging;
using MusicData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicData.Services.DataReader.DataHandlers
{
    public class CollectionDataHandler
    {
        private ILogger<CollectionDataHandler> _logger;
        protected new HandlerTypeEnum HANDLER_TYPE = HandlerTypeEnum.ARTIST;
        protected const char UNICODE_ENTRY_SPLIT = '\u0001';
        protected const char UNICODE_ENTRY_NEWLINE = '\u0002';
        protected const char SKIP_LINE = '#';

        public CollectionDataHandler(ILogger<CollectionDataHandler> logger)
        {
            _logger = logger;
        }

        public Task<IDictionary<string, List<Collection>>> Handle()
        {
            return Task.Run(() => GetCollections());
        }

        private Task<IDictionary<string, List<Collection>>> GetCollections()
        {
            string line;

            // Read the file and display it line by line.  
            string currentDir = Environment.CurrentDirectory + "\\Data\\Collection";
            System.IO.StreamReader file =
                new(currentDir);
            IDictionary<string, List<Collection>> collections = new Dictionary<string, List<Collection>>();

            while ((line = file.ReadLine()) != null)
            {
                if (!Equals(line.First(), SKIP_LINE)) // Skip Table MetaData
                {
                    try
                    {
                        line = line.Trim(UNICODE_ENTRY_NEWLINE);
                        string[] dataEntry = line.Split(UNICODE_ENTRY_SPLIT);
                        var collectionEntry = new Collection
                        {
                            ExportDate = (string.IsNullOrEmpty(dataEntry[0])) ? null : dataEntry[0],
                            CollectionId = (string.IsNullOrEmpty(dataEntry[1])) ? null : dataEntry[1],
                            Name = (string.IsNullOrEmpty(dataEntry[2])) ? null : dataEntry[2],
                            TitleVersions = (string.IsNullOrEmpty(dataEntry[3])) ? null : dataEntry[3],
                            SearchTerms = (string.IsNullOrEmpty(dataEntry[4])) ? null : dataEntry[4],
                            ParentalAdvisory = (string.IsNullOrEmpty(dataEntry[5])) ? null : dataEntry[5],
                            ArtistDisplayName = (string.IsNullOrEmpty(dataEntry[6])) ? null : dataEntry[6],
                            ViewUrl = (string.IsNullOrEmpty(dataEntry[7])) ? null : dataEntry[7],
                            ArtworkUrl = (string.IsNullOrEmpty(dataEntry[8])) ? null : dataEntry[8],
                            OriginalReleaseDate = (string.IsNullOrEmpty(dataEntry[9])) ? null : dataEntry[9],
                            ITunesReleaseDate = (string.IsNullOrEmpty(dataEntry[10])) ? null : dataEntry[10],
                            LabelStudio = (string.IsNullOrEmpty(dataEntry[11])) ? null : dataEntry[11],
                            ContentProviderName = (string.IsNullOrEmpty(dataEntry[12])) ? null : dataEntry[12],
                            Copyright = (string.IsNullOrEmpty(dataEntry[13])) ? null : dataEntry[13],
                            PLine = (string.IsNullOrEmpty(dataEntry[14])) ? null : dataEntry[14],
                            MediaTypeId = (string.IsNullOrEmpty(dataEntry[15])) ? null : dataEntry[15],
                            IsCompilation = string.Equals(dataEntry[16], "1"),
                            CollectionTypeId = (string.IsNullOrEmpty(dataEntry[17])) ? null : dataEntry[17]
                        };

                        if (collections.ContainsKey(collectionEntry.CollectionId))
                        {
                            collections[collectionEntry.CollectionId].Add(collectionEntry);
                        }
                        else
                        {
                            collections.Add(collectionEntry.CollectionId, new List<Collection> { collectionEntry });
                        }
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        _logger.LogError(ex, "Bad Data Line Format");
                        continue;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Unable to Read Collection Data");
                        continue;
                    }
                }
            }
            file.Close();
            return Task.FromResult(collections);
        }

        public HandlerTypeEnum GetHandleType()
        {
            return HANDLER_TYPE;
        }
    }
}
