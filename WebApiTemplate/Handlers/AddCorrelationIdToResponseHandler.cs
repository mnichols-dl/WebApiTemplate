using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiTemplate.Handlers
{
    /// <summary>
    /// A custom <see cref="System.Net.Http.DelegatingHandler"/> used to add the correlation ID to the response headers
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