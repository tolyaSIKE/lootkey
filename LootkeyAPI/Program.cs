using LootkeyAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace LootkeyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
