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
    [Authorize("Founder")]
    [Route("api/Identity")]
    public class IdentityController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(new { FirstName = "Dr", LastName = "Fakhravari" });
        }
    }
}