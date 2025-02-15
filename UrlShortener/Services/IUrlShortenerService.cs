namespace UrlShortener.Services
{
    public interface IUrlShortenerService
    {
        string CreateShortUrl(string originalUrl);
        string? GetOriginalUrl(string shortCode);
    }
}
