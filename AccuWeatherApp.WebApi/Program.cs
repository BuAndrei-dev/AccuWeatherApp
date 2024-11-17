using AccuWeatherApp.Data.Context;
using AccuWeatherApp.Service;
using AccuWeatherApp.Service.Configuration;
using AccuWeatherApp.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AccuWeatherApp.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddDbContext<WeatherDbContext>(options => options.UseInMemoryDatabase("AccuWeatherDb"));
            builder.Services.Configure<WeatherApiConfiguration>(builder.Configuration.GetSection("WeatherApi"));

            builder.Logging.AddConsole();
            builder.Logging.SetMinimumLevel(LogLevel.Debug);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(swg =>
            {
                swg.SwaggerDoc("AccuWeatherAPP", new OpenApiInfo
                {
                    Title = "AccuWeather API",
                    Description = "API for searching cities and fetching current weather data and rain forecast"
                });
                swg.EnableAnnotations();
            });

            builder.Services.AddHttpClient();
            builder.Services.AddScoped<ICityService, CityService>();
            builder.Services.AddScoped<IWeatherService, WeatherService>();

            builder.Logging.AddConsole();
            builder.Logging.SetMinimumLevel(LogLevel.Debug);

            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/AccuWeatherAPP/swagger.json", "AccuWeather API"); });

            app.UseRouting();
            app.MapControllers();
            app.Run();
        }
    }
}