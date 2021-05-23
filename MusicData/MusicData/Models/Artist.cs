﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicData.Models
{
    public class Artist
    {
        public string ExportDate { get; set; }
        public string ArtistId { get; set; }
        public string Name { get; set; }
        public bool IsActualArtist { get; set; }
        public string ViewUrl { get; set; }
        public string ArtistTypeId { get; set; }
    }
}
