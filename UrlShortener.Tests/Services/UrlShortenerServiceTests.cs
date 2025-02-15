using Microsoft.Extensions.Configuration;
using Moq;
using UrlShortener.Repositories;
using UrlShortener.Services;
using Xunit;

namespace UrlShortener.Tests.Services
{
    public class UrlShortenerServiceTests
    {
        private readonly Mock<IUrlRepository> _urlRepositoryMock;
        private readonly UrlShortenerService _service;

        public UrlShortenerServiceTests()
        {
            _urlRepositoryMock = new Mock<IUrlRepository>();
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["UrlShortener:Domain"]).Returns("http://short.url");
            _service = new UrlShortenerService(_urlRepositoryMock.Object, configurationMock.Object);
        }

        [Fact]
        public void CreateShortUrl_ShouldReturnShortUrl()
        {
            var originalUrl = "http://example.com";
            _urlRepositoryMock.Setup(r => r.GetShortCode(originalUrl)).Returns((string)null);

            var result = _service.CreateShortUrl(originalUrl);

            Assert.StartsWith("http://short.url/", result);
        }

        [Fact]
        public void GetOriginalUrl_ShouldReturnOriginalUrl()
        {
            var shortCode = "abc123";
            var originalUrl = "http://example.com";
            _urlRepositoryMock.Setup(r => r.GetUrl(shortCode)).Returns(originalUrl);

            var result = _service.GetOriginalUrl(shortCode);

            Assert.Equal(originalUrl, result);
        }
    }
}
