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
        static void Main(string[] args)
        {
            var response = Task.Run(()=> MainAsync()).Result;

            Console.WriteLine(response);

            Console.ReadLine();
        }

        public static async Task<string> MainAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:5000");

                var httpContent = new StringContent("{Email: \"mosalla@gmail.com\", Password: \"123654\", RememberMe: \"true\"}", Encoding.UTF8, "application/json");

                var responseToken = await httpClient.PostAsync("/Token/Generate", httpContent);

                var accessToken = await responseToken.Content.ReadAsStringAsync();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var productResponse = await httpClient.GetAsync("/api/Product");

                var product = await productResponse.Content.ReadAsStringAsync();

                return product;
            }
        }
    }
}