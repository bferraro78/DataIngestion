using Microsoft.Extensions.Logging;
using MusicData.Models;
using MusicData.Models.Response;
using MusicData.Services.DataReader;
using MusicData.Services.Factory;
using System;
using System.Collections.Generic;

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


        public MusicDataProvider(ILogger<MusicDataProvider> logger, IMusicDataRetrieverFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }


        public AlbumIndexResponse GetAlbumIndex()
        {
            var response = new AlbumIndexResponse();
            try
            {
                var proxy = _factory.GetDataRetriever();
                var data = proxy.GetMusicData();


                var albumIndex = "beanpus";
                //var isPrimary = data.ArtistCollections.FindAll(a => a.IsPrimaryArtist == true);
                //var isComp = data.Collections.FindAll(a => a.IsCompilation == true);
                //var isActualA = data.Artists.FindAll(a => a.IsActualArtist != true);


                List<Album> albums = new List<Album>();

                foreach (var artistCollection in data.ArtistCollections)
                {
                    var collection = data.Collections.Find(c => c.CollectionId.Equals(artistCollection.CollectionId));
                    var cid = artistCollection.CollectionId;

                    var collectionMatch = data.CollectionMatches.Find(cm => cm.CollectionId.Equals(artistCollection.CollectionId));

                    // Get all artists associated with the collection
                    var artistsInvolved = data.ArtistCollections.FindAll(ac => ac.CollectionId.Equals(cid));
                    if (artistsInvolved.Count > 1)
                    {
                        var f = 0;
                        foreach (var g in artistsInvolved)
                        { 
                            var realArtist = data.Artists.Find(a => g.ArtistId.Equals(a.ArtistId));
                            if (realArtist != null)
                            {
                                var gf = 0;
                            }
                        }

                        


                    }

                    //var album = new Album
                    //{
                    //    Id = collection.CollectionId,
                    //    Name = collection.Name,
                    //    Url = collection.ViewUrl,
                    //    Upc = collectionMatch.Upc,
                    //    ReleaseDate = DateTime.Parse(collection.OriginalReleaseDate),
                    //    IsCompilation = collection.IsCompilation,
                    //    Label = collection.LabelStudio,
                    //    ImageUrl = collection.ArtworkUrl
                    //};


                }

                // TODO - Create Album objectBusiness Logic 


                // TODO - push album list object to an elastic index


                // TODO - map errors

                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Data = new Models.Response.Index();
                response.Data.IndexUrl = albumIndex;
            }
            catch (Exception ex)
            {
                // Log Stack trace exception
                response.ErrorMessage = ex.Message;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }

        private void MapData(ArtistCollection artistCollection, List<Album> albums)
        { 
        
        }

    }
}
