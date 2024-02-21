using AutoMapper;
using Contracts;
using Entities.Exceptions;
using UrlManagementObj = Entities.Models.UrlManagement;
using Moq;
using Service;
using Shared.DataTransferObjects;
using System;
using Xunit;

namespace UnitTestSuite
{
    public class UrlManagementServiceTest
    {
        private readonly Mock<IRepositoryManager> _repositoryMock;
        private readonly Mock<ILoggerManager> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UrlManagementService _service;

        public UrlManagementServiceTest()
    
        {
            _repositoryMock = new Mock<IRepositoryManager>();
            _loggerMock = new Mock<ILoggerManager>();
            _mapperMock = new Mock<IMapper>();
            _service = new UrlManagementService(_repositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async void CreateShortUrl_ValidUrl_ReturnsShortUrl()
        {
            // Arrange
            string url = "http://www.example.com";
            string scheme = "http";
            string host = "localhost";
            _mapperMock.Setup(m => m.Map<UrlManagementObj>(It.IsAny<UrlForShortUrlCreationDto>())).Returns(new UrlManagementObj());
            _mapperMock.Setup(m => m.Map<UrlManagmentDto>(It.IsAny<UrlManagementObj>())).Returns(new UrlManagmentDto() { ShortUrl = url }); ;

            // Act
            var result = await _service.CreateShortUrlAsync(url);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ShortUrl);
        }

        [Fact]
        public async Task GetLongUrl_ValidShortUrl_ReturnsLongUrlAsync()
        {
            // Arrange
            string shortUrl = "test";
            //_repositoryMock.Setup(r => r.UrlManagement.GetLongUrlAsync(shortUrl, false)).Returns(new UrlManagementObj());
            _mapperMock.Setup(m => m.Map<UrlManagmentDto>(It.IsAny<UrlManagementObj>())).Returns(new UrlManagmentDto() { OriginalUrl = "http://www.test.ai" });


            // Act
            var result = await _service.GetLongUrlAsync(shortUrl, false);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.OriginalUrl);
        }

        [Fact]
        public async Task CreateShortUrl_InvalidUrl_ThrowsException()
        {
            // Arrange
            string url = "invalid url";
        
            // Act & Assert
            await Assert.ThrowsAsync<NotValidUrlException>(() => _service.CreateShortUrlAsync(url));
        }

        [Fact]
        public async Task GetLongUrl_InvalidShortUrl_ThrowsException()
        {
            // Arrange
            string shortUrl = "invalid";

            // Act & Assert
            await Assert.ThrowsAsync<ShortUrlNotFoundException>(() => _service.GetLongUrlAsync(shortUrl, false));
        }
    }
}
