using AccuWeatherApp.Data.Models.City;
using AccuWeatherApp.Data.Models.Weather;
using Microsoft.EntityFrameworkCore;

namespace AccuWeatherApp.Data.Context
{
    public class WeatherDbContext(DbContextOptions<WeatherDbContext> options) : DbContext(options)
    {
        public DbSet<WeatherForecast> WeatherForecast { get; set; }
        public DbSet<CityResult> CityResult { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<AdministrativeArea> AdministrativeArea { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CityResult>().HasKey(c => c.Id);
            modelBuilder.Entity<Country>().HasKey(c => c.Id);
            modelBuilder.Entity<AdministrativeArea>().HasKey(c => c.Id);
            modelBuilder.Entity<WeatherForecast>().HasKey(w => w.Id);
            modelBuilder.Entity<WeatherForecast>().OwnsOne(t => t.Temperature, temp =>
            {
                temp.OwnsOne(t => t.Celsius);
                temp.OwnsOne(t => t.Fahrenheit);
            });
        }
    }
}