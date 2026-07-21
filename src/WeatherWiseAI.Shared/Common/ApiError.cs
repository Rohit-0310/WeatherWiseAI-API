namespace WeatherWiseAI.Shared.Common;

public class ApiError
{
    public string Message { get; set; } = string.Empty;
    public string? Code { get; set; }
    public IEnumerable<ErrorDetail> Details { get; set; } = Array.Empty<ErrorDetail>();
}
