using Microsoft.Extensions.Configuration;
using MediaData.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public BulkElasticDataInjector(IConfiguration configuration)
        {
            _elasticIndexUri = configuration["ElasticConfiguration:Uri"];
            _albumIndex = configuration["ElasticInjectionProperties:albumIndex"];
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
            }

        }

        private List<List<TData>> Split<TData>(List<TData> source)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 1000)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

    }
}
