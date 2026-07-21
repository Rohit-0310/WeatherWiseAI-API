namespace WeatherWiseAI.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherWiseAI.Infrastructure.Configuration;
using WeatherWiseAI.Infrastructure.OpenMeteo.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenMeteoIntegration(configuration);

        // Keep Gemini options for future sprints
        services.Configure<GeminiOptions>(configuration.GetSection(GeminiOptions.SectionName));

        return services;
    }
}
