namespace WeatherWiseAI.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherWiseAI.Infrastructure.Configuration;
using WeatherWiseAI.Infrastructure.OpenMeteo.Extensions;
using WeatherWiseAI.Infrastructure.Gemini.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenMeteoIntegration(configuration);
        services.AddGeminiServices(configuration);

        return services;
    }
}
