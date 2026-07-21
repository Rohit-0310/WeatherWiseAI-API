namespace WeatherWiseAI.Infrastructure.OpenMeteo.Extensions;

using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using Refit;
using WeatherWiseAI.Application.Interfaces;
using WeatherWiseAI.Infrastructure.OpenMeteo.Clients;
using WeatherWiseAI.Infrastructure.OpenMeteo.Configuration;
using WeatherWiseAI.Infrastructure.OpenMeteo.Services;

public static class OpenMeteoServiceCollectionExtensions
{
    public static IServiceCollection AddOpenMeteoIntegration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OpenMeteoOptions>(configuration.GetSection(OpenMeteoOptions.SectionName));

        services.AddTransient<OpenMeteoLoggingHandler>();

        services.AddRefitClient<IOpenMeteoClient>()
            .ConfigureHttpClient((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<OpenMeteoOptions>>().Value;
                client.BaseAddress = new Uri(options.BaseUrl);
                client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.UserAgent.ParseAdd("WeatherWiseAI/1.0");
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            })
            .AddHttpMessageHandler<OpenMeteoLoggingHandler>()
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());

        services.AddScoped<IWeatherProvider, OpenMeteoWeatherProvider>();
        services.AddScoped<WeatherWiseAI.Application.Location.Interfaces.ILocationProvider, OpenMeteoLocationProvider>();

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }
}
