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
            var indexResponse = _musicService.GetAlbumIndex();
            if (!string.IsNullOrEmpty(indexResponse.ErrorMessage))
            {
                Console.WriteLine($"Here is your Album Index: {indexResponse.ErrorMessage}");
            }
            else
            { 
                Console.WriteLine($"Here is your Album Index: {indexResponse.IndexUrl}");
                Console.WriteLine($"Here is your Album Index: {indexResponse.DashboardUrl}");
            }
        }
    }
}
