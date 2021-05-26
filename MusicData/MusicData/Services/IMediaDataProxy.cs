using MediaData.Constants;
using MusicData.Models;
using System.Threading.Tasks;

namespace MusicData.Services
{
    public interface IMediaDataProxy
    {
        ArtistDataModel GetMusicData();
        void SetHandlerType(HandlerTypeEnum type);
    }
}
