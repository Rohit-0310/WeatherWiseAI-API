namespace WeatherWiseAI.Application.DTOs;

using System;
using System.Collections.Generic;

public class ProviderHourlyWeatherDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Timezone { get; set; } = string.Empty;
    public List<ProviderHourlyForecastDto> HourlyForecasts { get; set; } = new();
}

public class ProviderHourlyForecastDto
{
    public DateTime Time { get; set; }
    public double Temperature { get; set; }
    public double WindSpeed { get; set; }
    public int WeatherCode { get; set; }
    public int PrecipitationChance { get; set; }
}
