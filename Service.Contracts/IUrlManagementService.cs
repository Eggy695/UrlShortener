using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IUrlManagementService
    {
        Task<UrlManagmentDto> GetLongUrlAsync(string shortUrl, bool trackChanges);

        Task<UrlManagmentDto> CreateShortUrlAsync(string url, string scheme, string host);

    }
}
