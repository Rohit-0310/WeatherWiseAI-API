namespace WeatherWiseAI.Infrastructure.OpenMeteo.Clients;

using System.Diagnostics;
using Microsoft.Extensions.Logging;

public class OpenMeteoLoggingHandler : DelegatingHandler
{
    private readonly ILogger<OpenMeteoLoggingHandler> _logger;

    public OpenMeteoLoggingHandler(ILogger<OpenMeteoLoggingHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sending request to Open-Meteo: {Method} {Uri}", request.Method, request.RequestUri);
        
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            var response = await base.SendAsync(request, cancellationToken);
            stopwatch.Stop();
            
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Received successful response from Open-Meteo. Time taken: {ElapsedMilliseconds}ms", stopwatch.ElapsedMilliseconds);
            }
            else
            {
                _logger.LogWarning("Received non-success status code {StatusCode} from Open-Meteo. Time taken: {ElapsedMilliseconds}ms", response.StatusCode, stopwatch.ElapsedMilliseconds);
            }
            
            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex, "Request to Open-Meteo failed after {ElapsedMilliseconds}ms", stopwatch.ElapsedMilliseconds);
            throw;
        }
    }
}
