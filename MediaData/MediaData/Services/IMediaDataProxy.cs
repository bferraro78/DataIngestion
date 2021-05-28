using MediaData.Constants;
using MediaData.Models;
using System.Threading.Tasks;

namespace MediaData.Services
{
    public interface IMediaDataProxy
    {
        MediaDataModel GetMediaData();
    }
}
