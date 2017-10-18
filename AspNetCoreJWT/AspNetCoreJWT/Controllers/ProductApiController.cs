using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreJWT.Models.AccountViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCoreJWT.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductApiController : Controller
    {
        public JsonResult Get()
        {
            var products = new { Products = new[] { new { Id = 1, Name = "Shampoo" }, new { Id = 2, Name = "Panda Bearer" }, new { Id = 3, Name = "JWT Bearer" } } };

            return Json(products);
        }
    }
}