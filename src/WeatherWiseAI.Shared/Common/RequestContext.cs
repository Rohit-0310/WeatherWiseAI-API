namespace WeatherWiseAI.Shared.Common;

public class RequestContext
{
    public string CorrelationId { get; set; } = Guid.NewGuid().ToString();
    public string? UserId { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
