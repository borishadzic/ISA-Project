using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Properties;
using System.Web.Http;
using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Claims;

namespace ISofA.WebAPI.Filters
{
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "We want to support extensibility")]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class ISofAAuthorizationAttribute : AuthorizationFilterAttribute
    {

        private readonly object _typeId = new object();

        private ISofAUserRole _role = ISofAUserRole.User;
        
        public ISofAUserRole Role { get { return _role; } set { _role = value; } }

        /// <summary>
        /// Gets a unique identifier for this <see cref="T:System.Attribute"/>.
        /// </summary>
        /// <returns>The unique identifier for the attribute.</returns>
        public override object TypeId {
            get { return _typeId; }
        }

        /// <summary>
        /// Determines whether access for this particular request is authorized. This method uses the user <see cref="IPrincipal"/>
        /// returned via <see cref="HttpRequestContext.Principal"/>. Authorization is denied if the user is not authenticated,
        /// the user is not in the authorized group of <see cref="Users"/> (if defined), or if the user is not in any of the authorized 
        /// <see cref="Roles"/> (if defined).
        /// </summary>
        /// <param name="actionContext">The context.</param>
        /// <returns><c>true</c> if access is authorized; otherwise <c>false</c>.</returns>
        protected virtual bool IsAuthorized(HttpActionContext actionContext)
        {
            ClaimsPrincipal user = (ClaimsPrincipal)actionContext.ControllerContext.RequestContext.Principal;
            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return false;
            }

            if (_role == ISofAUserRole.User)
                return true;
            
            ISofAUserRole userRole = (ISofAUserRole)int.Parse(user.FindFirst(ISofAClaimTypes.ISofAUserRole).Value);

            if (userRole == ISofAUserRole.SysAdmin)
                return true;

            if (_role == ISofAUserRole.SysAdmin)
                return false;

            int theaterId = Convert.ToInt32(actionContext.ControllerContext.RouteData.Values["theaterId"]);
            int userAdminOf = int.Parse(user.FindFirst(ISofAClaimTypes.ISofAAdminOf).Value);

            if ((int)userRole > (int)_role && userAdminOf == theaterId)
                return true;

            return false;            
        }

        /// <summary>
        /// Called when an action is being authorized. This method uses the user <see cref="IPrincipal"/>
        /// returned via <see cref="HttpRequestContext.Principal"/>. Authorization is denied if
        /// - the request is not associated with any user.
        /// - the user is not authenticated,
        /// - the user is authenticated but is not in the authorized group of <see cref="Users"/> (if defined), or if the user
        /// is not in any of the authorized <see cref="Roles"/> (if defined).
        /// 
        /// If authorization is denied then this method will invoke <see cref="HandleUnauthorizedRequest(HttpActionContext)"/> to process the unauthorized request.
        /// </summary>
        /// <remarks>You can use <see cref="AllowAnonymousAttribute"/> to cause authorization checks to be skipped for a particular
        /// action or controller.</remarks>
        /// <seealso cref="IsAuthorized(HttpActionContext)" />
        /// <param name="actionContext">The context.</param>
        /// <exception cref="ArgumentNullException">The context parameter is null.</exception>
        public override void OnAuthorization(HttpActionContext actionContext)
        {

            if (SkipAuthorization(actionContext))
            {
                return;
            }

            if (!IsAuthorized(actionContext))
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }

        /// <summary>
        /// Processes requests that fail authorization. This default implementation creates a new response with the
        /// Unauthorized status code. Override this method to provide your own handling for unauthorized requests.
        /// </summary>
        /// <param name="actionContext">The context.</param>
        protected virtual void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "FanZone Admins Only");
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            Contract.Assert(actionContext != null);

            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                   || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }

    }
}