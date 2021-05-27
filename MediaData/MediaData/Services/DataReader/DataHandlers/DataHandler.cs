using MediaData.Constants;
using MediaData.Models;

namespace MediaData.Services.DataReader.DataHandlers
{
    public abstract class DataHandler 
    {
        protected const char UNICODE_ENTRY_SPLIT = '\u0001';
        protected const char UNICODE_ENTRY_NEWLINE = '\u0002';
        protected const char SKIP_LINE = '#';
    }
}
