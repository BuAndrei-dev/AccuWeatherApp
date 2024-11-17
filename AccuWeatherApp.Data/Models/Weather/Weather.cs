namespace AccuWeatherApp.Data.Models.Weather
{
    /// <summary>
    ///     Model used to get the Weather Response from Accu Weather API
    /// </summary>
    public class Weather
    {
        public int Id { get; set; }
        public double TemperatureC { get; set; }
        public double TemperatureF { get; set; }
        public bool IsRain { get; set; }
    }
}