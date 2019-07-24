using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreOperations.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;

namespace WebApplication1
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<adventureworksContext>(options =>
                options.UseSqlServer("Server=tcp:adventureworksstaging.database.windows.net,1433;Initial Catalog=adventureworks;Persist Security Info=False;User ID=Umesh;Password=!@#Complex123;MultipleActiveResultSets=False;Encrypt=True;" +
                "TrustServerCertificate=False;Connection Timeout=30;"));


            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<adventureworksContext>();

            IdentityModelEventSource.ShowPII = true;

            services.AddAuthentication(options => {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            }).AddCookie("Cookies")
              .AddOpenIdConnect("oidc",options => {
                  options.SignInScheme = "Cookies";
                  options.Authority = "https://localhost:5001";
                  options.ClientId = "CoreClient";
                  options.ResponseType ="code id_token";
                  options.Scope.Add("openid");
                  options.Scope.Add("profile");
                  options.SaveTokens = true;
                  options.ClientSecret = "secret";
              })
            ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseStaticFiles();
           
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Register}/{id?}");
            });

        }
    }
}
