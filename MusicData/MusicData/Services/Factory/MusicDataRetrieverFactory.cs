using MusicData.Services.DataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicData.Services.Factory
{
    public interface IMusicDataRetrieverFactory
    {
        IMediaDataProxy GetDataRetriever();
    }
    public class MusicDataRetrieverFactory : IMusicDataRetrieverFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public MusicDataRetrieverFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IMediaDataProxy GetDataRetriever()
        {
            // TODO - retrieve this for either the google download service OR go with the file data retreiver
            return (IMediaDataProxy)_serviceProvider.GetService(typeof(ArtistDataReader));
        }
    }
}
