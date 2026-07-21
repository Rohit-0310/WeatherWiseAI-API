namespace WeatherWiseAI.Application.Weather.Hourly.Responses;

using System.Collections.Generic;

public class HourlyResponseDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Timezone { get; set; } = string.Empty;
    public string Units { get; set; } = string.Empty;
    public List<HourlyForecastDto> Hours { get; set; } = new();
}
