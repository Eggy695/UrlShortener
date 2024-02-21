namespace Contracts
{
    using Entities.Models;

    public interface IUrlManagementRepository
    {
        IEnumerable<UrlManagement> GetAllUrls(bool trackChanges);

        void CreateShortUrl(UrlManagement longUrl);

        UrlManagement GetLongUrl(string shortUrl, bool trackChanges);

    }
}
