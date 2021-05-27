using MediaData.Constants;
using Microsoft.Extensions.Logging;
using MusicData.Models;
using MusicData.Models.Response;
using MusicData.Services.DataReader.DataHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicData.Services.DataReader
{

    public class ArtistDataReader : IMediaDataProxy
    {
        private readonly IEnumerable<IDataHandler> _handlers;
        private ILoggerFactory _loggerFactory;
        protected HandlerTypeEnum HANDLER_TYPE;

        public ArtistDataReader(ILoggerFactory loggerFactory)
        {
            //_handlers = handlers;
            _loggerFactory = loggerFactory;
        }

        public void SetHandlerType(HandlerTypeEnum type)
        {
            HANDLER_TYPE = type;
        }

        public ArtistDataModel GetMusicData()
        {
            var model = new ArtistDataModel();
            //foreach (IDataHandler handler in _handlers)
            //{
            //    if (handler.GetHandleType() == HANDLER_TYPE || handler.GetHandleType() == HandlerTypeEnum.DEFAULT)
            //    { 
            //        handler.Handle(ref model);
            //    }
            //}


            Task<IDictionary<string, List<Collection>>> readCollectionsTask = new CollectionDataHandler(_loggerFactory.CreateLogger<CollectionDataHandler>()).Handle();
            Task<IDictionary<string, List<Artist>>> readArtistsTask = new ArtistDataHandler(_loggerFactory.CreateLogger<ArtistDataHandler>()).Handle();
            Task<IDictionary<string, List<ArtistCollection>>> readArtistCollectionsTask = new ArtistCollectionDataHandler(_loggerFactory.CreateLogger<ArtistCollectionDataHandler>()).Handle();
            Task<IDictionary<string, List<CollectionMatch>>> readCollectionMacthesTask = new CollectionMatchDataHandler(_loggerFactory.CreateLogger<CollectionMatchDataHandler>()).Handle();

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
