using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApiTemplate.Handlers
{
    /// <summary>
    /// A custom <cref="System.Net.Http.DelegatingHandler">DelegatingHandler</cref> used to add the correlation ID to the response headers
    /// </summary>
    public class AddCorrelationIdToResponseHandler : DelegatingHandler
    {
        private const string _correlationIdHeaderName = "X-Correlation-ID";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            response.Headers.Add(_correlationIdHeaderName, request.GetCorrelationId().ToString());

            return response;
        }
    }
}