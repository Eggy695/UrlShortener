namespace UrlManagement.Presentation.Controllers
{
    using Asp.Versioning;
    using Entities.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Service.Contracts;

    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("shorturl/")]
    [ApiController]
    public class UrlManagementController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly IMemoryCache _cache;

        public UrlManagementController(IServiceManager service, IMemoryCache memoryCache)
        {
            _service = service;
            _cache = memoryCache;
        }

        [HttpGet("{*url}", Name = "GetLongUrl")]
        public async Task<IActionResult> GetLongUrl([FromRoute] string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new NotValidUrlException(url);

            var urlObject = await _cache.GetOrCreateAsync(url, async entry =>
            {
                // Set cache to remove entry after 5 minutes of inactivity
                entry.SlidingExpiration = TimeSpan.FromMinutes(5);
                return await _service.UrlManagementService.GetLongUrlAsync(url, false);
            });

            if (urlObject == null || string.IsNullOrWhiteSpace(urlObject.OriginalUrl))
            {
                throw new LongUrlNotFoundException($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.Path}{url}");
            }

            return RedirectPreserveMethod(urlObject.OriginalUrl);
        }

        [HttpPost(Name = "CreateShortUrl")]
        public async Task<IActionResult> CreateShortUrl([FromBody] string longUrl)
        {
            if (string.IsNullOrWhiteSpace(longUrl))
                throw new NotValidUrlException(longUrl);

            var createdShortUrl = await _service.UrlManagementService.CreateShortUrlAsync(longUrl);

            return Ok($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.Path}{createdShortUrl.ShortUrl}");
        }

    }
}
