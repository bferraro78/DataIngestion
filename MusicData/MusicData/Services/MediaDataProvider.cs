using MediaData.Constants;
using MediaData.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MusicData.Models;
using MusicData.Models.Response;
using MusicData.Services.Factory;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicData.Services
{
    public interface IMusicDataProvider
    {
        AlbumIndexResponse GetAlbumIndex();
    }

    public class MusicDataProvider : IMusicDataProvider
    {
        private readonly ILogger<MusicDataProvider> _logger;
        private readonly IMusicDataRetrieverFactory _factory;
        private readonly IBulkElasticDataInjector _bulkElasticDataInjector;
        private readonly IConfiguration _configuration;

        public MusicDataProvider(ILogger<MusicDataProvider> logger, IMusicDataRetrieverFactory factory,
            IBulkElasticDataInjector bulkElasticDataInjector, IConfiguration configuration)
        {
            _logger = logger;
            _factory = factory;
            _bulkElasticDataInjector = bulkElasticDataInjector;
            _configuration = configuration;
        }


        public AlbumIndexResponse GetAlbumIndex()
        {
            var response = new AlbumIndexResponse();
            try
            {
                var proxy = _factory.GetDataRetriever();
                proxy.SetHandlerType(HandlerTypeEnum.ARTIST);
                var data = proxy.GetMusicData();

                // Create Album objectBusiness Logic 
                List<Album> albums = new();

                foreach (KeyValuePair<string, List<ArtistCollection>> kvp in data.ArtistCollections)
                {
                    IEnumerable<string> f = new List<string>();
                    Collection collection = null;
                    CollectionMatch collectionMatch = null;

                    if (data.Collections.ContainsKey(kvp.Key))
                    { 
                        collection = data.Collections[kvp.Key].FirstOrDefault();
                    }

                    if (data.CollectionMatches.ContainsKey(kvp.Key))
                    {
                        collectionMatch = data.CollectionMatches[kvp.Key].FirstOrDefault();
                    }

                    if (collection != null)
                    {
                        albums.Add(new Album
                        {
                            Id = (string.IsNullOrEmpty(collection.CollectionId)) ? string.Empty : collection.CollectionId,
                            Name = (string.IsNullOrEmpty(collection.Name)) ? string.Empty : collection.Name,
                            Url = (string.IsNullOrEmpty(collection.ViewUrl)) ? string.Empty : collection.ViewUrl,
                            Upc = (string.IsNullOrEmpty(collectionMatch.Upc)) ? string.Empty : collectionMatch.Upc,
                            ReleaseDate = (string.IsNullOrEmpty(collection.OriginalReleaseDate)) ? DateTime.MinValue : DateTime.Parse(collection.OriginalReleaseDate),
                            IsCompilation = collection.IsCompilation,
                            Label = (string.IsNullOrEmpty(collection.LabelStudio)) ? string.Empty : collection.LabelStudio,
                            ImageUrl = (string.IsNullOrEmpty(collection.ArtworkUrl)) ? string.Empty : collection.ArtworkUrl
                        });
                    }
                }

                foreach (var album in albums)
                {
                    // Get all artists associated with the collection

                    if (data.ArtistCollections.ContainsKey(album.Id))
                    { 
                        var artistsInvolved = data.ArtistCollections[album.Id];
                        artistsInvolved = artistsInvolved.Distinct().ToList();
                        var artists = new List<ArtistAlbum>();
                        foreach (var artistInvolved in artistsInvolved)
                        {
                            var artistId = artistInvolved.ArtistId;
                            if (data.Artists.ContainsKey(artistId))
                            { 
                                var existingArtist = data.Artists[artistId].FirstOrDefault(); // Only one artistId associated to an artist
                                artists.Add(new ArtistAlbum { Id = artistId, Name = existingArtist.Name });
                            }
                        }
                        album.Artists = artists;
                    }
                }

                albums = albums.Where(album => album.Artists.Count > 0).ToList(); // Remove any albums with no artist data
                _bulkElasticDataInjector.InjectAlbums(albums);

                // unit testable model converter (interface for convert to album)
                // TODO - map errors
                // TODO - unit tets

                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Data = new Models.Response.Index
                {
                    IndexUrl = _configuration["ElasticConfiguration:Uri"],
                    DashboardUrl = _configuration["ElasticConfiguration:DashbaordUri"]
                };
            }
            catch (Exception ex)
            {
                // Log Stack trace exception
                _logger.LogError(ex, "Failed to create Ablbum Index");
                response.ErrorMessage = ex.Message;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }



    }
}
