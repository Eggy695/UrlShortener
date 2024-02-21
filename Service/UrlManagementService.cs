using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.Text.RegularExpressions;

namespace Service
{  
    internal sealed class UrlManagementService : IUrlManagementService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public UrlManagementService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UrlManagmentDto> CreateShortUrlAsync(string url, string scheme, string host)
        {
            if (!IsValidLongUrl(url))
                throw new NotValidUrlException(url);

            var urlForShortUrlCreationDto = new UrlForShortUrlCreationDto
            {
                OriginalUrl = url,
                ShortUrl = CreateShortUrl()
            };

            var urlEntity = _mapper.Map<UrlManagement>(urlForShortUrlCreationDto);

            _repository.UrlManagement.CreateShortUrl(urlEntity);
            await _repository.SaveAsync();

            var urlToReturn = _mapper.Map<UrlManagmentDto>(urlEntity);

            if (urlToReturn.ShortUrl != null)
            {
                urlToReturn.ShortUrl = ConstructShortUrlWithDomain(urlToReturn.ShortUrl, scheme, host);
            }

            return urlToReturn;
        }

        public async Task<UrlManagmentDto> GetLongUrlAsync(string shortUrl, bool trackChanges)
        { 

            // will have to strip the domain and only pass everything after the last / to the repository
            var longUrl = await _repository.UrlManagement.GetLongUrlAsync(shortUrl, trackChanges);

            if (longUrl is null)
                throw new ShortUrlNotFoundException(shortUrl);

            var longUrlDto = _mapper.Map<UrlManagmentDto>(longUrl);

            return longUrlDto;
        }

        private bool IsValidLongUrl(string url)
        {
            string strRegex = @"((http|https)://)(www.)?" +
                "[a-zA-Z0-9@:%._\\+~#?&//=]" +
                "{2,256}\\.[a-z]" +
                "{2,6}\\b([-a-zA-Z0-9@:%" +
                "._\\+~#?&//=]*)";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(url))
                return (true);
            else
                return (false);
        }

        private string CreateShortUrl()
        {
            // create a short version of the url
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var randomString = new string(Enumerable.Repeat(chars, 7)
                .Select(x => x[random.Next(x.Length)]).ToArray());

            return randomString;
        }

        private string ConstructShortUrlWithDomain(string shortUrl, string scheme, string host)
        {
            return $"{scheme}://{host}/{shortUrl}";
        }
    }
   
}
