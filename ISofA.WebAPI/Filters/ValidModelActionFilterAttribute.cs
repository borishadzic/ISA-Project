using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ISofA.WebAPI.Filters
{
    public class ValidModelActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var ms = (actionContext.ControllerContext.Controller as ApiController).ModelState;
            if (ms.IsValid)
                throw new InvalidModelStateException();
        }

        //public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        //{
        //    var ms = (actionContext.ControllerContext.Controller as ApiController).ModelState;
            
        //    return base.OnActionExecutingAsync(actionContext, cancellationToken);
        //}
    }
}