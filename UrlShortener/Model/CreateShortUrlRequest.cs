using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Model
{
    public class CreateShortUrlRequest
    {
        [Required]
        [Url]
        public string OriginalUrl { get; set; }
    }
}
