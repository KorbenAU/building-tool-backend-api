using Microservice.API.Extensions;
using Microservice.Business;
using Microservice.Business.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Microservice.API.Filters
{
    public class RequireSessionActionFilter : IAuthorizationFilter
    {
        private readonly ISessionManagement _sessionManagement;

        public RequireSessionActionFilter(ISessionManagement sessionManagement)
        {
            _sessionManagement = sessionManagement;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var sessionId = context.HttpContext.Request.GetValueFromHeader(Constants.SessionHeaderName);

            var ipAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();

#if DEBUG
            if (ipAddress == "::1")
                ipAddress = "127.0.0.1";
#endif

            var isValid = _sessionManagement.IsSessionValid(sessionId, ipAddress);

            if (!isValid)
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
        }
    }
    
    public class RequireSessionAttribute : TypeFilterAttribute
    {
        public RequireSessionAttribute()
            : base(typeof(RequireSessionActionFilter))
        {
        }
    }
}