using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherWiseAI.Application.Weather.AiSummary.Interfaces;
using WeatherWiseAI.Infrastructure.Configuration;
using WeatherWiseAI.Infrastructure.Gemini.Services;

namespace WeatherWiseAI.Infrastructure.Gemini.Extensions;

public static class GeminiServiceCollectionExtensions
{
    public static IServiceCollection AddGeminiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var geminiSection = configuration.GetSection("Gemini");
        services.Configure<GeminiOptions>(geminiSection);

        services.AddHttpClient<IAiSummaryService, GeminiAiSummaryService>((serviceProvider, client) =>
        {
            var options = geminiSection.Get<GeminiOptions>();
            client.BaseAddress = new Uri(options?.BaseUrl ?? "https://generativelanguage.googleapis.com");
        });

        return services;
    }
}
