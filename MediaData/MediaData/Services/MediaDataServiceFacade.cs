using Microsoft.Extensions.Logging;
using MusicData.Models.Response;

namespace MusicData.Services
{
    public interface IMusicDataServiceFacade
    {
        AlbumIndexResponse GetAlbumIndex();
    }

    public class MediaDataServiceFacade : IMusicDataServiceFacade
    {
        private readonly ILogger<MediaDataServiceFacade> _logger;
        private readonly IMusicDataProvider _provider;


        public MediaDataServiceFacade(ILogger<MediaDataServiceFacade> logger, IMusicDataProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }


        public AlbumIndexResponse GetAlbumIndex()
        {
            return _provider.GetAlbumIndex();
        }

    }
}
