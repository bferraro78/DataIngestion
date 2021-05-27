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
    public class ArtistHandlerTests
    {

        public Mock<ILoggerFactory> loggerFactory;

        public ArtistHandlerTests()
        {
            var mockLogger = new Mock<ILogger<ArtistDataHandler>>();
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
        [InlineData("754259", "Mariss Jansons")]
        [InlineData("875728", "Clay Blaker")]
        [InlineData("15986478", "John Alexander")]

        public void TestDataHandle_Success(string key, string name)
        {
            Task<IDictionary<string, List<Artist>>> readTask = new ArtistDataHandler(loggerFactory.Object.CreateLogger<ArtistDataHandler>()).Handle("\\..\\..\\..\\TestData\\artistGoodTestData.txt");
            Task.WhenAll(readTask);
            var artistDataModel = new ArtistDataModel();
            artistDataModel.Artists = readTask.Result;
            Assert.True(artistDataModel.Artists.Count > 0);
            Assert.True(artistDataModel.Artists.ContainsKey(key));
            Assert.Equal(artistDataModel.Artists[key][0].Name, name);
        }

        [Fact]
        public void TestDataHandle_NoData()
        {
            Task<IDictionary<string, List<Artist>>> readTask = new ArtistDataHandler(loggerFactory.Object.CreateLogger<ArtistDataHandler>()).Handle("\\..\\..\\..\\TestData\\artistEmptyTestData.txt");
            Task.WhenAll(readTask);
            var artistDataModel = new ArtistDataModel();
            artistDataModel.Artists = readTask.Result;
            Assert.True(artistDataModel.Artists != null);
            Assert.True(artistDataModel.Artists.Count == 0);
        }

        /// <summary>
        /// Null Exceptions and Bad formated lines when running through data entry
        /// </summary>
        [Theory]
        [InlineData("17407065")]
        public void TestDataHandle_BadFormat(string key) 
        {
            Task<IDictionary<string, List<Artist>>> readTask = new ArtistDataHandler(loggerFactory.Object.CreateLogger<ArtistDataHandler>()).Handle("\\..\\..\\..\\TestData\\artistBadFormatTestData.txt");
            Task.WhenAll(readTask);
            var artistDataModel = new ArtistDataModel();
            artistDataModel.Artists = readTask.Result;
            Assert.True(artistDataModel.Artists.Count > 0);
            Assert.True(!artistDataModel.Artists.ContainsKey(key));
        }
    }
}
