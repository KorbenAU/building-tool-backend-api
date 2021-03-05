using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.Business.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.API.Controllers
{
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly IBusiness _business;

        public ApiController(IBusiness business)
        {
            _business = business;
        }

        [HttpGet]
        [Route("sample")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "Sample APIs")]
        public IActionResult SampleApiCall()
        {
            return Ok(_business.WriteToDatabase());
        }

    }
}
