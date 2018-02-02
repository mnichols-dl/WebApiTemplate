using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiTemplate.Tests.Handlers
{
    public class TestDelegatingHandler : DelegatingHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunction;

        public TestDelegatingHandler()
        {
            _handlerFunction = (request, cancellationToken) => ReturnSuccess();
        }

        public TestDelegatingHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunction) {
            _handlerFunction = handlerFunction;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _handlerFunction(request, cancellationToken);
        }

        public static Task<HttpResponseMessage> ReturnSuccess()
        {
            return Task.Factory.StartNew(() => new HttpResponseMessage(System.Net.HttpStatusCode.OK));
        }
    }
}
