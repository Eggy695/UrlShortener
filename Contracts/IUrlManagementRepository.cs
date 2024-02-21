namespace Contracts
{
    using Entities.Models;

    public interface IUrlManagementRepository
    {
        void CreateShortUrl(UrlManagement longUrl);

        Task<UrlManagement> GetLongUrlAsync(string shortUrl, bool trackChanges);

    }
}
