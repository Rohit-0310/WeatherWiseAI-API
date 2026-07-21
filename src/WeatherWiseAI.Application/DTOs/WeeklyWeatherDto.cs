namespace WeatherWiseAI.Application.DTOs;

using System;
using System.Collections.Generic;

public class WeeklyWeatherDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Timezone { get; set; } = string.Empty;
    public List<DailyWeatherDto> DailyForecasts { get; set; } = new();
}

public class DailyWeatherDto
{
    public DateTime Date { get; set; }
    public int WeatherCode { get; set; }
    public double MaxTemperature { get; set; }
    public double MinTemperature { get; set; }
    public double MaxWindSpeed { get; set; }
}
