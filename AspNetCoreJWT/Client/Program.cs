using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:5000");

                var accessToken = await GetAccessToken();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.GetAsync("/api/Product");

                var formattedResponse = $"Status Code: {response.StatusCode}\nContent: {await response.Content.ReadAsStringAsync()}";

                Console.WriteLine(formattedResponse);
            }

            Console.ReadLine();
        }

        public static async Task<string> GetAccessToken()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:5000");

                var httpContent = new StringContent("{\"Email\": \"mosalla@gmail.com\", \"Password\": \"123654\"}", Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("/Token/Generate", httpContent);

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}