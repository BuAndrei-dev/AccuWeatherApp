using Newtonsoft.Json;

namespace AccuWeatherApp.Data.Models.City
{
    public class AdministrativeArea
    {
        [JsonProperty("ID")] public string? Id { get; set; }

        [JsonProperty("LocalizedName")] public string? CountryName { get; set; }
    }
}