using MusicData.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicData.Services
{
    public interface IMusicDataProxy
    {
        MusicDataResponse GetMusicData();
    }
}
