namespace WeatherWiseAI.Application.Weather.Forecast.Requests;

using WeatherWiseAI.Application.Weather.Shared.DTOs;

public class ForecastRequest
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public UnitSystem Units { get; set; } = UnitSystem.Metric;
    public string? Language { get; set; }
    public string? Timezone { get; set; }
}
