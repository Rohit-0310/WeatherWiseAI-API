namespace WeatherWiseAI.Application;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<WeatherWiseAI.Application.Weather.Services.IWeatherService, WeatherWiseAI.Application.Weather.Services.WeatherService>();
        services.AddScoped<WeatherWiseAI.Application.Weather.Forecast.Interfaces.IForecastService, WeatherWiseAI.Application.Weather.Forecast.Services.ForecastService>();
        services.AddScoped<WeatherWiseAI.Application.Weather.Hourly.Interfaces.IHourlyService, WeatherWiseAI.Application.Weather.Hourly.Services.HourlyService>();
        services.AddSingleton<WeatherWiseAI.Application.Weather.Shared.Services.IWeatherConditionMapper, WeatherWiseAI.Application.Weather.Shared.Services.WeatherConditionMapper>();
        services.AddScoped<WeatherWiseAI.Application.Weather.Shared.Services.IWeatherContextBuilder, WeatherWiseAI.Application.Weather.Shared.Services.WeatherContextBuilder>();
        services.AddScoped<WeatherWiseAI.Application.Location.Interfaces.ILocationService, WeatherWiseAI.Application.Location.Services.LocationService>();
        services.AddScoped<WeatherWiseAI.Application.Weather.Astronomy.Interfaces.IAstronomyService, WeatherWiseAI.Application.Weather.Astronomy.Services.AstronomyService>();

        return services;
    }
}
