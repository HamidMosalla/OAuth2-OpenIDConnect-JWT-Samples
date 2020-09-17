using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using IdentityModel.Client;

namespace MvcClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize("Founder")]
        public IActionResult Secure()
        {
            ViewData["Message"] = "Secure page.";

            return View();
        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> CallApiUsingUserAccessToken()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            // ApiResource
            var content = await client.GetStringAsync("https://localhost:44383/api/resource-with-policy");

            return View("Json", content);
        }

        public async Task<IActionResult> CallApiUsingClientCredentials()
        {
            // AspNetCoreIdentityServer
            var httpClient = new HttpClient();

            var openIdConnectEndPoint = await httpClient.GetDiscoveryDocumentAsync("https://localhost:44384");

            var accessToken = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = openIdConnectEndPoint.TokenEndpoint,
                ClientId = "mvc",
                ClientSecret = "secret",
                Scope = "Api1",
            });

            var client = new HttpClient();
            client.SetBearerToken(accessToken.AccessToken);
            // ApiResource
            var content = await client.GetStringAsync("https://localhost:44383/api/resource-without-policy");

            return View("Json", content);
        }
    }
}