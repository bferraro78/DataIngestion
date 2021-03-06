using MediaData.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaData.Services
{
    public interface IBulkElasticDataInjector
    {
        void InjectAlbums(List<Album> albums);
    }

    public class BulkElasticDataInjector : IBulkElasticDataInjector
    {
        public string _elasticIndexUri;
        public string _albumIndex;
        private int BULK_DATA_SPLIT = 1000;
        private ILogger<BulkElasticDataInjector> _logger;
        public BulkElasticDataInjector(IConfiguration configuration, ILogger<BulkElasticDataInjector> logger)
        {
            _elasticIndexUri = configuration["ElasticConfiguration:Uri"];
            _albumIndex = configuration["ElasticInjectionProperties:albumIndex"];
            _logger = logger;
        }

        public void InjectAlbums(List<Album> albums)
        {
            // Push album list object to an elastic index
            var settings = new ConnectionSettings(new Uri(_elasticIndexUri))
            .DefaultIndex(_albumIndex);
            var client = new ElasticClient(settings);

            var splitAlbums = Split(albums);
            foreach (var albumsSection in splitAlbums)
            {
                var r = client.Bulk(a => a.IndexMany(albumsSection));
                if (!r.IsValid)
                {
                    _logger.LogError($"Something went wrong when injecting a bulk album set to the ElasticIndex: {r.ServerError.ToString()}");
                }
            }
        }

        private List<List<TData>> Split<TData>(List<TData> source)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / BULK_DATA_SPLIT)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

    }
}
