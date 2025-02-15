using Microsoft.Extensions.Configuration;
using Moq;
using System;
using UrlShortener.Repositories;
using UrlShortener.Services;
using Xunit;

namespace UrlShortener.Tests.Services
{
    public class UrlShortenerServiceExceptionTests
    {
        private readonly Mock<IUrlRepository> _urlRepositoryMock;
        private readonly UrlShortenerService _service;

        public UrlShortenerServiceExceptionTests()
        {
            _urlRepositoryMock = new Mock<IUrlRepository>();
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["UrlShortener:Domain"]).Returns("http://short.url");
            _service = new UrlShortenerService(_urlRepositoryMock.Object, configurationMock.Object);
        }

        [Fact]
        public void CreateShortUrl_ShouldThrowApplicationException_WhenRepositoryThrowsException()
        {
            var originalUrl = "http://example.com";
            _urlRepositoryMock.Setup(r => r.GetShortCode(originalUrl)).Throws(new Exception("Repository exception"));

            var exception = Assert.Throws<ApplicationException>(() => _service.CreateShortUrl(originalUrl));

            Assert.Equal("An error occurred while creating the short URL.", exception.Message);
        }

        [Fact]
        public void GetOriginalUrl_ShouldThrowApplicationException_WhenRepositoryThrowsException()
        {
            var shortCode = "abc123";
            _urlRepositoryMock.Setup(r => r.GetUrl(shortCode)).Throws(new Exception("Repository exception"));

            var exception = Assert.Throws<ApplicationException>(() => _service.GetOriginalUrl(shortCode));

            Assert.Equal("An error occurred while retrieving the original URL.", exception.Message);
        }
    }
}
