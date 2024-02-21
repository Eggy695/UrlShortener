namespace UrlManagement.Presentation.Controllers
{
    using Asp.Versioning;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.OutputCaching;
    using Service.Contracts;

    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/url-shortner")]
    [ApiController]
    public class UrlManagementController : ControllerBase
    {
        private readonly IServiceManager _service;

        public UrlManagementController(IServiceManager service) => _service = service;


        [HttpGet("id/{*url}", Name = "GetLongUrl")]
        public async Task<IActionResult> GetLongUrl([FromRoute] string url)
        {
            var urlObject = await _service.UrlManagementService.GetLongUrlAsync(url, false);

            if (urlObject == null || string.IsNullOrEmpty(urlObject.OriginalUrl))
            {
                return BadRequest("URL object is null or original URL is empty");
            }

            return Redirect(urlObject.OriginalUrl);
        }

        [HttpPost(Name = "CreateShortUrl")]
        public async Task<IActionResult> CreateShortUrl([FromBody] string longUrl)
        {
            if (string.IsNullOrWhiteSpace(longUrl))
                return BadRequest("URL object is null");

            var createdShortUrl = await _service.UrlManagementService.CreateShortUrlAsync(longUrl);

            return Ok(createdShortUrl.ShortUrl);
        }

    }
}
