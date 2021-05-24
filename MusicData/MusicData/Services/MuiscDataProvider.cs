using Microsoft.Extensions.Logging;
using MusicData.Models.Response;
using MusicData.Services.DataReader;
using MusicData.Services.Factory;
using System;

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


        public MusicDataProvider(ILogger<MusicDataProvider> logger, IMusicDataRetrieverFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }


        public AlbumIndexResponse GetAlbumIndex()
        {
            var response = new AlbumIndexResponse();
            try
            {
                var proxy = _factory.GetDataRetriever();
                var data = proxy.GetMusicData();


                var albumIndex = "beanpus";
                var isPrimary = data.ArtistCollections.FindAll(a => a.IsPrimaryArtist == true);
                var isComp = data.Collections.FindAll(a => a.IsCompilation == true);



                // TODO - Create Album objectBusiness Logic 


                // TODO - push album list object to an elastic index


                // TODO - map errors

                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Data = new Models.Response.Index();
                response.Data.IndexUrl = albumIndex;
            }
            catch (Exception ex)
            {
                // Log Stack trace exception
                response.ErrorMessage = ex.Message;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }

    }
}
