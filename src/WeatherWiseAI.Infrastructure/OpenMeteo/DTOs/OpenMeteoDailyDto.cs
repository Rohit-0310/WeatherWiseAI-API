namespace WeatherWiseAI.Infrastructure.OpenMeteo.DTOs;

using System.Text.Json.Serialization;
using System.Collections.Generic;

public class OpenMeteoDailyDto
{
    [JsonPropertyName("time")]
    public List<string> Time { get; set; } = new();

    [JsonPropertyName("weathercode")]
    public List<int> WeatherCode { get; set; } = new();

    [JsonPropertyName("temperature_2m_max")]
    public List<double> TemperatureMax { get; set; } = new();

    [JsonPropertyName("temperature_2m_min")]
    public List<double> TemperatureMin { get; set; } = new();

    [JsonPropertyName("windspeed_10m_max")]
    public List<double> WindSpeedMax { get; set; } = new();
}
