namespace WeatherWiseAI.Application.Weather.Hourly.Requests;

using WeatherWiseAI.Application.Weather.Shared.DTOs;

public class HourlyRequest
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int Hours { get; set; } = 24;
    public UnitSystem Units { get; set; } = UnitSystem.Metric;
    public string? Language { get; set; }
    public string? Timezone { get; set; }
}
