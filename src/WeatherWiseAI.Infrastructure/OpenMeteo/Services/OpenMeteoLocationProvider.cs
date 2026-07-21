namespace WeatherWiseAI.Infrastructure.OpenMeteo.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WeatherWiseAI.Application.Location.DTOs;
using WeatherWiseAI.Application.Location.Interfaces;
using WeatherWiseAI.Infrastructure.OpenMeteo.Clients;

public class OpenMeteoLocationProvider : ILocationProvider
{
    private readonly IOpenMeteoClient _client;
    private readonly ILogger<OpenMeteoLocationProvider> _logger;

    public OpenMeteoLocationProvider(IOpenMeteoClient client, ILogger<OpenMeteoLocationProvider> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<List<LocationResponseDto>> SearchLocationsAsync(string query, int count, string language, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching locations for query: {Query}", query);
        var response = await _client.SearchLocationsAsync(query, count, language, "json", cancellationToken);
        
        if (response?.Results == null) return new List<LocationResponseDto>();
        
        return response.Results.Select(Map).ToList();
    }

    public async Task<LocationResponseDto?> ReverseGeocodeAsync(double latitude, double longitude, string language, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Reverse geocoding Lat: {Lat}, Lon: {Lon}", latitude, longitude);
        var response = await _client.ReverseGeocodeAsync(latitude, longitude, 1, language, cancellationToken);
        
        var result = response?.Results?.FirstOrDefault();
        return result != null ? Map(result) : null;
    }

    private static LocationResponseDto Map(DTOs.OpenMeteoLocationDto src)
    {
        return new LocationResponseDto
        {
            Id = src.Id,
            Name = src.Name,
            Latitude = src.Latitude,
            Longitude = src.Longitude,
            Country = src.Country,
            Region = src.Admin1,
            Timezone = src.Timezone
        };
    }
}
