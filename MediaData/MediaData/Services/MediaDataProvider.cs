using MediaData.Constants;
using MediaData.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MediaData.Models;
using MediaData.Models.Response;
using MediaData.Services.Factory;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaData.Services
{
    public interface IMediaDataProvider
    {
        AlbumIndexResponse GetAlbumIndex();
    }

    public class MediaDataProvider : IMediaDataProvider
    {
        private readonly ILogger<MediaDataProvider> _logger;
        private readonly IMediaDataRetrieverFactory _factory;
        private readonly IBulkElasticDataInjector _bulkElasticDataInjector;
        private readonly IConfiguration _configuration;

        public MediaDataProvider(ILogger<MediaDataProvider> logger, IMediaDataRetrieverFactory factory,
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
                var MediaData = proxy.GetMediaData();
                var albums = IndexDataConverter.CreateAlbumData(MediaData);
                _bulkElasticDataInjector.InjectAlbums(albums);

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
