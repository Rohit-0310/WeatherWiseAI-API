namespace WeatherWiseAI.Application.Weather.AiSummary.Requests;

public class AiSummaryRequest
{
    public string Location { get; set; } = string.Empty;
    public string Temperature { get; set; } = string.Empty;
    public string Condition { get; set; } = string.Empty;
    public string Humidity { get; set; } = string.Empty;
    public string Wind { get; set; } = string.Empty;
    public string UvIndex { get; set; } = string.Empty;
}
