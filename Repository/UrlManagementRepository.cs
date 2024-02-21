namespace Repository
{
    using Contracts;
    using Entities.Exceptions;
    using Entities.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class UrlManagementRepository : RepositoryBase<UrlManagement>, IUrlManagementRepository
    {
        public UrlManagementRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) 
        { 
        }

        public void CreateShortUrl(UrlManagement longUrl) => Create(longUrl);

        public async Task<UrlManagement> GetLongUrlAsync(string shortUrl, bool trackChanges)
        {
            if (string.IsNullOrEmpty(shortUrl))
            {
                throw new NotValidUrlException(shortUrl);
            }

            var urlManagement = await FindByCondition(expression: c => c.ShortUrl != null && c.ShortUrl.Equals(shortUrl), trackChanges)
                     .SingleOrDefaultAsync();

            if (urlManagement == null)
            {
                throw new LongUrlNotFoundException(shortUrl);
            }

            return urlManagement;
        }
    }
}
