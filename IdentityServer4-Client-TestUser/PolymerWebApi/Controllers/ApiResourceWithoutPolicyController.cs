using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PolymerWebApi.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/ApiResourceWithoutPolicy")]
    public class ApiResourceWithoutPolicyController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(new { ResourceType = "Without Policy", ResourceName = "Api1" });
        }
    }
}