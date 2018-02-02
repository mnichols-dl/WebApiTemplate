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
    /// A <cref="System.Net.Http.DelegatingHandler">DelegatingHandler</cref> used to log incoming HTTP requests and add request properties to logging context
    /// </summary>
    public class LogRequestHandler : DelegatingHandler
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("Requests");
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // LogicalThreadContext uses CallContext to hold properties, so these properties will be accessible
            // for the duration of the call across any number of threads

            // Add correlation ID from request to logging context
            var correlationId = request.GetCorrelationId();
            log4net.LogicalThreadContext.Properties["correlationId"] = correlationId;

            // Add IP address to logging context if able to resolve from HttpContext
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                var httpContext = request.Properties["MS_HttpContext"] as HttpContextWrapper;
                if (httpContext != null)
                {
                    log4net.LogicalThreadContext.Properties["ip"] = httpContext.Request.UserHostAddress;
                }
            }
            
            log.Debug($"{request.Method.Method} {request.RequestUri}");
            return base.SendAsync(request, cancellationToken);
        }
    }
}