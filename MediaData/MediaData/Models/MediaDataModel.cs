using System.Collections.Generic;

namespace MediaData.Models
{
    /// <summary>
    /// Extend to include other SQL tables for new IMediaDataProxy
    /// </summary>
    public class MediaDataModel
    {
        
        public IDictionary<string, List<Artist>> Artists { get; set; }
        public IDictionary<string, List<ArtistCollection>> ArtistCollections { get; set; }
        public IDictionary<string, List<Collection>> Collections { get; set; }
        public IDictionary<string, List<CollectionMatch>> CollectionMatches { get; set; }
    }
}
