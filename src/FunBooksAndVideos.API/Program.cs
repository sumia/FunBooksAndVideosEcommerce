
using FunBooksAndVideos.API.Extensions;
using FunBooksAndVideos.API.Middlewares;

namespace FunBooksAndVideos.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.ConfigureLogging();
            builder.ConfigureDatabase();
            builder.ConfigureServices();
            builder.ConfigureOrderRulesFactory();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.ConfigureApiVersioning();

            // Swagger/OpenAPI 
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.ApplyDatabaseMigrations();
            } else
            {
                app.UseMiddleware<ExceptionHandlingMiddleware>();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
