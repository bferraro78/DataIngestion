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
            // Add config / create a placeholder google download service that would download and save files to the Data folder
            // Pass in value and use this example as using another data retriever
            return (IMediaDataProxy)_serviceProvider.GetService(typeof(ArtistDataReader));
        }
    }
}
