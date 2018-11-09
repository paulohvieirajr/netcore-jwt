using JwtToken.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtToken.Filters
{
    public class ExceptionWebApiFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //Log.Error(string.Format("Error processing {0}.", context.HttpContext.Request.ToUri().ToString()), context.Exception);

            context.HttpContext.Response.StatusCode = 500;
            context.Result = new JsonResult(new GenericResponse<string>() { sucess = false, error_message = $"Contate o administrador. {context.Exception.Message}" });

            base.OnException(context);
        }
    }
}
