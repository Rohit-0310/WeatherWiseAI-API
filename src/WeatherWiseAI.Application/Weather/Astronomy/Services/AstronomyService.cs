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
    public Task<AstronomyResponse> GetAstronomyDataAsync(AstronomyRequest request, CancellationToken cancellationToken = default)
    {
        var targetDate = request.Date ?? DateTime.UtcNow;
        var c = new Coordinate(request.Latitude, request.Longitude, targetDate);

        var celestial = c.CelestialInfo;
        
        // Handle polar regions where sun might not set or rise
        var sunrise = celestial.SunRise ?? targetDate.Date.AddHours(6); 
        var sunset = celestial.SunSet ?? targetDate.Date.AddHours(18);
        
        var dayLength = sunset - sunrise;
        if (dayLength.TotalHours < 0) dayLength = TimeSpan.FromHours(24) + dayLength;

        // Calculate solar noon manually if CoordinateSharp doesn't expose it directly
        var solarNoon = sunrise + TimeSpan.FromTicks(dayLength.Ticks / 2);

        var moonPhaseName = celestial.MoonIllum.PhaseName; 

        var response = new AstronomyResponse
        {
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Date = targetDate,
            Sunrise = sunrise,
            Sunset = sunset,
            SolarNoon = solarNoon,
            DayLength = dayLength,
            MoonPhaseName = moonPhaseName,
            MoonIllumination = celestial.MoonIllum.Fraction * 100 // Convert to percentage
        };

        return Task.FromResult(response);
    }
}
