using MediaData.Constants;
using MediaData.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MusicData.Models;
using MusicData.Models.Response;
using MusicData.Services.Factory;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicData.Services
{
    public interface IMusicDataProvider
    {
        AlbumIndexResponse GetAlbumIndex();
    }

    public class MusicDataProvider : IMusicDataProvider
    {
        private readonly ILogger<MusicDataProvider> _logger;
        private readonly IMusicDataRetrieverFactory _factory;
        private readonly IBulkElasticDataInjector _bulkElasticDataInjector;
        private readonly IConfiguration _configuration;

        public MusicDataProvider(ILogger<MusicDataProvider> logger, IMusicDataRetrieverFactory factory,
            IBulkElasticDataInjector bulkElasticDataInjector, IConfiguration configuration)
        {
            _logger = logger;
            _factory = factory;
            _bulkElasticDataInjector = bulkElasticDataInjector;
            _configuration = configuration;
        }


        public AlbumIndexResponse GetAlbumIndex()
        {
            var response = new AlbumIndexResponse();
            try
            {
                var proxy = _factory.GetDataRetriever(DataReaderTypeEnum.ALBUM_DATA);
                //proxy.SetHandlerType(HandlerTypeEnum.ARTIST);
                var musicData = proxy.GetMusicData();

                var albums = IndexDataConverter.CreateAlbumData(musicData);
                _bulkElasticDataInjector.InjectAlbums(albums);

                // TODO - unit tets

                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Data = new Models.Response.Index
                {
                    IndexUrl = _configuration["ElasticConfiguration:Uri"],
                    DashboardUrl = _configuration["ElasticConfiguration:DashbaordUri"]
                };
            }
            catch (Exception ex)
            {
                // Log Stack trace exception
                _logger.LogError(ex, "Failed to create Ablbum Index");
                response.ErrorMessage = "There was an error retrieving your elastic index";
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }



    }
}
