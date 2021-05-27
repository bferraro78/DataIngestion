using MediaData.Constants;
using MusicData.Models;

namespace MusicData.Services.DataReader.DataHandlers
{
    public interface IDataHandler
    {
        void Handle(ref ArtistDataModel model);
        HandlerTypeEnum GetHandleType();
    }

    public abstract class DataHandler : IDataHandler
    {
        protected const char UNICODE_ENTRY_SPLIT = '\u0001';
        protected const char UNICODE_ENTRY_NEWLINE = '\u0002';
        protected const char SKIP_LINE = '#';
        protected HandlerTypeEnum HANDLER_TYPE = HandlerTypeEnum.DEFAULT;

        //public abstract void Handle(ref ArtistDataModel model);
        public abstract void Handle(ref ArtistDataModel model);

        public virtual HandlerTypeEnum GetHandleType()
        {
            return HANDLER_TYPE;
        }

    }
}
