using MusicData.Models;

namespace MusicData.Services
{
    public interface IMediaDataProxy
    {
        ArtistDataModel GetMusicData();
    }
}
