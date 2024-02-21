using Moq;
using AutoMapper;
using Contracts;
using Service;

namespace UnitTestSuite
{
    public class ServiceManagerTest
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly Mock<ILoggerManager> _mockLoggerManager;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ServiceManager _serviceManager;

        public ServiceManagerTest()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockLoggerManager = new Mock<ILoggerManager>();
            _mockMapper = new Mock<IMapper>();
            _serviceManager = new ServiceManager(_mockRepositoryManager.Object, _mockLoggerManager.Object, _mockMapper.Object);
        }

        [Fact]
        public void UrlManagementService_ShouldReturnNotNull()
        {
            var result = _serviceManager.UrlManagementService;
            Assert.NotNull(result);
        }
    }
}
