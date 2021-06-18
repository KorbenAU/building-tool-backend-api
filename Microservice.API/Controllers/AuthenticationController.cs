using System;
using System.Linq;
using Microservice.API.Extensions;
using Microservice.API.Filters;
using Microservice.Business;
using Microservice.Business.Business;
using Microservice.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.API.Controllers
{
    [Route("api/authentication")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthentication _authentication;

        public AuthenticationController(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "Authentication")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                    ModelState.Values.SelectMany(
                        v => v.Errors.Select(
                            b => b.ErrorMessage)));

            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

#if DEBUG
            if (ipAddress == "::1")
                ipAddress = "127.0.0.1";
#endif

            var sessionIdentifier = _authentication.Login(model, ipAddress, out string errorMessage);

            if (sessionIdentifier == null)
                return BadRequest(errorMessage);

            return Ok(sessionIdentifier);
        }

        [HttpPost]
        [Route("logout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "Authentication")]
        [RequireSession]
        public IActionResult Logout()
        {
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

#if DEBUG
            if (ipAddress == "::1")
                ipAddress = "127.0.0.1";
#endif

            _authentication.Logout(
                HttpContext.Request.GetValueFromHeader(Constants.SessionHeaderName),
                ipAddress);

            return NoContent();
        }
    }
}