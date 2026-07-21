namespace WeatherWiseAI.Application.Weather.Forecast.Responses;

using System.Collections.Generic;

public class ForecastResponseDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Timezone { get; set; } = string.Empty;
    public string Units { get; set; } = string.Empty;
    public List<ForecastDayDto> Days { get; set; } = new();
}
