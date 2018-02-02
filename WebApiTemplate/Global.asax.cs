using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApiTemplate
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("Application_Error");

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
            Exception ex = sender as Exception ?? Server.GetLastError().GetBaseException() ?? new Exception("Unable to retrieve exception details");

            log.Error("Unhandled exception", ex);
        }
    }
}
