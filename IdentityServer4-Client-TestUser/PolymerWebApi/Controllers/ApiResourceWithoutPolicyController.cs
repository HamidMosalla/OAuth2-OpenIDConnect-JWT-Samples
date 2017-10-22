using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiResource.Controllers
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