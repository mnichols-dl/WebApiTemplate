using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApiTemplate
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private readonly log4net.ILog log = log4net.LogManager.GetLogger("Application_Error");

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException() ?? new Exception("Unable to retrieve exception details");
            Server.ClearError();
            log.Error("Unhandled exception", ex);
        }
    }
}
