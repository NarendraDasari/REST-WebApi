using Autofac.Integration.WebApi;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace Api.Web.Extensions
{
    public class AppExceptionLogger : ExceptionLogger
    {
        //TODO Need to inject the ILog - Dependency

        private ILog logger = LogManager.GetLogger("Api Logger");
        public override void Log(ExceptionLoggerContext context)
        {
            logger.ErrorFormat("Exception Had Occured {3}Request Uri: {0}{3}Message: {1}{3}StackTrace: {2}", context.Request.RequestUri.AbsoluteUri, context.Exception.Message, context.Exception.StackTrace,Environment.NewLine);
           // base.Log(context);
        }
    }
}