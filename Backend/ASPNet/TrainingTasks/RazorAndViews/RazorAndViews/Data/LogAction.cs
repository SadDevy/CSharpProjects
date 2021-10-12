using System;
using System.Reflection;
using Introduction.Data.LoggerProviders;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Introduction.Data
{
    public class LogAction : Attribute, IActionFilter
    {
        private readonly ILogger logger;
        private readonly bool isLogging;

        public LogAction(bool isLogging = false)
        {
            logger = new FileLogger("logger.txt");
            this.isLogging = isLogging;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!isLogging)
                return;

            logger.LogDebug($"Method: {MethodBase.GetCurrentMethod()}");
            logger.LogDebug($"Path:  {context.HttpContext.Request.Path}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!isLogging)
                return;

            logger.LogDebug($"Method: {MethodBase.GetCurrentMethod()}");
            logger.LogDebug($"Path:  {context.HttpContext.Request.Path}");
        }
    }
}
