using Microsoft.AspNetCore.Mvc;
using Moq;
using UrlShortener.Controllers;
using UrlShortener.Model;
using UrlShortener.Services;
using Xunit;

namespace UrlShortener.Tests.Controllers
{
    public class UrlShortenerControllerTests
    {
        private readonly Mock<IUrlShortenerService> _urlShortenerServiceMock;
        private readonly UrlShortenerController _controller;

        public UrlShortenerControllerTests()
        {
            _urlShortenerServiceMock = new Mock<IUrlShortenerService>();
            _controller = new UrlShortenerController(_urlShortenerServiceMock.Object);
        }

        [Fact]
        public void CreateShortUrl_ShouldReturnCreatedResult()
        {
            var request = new CreateShortUrlRequest { OriginalUrl = "http://example.com" };
            _urlShortenerServiceMock.Setup(s => s.CreateShortUrl(request.OriginalUrl)).Returns("http://short.url/abc123");

            var result = _controller.CreateShortUrl(request) as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public void GetShortUrl_ShouldRedirectToOriginalUrl()
        {
            var shortCode = "abc123";
            var originalUrl = "http://example.com";
            _urlShortenerServiceMock.Setup(s => s.GetOriginalUrl(shortCode)).Returns(originalUrl);

            var result = _controller.GetShortUrl(shortCode) as RedirectResult;

            Assert.NotNull(result);
            Assert.Equal(originalUrl, result.Url);
        }
    }
}
