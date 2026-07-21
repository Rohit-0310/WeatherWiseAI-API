namespace WeatherWiseAI.Infrastructure.OpenMeteo.Configuration;

public class OpenMeteoOptions
{
    public const string SectionName = "OpenMeteo";
    public string BaseUrl { get; set; } = string.Empty;
    public int TimeoutSeconds { get; set; } = 30;
}
