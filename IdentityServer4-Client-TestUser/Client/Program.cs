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
            var withoutPolicyReponse = Task.Run(MainAsyncWithClientSecretWithoutPolicy).Result;
            var withPolicyReponse = Task.Run(MainAsyncWithUserPasswordWithPolicy).Result;

            Console.WriteLine(withoutPolicyReponse);
            Console.WriteLine(withPolicyReponse);

            Console.ReadLine();
        }

        public static async Task<string> MainAsyncWithClientSecretWithoutPolicy()
        {
            async Task<string> GetAccessTokenForMainAsyncWithClientSecretWithoutPolicy()
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
                var accessToken = await GetAccessTokenForMainAsyncWithClientSecretWithoutPolicy();

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
        public static async Task<string> MainAsyncWithUserPasswordWithPolicy()
        {
            async Task<string> GetAccessTokenForMainAsyncWithUserPasswordWithPolicy()
            {
                var discoveryResponse = await DiscoveryClient.GetAsync("http://localhost:5000");
                // request token
                var tokenClient = new TokenClient(discoveryResponse.TokenEndpoint, "ro.client1", "123654");
                var accessToken = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "Api1");

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
                var accessToken = await GetAccessTokenForMainAsyncWithUserPasswordWithPolicy();

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