using MediaData.Constants;
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
        private readonly IEnumerable<IDataHandler> _handlers;
        protected HandlerTypeEnum HANDLER_TYPE;

        public ArtistDataReader(IEnumerable<IDataHandler> handlers)
        {
            _handlers = handlers;
        }

        public void SetHandlerType(HandlerTypeEnum type)
        {
            HANDLER_TYPE = type;
        }

        public ArtistDataModel GetMusicData()
        {
            var model = new ArtistDataModel();
            foreach (IDataHandler handler in _handlers)
            {
                if (handler.GetHandleType() == HANDLER_TYPE || handler.GetHandleType() == HandlerTypeEnum.DEFAULT)
                { 
                    handler.Handle(ref model);
                }
            }
            return model;
        }

    }
}
