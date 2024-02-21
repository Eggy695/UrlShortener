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

        [HttpGet]
        public IActionResult GetUrls() 
        {   
            var urls = _service.UrlManagementService.GetAllUrls(false);
            
            return Ok(urls);           
        }

        [HttpGet("id/{shortUrl}", Name = "GetLongUrl")]
        public IActionResult GetLongUrl(string shortUrl)
        {
            var urls = _service.UrlManagementService.GetLongUrl(shortUrl, false);

            return Ok(urls.OriginalUrl);
        }

        [HttpPost(Name = "CreateShortUrl")]
        public IActionResult CreateShortUrl([FromBody]string longUrl)
        {
            if (string.IsNullOrWhiteSpace(longUrl))
                return BadRequest("URL object is null");

            var createdShortUrl = _service.UrlManagementService.CreateShortUrl(longUrl, HttpContext.Request.Scheme, HttpContext.Request.Host.ToString());
            return CreatedAtRoute("CreateShortUrl", new { id = createdShortUrl.ShortUrl },
            createdShortUrl);
        }

    }
}
