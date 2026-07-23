namespace WeatherWiseAI.Application.Weather.Astronomy.Responses;

using System;

public class AstronomyResponse
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime Date { get; set; }
    public DateTime Sunrise { get; set; }
    public DateTime Sunset { get; set; }
    public DateTime SolarNoon { get; set; }
    public TimeSpan DayLength { get; set; }
    public TimeSpan SunshineDuration { get; set; }
    public string MoonPhaseName { get; set; } = string.Empty;
    public double MoonIllumination { get; set; }
    public DateTime? MoonRise { get; set; }
    public DateTime? MoonSet { get; set; }
}
