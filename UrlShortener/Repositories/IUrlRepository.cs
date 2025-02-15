namespace UrlShortener.Repositories
{
    public interface IUrlRepository
    {
        void AddUrl(string shortCode, string originalUrl);
        string? GetUrl(string shortCode);
        string? GetShortCode(string originalUrl);
    }
}

