using Microsoft.Extensions.Configuration;
using MusicData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicData.Services.DataReader.DataHandlers
{
    public interface IDataHandler
    {
        void Handle(ref ArtistDataModel model);
    }

    public abstract class DataHandler : IDataHandler
    {
        protected const char UNICODE_ENTRY_SPLIT = '\u0001';
        protected const char UNICODE_ENTRY_NEWLINE = '\u0002';
        protected const char SKIP_LINE = '#';

        public abstract void Handle(ref ArtistDataModel model);


    }
}
