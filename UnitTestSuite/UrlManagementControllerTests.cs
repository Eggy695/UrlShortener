using Moq;
using UrlManagement.Presentation.Controllers;
using Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestSuite
{
    public class UrlManagementControllerTests
    {
        private readonly Mock<IServiceManager> _mockService;
        private readonly UrlManagementController _controller;

        public UrlManagementControllerTests()
        {
            _mockService = new Mock<IServiceManager>();
            _controller = new UrlManagementController(_mockService.Object);
        }

        [Fact]
        public async void GetLongUrl_WhenCalledWithValidShortUrl_ReturnsOkResult()
        {
            // Arrange
            var shortUrl = "abc123";
            var longUrl = "https://www.example.com";
          // await _mockService.Setup(service => service.UrlManagementService.GetLongUrlAsync(shortUrl, false))
             //   .Returns(new Shared.DataTransferObjects.UrlManagmentDto { ShortUrl = shortUrl, OriginalUrl = longUrl });

            // Act
            var result = _controller.GetLongUrl(shortUrl);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(longUrl, okResult.Value);
        }

        [Fact]
        public void GetLongUrl_WhenCalledWithInvalidShortUrl_ReturnsBadRequest()
        {
            // Arrange
            var shortUrl = "invalid";
            //_mockService.Setup(service => service.UrlManagementService.GetLongUrlAsync(shortUrl, false))
           //     .Returns(null as Shared.DataTransferObjects.UrlManagmentDto);

            // Act
            var result = _controller.GetLongUrl(shortUrl);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("URL object is null", badRequestResult.Value);
        }

        [Fact]
        public void CreateShortUrl_WhenCalledWithNull_ReturnsBadRequest()
        {
            // Arrange
            string? longUrl = null;

            // Act
            var result = _controller.CreateShortUrl(longUrl);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void CreateShortUrl_WhenCalledWithValidUrl_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            string longUrl = "http://example.com";
            var shortUrl = new Shared.DataTransferObjects.UrlManagmentDto { OriginalUrl = longUrl, ShortUrl = "abc123" };
            //_mockService.Setup(s => s.UrlManagementService.CreateShortUrl(longUrl, It.IsAny<string>(), It.IsAny<string>())).Returns(shortUrl);

            // Act
            var result = _controller.CreateShortUrl(longUrl);

            // Assert
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal(shortUrl, createdAtRouteResult.Value);
            Assert.Equal("CreateShortUrl", createdAtRouteResult.RouteName);
            Assert.Equal(shortUrl.ShortUrl, createdAtRouteResult.RouteValues["id"]?.ToString());
        }
    }
}
