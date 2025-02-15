using UrlShortener.Repositories;
using Xunit;

namespace UrlShortener.Tests.Repositories
{
    public class InMemoryUrlRepositoryTests
    {
        private readonly InMemoryUrlRepository _repository;

        public InMemoryUrlRepositoryTests()
        {
            _repository = new InMemoryUrlRepository();
        }

        [Fact]
        public void AddUrl_ShouldStoreUrl()
        {
            var shortCode = "abc123";
            var originalUrl = "http://example.com";

            _repository.AddUrl(shortCode, originalUrl);

            var result = _repository.GetUrl(shortCode);
            Assert.Equal(originalUrl, result);
        }

        [Fact]
        public void GetUrl_ShouldReturnNullForNonExistentCode()
        {
            var result = _repository.GetUrl("nonexistent");
            Assert.Null(result);
        }
    }
}
