using System.Collections.Concurrent;

namespace UrlShortener.Repositories
{
    public class InMemoryUrlRepository : IUrlRepository
    {
        private readonly ConcurrentDictionary<string, string> _urlMappings = new();
        private readonly ConcurrentDictionary<string, string> _reverseUrlMappings = new(); // Reverse mapping

        public void AddUrl(string shortCode, string originalUrl)
        {
            _urlMappings[shortCode] = originalUrl;
            _reverseUrlMappings[originalUrl] = shortCode;
        }

        public string? GetUrl(string shortCode)
        {
            _urlMappings.TryGetValue(shortCode, out var originalUrl);
            return originalUrl;
        }

        public string? GetShortCode(string originalUrl)
        {
            _reverseUrlMappings.TryGetValue(originalUrl, out var shortCode);
            return shortCode;
        }
    }
}
