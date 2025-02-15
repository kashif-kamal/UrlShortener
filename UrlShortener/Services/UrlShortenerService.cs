using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using UrlShortener.Repositories;

namespace UrlShortener.Services
{
    public class UrlShortenerService : IUrlShortenerService
    {
        private readonly IUrlRepository _urlRepository;
        private readonly string _domain;
        private static readonly Random Random = new();

        public UrlShortenerService(IUrlRepository urlRepository, IConfiguration configuration)
        {
            _urlRepository = urlRepository;
            _domain = configuration["UrlShortener:Domain"] ?? throw new ApplicationException("Domain not set");
        }

        public string CreateShortUrl(string originalUrl)
        {
            try
            {
                var existingShortCode = _urlRepository.GetShortCode(originalUrl);
                if (existingShortCode != null)
                {
                    return $"{_domain}/{existingShortCode}";
                }

                var shortCode = GenerateUniqueShortCode();
                _urlRepository.AddUrl(shortCode, originalUrl);
                return $"{_domain}/{shortCode}";
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                throw new ApplicationException("An error occurred while creating the short URL.", ex);
            }
        }

        public string? GetOriginalUrl(string shortCode)
        {
            try
            {
                return _urlRepository.GetUrl(shortCode);
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                throw new ApplicationException("An error occurred while retrieving the original URL.", ex);
            }
        }

        private string GenerateUniqueShortCode(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string shortCode;

            do
            {
                shortCode = new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[Random.Next(s.Length)]).ToArray());
            } while (_urlRepository.GetUrl(shortCode) != null);

            return shortCode;
        }
    }
}
