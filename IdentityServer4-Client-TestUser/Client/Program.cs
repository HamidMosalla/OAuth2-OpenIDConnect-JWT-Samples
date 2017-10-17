using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();

            //program.GetResourceWithClientSecret();
            program.GetResouceWithUserPassword();

            Console.ReadLine();
        }

        public async Task GetResourceWithClientSecret()
        {
            var openIdConnectEndPoint = await DiscoveryClient.GetAsync("http://localhost:5000");
            var tokenClient = new TokenClient(openIdConnectEndPoint.TokenEndpoint, "DrFakhravari_Himself", "Resherper!");
            var accessToken = await tokenClient.RequestClientCredentialsAsync("PolymerApi");

            if (accessToken.IsError)
            {
                Console.WriteLine(accessToken.Error);
                return;
            }

            Console.WriteLine(accessToken.Json);

            Console.Clear();

            using (var client = new HttpClient())
            {
                client.SetBearerToken(accessToken.AccessToken);

                var response = await client.GetAsync("http://localhost:5001/api/identity");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                    Console.WriteLine(JArray.Parse(content));
                }
            }
        }

        public async Task GetResouceWithUserPassword()
        {
            var discoveryResponse = await DiscoveryClient.GetAsync("http://localhost:5000");
            // request token
            var tokenClient = new TokenClient(discoveryResponse.TokenEndpoint, "ro.DrFakhravari_Himself", "Resherper!");
            var accessToken = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "PolymerApi");

            if (accessToken.IsError)
            {
                Console.WriteLine(accessToken.Error);
                return;
            }

            Console.WriteLine(accessToken.Json);
            Console.WriteLine("\n\n");

            Console.Clear();

            using (var client = new HttpClient())
            {
                client.SetBearerToken(accessToken.AccessToken);

                var response = await client.GetAsync("http://localhost:5001/api/identity");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                    Console.WriteLine(JArray.Parse(content));
                }
            }

        }
    }
}