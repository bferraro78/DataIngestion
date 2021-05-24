using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicData.Models.Response
{
    public class MusicDataResponse : BaseResponse
    {
        public List<Artist> Artists { get; set; }
        public List<ArtistCollection> ArtistCollections { get; set; }
        public List<Collection> Collections { get; set; }
        public List<CollectionMatch> CollectionMatches { get; set; }

    }
}
