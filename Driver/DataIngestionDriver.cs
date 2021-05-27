using System;
using Driver.Src.Services;

namespace Driver
{
    public class DataIngestionDriver
    {
        private readonly IMusicService _musicService;
        public DataIngestionDriver(IMusicService musicService)
        {
            _musicService = musicService;
        }

        public void Run()
        {
            _musicService.GetAlbumIndex();
        }
    }
}
