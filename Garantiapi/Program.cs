using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Garantiapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



           builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
               {
                   //Token'ý yayýnlayan Auth Server adresi bildiriliyor. Yani yetkiyi daðýtan mekanizmanýn adresi bildirilerek ilgili API ile iliþkilendiriliyor.
                   options.MetadataAddress = "https://localhost:1000/.well-known/openid-configuration"; // Doðrudan metadata adresini belirtin
                   //Auth Server uygulamasýndaki 'Garanti' isimli resource ile bu API iliþkilendiriliyor.
                   options.Audience = "Garanti";
               });
            builder.Services.AddAuthorization(_ =>
            {
                _.AddPolicy("ReadGaranti", policy => policy.RequireClaim("scope", "Garanti.Read"));
                _.AddPolicy("WriteGaranti", policy => policy.RequireClaim("scope", "Garanti.Write"));
                _.AddPolicy("ReadWriteGaranti", policy => policy.RequireClaim("scope", "Garanti.Write", "Garanti.Read"));
                _.AddPolicy("AllGaranti", policy => policy.RequireClaim("scope", "Garanti.Admin"));
                _.AddPolicy("ReadHalkBank", policy => policy.RequireClaim("scope", "HalkBank.Read"));
                _.AddPolicy("WriteHalkBank", policy => policy.RequireClaim("scope", "HalkBank.Write"));
                _.AddPolicy("ReadWriteHalkBank", policy => policy.RequireClaim("scope", "HalkBank.Write", "HalkBank.Read"));
                _.AddPolicy("AllHalkBank", policy => policy.RequireClaim("scope", "HalkBank.Admin"));
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
          //  app.UseIdentityServer(); // Add IdentityServer middleware
            app.UseAuthentication(); // Add Authentication middleware
            app.UseAuthorization();  // Add Authorization middleware



            app.MapControllers();

            app.Run();
        }
    }
}
//görüldüðü üzere tüm bilgiler karþýmýzdadýr. Burada aud(Audience) deðeri bu JWT’nin hangi resource tarafýndan kabul edileceðini, client_id deðeri istek yapan client’ýn Auth Server’da ki kimlik bilgisini ve scope deðeri ise ilgili client’ýn taþýdýðý tüm yetkilerini bildirmektedir.