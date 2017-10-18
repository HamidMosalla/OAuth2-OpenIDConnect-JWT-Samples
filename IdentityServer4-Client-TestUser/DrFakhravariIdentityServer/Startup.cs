using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrFakhravariIdentityServer.Configurations;
using DrFakhravariIdentityServer.IdentityServerConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DrFakhravariIdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddInMemoryApiResources(Config.GetApiResources())
                    .AddInMemoryClients(Config.GetClients())
                    .AddTestUsers(Config.GetUsers())
                    .AddProfileService<ProfileService>();

            services.AddAuthorization(options => options.AddPolicy("Founder", policy => policy.RequireClaim("Employee", "Mosalla")));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
        }
    }
}