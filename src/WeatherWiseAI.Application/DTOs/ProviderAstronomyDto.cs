namespace WeatherWiseAI.Application.DTOs;

using System;

public class ProviderAstronomyDto
{
    public DateTime Sunrise { get; set; }
    public DateTime Sunset { get; set; }
    public double DaylightDuration { get; set; }
    public double SunshineDuration { get; set; }
}
