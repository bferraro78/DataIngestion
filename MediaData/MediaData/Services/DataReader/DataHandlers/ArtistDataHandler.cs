using MediaData.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MediaData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaData.Services.DataReader.DataHandlers
{
    public class ArtistDataHandler : DataHandler
    {
        private ILogger<ArtistDataHandler> _logger;
        public ArtistDataHandler(ILogger<ArtistDataHandler> logger)
        {
            _logger = logger;
        }

        public  Task<IDictionary<string, List<Artist>>> Handle(string dataLocation)
        {
            return Task.Run(() => GetArtists(dataLocation));
        }


        private Task<IDictionary<string, List<Artist>>> GetArtists(string dataLocation)
        {
            string line;

            // Read the file and display it line by line.  
            string currentDir = Environment.CurrentDirectory + dataLocation;
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
    }
}
