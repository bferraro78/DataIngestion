using MusicData.Models;
using MusicData.Models.Response;
using MusicData.Services.DataReader.DataHandlers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicData.Services.DataReader
{

    public class ArtistDataReader : IMediaDataProxy
    {
        // TODO - unit test - exception reguarding bad file format?
        private IEnumerable<IDataHandler> _handlers;

        public ArtistDataReader(IEnumerable<IDataHandler> handlers)
        {
            _handlers = handlers;
        }

        public ArtistDataModel GetMusicData()
        {
            var model = new ArtistDataModel();
            foreach (IDataHandler handler in _handlers)
            {
                handler.Handle(ref model);
            }
            return model;
        }

    }
}
