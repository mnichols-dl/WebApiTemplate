using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace WebApiTemplate.Filters
{
    /// <summary>
    /// A custom <see cref="System.Web.Http.Filters.ExceptionFilterAttribute"/> used to log unhandled exceptions from Web API controllers
    /// </summary>
    public class LogUnhandledExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly log4net.ILog _log;

        public LogUnhandledExceptionFilterAttribute(log4net.ILog log)
        {
            _log = log;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            _log.Error("Unhandled Exception", actionExecutedContext.Exception);
            base.OnException(actionExecutedContext);
        }
    }
}