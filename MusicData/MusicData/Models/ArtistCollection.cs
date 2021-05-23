using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicData.Models
{
    public class ArtistCollection
    {
        public string ExportDate { get; set; }
        public string CollectionId { get; set; }
        public string ArtistId { get; set; }
        public bool IsPrimaryArtist { get; set; }
        public string RoleId { get; set; }
    }
}
