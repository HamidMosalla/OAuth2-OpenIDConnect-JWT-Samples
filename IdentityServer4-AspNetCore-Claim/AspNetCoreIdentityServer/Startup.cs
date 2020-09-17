using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreIdentityServer.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AspNetCoreIdentityServer.Data;
using AspNetCoreIdentityServer.Models;
using AspNetCoreIdentityServer.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

namespace AspNetCoreIdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
                {
                    option.Password.RequireDigit = false;
                    option.Password.RequiredLength = 3;
                    option.Password.RequiredUniqueChars = 0;
                    option.Password.RequireLowercase = false;
                    option.Password.RequireNonAlphanumeric = false;
                    option.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryApiScopes(Config.GetApiScopes())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<ProfileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();
            app.UseDatabaseErrorPage();

            app.UseStaticFiles();

            CreateDatabaseAndAddUser(app);

            app.UseRouting();

            //UseAuthentication not needed, since UseIdentityServer adds the authentication middleware
            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(opt =>
            {
                opt.MapDefaultControllerRoute();
            });
        }

        private static void CreateDatabaseAndAddUser(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.Migrate();

                if (context.Users.Any()) return;

                var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                var user = new ApplicationUser
                {
                    UserName = "mosalla@gmail.com",
                    Email = "mosalla@gmail.com",
                    EmailConfirmed = true,
                };

                userManager.CreateAsync(user, "123654").GetAwaiter().GetResult();

                var claim = new Claim("Employee", "Mosalla");

                userManager.AddClaimAsync(user, claim).GetAwaiter().GetResult();
            }
        }
    }
}
