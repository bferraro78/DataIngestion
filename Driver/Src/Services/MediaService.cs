using DataIngestion.Src.Responses;
using DataIngestion.Src.Services;
using Microsoft.Extensions.Configuration;
using System;

namespace Driver.Src.Services
{
    public interface IMusicService
    {
        //Server=tcp:linkfiredata.database.windows.net,1433;Initial Catalog=MusicData;Persist Security Info=False;User ID=ultraski78;Password=Soad2441;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
        string GetAlbumIndex();
    }
    
    public class MediaService : IMusicService
    {
        private readonly string _url;
        private readonly IConfiguration _config;
        private readonly IDataIngestionWebClient _client;


        // http client to connect to MusicService
        public MediaService(IConfiguration config, IDataIngestionWebClient client)
        {
            _config = config;
            _client = client;
        }

        public string GetAlbumIndex()
        {
            if (_config["AppSettings:DataService"].Equals("LocalFile"))
            {
                var requestUrl = _config["AppSettings:LocalFileServiceUrl"];
                var indexResponse = _client.Get<IndexResponse>($"{requestUrl}/MusicData");

                if (string.IsNullOrEmpty(indexResponse.ErrorMessage) && indexResponse.Data != null)
                {
                    return indexResponse.Data.IndexUrl;
                }

                // TODO - Error Handle (log ErrorMessage)

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
            return "There was an error retrieving your elastic index";
        }
    }
}
