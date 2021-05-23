using Microsoft.Extensions.Logging;
using MusicData.Models.Response;

namespace MusicData.Services
{
    public interface IMusicDataServiceFacade
    {
        AlbumIndexResponse GetAlbumIndex();
    }

    public class MusicDataServiceFacade : IMusicDataServiceFacade
    {
        private readonly ILogger<MusicDataServiceFacade> _logger;
        private readonly IMusicDataProvider _provider;


        public MusicDataServiceFacade(ILogger<MusicDataServiceFacade> logger, IMusicDataProvider provider)
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
