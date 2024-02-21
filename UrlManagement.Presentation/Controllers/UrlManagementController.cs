namespace UrlManagement.Presentation.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Service.Contracts;

    [Route("api/url-shortner")]
    [ApiController]
    public class UrlManagementController : ControllerBase
    {
        private readonly IServiceManager _service;

        public UrlManagementController(IServiceManager service) => _service = service;

        [HttpGet("id/{shortUrl}", Name = "GetLongUrl")]
        public async Task<IActionResult> GetLongUrl(string shortUrl)
        {
            var urlObject = await _service.UrlManagementService.GetLongUrlAsync(shortUrl, false);

            if (urlObject == null || string.IsNullOrEmpty(urlObject.OriginalUrl))
            {
                return BadRequest("URL object is null or original URL is empty");
            }

            return Redirect(urlObject.OriginalUrl);
        }

        [HttpPost(Name = "CreateShortUrl")]
        public async Task<IActionResult> CreateShortUrl([FromBody]string longUrl)
        {
            if (string.IsNullOrWhiteSpace(longUrl))
                return BadRequest("URL object is null");

            var createdShortUrl = await _service.UrlManagementService.CreateShortUrlAsync(longUrl, HttpContext.Request.Scheme, HttpContext.Request.Host.ToString());
            
            return CreatedAtRoute("CreateShortUrl", new { id = createdShortUrl.ShortUrl },
            createdShortUrl);
        }

    }
}
