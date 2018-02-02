using System.Collections.Generic;
using NUnit.Framework;
using WebApiTemplate.Handlers;
using System.Net.Http;
using System.Net;

namespace WebApiTemplate.Tests.Handlers
{
    [TestFixture]
    public class AddCorrelationIdToResponseHandlerTests
    {
        [Test]
        public void TestDelegatingHandlerShouldNotAddCorrelationIdToResponseHeaders()
        {
            // Arrange
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://www.example.com");

            var handler = new TestDelegatingHandler();

            var client = new HttpClient(handler);

            // Act
            var response = client.SendAsync(httpRequestMessage).Result;
            IEnumerable<string> correlationHeaders;
            response.Headers.TryGetValues("X-Correlation-ID", out correlationHeaders);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(correlationHeaders, Is.Null);
        }

        [Test]
        public void ShouldAddCorrelationIdToResponseHeaders()
        {
            // Arrange
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://www.example.com");

            var handler = new AddCorrelationIdToResponseHandler()
            {
                InnerHandler = new TestDelegatingHandler()
            };

            var client = new HttpClient(handler);

            // Act
            var response = client.SendAsync(httpRequestMessage).Result;
            IEnumerable<string> correlationHeaders;
            response.Headers.TryGetValues("X-Correlation-ID", out correlationHeaders);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(correlationHeaders, Is.Not.Null.Or.Empty);
        }
    }
}
