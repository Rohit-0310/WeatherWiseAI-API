namespace WeatherWiseAI.Application.Weather.Shared.DTOs;

public class WeatherConditionDto
{
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ProviderCode { get; set; }
}
