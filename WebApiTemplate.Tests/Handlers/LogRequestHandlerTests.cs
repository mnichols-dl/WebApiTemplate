using Moq;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Web;
using WebApiTemplate.Handlers;

namespace WebApiTemplate.Tests.Handlers
{
    [TestFixture]
    public class LogRequestHandlerTests
    {
        [Test]
        public void ShouldLogRequestHttpAction()
        {
            // Arrange
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://www.example.com/");

            Mock<log4net.ILog> mockLog = new Mock<log4net.ILog>();
            var handler = new LogRequestHandler(mockLog.Object)
            {
                InnerHandler = new TestDelegatingHandler()
            };
            var client = new HttpClient(handler);

            // Act
            var response = client.SendAsync(httpRequestMessage).Result;

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            mockLog.Verify(log => log.Debug("GET http://www.example.com/"), Times.Once);
        }

        [Test]
        public void ShouldAddCorrelationIdToLoggingContext()
        {
            // Arrange
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://www.example.com/");

            var mockHttpContext = new Mock<HttpContextBase>();
            mockHttpContext.SetupGet<string>(ctx => ctx.Request.UserHostAddress).Returns("127.0.0.1");
            httpRequestMessage.Properties["MS_HttpContext"] = mockHttpContext.Object;
            var correlationId = httpRequestMessage.GetCorrelationId();

            Mock<log4net.ILog> mockLog = new Mock<log4net.ILog>();
            var handler = new LogRequestHandler(mockLog.Object)
            {
                InnerHandler = new TestDelegatingHandler()
            };
            var client = new HttpClient(handler);

            // Act
            var response = client.SendAsync(httpRequestMessage).Result;

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(log4net.LogicalThreadContext.Properties["ip"], Is.EqualTo("127.0.0.1"));
            Assert.That(log4net.LogicalThreadContext.Properties["correlationId"], Is.Not.Null.And.EqualTo(correlationId));
        }
    }
}
