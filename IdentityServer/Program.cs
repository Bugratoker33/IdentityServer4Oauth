using AuthApi.Class;
using IdentityServer.Data;
using IdentityServer.Models;
using IdentityServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static IdentityServer4.IdentityServerConstants;


//services.AddIdentity<ApplicationUser, IdentityRole>()
//               .AddEntityFrameworkStores<ApplicationDbContext>()
//               .AddDefaultTokenProviders();

//var builder = services.AddIdentityServer(options =>
//{
//    options.Events.RaiseErrorEvents = true;
//    options.Events.RaiseInformationEvents = true;
//    options.Events.RaiseFailureEvents = true;
//    options.Events.RaiseSuccessEvents = true;

//    // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
//    options.EmitStaticAudienceClaim = true;
//})
//    .AddInMemoryIdentityResources(Config.IdentityResources)
//    .AddInMemoryApiResources(Config.ApiResources)
//    .AddInMemoryApiScopes(Config.ApiScopes)
//    .AddInMemoryClients(Config.Clients)
//    .AddAspNetIdentity<ApplicationUser>();


namespace IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddLocalApiAuthentication();

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            })
              .AddInMemoryIdentityResources(Config.GetIdentityResources())
              .AddInMemoryApiResources(Config.GetApiResources())
              .AddInMemoryApiScopes(Config.GetApiScopes())
              .AddInMemoryClients(Config.GetClients())
              .AddAspNetIdentity<ApplicationUser>()              
              .AddDeveloperSigningCredential();
            //builder.Services.AddAuthentication()
            //    .AddGoogle(options =>
            //    {
            //        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

            //        // register your IdentityServer with Google at https://console.developers.google.com
            //        // enable the Google+ API
            //        // set the redirect URI to https://localhost:5001/signin-google
            //        options.ClientId = "copy client ID from Google here";
            //        options.ClientSecret = "copy client secret from Google here";
            //    });
            builder.Services.AddAuthentication();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles(); //wwwroot'a eriþim için
            
            app.UseRouting();
            
            app.UseIdentityServer();
            
            app.UseAuthentication(); //kimlik doðrulama için
            
            app.UseAuthorization(); //yetkilendirme için

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute(); //url rotasý için
            });
            //app.UseStaticFiles();

            //   app.UseRouting();

            //  app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}