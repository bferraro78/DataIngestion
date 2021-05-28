using MediaData.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MediaData.Models;
using MediaData.Models.Response;
using MediaData.Services.DataReader.DataHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaData.Services.DataReader
{

    public class ArtistDataReader : IMediaDataProxy
    {
        private ILoggerFactory _loggerFactory;
        private readonly IConfiguration _configuration;

        public ArtistDataReader(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        public MediaDataModel GetMediaData()
        {
            var model = new MediaDataModel();

            Task<IDictionary<string, List<Collection>>> readCollectionsTask = new CollectionDataHandler(_loggerFactory.CreateLogger<CollectionDataHandler>()).Handle(_configuration["DataLocation:collectionData"]);
            Task<IDictionary<string, List<Artist>>> readArtistsTask = new ArtistDataHandler(_loggerFactory.CreateLogger<ArtistDataHandler>()).Handle(_configuration["DataLocation:artistData"]);
            Task<IDictionary<string, List<ArtistCollection>>> readArtistCollectionsTask = new ArtistCollectionDataHandler(_loggerFactory.CreateLogger<ArtistCollectionDataHandler>()).Handle(_configuration["DataLocation:artistCollectionData"]);
            Task<IDictionary<string, List<CollectionMatch>>> readCollectionMacthesTask = new CollectionMatchDataHandler(_loggerFactory.CreateLogger<CollectionMatchDataHandler>()).Handle(_configuration["DataLocation:collectionMatchData"]);

            Task[] tasks = new Task[] { readCollectionsTask, readArtistsTask, readArtistCollectionsTask, readCollectionMacthesTask };
            Task.WaitAll(tasks);

            model.Artists = readArtistsTask.Result;
            model.Collections = readCollectionsTask.Result;
            model.CollectionMatches = readCollectionMacthesTask.Result;
            model.ArtistCollections = readArtistCollectionsTask.Result;

            return model;
        }

    }
}
