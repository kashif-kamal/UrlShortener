using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using UrlShortener.Middleware;
using Xunit;

namespace UrlShortener.Tests.Middleware
{
    public class ExceptionMiddlewareTests
    {
        private readonly Mock<RequestDelegate> _nextMock;
        private readonly Mock<ILogger<ExceptionMiddleware>> _loggerMock;
        private readonly ExceptionMiddleware _middleware;

        public ExceptionMiddlewareTests()
        {
            _nextMock = new Mock<RequestDelegate>();
            _loggerMock = new Mock<ILogger<ExceptionMiddleware>>();
            _middleware = new ExceptionMiddleware(_nextMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task InvokeAsync_ShouldHandleException()
        {
            _nextMock.Setup(n => n(It.IsAny<HttpContext>())).Throws(new Exception("Test exception"));

            var context = new DefaultHttpContext();
            var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _middleware.InvokeAsync(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var response = await reader.ReadToEndAsync();

            Assert.Equal(500, context.Response.StatusCode);
            Assert.Contains("Internal Server Error from the custom middleware.", response);
        }
    }
}
