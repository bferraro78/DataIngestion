using System;
using Driver.Src.DataExtract;

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
            Console.WriteLine("Goodbye cruel world!");


        }
    }
}
