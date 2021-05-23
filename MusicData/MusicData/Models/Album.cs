using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MusicData.Models
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
        [DataMember]
        public List<ArtistAlbum> Artists { get; set; }


        /*
         {
            "id": "1255407551", // collection_id - Collection?
            "name": "Nishana - Single", // name - Collection
            "url": "http://ms.com/album/nishana-single/1255407551?uo=5", // view_url - Collection
            "upc": "191061793557", // collection match
            "releaseDate": "2017-06-10T00:00:00", // original_release_date - Collection ?
            "isCompilation": false, // Collection
            "label": "Aark Records", // label_studio collection
            "imageUrl": "http://img.com/image/thumb/Music117/v4/92/b8/51/92b85100-13c8-8fa4-0856-bb27276fdf87/191061793557.jpg/170x170bb.jpg",// artwork url collection
            "artists": [
              {
                "id": "935585671",
                "name": "Anmol Dhaliwal"
              }
            ]
          }
         */

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
