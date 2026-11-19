namespace WeatherWiseAI.Infrastructure.OpenMeteo.DTOs;

using System.Text.Json.Serialization;

public class OpenMeteoAirQualityResponse
{
    [JsonPropertyName("current")]
    public OpenMeteoAirQualityCurrentDto? Current { get; set; }
}

public class OpenMeteoAirQualityCurrentDto
{
    [JsonPropertyName("european_aqi")]
    public int? EuropeanAqi { get; set; }

    [JsonPropertyName("us_aqi")]
    public int UsAqi { get; set; }
}
