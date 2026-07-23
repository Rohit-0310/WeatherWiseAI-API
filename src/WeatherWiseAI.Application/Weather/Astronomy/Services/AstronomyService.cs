namespace WeatherWiseAI.Application.Weather.Astronomy.Services;

using System;
using System.Threading;
using System.Threading.Tasks;
using CoordinateSharp;
using WeatherWiseAI.Application.Weather.Astronomy.Interfaces;
using WeatherWiseAI.Application.Weather.Astronomy.Requests;
using WeatherWiseAI.Application.Weather.Astronomy.Responses;

public class AstronomyService : IAstronomyService
{
    private readonly WeatherWiseAI.Application.Interfaces.IWeatherProvider _weatherProvider;

    public AstronomyService(WeatherWiseAI.Application.Interfaces.IWeatherProvider weatherProvider)
    {
        _weatherProvider = weatherProvider;
    }

    public async Task<AstronomyResponse> GetAstronomyDataAsync(AstronomyRequest request, CancellationToken cancellationToken = default)
    {
        var targetDate = request.Date ?? DateTime.UtcNow;
        
        // Fetch data from Open-Meteo
        var providerData = await _weatherProvider.GetAstronomyDataAsync(request.Latitude, request.Longitude, targetDate, "auto", cancellationToken);

        // Fetch lunar data from CoordinateSharp
        var c = new Coordinate(request.Latitude, request.Longitude, targetDate);
        var celestial = c.CelestialInfo;
        
        // Calculate solar noon manually using Open-Meteo's daylight duration (or falling back to standard math)
        var dayLength = TimeSpan.FromSeconds(providerData.DaylightDuration);
        var solarNoon = providerData.Sunrise + TimeSpan.FromTicks(dayLength.Ticks / 2);

        var response = new AstronomyResponse
        {
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Date = targetDate,
            Sunrise = providerData.Sunrise,
            Sunset = providerData.Sunset,
            SolarNoon = solarNoon,
            DayLength = dayLength,
            SunshineDuration = TimeSpan.FromSeconds(providerData.SunshineDuration),
            MoonPhaseName = celestial.MoonIllum.PhaseName,
            MoonIllumination = celestial.MoonIllum.Fraction * 100, // Convert to percentage
            MoonRise = celestial.MoonRise,
            MoonSet = celestial.MoonSet
        };

        return response;
    }
}
