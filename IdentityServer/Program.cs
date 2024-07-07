using AuthApi.Class;

namespace IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddIdentityServer()
              .AddInMemoryApiResources(Config.GetApiResources())
              .AddInMemoryApiScopes(Config.GetApiScopes())
              .AddInMemoryClients(Config.GetClients())
              .AddDeveloperSigningCredential();
            //  builder.Services.AddControllersWithViews();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseRouting();
            app.UseStaticFiles(); //wwwroot'a eriþim için
            app.UseAuthentication(); //kimlik doðrulama için
            app.UseAuthorization(); //yetkilendirme için

            app.UseIdentityServer();

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