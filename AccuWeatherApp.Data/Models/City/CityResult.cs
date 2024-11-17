using Newtonsoft.Json;

namespace AccuWeatherApp.Data.Models.City
{
    /// <summary>
    ///     Model used to get City Results from Accu Weather API
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class CityResult
    {
        public int Id { get; set; }

        [JsonProperty(nameof(Key))] public string? Key { get; set; }

        [JsonProperty("LocalizedName")] public string? CityName { get; set; }

        [JsonProperty("Country")] public Country? Country { get; set; }

        [JsonProperty("AdministrativeArea")] public AdministrativeArea? AdministrativeArea { get; set; }
    }
}