namespace WeatherWiseAI.Application.Weather.Hourly.Responses;

using System;
using WeatherWiseAI.Application.Weather.Shared.DTOs;

public class HourlyForecastDto
{
    public DateTime Time { get; set; }
    public double Temperature { get; set; }
    public double WindSpeed { get; set; }
    public int PrecipitationChance { get; set; }
    public WeatherConditionDto Condition { get; set; } = new();
}
