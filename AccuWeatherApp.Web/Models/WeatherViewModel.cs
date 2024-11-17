namespace AccuWeatherApp.Web.Models
{
    public class WeatherViewModel
    {
        public string? Id { get; set; }
        public double TemperatureC { get; set; }
        public double TemperatureF { get; set; }
        public bool IsRain { get; set; }
    }
}