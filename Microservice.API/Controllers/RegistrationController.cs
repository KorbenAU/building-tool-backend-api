using System.Linq;
using Microservice.Business.Business;
using Microservice.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.API.Controllers
{
    [Route("api/registration")]
    public class RegistrationController : Controller
    {
         private readonly IRegistration _registration;
 
         public RegistrationController(IRegistration registration)
         {
             _registration = registration;
         }
 
         [HttpPost]
         [Route("register")]
         [ProducesResponseType(StatusCodes.Status204NoContent)]
         [ProducesResponseType(StatusCodes.Status400BadRequest)]
         [ProducesResponseType(StatusCodes.Status500InternalServerError)]
         [ApiExplorerSettings(GroupName = "Registration")]
         public IActionResult Register([FromBody] RegisterModel model)
         {
             if (!ModelState.IsValid)
                 return BadRequest(
                     ModelState.Values.SelectMany(
                         v => v.Errors.Select(
                             b => b.ErrorMessage)));
 
             var ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
 
 #if DEBUG
             if (ipAddress == "::1") ipAddress = "127.0.0.1";
 #endif
             
             var register = _registration.Register(model, ipAddress, out string error);
 
             if (register == null)
                 return BadRequest(error);
 
             return NoContent();
         }       
    }
}