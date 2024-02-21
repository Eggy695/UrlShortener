using AutoMapper;
using Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUrlManagementService> _urlManagementService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _urlManagementService = new Lazy<IUrlManagementService>(() => new
            UrlManagementService(repositoryManager, logger, mapper));
        }

        public IUrlManagementService UrlManagementService => _urlManagementService.Value;
    }
    
}
