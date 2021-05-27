﻿using MediaData.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MusicData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicData.Services.DataReader.DataHandlers
{
    public class ArtistDataHandler
    {
        private ILogger<ArtistDataHandler> _logger;
        protected new HandlerTypeEnum HANDLER_TYPE = HandlerTypeEnum.ARTIST;
        protected const char UNICODE_ENTRY_SPLIT = '\u0001';
        protected const char UNICODE_ENTRY_NEWLINE = '\u0002';
        protected const char SKIP_LINE = '#';

        public ArtistDataHandler(ILogger<ArtistDataHandler> logger)
        {
            _logger = logger;
        }

        public  Task<IDictionary<string, List<Artist>>> Handle()
        {
            return Task.Run(() => GetArtists());
        }


        private Task<IDictionary<string, List<Artist>>> GetArtists()
        {
            string line;

            // Read the file and display it line by line.  
            string currentDir = Environment.CurrentDirectory + "\\Data\\Artist";
            System.IO.StreamReader file =
                new(currentDir);
            IDictionary<string, List<Artist>> artists = new Dictionary<string, List<Artist>>();

            while ((line = file.ReadLine()) != null)
            {
                if (!Equals(line.First(), SKIP_LINE)) // Skip Table MetaData
                {
                    try
                    {
                        line = line.Trim(UNICODE_ENTRY_NEWLINE);
                        string[] dataEntry = line.Split(UNICODE_ENTRY_SPLIT);
                        var artistEntry = new Artist
                        {
                            ExportDate = (string.IsNullOrEmpty(dataEntry[0])) ? null : dataEntry[0],
                            ArtistId = (string.IsNullOrEmpty(dataEntry[1])) ? null : dataEntry[1],
                            Name = (string.IsNullOrEmpty(dataEntry[2])) ? null : dataEntry[2],
                            IsActualArtist = string.Equals(dataEntry[3], "1"),
                            ViewUrl = (string.IsNullOrEmpty(dataEntry[4])) ? null : dataEntry[4],
                            ArtistTypeId = (string.IsNullOrEmpty(dataEntry[5])) ? null : dataEntry[5]
                        };
                        if (artists.ContainsKey(artistEntry.ArtistId))
                        {
                            artists[artistEntry.ArtistId].Add(artistEntry);
                        }
                        else
                        {
                            artists.Add(artistEntry.ArtistId, new List<Artist> { artistEntry });
                        }
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        _logger.LogError(ex, "Bad Data Line Format");
                        continue;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Unable to Read Artist Data");
                        continue;
                    }
                }
            }
            file.Close();
            return Task.FromResult(artists);
        }
        public HandlerTypeEnum GetHandleType()
        {
            return HANDLER_TYPE;
        }
    }
}
