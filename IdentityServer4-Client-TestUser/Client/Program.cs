using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //var requestWithoutPolicyResponse = Task.Run(RequestWithClientCredentialsWithoutPolicy).Result;
            //var requestWithClientCredetials = Task.Run(RequestWithClientCredentialsWithPolicy).Result;
            var requestWithResourceOwnerPassword = Task.Run(RequestWithResourceOwnerPasswordWithPolicy).Result;

            //Console.WriteLine($"{nameof(requestWithoutPolicyResponse)} : {requestWithoutPolicyResponse}");
            //Console.WriteLine($"{nameof(requestWithClientCredetials)} : {requestWithClientCredetials}");
            Console.WriteLine($"{nameof(requestWithResourceOwnerPassword)} : {requestWithResourceOwnerPassword}");

            Console.ReadLine();
        }

        public static async Task<string> RequestWithClientCredentialsWithoutPolicy()
        {
            async Task<string> GetAccessToken()
            {
                var openIdConnectEndPoint = await DiscoveryClient.GetAsync("http://localhost:5000");
                var tokenClient = new TokenClient(openIdConnectEndPoint.TokenEndpoint, "client1", "123654");
                var accessToken = await tokenClient.RequestClientCredentialsAsync("Api1");

                if (accessToken.IsError)
                {
                    Console.WriteLine(accessToken.Error);
                    return accessToken.Error;
                }

                Console.WriteLine(accessToken.Json);

                return accessToken.AccessToken;
            }

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
            async Task<string> GetAccessToken()
            {
                var openIdConnectEndPoint = await DiscoveryClient.GetAsync("http://localhost:5000");
                var tokenClient = new TokenClient(openIdConnectEndPoint.TokenEndpoint, "client1", "123654");
                var accessToken = await tokenClient.RequestClientCredentialsAsync("Api1");

                if (accessToken.IsError)
                {
                    Console.WriteLine(accessToken.Error);
                    return accessToken.Error;
                }

                Console.WriteLine(accessToken.Json);

                return accessToken.AccessToken;
            }

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
            async Task<string> GetAccessToken()
            {
                var discoveryResponse = await DiscoveryClient.GetAsync("http://localhost:5000");
                // request token
                var tokenClient = new TokenClient(discoveryResponse.TokenEndpoint, "ro.client1", "123654");
                var accessToken = await tokenClient.RequestResourceOwnerPasswordAsync("mosalla", "password", "Api1");

                if (accessToken.IsError)
                {
                    Console.WriteLine(accessToken.Error);
                    return accessToken.Error;
                }

                Console.WriteLine(accessToken.Json);

                return accessToken.AccessToken;
            }

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
    }
}