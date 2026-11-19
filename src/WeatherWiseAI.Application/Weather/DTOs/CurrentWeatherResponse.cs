namespace WeatherWiseAI.Application.Weather.DTOs;

public class CurrentWeatherResponse
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Temperature { get; set; }
    public double WindSpeed { get; set; }
    public WeatherWiseAI.Application.Weather.Shared.DTOs.WeatherConditionDto Condition { get; set; } = new();
    public string Units { get; set; } = string.Empty;
    public double FeelsLike { get; set; }
    public double Humidity { get; set; }
    public double Pressure { get; set; }
    public double Visibility { get; set; }
    public double WindDirection { get; set; }
    public int IsDay { get; set; }
    public int? Aqi { get; set; }
    public double MaxTemperature { get; set; }
    public double MinTemperature { get; set; }
    public double UvIndex { get; set; }
    public int PrecipitationChance { get; set; }
}
