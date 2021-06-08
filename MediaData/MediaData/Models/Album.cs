using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MediaData.Models
{
    [DataContract]
    public class Album
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string Upc { get; set; }
        [DataMember(Name="releaseDate")]
        public DateTime ReleaseDate { get; set; }
        [DataMember(Name="isCompilation")]
        public bool IsCompilation { get; set; }
        [DataMember]
        public string Label { get; set; }
        [DataMember(Name="imageUrl")]
        public string ImageUrl { get; set; }
        [DataMember(Name="artists")]
        public List<ArtistAlbum> Artists { get; set; }
    }

    [DataContract]
    public class ArtistAlbum 
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
