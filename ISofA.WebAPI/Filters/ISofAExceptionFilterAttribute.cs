using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace ISofA.WebAPI.Filters
{

    [Serializable]
    public class InvalidModelStateException : Exception
    {
        public InvalidModelStateException() { }
    }

    public class ISofAExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is InvalidModelStateException)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, context.ActionContext.ModelState);
            }         
        }

    }
}