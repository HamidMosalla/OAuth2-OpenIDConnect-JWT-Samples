using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreJWT.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCoreJWT.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductApiController : Controller
    {
        public JsonResult Get()
        {
            return Json(new {Id = 1, Name = "Discovery Based Shampoo>!"});
        }
    }
}