using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiResource.Controllers
{
    [Produces("application/json")]
    [Authorize("Founder")]
    [Route("api/ApiResourceWithPolicy")]
    public class ApiResourceWithPolicyController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(new { ResourceType = "With Policy", ResourceName = "Api2" });
        }
    }
}