using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreJWT.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Founder")]
    public class ProductApiController : Controller
    {
        public JsonResult Get()
        {
            var products = new { Products = new[] { new { Id = 1, Name = "Shampoo" }, new { Id = 2, Name = "Panda Bearer" }, new { Id = 3, Name = "JWT Bearer" } } };

            return Json(products);
        }
    }
}