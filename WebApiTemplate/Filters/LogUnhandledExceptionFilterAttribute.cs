using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace WebApiTemplate.Filters
{
    /// <summary>
    /// A custom <cref="System.Web.Http.Filters.ExceptionFilterAttribute">ExceptionFilterAttribute</cref> used to log unhandled exceptions
    /// </summary>
    public class LogUnhandledExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("UnhandledExceptions");

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            log.Error("Unhandled Exception", actionExecutedContext.Exception);
            base.OnException(actionExecutedContext);
        }
    }
}