using Microsoft.Extensions.Logging;
using MusicData.Models.Response;
using MusicData.Services.DataReader;


namespace MusicData.Services
{
    public interface IMusicDataProvider
    {
        AlbumIndexResponse GetAlbumIndex();
    }

    public class MusicDataProvider : IMusicDataProvider
    {
        private readonly ILogger<MusicDataProvider> _logger;

        public MusicDataProvider(ILogger<MusicDataProvider> logger)
        {
            _logger = logger;
        }


        public AlbumIndexResponse GetAlbumIndex()
        {
            var collections = FileDataReader.GetCollection();
            var artists = FileDataReader.GetArtist();
            var artistCollections = FileDataReader.GetArtistCollections();
            var MatchCollections = FileDataReader.GetMatchCollections();


            var albumIndex = "";

            var response = new AlbumIndexResponse();
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.AlbumElasticIndex = albumIndex;

            return response;
        }

    }
}
