using AutoMapper;
using Contracts;
using Entities.Exceptions;
using UrlManagementObj = Entities.Models.UrlManagement;
using Moq;
using Service;
using Shared.DataTransferObjects;

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
        public async Task CreateShortUrl_InvalidUrl_ThrowsException()
        {
            // Arrange
            string url = "invalid url";
        
            // Act & Assert
            await Assert.ThrowsAsync<NotValidUrlException>(() => _service.CreateShortUrlAsync(url));
        }

        [Fact]
        public async Task GetLongUrlAsync_ValidShortUrl_ReturnsUrlManagmentDto()
        {
            // Arrange
            string shortUrl = "abc123";
            var urlManagement = new UrlManagementObj { OriginalUrl = "https://www.example.com", ShortUrl = shortUrl };
            var urlManagmentDto = new UrlManagmentDto { OriginalUrl = "https://www.example.com", ShortUrl = shortUrl };

            _repositoryMock.Setup(r => r.UrlManagement.GetLongUrlAsync(shortUrl, false)).ReturnsAsync(urlManagement);
            _mapperMock.Setup(m => m.Map<UrlManagmentDto>(It.IsAny<UrlManagementObj>())).Returns(urlManagmentDto);

            // Act
            var result = await _service.GetLongUrlAsync(shortUrl, false);

            // Assert
            Assert.Equal(urlManagmentDto, result);
        }
    }
}
