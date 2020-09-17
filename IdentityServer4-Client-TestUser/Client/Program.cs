using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            /*
                More info: http://hamidmosalla.com/2017/10/19/policy-based-authorization-using-asp-net-core-2-and-json-web-token-jwt/
            */

            var requestWithoutPolicyResponse = await RequestWithClientCredentialsWithoutPolicy();
            var requestWithClientCredetials = await RequestWithClientCredentialsWithPolicy();
            var requestWithResourceOwnerPassword = await RequestWithResourceOwnerPasswordWithPolicy();

            Console.WriteLine($"{nameof(requestWithoutPolicyResponse)} : {requestWithoutPolicyResponse}");
            Console.WriteLine($"{nameof(requestWithClientCredetials)} : {requestWithClientCredetials}");
            Console.WriteLine($"{nameof(requestWithResourceOwnerPassword)} : {requestWithResourceOwnerPassword}");

            Console.ReadLine();
        }

        private static async Task<string> GetAccessToken()
        {
            var httpClient = new HttpClient();

            var openIdConnectEndPoint = await httpClient.GetDiscoveryDocumentAsync("http://localhost:5000");

            var accessToken = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = openIdConnectEndPoint.TokenEndpoint,
                ClientId = "client1",
                ClientSecret = "123654",
                Scope = "api1.read",
            });

            if (accessToken.IsError)
            {
                Console.WriteLine(accessToken.Error);
                return accessToken.Error;
            }

            Console.WriteLine(accessToken.Json);

            return accessToken.AccessToken;
        }

        private static async Task<string> GetAccessTokenPasswordTokenRequest()
        {
            var httpClient = new HttpClient();

            var openIdConnectEndPoint = await httpClient.GetDiscoveryDocumentAsync("http://localhost:5000");

            PasswordTokenRequest passwordTokenRequest = new PasswordTokenRequest()
            {
                Address = openIdConnectEndPoint.TokenEndpoint,
                ClientId = "ro.client1",
                ClientSecret = "123654",
                GrantType = OidcConstants.GrantTypes.AuthorizationCode,
                Scope = "Api1",
                UserName = "mosalla",
                Password = "password"
            };

            var accessToken = await httpClient.RequestPasswordTokenAsync(passwordTokenRequest);

            if (accessToken.IsError)
            {
                Console.WriteLine(accessToken.Error);
                return accessToken.Error;
            }

            Console.WriteLine(accessToken.Json);

            return accessToken.AccessToken;
        }

        public static async Task<string> RequestWithClientCredentialsWithoutPolicy()
        {
            using (var client = new HttpClient())
            {
                var accessToken = await GetAccessToken();

                client.SetBearerToken(accessToken);

                var response = await client.GetAsync("http://localhost:5001/api/ApiResourceWithoutPolicy");

                if (!response.IsSuccessStatusCode)
                {
                    return response.StatusCode.ToString();
                }

                var content = await response.Content.ReadAsStringAsync();

                return content;
            }
        }

        public static async Task<string> RequestWithClientCredentialsWithPolicy()
        {
            using (var client = new HttpClient())
            {
                var accessToken = await GetAccessToken();

                client.SetBearerToken(accessToken);

                var response = await client.GetAsync("http://localhost:5001/api/ApiResourceWithPolicy");

                if (!response.IsSuccessStatusCode)
                {
                    return response.StatusCode.ToString();
                }

                var content = await response.Content.ReadAsStringAsync();

                return content;
            }
        }

        public static async Task<string> RequestWithResourceOwnerPasswordWithPolicy()
        {
            using (var client = new HttpClient())
            {
                var accessToken = await GetAccessTokenPasswordTokenRequest();

                client.SetBearerToken(accessToken);

                var response = await client.GetAsync("http://localhost:5001/api/ApiResourceWithPolicy");

                if (!response.IsSuccessStatusCode)
                {
                    return response.StatusCode.ToString();
                }

                var content = await response.Content.ReadAsStringAsync();

                return content;
            }
        }
    }
}