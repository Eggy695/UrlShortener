using Moq;
using UrlManagement.Presentation.Controllers;
using Service.Contracts;
using Microsoft.Extensions.Caching.Memory;
using Entities.Exceptions;

namespace UnitTestSuite
{
    public class UrlManagementControllerTests
    {
        private readonly Mock<IServiceManager> _mockService;
        private readonly Mock<IUrlManagementService> _urlManagementServiceMock;
        private readonly Mock<IMemoryCache> _cache;
        private readonly UrlManagementController _controller;

        public UrlManagementControllerTests()
        {
            _mockService = new Mock<IServiceManager>();
            _urlManagementServiceMock = new Mock<IUrlManagementService>();
            _cache = new Mock<IMemoryCache>();

            _mockService.SetupGet(x => x.UrlManagementService).Returns(_urlManagementServiceMock.Object);

            _controller = new UrlManagementController(_mockService.Object, _cache.Object);
        }

        [Fact]
        public async Task GetLongUrl_WhenUrlIsNull_ThrowsNotValidUrlException()
        {
            await Assert.ThrowsAsync<NotValidUrlException>(() => _controller.GetLongUrl(null));
        }

        [Fact]
        public async Task GetLongUrl_WhenUrlIsWhitespace_ThrowsNotValidUrlException()
        {
            await Assert.ThrowsAsync<NotValidUrlException>(() => _controller.GetLongUrl(" "));
        }

        [Fact]
        public async Task CreateShortUrl_WhenLongUrlIsNull_ThrowsNotValidUrlException()
        {
            await Assert.ThrowsAsync<NotValidUrlException>(() => _controller.CreateShortUrl(null));
        }

        [Fact]
        public async Task CreateShortUrl_WhenLongUrlIsWhitespace_ThrowsNotValidUrlException()
        {
            await Assert.ThrowsAsync<NotValidUrlException>(() => _controller.CreateShortUrl(" "));
        }

    }
}
