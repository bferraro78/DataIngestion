using Microsoft.Extensions.Logging;
using Moq;
using MediaData.Models;
using MediaData.Services.DataReader.DataHandlers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using MediaData.Services;

namespace MediaDataUnitTests
{
    public class IndexDataConverterTests
    {
        [Fact]
        public void TestCreateAlbumData_Success()
        {

            var albums = IndexDataConverter.CreateAlbumData(CreateSuccessMockMediaData());
            Assert.True(albums.Count == 3); // 5 collections minus 2 for Artist Collections w/o existing artists

            // Asserts for Collection Data
            Assert.Equal("Album About Nothing", albums[0].Name);
            Assert.Equal("Roc Nation", albums[0].Label);
            Assert.Equal("423534523", albums[0].Upc);

            // Null Data Colection With Artists
            Assert.Equal("", albums[1].Label);
            Assert.Equal("347667", albums[1].Upc);



            // Artist Asserts
            Assert.True(albums[0].Artists.Count == 1);
            Assert.True(albums[1].Artists.Count == 2);
            Assert.True(albums[2].Artists.Count == 1);
            Assert.Equal("John", albums[1].Artists[0].Name);
            Assert.Equal("Darron", albums[1].Artists[1].Name);
        }

        private MediaDataModel CreateSuccessMockMediaData()
        {
            var data = new MediaDataModel();

            IDictionary<string, List<Artist>> Artists = new Dictionary<string, List<Artist>>();

            Artists.Add("1", new List<Artist> {
                new Artist {
                ExportDate = "10-12-2021",
                ArtistId = "1",
                Name = "Serj",
                IsActualArtist = true,
                ViewUrl = "http//localhost.com",
                ArtistTypeId = "http//localhost.com/image.jpg"
                }
            });

            Artists.Add("2", new List<Artist> {
                new Artist {
                ExportDate = "10-12-2021",
                ArtistId = "2",
                Name = "John",
                IsActualArtist = true,
                ViewUrl = "http//localhost.com",
                ArtistTypeId = "http//localhost.com/image.jpg"
                }
            });

            Artists.Add("3", new List<Artist> {
                new Artist {
                ExportDate = "10-12-2021",
                ArtistId = "3",
                Name = "Darron",
                IsActualArtist = true,
                ViewUrl = "http//localhost.com",
                ArtistTypeId = "http//localhost.com/image.jpg"
                }
            });

            Artists.Add("4", new List<Artist> {
                new Artist {
                ExportDate = "10-12-2021",
                ArtistId = "4",
                Name = "Shavo",
                IsActualArtist = true,
                ViewUrl = "http//localhost.com",
                ArtistTypeId = "http//localhost.com/image.jpg"
                }
            });

            IDictionary<string, List<ArtistCollection>> ArtistCollections = new Dictionary<string, List<ArtistCollection>>();

            ArtistCollections.Add("0001", new List<ArtistCollection> {
                new ArtistCollection {
                ExportDate = "10-12-2021",
                ArtistId = "1",
                CollectionId = "0001",
                IsPrimaryArtist = true,
                RoleId = ""
                }
            });

            ArtistCollections.Add("0002", new List<ArtistCollection> {
                new ArtistCollection {
                ExportDate = "10-12-2021",
                ArtistId = "2",
                CollectionId = "0002",
                IsPrimaryArtist = true,
                RoleId = "0"
                },
                new ArtistCollection {
                ExportDate = "10-12-2021",
                ArtistId = "3",
                CollectionId = "0003",
                IsPrimaryArtist = true,
                RoleId = "7"
                }
            });

            ArtistCollections.Add("0003", new List<ArtistCollection> {

            });

            ArtistCollections.Add("0004", new List<ArtistCollection> {
                new ArtistCollection {
                ExportDate = "10-12-2021",
                ArtistId = "4",
                CollectionId = "0004",
                IsPrimaryArtist = true,
                RoleId = "1"
                }
            });

            ArtistCollections.Add("0005", new List<ArtistCollection> {
                new ArtistCollection {
                ExportDate = "10-12-2021",
                ArtistId = "6",
                CollectionId = "0005",
                IsPrimaryArtist = true,
                RoleId = "1"
                }
            });


            IDictionary<string, List<Collection>> Collections = new Dictionary<string, List<Collection>>();
            Collections.Add("0001", new List<Collection> {
                new Collection {
                ExportDate = "06-20-2017",
                CollectionId = "0001",
                Name = "Album About Nothing",
                TitleVersions = "titleWale",
                SearchTerms = "hiphop/rap",
                ParentalAdvisory = "R",
                ArtistDisplayName = "Wale",
                ViewUrl ="https://wale.com",
                ArtworkUrl = "http://img.com/image/thumb/Music117/v4/92/b8/51/92b85100-13c8-8fa4-0856-bb27276fdf87/191061793557.jpg/170x170bb.jpg",
                OriginalReleaseDate = "06-25-2017",
                ITunesReleaseDate = "06-25-2017",
                LabelStudio = "Roc Nation",
                ContentProviderName = "",
                Copyright = "Wale inc",
                PLine = "234578698724",
                MediaTypeId = "mp3",
                IsCompilation = false,
                CollectionTypeId = "1"
                }
            });

            Collections.Add("0002", new List<Collection> {
                new Collection {
                ExportDate = "",
                CollectionId = "0002",
                Name = null,
                TitleVersions = null,
                SearchTerms = null,
                ParentalAdvisory = null,
                ArtistDisplayName = null,
                ViewUrl = null,
                ArtworkUrl = null,
                OriginalReleaseDate = null,
                ITunesReleaseDate = null,
                LabelStudio = null,
                ContentProviderName = null,
                Copyright = null,
                PLine = null,
                MediaTypeId = null,
                IsCompilation = false,
                CollectionTypeId = null
                }
            });

            Collections.Add("0003", new List<Collection> {
                new Collection {
                ExportDate = "10-12-2021",
                CollectionId = "0003",
                Name = "",
                TitleVersions = "",
                SearchTerms = "",
                ParentalAdvisory = "",
                ArtistDisplayName = "",
                ViewUrl ="",
                ArtworkUrl = "",
                OriginalReleaseDate = "",
                ITunesReleaseDate = "",
                LabelStudio = "",
                ContentProviderName = "",
                Copyright = "",
                PLine = "",
                MediaTypeId = "",
                IsCompilation = false,
                CollectionTypeId = ""
                }
            });

            Collections.Add("0004", new List<Collection> {
                new Collection {
                ExportDate = "10-12-2021",
                CollectionId = "0004",
                Name = "",
                TitleVersions = "",
                SearchTerms = "",
                ParentalAdvisory = "",
                ArtistDisplayName = "",
                ViewUrl ="",
                ArtworkUrl = "",
                OriginalReleaseDate = "",
                ITunesReleaseDate = "",
                LabelStudio = "",
                ContentProviderName = "",
                Copyright = "",
                PLine = "",
                MediaTypeId = "",
                IsCompilation = false,
                CollectionTypeId = ""
                }
            });

            Collections.Add("0005", new List<Collection> {
                new Collection {
                ExportDate = "10-12-2021",
                CollectionId = "0005",
                Name = "",
                TitleVersions = "",
                SearchTerms = "",
                ParentalAdvisory = "",
                ArtistDisplayName = "",
                ViewUrl ="",
                ArtworkUrl = "",
                OriginalReleaseDate = "",
                ITunesReleaseDate = "",
                LabelStudio = "",
                ContentProviderName = "",
                Copyright = "",
                PLine = "",
                MediaTypeId = "",
                IsCompilation = false,
                CollectionTypeId = ""
                }
            });


            IDictionary<string, List<CollectionMatch>> CollectionMatches = new Dictionary<string, List<CollectionMatch>>();
            CollectionMatches.Add("0001", new List<CollectionMatch> {
                new CollectionMatch {
                Upc = "423534523",
                CollectionId = "0001"
                }
            });

            CollectionMatches.Add("0002", new List<CollectionMatch> {
                new CollectionMatch {
                Upc = "347667",
                CollectionId = "0002"
                }
            });

            CollectionMatches.Add("0003", new List<CollectionMatch> {
                new CollectionMatch {
                Upc = "4769858679",
                CollectionId = "0003"
                }
            });

            CollectionMatches.Add("0004", new List<CollectionMatch> {
                new CollectionMatch {
                Upc = "3463456",
                CollectionId = "0004"
                }
            });

            CollectionMatches.Add("0005", new List<CollectionMatch> {
                new CollectionMatch {
                Upc = "768567865",
                CollectionId = "0005"
                }
            });




            data.Artists = Artists;
            data.CollectionMatches = CollectionMatches;
            data.ArtistCollections = ArtistCollections;
            data.Collections = Collections;
            return data;
        }
    }
}
