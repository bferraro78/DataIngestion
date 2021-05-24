using System.Collections.Generic;

namespace MusicData.Models
{
    public class ArtistDataModel
    {
        public List<Artist> Artists { get; set; }
        public List<ArtistCollection> ArtistCollections { get; set; }
        public List<Collection> Collections { get; set; }
        public List<CollectionMatch> CollectionMatches { get; set; }
    }
}
