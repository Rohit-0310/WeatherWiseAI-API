namespace WeatherWiseAI.Infrastructure.OpenMeteo.Clients;

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

public class GeocodingUrlRewritingHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.RequestUri != null && 
            (request.RequestUri.AbsolutePath.StartsWith("/v1/search", StringComparison.OrdinalIgnoreCase) || 
             request.RequestUri.AbsolutePath.StartsWith("/v1/reverse", StringComparison.OrdinalIgnoreCase)))
        {
            var uriBuilder = new UriBuilder(request.RequestUri)
            {
                Host = "geocoding-api.open-meteo.com"
            };
            request.RequestUri = uriBuilder.Uri;
        }

        return base.SendAsync(request, cancellationToken);
    }
}
