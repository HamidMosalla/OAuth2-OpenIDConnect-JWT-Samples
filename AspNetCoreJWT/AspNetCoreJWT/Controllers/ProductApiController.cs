using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreJWT.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trusted")]
    public class ProductApiController : Controller
    {
        public OkObjectResult Get()
        {
            var products = new { Products = new[] { new { Id = 1, Name = "Normal Bear" }, new { Id = 2, Name = "Panda Bearer" }, new { Id = 3, Name = "JWT Bearer" } } };

            return Ok(products);
        }
    }
}