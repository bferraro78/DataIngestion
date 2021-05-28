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
    public class CollectionDataHandlerTests
    {
        public Mock<ILoggerFactory> loggerFactory;

        public CollectionDataHandlerTests()
        {
            var mockLogger = new Mock<ILogger<CollectionDataHandler>>();
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
        [InlineData("1439161680", "Ранила - Single")]
        [InlineData("1438150752", "Невыносимо - Single")]

        public void TestDataHandle_Success(string key, string name)
        {
            Task<IDictionary<string, List<Collection>>> readTask = new CollectionDataHandler(loggerFactory.Object.CreateLogger<CollectionDataHandler>()).Handle("\\..\\..\\..\\TestData\\collectionGoodTestData.txt");
            Task.WhenAll(readTask);
            var artistDataModel = new MediaDataModel();
            artistDataModel.Collections = readTask.Result;
            Assert.True(artistDataModel.Collections.Count > 0);
            Assert.True(artistDataModel.Collections.ContainsKey(key));
            Assert.Equal(artistDataModel.Collections[key][0].Name, name);
        }

        [Fact]
        public void TestDataHandle_NoData()
        {
            Task<IDictionary<string, List<Collection>>> readTask = new CollectionDataHandler(loggerFactory.Object.CreateLogger<CollectionDataHandler>()).Handle("\\..\\..\\..\\TestData\\collectionEmptyTestData.txt");
            Task.WhenAll(readTask);
            var artistDataModel = new MediaDataModel();
            artistDataModel.Collections = readTask.Result;
            Assert.True(artistDataModel.Collections != null);
            Assert.True(artistDataModel.Collections.Count == 0);
        }

        /// <summary>
        /// Null Exceptions and Bad formated lines when running through data entry
        /// </summary>
        [Theory]
        [InlineData("1547000792")]
        public void TestDataHandle_BadFormat(string key)
        {
            Task<IDictionary<string, List<Collection>>> readTask = new CollectionDataHandler(loggerFactory.Object.CreateLogger<CollectionDataHandler>()).Handle("\\..\\..\\..\\TestData\\collectionBadFormatTestData.txt");
            Task.WhenAll(readTask);
            var artistDataModel = new MediaDataModel();
            artistDataModel.Collections = readTask.Result;
            Assert.True(artistDataModel.Collections.Count > 0);
            Assert.True(!artistDataModel.Collections.ContainsKey(key));
        }
    }
}
