using DataIngestion.Src.Responses;
using DataIngestion.Src.Services;
using Microsoft.Extensions.Configuration;
using System;

namespace Driver.Src.Services
{
    public interface IMusicService
    {
        IndexResponse GetAlbumIndex();
    }
    
    public class MediaService : IMusicService
    {
        private readonly IConfiguration _config;
        private readonly IDataIngestionWebClient _client;


        // http client to connect to MusicService
        public MediaService(IConfiguration config, IDataIngestionWebClient client)
        {
            _config = config;
            _client = client;
        }

        public IndexResponse GetAlbumIndex()
        {
            if (_config["AppSettings:DataService"].Equals("LocalFile"))
            {
                var requestUrl = _config["AppSettings:LocalFileServiceUrl"];
                var indexResponse = _client.Get<IndexResponse>($"{requestUrl}/MediaData/GetAlbumIndex");

                if (indexResponse.Data != null)
                {
                    if (!string.IsNullOrEmpty(indexResponse.ErrorMessage))
                    {
                        indexResponse.Data.ErrorMessage = indexResponse.ErrorMessage;
                    }
                    return indexResponse.Data;
                }
            }
            else
            {
                // Call DBService. Steps service would take:
                // 1. Download Data (locally or from authoirty like google drive documents)
                // 2. Populate Database tables
                // 3. Query tables to create Album list object
                // 4. Insert into Elastic Index
                // 5. Return IndexUrl
            }
            return new IndexResponse { ErrorMessage = "There was an error retrieving your elastic index" };
        }
    }
}
