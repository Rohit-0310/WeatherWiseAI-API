namespace WeatherWiseAI.Infrastructure.Configuration;

public class GeminiOptions
{
    public const string SectionName = "Gemini";
    public string BaseUrl { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string ModelName { get; set; } = string.Empty;
}
