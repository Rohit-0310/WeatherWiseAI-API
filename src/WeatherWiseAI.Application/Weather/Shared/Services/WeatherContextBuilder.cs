namespace WeatherWiseAI.Application.Weather.Shared.Services;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherWiseAI.Application.Interfaces;
using WeatherWiseAI.Application.Location.Interfaces;
using WeatherWiseAI.Application.Weather.Shared.WeatherContext;

public class WeatherContextBuilder : IWeatherContextBuilder
{
    private readonly IWeatherProvider _weatherProvider;
    private readonly ILocationProvider _locationProvider;
    private readonly IWeatherConditionMapper _conditionMapper;

    public WeatherContextBuilder(IWeatherProvider weatherProvider, ILocationProvider locationProvider, IWeatherConditionMapper conditionMapper)
    {
        _weatherProvider = weatherProvider;
        _locationProvider = locationProvider;
        _conditionMapper = conditionMapper;
    }

    public async Task<WeatherContext> BuildContextAsync(double latitude, double longitude, string units, string language, CancellationToken cancellationToken = default)
    {
        var locationTask = _locationProvider.ReverseGeocodeAsync(latitude, longitude, language, cancellationToken);
        var currentTask = _weatherProvider.GetCurrentWeatherAsync(latitude, longitude, units, null, cancellationToken);
        var hourlyTask = _weatherProvider.GetHourlyForecastAsync(latitude, longitude, 24, units, null, cancellationToken);
        var weeklyTask = _weatherProvider.GetWeeklyForecastAsync(latitude, longitude, units, null, cancellationToken);

        await Task.WhenAll(locationTask, currentTask, hourlyTask, weeklyTask);

        var loc = await locationTask;
        var cur = await currentTask;
        var hrl = await hourlyTask;
        var wkl = await weeklyTask;

        var isImperial = units.Equals("Imperial", StringComparison.OrdinalIgnoreCase);

        var context = new WeatherContext
        {
            Metadata = new WeatherMetadata { GeneratedAt = DateTime.UtcNow },
            Units = new WeatherUnits 
            { 
                Temperature = isImperial ? "°F" : "°C",
                WindSpeed = isImperial ? "mph" : "km/h"
            },
            Location = loc != null ? new WeatherLocation
            {
                Name = loc.Name,
                Region = loc.Region,
                Country = loc.Country,
                Latitude = loc.Latitude,
                Longitude = loc.Longitude,
                Timezone = loc.Timezone
            } : new WeatherLocation { Latitude = latitude, Longitude = longitude }
        };

        if (cur != null)
        {
            context.Current = new WeatherCurrent
            {
                Temperature = cur.Temperature,
                WindSpeed = cur.WindSpeed,
                Condition = _conditionMapper.MapToNormalizedCondition(cur.WeatherCode)
            };
        }

        if (hrl != null && hrl.HourlyForecasts.Any())
        {
            context.Hourly = new WeatherHourly
            {
                Snapshots = hrl.HourlyForecasts.Select(h => new HourlyWeatherSnapshot
                {
                    Time = h.Time,
                    Temperature = h.Temperature,
                    WindSpeed = h.WindSpeed,
                    Condition = _conditionMapper.MapToNormalizedCondition(h.WeatherCode)
                }).ToList()
            };
        }

        if (wkl != null && wkl.DailyForecasts.Any())
        {
            context.Weekly = new WeatherWeekly
            {
                Days = wkl.DailyForecasts.Select(d => new DailyWeatherSnapshot
                {
                    Date = d.Date,
                    MaxTemperature = d.MaxTemperature,
                    MinTemperature = d.MinTemperature,
                    Condition = _conditionMapper.MapToNormalizedCondition(d.WeatherCode)
                }).ToList()
            };
        }

        context.Hints = GenerateHints(context.Current, context.Hourly, context.Weekly, isImperial);

        return context;
    }

    private static AIHints GenerateHints(WeatherCurrent? current, WeatherHourly? hourly, WeatherWeekly? weekly, bool isImperial)
    {
        var hints = new AIHints();
        
        if (current == null) return hints;

        double hotThreshold = isImperial ? 86.0 : 30.0;
        double coldThreshold = isImperial ? 41.0 : 5.0;
        double highWindThreshold = isImperial ? 20.0 : 32.0;
        
        hints.HotWeather = current.Temperature >= hotThreshold;
        hints.ColdWeather = current.Temperature <= coldThreshold;
        hints.Windy = current.WindSpeed >= highWindThreshold;

        var condName = current.Condition.Name;
        hints.PoorVisibility = condName == "Fog" || condName == "HeavyRain" || condName == "Snow";
        hints.UmbrellaRecommended = condName == "Rain" || condName == "HeavyRain" || condName == "Thunderstorm";
        hints.HeavyRainExpected = condName == "HeavyRain" || condName == "Thunderstorm";

        bool hasRainHourly = hourly?.Snapshots.Take(12).Any(s => s.Condition.Name == "Rain" || s.Condition.Name == "HeavyRain" || s.Condition.Name == "Thunderstorm") ?? false;
        if (hasRainHourly) hints.UmbrellaRecommended = true;

        hints.OutdoorSuitable = !hints.UmbrellaRecommended && !hints.Windy && condName != "Snow" && condName != "Fog" && !hints.HotWeather && !hints.ColdWeather;

        if (hints.HotWeather && !hints.Windy) hints.ComfortLevel = "Uncomfortably Hot";
        else if (hints.ColdWeather && hints.Windy) hints.ComfortLevel = "Very Cold with Wind Chill";
        else if (hints.OutdoorSuitable) hints.ComfortLevel = "Pleasant";
        else hints.ComfortLevel = "Moderate";

        if (hints.HeavyRainExpected || hints.PoorVisibility || (condName == "Snow" && hints.Windy)) hints.TravelRisk = "High";
        else if (hints.UmbrellaRecommended || hints.Windy || condName == "Snow") hints.TravelRisk = "Moderate";
        else hints.TravelRisk = "Low";

        if (hints.OutdoorSuitable && !hints.Windy) hints.ExerciseSuitability = "Excellent";
        else if (hints.UmbrellaRecommended || hints.ColdWeather || hints.HotWeather) hints.ExerciseSuitability = "Poor (Consider Indoor)";
        else hints.ExerciseSuitability = "Fair";

        return hints;
    }
}
