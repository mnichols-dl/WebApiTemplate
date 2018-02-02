using System.Web.Http;
using WebApiTemplate.Filters;
using WebApiTemplate.Handlers;

namespace WebApiTemplate
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Add custom Filters and MessageHandlers
            config.Filters.Add(new LogUnhandledExceptionFilterAttribute(log4net.LogManager.GetLogger("Log")));
            config.MessageHandlers.Add(new AddCorrelationIdToResponseHandler());
            config.MessageHandlers.Add(new LogRequestHandler(log4net.LogManager.GetLogger("Log")));
        }
    }
}
