using JwtToken.Util;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtToken.Filters
{
    public class LogActionWebApiFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Log.Information(string.Format("{0}.{1} to be process", context.Controller.ToString(), context.HttpContext.Request.ToUri().ToString()));
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Log.Information(string.Format("{0}.{1} has been processed", context.Controller.ToString(), context.HttpContext.Request.ToUri().ToString()));
            base.OnActionExecuted(context);
        }
    }
}
