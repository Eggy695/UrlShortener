namespace Repository
{
    using Contracts;
    using Entities.Models;
    using System.Collections.Generic;

    public class UrlManagementRepository : RepositoryBase<UrlManagement>, IUrlManagementRepository
    {
        public UrlManagementRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) 
        { 
        }

        public IEnumerable<UrlManagement> GetAllUrls(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(c => c.ShortUrl)            .ToList();

        public void CreateShortUrl(UrlManagement longUrl) => Create(longUrl);

        public UrlManagement GetLongUrl(string shortUrl, bool trackChanges) =>
          FindByCondition(expression: c => c.ShortUrl.Equals(shortUrl),
                          trackChanges)
            .SingleOrDefault();
    }
}
