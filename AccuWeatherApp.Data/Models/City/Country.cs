using Newtonsoft.Json;

namespace AccuWeatherApp.Data.Models.City
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Country
    {
        [JsonProperty("ID")] public string? Id { get; set; }

        [JsonProperty("LocalizedName")] public string? CountryName { get; set; }
    }
}