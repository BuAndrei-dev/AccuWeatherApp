using AccuWeatherApp.Data.Models.City;
using AccuWeatherApp.Data.Models.Weather;
using Microsoft.EntityFrameworkCore;

namespace AccuWeatherApp.Data.Context
{
    public class WeatherDbContext(DbContextOptions<WeatherDbContext> options) : DbContext(options)
    {
        public DbSet<Weather> Weather { get; set; }
        public DbSet<City> City { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasKey(c => c.Id);
            modelBuilder.Entity<Weather>().HasKey(w => w.Id);
        }
    }
}