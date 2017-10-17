using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiResource.Controllers
{
    [Produces("application/json")]
    public class IdentityController : Controller
    {
        [HttpGet]
        [Authorize("Founder")]
        [Route("api/resource-with-policy")]
        public IActionResult ResourceWithPolicy()
        {
            return new JsonResult(new { FirstName = "Dr", LastName = "Fakhravari" });
        }

        [HttpGet]
        [Authorize]
        [Route("api/resource-without-policy")]
        public IActionResult ResourceWithoutPolicy()
        {
            return new JsonResult(new { FirstName = "Dr", LastName = "Fakhravari" });
        }
    }
}