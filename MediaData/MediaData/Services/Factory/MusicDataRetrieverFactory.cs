using MediaData.Constants;
using MusicData.Services.DataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicData.Services.Factory
{
    public interface IMusicDataRetrieverFactory
    {
        IMediaDataProxy GetDataRetriever(DataReaderTypeEnum dataReaderType);
    }
    public class MusicDataRetrieverFactory : IMusicDataRetrieverFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public MusicDataRetrieverFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IMediaDataProxy GetDataRetriever(DataReaderTypeEnum dataReaderType)
        {
            if (dataReaderType == DataReaderTypeEnum.ALBUM_DATA)
            {
                return (IMediaDataProxy)_serviceProvider.GetService(typeof(ArtistDataReader));
            }
            else
            {
                // Insert Default Data Reader Type (sticking with Artist Data reader for project sake)
                return (IMediaDataProxy)_serviceProvider.GetService(typeof(ArtistDataReader));
            }
        }
    }
}
