namespace WeatherWiseAI.Application.Weather.Forecast.Responses;

using System;
using WeatherWiseAI.Application.Weather.Shared.DTOs;

public class ForecastDayDto
{
    public DateTime Date { get; set; }
    public double MaxTemperature { get; set; }
    public double MinTemperature { get; set; }
    public double MaxWindSpeed { get; set; }
    public int PrecipitationChance { get; set; }
    public WeatherConditionDto Condition { get; set; } = new();
}
