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
    public class CollectionMatchDataHandlerTests
    {
        public Mock<ILoggerFactory> loggerFactory;

        public CollectionMatchDataHandlerTests()
        {
            var mockLogger = new Mock<ILogger<CollectionMatchDataHandler>>();
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
        [InlineData("1547000792", "195918210822")]
        [InlineData("1534034368", "0616293468627")]

        public void TestDataHandle_Success(string key, string upc)
        {
            Task<IDictionary<string, List<CollectionMatch>>> readTask = new CollectionMatchDataHandler(loggerFactory.Object.CreateLogger<CollectionMatchDataHandler>()).Handle("\\..\\..\\..\\TestData\\collectionMatchGoodTestData.txt");
            Task.WhenAll(readTask);
            var artistDataModel = new MediaDataModel();
            artistDataModel.CollectionMatches = readTask.Result;
            Assert.True(artistDataModel.CollectionMatches.Count > 0);
            Assert.True(artistDataModel.CollectionMatches.ContainsKey(key));
            Assert.Equal(artistDataModel.CollectionMatches[key][0].Upc, upc);
        }

        [Theory]
        [InlineData("1438539952")]
        public void TestDataHandle_SuccessNotFullLine(string key)
        {
            Task<IDictionary<string, List<CollectionMatch>>> readTask = new CollectionMatchDataHandler(loggerFactory.Object.CreateLogger<CollectionMatchDataHandler>()).Handle("\\..\\..\\..\\TestData\\collectionMatchGoodTestData.txt");
            Task.WhenAll(readTask);
            var artistDataModel = new MediaDataModel();
            artistDataModel.CollectionMatches = readTask.Result;
            Assert.True(artistDataModel.CollectionMatches.Count > 0);
            Assert.True(artistDataModel.CollectionMatches.ContainsKey(key));
            Assert.True(artistDataModel.CollectionMatches[key][0].Upc == null);
        }

        [Fact]
        public void TestDataHandle_NoData()
        {
            Task<IDictionary<string, List<CollectionMatch>>> readTask = new CollectionMatchDataHandler(loggerFactory.Object.CreateLogger<CollectionMatchDataHandler>()).Handle("\\..\\..\\..\\TestData\\collectionMatchEmptyTestData.txt");
            Task.WhenAll(readTask);
            var artistDataModel = new MediaDataModel();
            artistDataModel.CollectionMatches = readTask.Result;
            Assert.True(artistDataModel.CollectionMatches != null);
            Assert.True(artistDataModel.CollectionMatches.Count == 0);
        }

        /// <summary>
        /// Null Exceptions and Bad formated lines when running through data entry
        /// </summary>
        [Theory]
        [InlineData("17407065")]
        public void TestDataHandle_BadFormat(string key)
        {
            Task<IDictionary<string, List<CollectionMatch>>> readTask = new CollectionMatchDataHandler(loggerFactory.Object.CreateLogger<CollectionMatchDataHandler>()).Handle("\\..\\..\\..\\TestData\\collectionMatchBadFormatTestData.txt");
            Task.WhenAll(readTask);
            var artistDataModel = new MediaDataModel();
            artistDataModel.CollectionMatches = readTask.Result;
            Assert.True(artistDataModel.CollectionMatches.Count > 0);
            Assert.True(!artistDataModel.CollectionMatches.ContainsKey(key));
        }
    }
}
