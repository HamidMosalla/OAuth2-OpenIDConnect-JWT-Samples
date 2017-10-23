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
            return new JsonResult(new { ApiName = "Api1", AuthorizationType = "With Policy" });
        }

        [HttpGet]
        [Authorize]
        [Route("api/resource-without-policy")]
        public IActionResult ResourceWithoutPolicy()
        {
            return new JsonResult(new { ApiName = "Api1", AuthorizationType = "Without Policy" });
        }
    }
}