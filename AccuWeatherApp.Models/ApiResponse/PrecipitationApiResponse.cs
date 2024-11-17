using Newtonsoft.Json;

namespace AccuWeatherApp.Models.ApiResponse
{
    public class PrecipitationApiResponse
    {
        [JsonProperty("DailyForecasts")] 
        public List<DailyForecastResponse>? DailyForecasts { get; set; }
    }

    public class DailyForecastResponse
    {
        [JsonProperty("Date")] 
        public string? Date { get; set; }

        [JsonProperty("Day")] 
        public DayForecastResponse? Day { get; set; }
    }

    public class DayForecastResponse
    {
        [JsonProperty("HasPrecipitation")] 
        public bool HasPrecipitation { get; set; }
    }
}