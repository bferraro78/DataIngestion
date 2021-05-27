using Microsoft.Extensions.Logging;
using Moq;
using MediaData.Models;
using MediaData.Services.DataReader.DataHandlers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MediaDataUnitTests.Handlers
{
    public class ArtistCollectionDataHandlerTest
    {
        public Mock<ILoggerFactory> loggerFactory;

        public ArtistCollectionDataHandlerTest()
        {
            var mockLogger = new Mock<ILogger<ArtistCollectionDataHandler>>();
            mockLogger.Setup(
                m => m.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.IsAny<object>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()));
            loggerFactory = new Mock<ILoggerFactory>();
            loggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(() => mockLogger.Object);
        }

        [Theory]
        [InlineData("1473969404", "10459")]
        [InlineData("1549232495", "10440")]

        public void TestDataHandle_Success(string key, string artistId)
        {
            Task<IDictionary<string, List<ArtistCollection>>> readTask = new ArtistCollectionDataHandler(loggerFactory.Object.CreateLogger<ArtistCollectionDataHandler>()).Handle("\\..\\..\\..\\TestData\\artistCollectionGoodTestData.txt");
            Task.WhenAll(readTask);
            var artistDataModel = new ArtistDataModel();
            artistDataModel.ArtistCollections = readTask.Result;
            Assert.True(artistDataModel.ArtistCollections.Count > 0);
            Assert.True(artistDataModel.ArtistCollections.ContainsKey(key));
            Assert.Equal(artistDataModel.ArtistCollections[key][0].ArtistId, artistId);
            if (key.Equals("1549232495"))
            { 
                Assert.Equal(artistDataModel.ArtistCollections[key][1].ArtistId, artistId);
            }
        }

        [Theory]
        [InlineData("1084361780")]
        public void TestDataHandle_SuccessNotFullLine(string key)
        {
            Task<IDictionary<string, List<ArtistCollection>>> readTask = new ArtistCollectionDataHandler(loggerFactory.Object.CreateLogger<ArtistCollectionDataHandler>()).Handle("\\..\\..\\..\\TestData\\artistCollectionGoodTestData.txt");
            Task.WhenAll(readTask);
            var artistDataModel = new ArtistDataModel();
            artistDataModel.ArtistCollections = readTask.Result;
            Assert.True(artistDataModel.ArtistCollections.Count > 0);
            Assert.True(artistDataModel.ArtistCollections.ContainsKey(key));
            Assert.True(artistDataModel.ArtistCollections[key][0].ArtistId == null);
        }

        [Fact]
        public void TestDataHandle_NoData()
        {
            Task<IDictionary<string, List<ArtistCollection>>> readTask = new ArtistCollectionDataHandler(loggerFactory.Object.CreateLogger<ArtistCollectionDataHandler>()).Handle("\\..\\..\\..\\TestData\\artistCollectionEmptyTestData.txt");
            Task.WhenAll(readTask);
            var artistDataModel = new ArtistDataModel();
            artistDataModel.ArtistCollections = readTask.Result;
            Assert.True(artistDataModel.ArtistCollections != null);
            Assert.True(artistDataModel.ArtistCollections.Count == 0);
        }

        /// <summary>
        /// Null Exceptions and Bad formated lines when running through data entry
        /// </summary>
        [Theory]
        [InlineData("1549050644")]
        public void TestDataHandle_BadFormat(string key)
        {
            Task<IDictionary<string, List<ArtistCollection>>> readTask = new ArtistCollectionDataHandler(loggerFactory.Object.CreateLogger<ArtistCollectionDataHandler>()).Handle("\\..\\..\\..\\TestData\\artistCollectionBadFormatTestData.txt");
            Task.WhenAll(readTask);
            var artistDataModel = new ArtistDataModel();
            artistDataModel.ArtistCollections = readTask.Result;
            Assert.True(artistDataModel.ArtistCollections.Count > 0);
            Assert.True(!artistDataModel.ArtistCollections.ContainsKey(key));
        }
    }
}
