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

        public async Task<UrlManagmentDto> CreateShortUrlAsync(string url)
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

            return urlToReturn;
        }

        public async Task<UrlManagmentDto> GetLongUrlAsync(string shortUrl, bool trackChanges)
        {
            var longUrl = await _repository.UrlManagement.GetLongUrlAsync(shortUrl, trackChanges);

            if (longUrl is null)
                throw new ShortUrlNotFoundException(shortUrl);

            var longUrlDto = _mapper.Map<UrlManagmentDto>(longUrl);

            return longUrlDto;
        }

        private bool IsValidLongUrl(string url)
        {
            // Url starts with "http://" or "https://", optionally followed by "www.".
            // Then it expects any combination of alphanumeric characters or special characters like @:%._\+~#?&//=.
            // This part should be between 2 and 256 characters long and followed by a dot.
            // After the dot, it expects 2 to 6 lowercase letters, which represent the domain extension like .com, .org, etc.
            // Finally, it can end with any combination of alphanumeric characters or special characters like @:%._\+~#?&//=.
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
            // Create a short version of the url
            // 7 characters in base62 will generate roughly ~3500 Billion URLs. 62 to the power of 7 base 62 are [0–9][a-z][A-Z]
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var randomString = new string(Enumerable.Repeat(chars, 7)
                .Select(x => x[random.Next(x.Length)]).ToArray());

            return randomString;
        }
    }
   
}
