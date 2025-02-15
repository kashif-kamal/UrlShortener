using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Model;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlShortenerController : ControllerBase
    {
        private readonly IUrlShortenerService _urlShortenerService;

        public UrlShortenerController(IUrlShortenerService urlShortenerService)
        {
            _urlShortenerService = urlShortenerService;
        }

        // POST: api/UrlShortener
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateShortUrl([FromBody] CreateShortUrlRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shortCode = _urlShortenerService.CreateShortUrl(request.OriginalUrl);
            var shortUrl = $"http://short.url/{shortCode}";

            return CreatedAtAction(nameof(GetShortUrl), new { code = shortCode }, new { shortUrl });
        }

        // GET: api/UrlShortener/{code}
        [HttpGet("{code}")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetShortUrl(string code)
        {
            var originalUrl = _urlShortenerService.GetOriginalUrl(code);
            if (originalUrl != null)
            {
                return Redirect(originalUrl);
            }

            return NotFound();
        }
    }
}
