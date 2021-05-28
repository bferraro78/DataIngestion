using MediaData.Constants;
using MediaData.Services.DataReader;
using System;

namespace MediaData.Services.Factory
{
    public interface IMediaDataRetrieverFactory
    {
        IMediaDataProxy GetDataRetriever(DataReaderTypeEnum dataReaderType);
    }
    public class MediaDataRetrieverFactory : IMediaDataRetrieverFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public MediaDataRetrieverFactory(IServiceProvider serviceProvider)
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
