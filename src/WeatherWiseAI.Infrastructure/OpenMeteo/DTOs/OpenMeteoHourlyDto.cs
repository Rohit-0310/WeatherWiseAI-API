namespace WeatherWiseAI.Infrastructure.OpenMeteo.DTOs;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class OpenMeteoHourlyDto
{
    [JsonPropertyName("time")]
    public List<string> Time { get; set; } = new();

    [JsonPropertyName("temperature_2m")]
    public List<double> Temperature { get; set; } = new();

    [JsonPropertyName("windspeed_10m")]
    public List<double> WindSpeed { get; set; } = new();

    [JsonPropertyName("weathercode")]
    public List<int> WeatherCode { get; set; } = new();

    [JsonPropertyName("precipitation_probability")]
    public List<int> PrecipitationProbability { get; set; } = new();
}
