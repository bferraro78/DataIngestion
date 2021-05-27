using Microsoft.Extensions.Logging;
using MediaData.Models.Response;

namespace MediaData.Services
{
    public interface IMediaDataServiceFacade
    {
        AlbumIndexResponse GetAlbumIndex();
    }

    public class MediaDataServiceFacade : IMediaDataServiceFacade
    {
        private readonly ILogger<MediaDataServiceFacade> _logger;
        private readonly IMediaDataProvider _provider;


        public MediaDataServiceFacade(ILogger<MediaDataServiceFacade> logger, IMediaDataProvider provider)
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
