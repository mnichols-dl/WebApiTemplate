using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using WebApiTemplate.Filters;

namespace WebApiTemplate.Tests.Filters
{
    [TestFixture]
    public class LogUnhandledExceptionFilterAttributeTests
    {
        [Test]
        public void ShouldLogUnhandledException()
        {
            // Arrange
            var mockLog = new Mock<log4net.ILog>();
            var exception = new Exception("Test Exception");
            var actionExecutedContext = new HttpActionExecutedContext();
            actionExecutedContext.Exception = exception;
            var attribute = new LogUnhandledExceptionFilterAttribute(mockLog.Object);

            // Act
            attribute.OnException(actionExecutedContext);

            // Assert
            mockLog.Verify(log => log.Error("Unhandled Exception", exception), Times.Once);
        }
    }
}
