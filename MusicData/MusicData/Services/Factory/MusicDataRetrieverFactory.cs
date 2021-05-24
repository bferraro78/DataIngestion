using MusicData.Services.DataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicData.Services.Factory
{
    public interface IMusicDataRetrieverFactory
    {
        IMusicDataProxy GetDataRetriever();
    }
    public class MusicDataRetrieverFactory : IMusicDataRetrieverFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public MusicDataRetrieverFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IMusicDataProxy GetDataRetriever()
        {
            // TODO - retrieve this for either the google download service OR go with the file data retreiver
            return (IMusicDataProxy)_serviceProvider.GetService(typeof(FileDataRetriever));
        }
    }
}
