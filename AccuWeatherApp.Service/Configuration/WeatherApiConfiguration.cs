namespace AccuWeatherApp.Service.Configuration
{
    public class WeatherApiConfiguration
    {
        public string? ApiKey { get; set; }
        public string? AccuWeatherBaseUrl { get; set; }
        public string? LocationEndpoint { get; set; }
        public string? CurrentConditionsEndpoint { get; set; }
        public string? DailyForecastEndpoint { get; set; }
    }
}