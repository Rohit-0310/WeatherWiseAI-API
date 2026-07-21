namespace WeatherWiseAI.Application.Weather.DTOs;

public class CurrentWeatherResponse
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Temperature { get; set; }
    public double WindSpeed { get; set; }
    public WeatherWiseAI.Application.Weather.Shared.DTOs.WeatherConditionDto Condition { get; set; } = new();
    public string Units { get; set; } = string.Empty;
}
