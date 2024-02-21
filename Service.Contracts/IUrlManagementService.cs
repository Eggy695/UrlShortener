using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IUrlManagementService
    {
        IEnumerable<UrlManagmentDto> GetAllUrls(bool trackChanges);

        UrlManagmentDto GetLongUrl(string shortUrl, bool trackChanges);

        UrlManagmentDto CreateShortUrl(string url, string scheme, string host);

    }
}
