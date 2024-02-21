namespace Repository
{
    using Contracts;

    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IUrlManagementRepository> _urlManagementRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _urlManagementRepository = new Lazy<IUrlManagementRepository>(() => new UrlManagementRepository(repositoryContext));
        }

        public IUrlManagementRepository UrlManagement => _urlManagementRepository.Value;
        public void Save() => _repositoryContext.SaveChanges();

    }
}
