using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace HalkBankapi
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
                    //Token'� yay�nlayan Auth Server adresi bildiriliyor. Yani yetkiyi da��tan mekanizman�n adresi bildirilerek ilgili API ile ili�kilendiriliyor.
                    options.Authority = "https://localhost:1000";
                    //Auth Server uygulamas�ndaki 'Garanti' isimli resource ile bu API ili�kilendiriliyor.
                    options.Audience = "HalkBank";
                });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
