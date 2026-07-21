namespace WeatherWiseAI.Infrastructure.OpenMeteo.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
        _logger.LogInformation("Reverse geocoding Lat: {Latitude}, Lon: {Longitude}", latitude, longitude);

        // Since Open-Meteo does not support Reverse Geocoding natively yet,
        // we use BigDataCloud free API as a robust fallback.
        using var httpClient = new HttpClient();
        var url = $"https://api.bigdatacloud.net/data/reverse-geocode-client?latitude={latitude}&longitude={longitude}&localityLanguage={language}";
        
        try
        {
            var response = await httpClient.GetFromJsonAsync<System.Text.Json.Nodes.JsonNode>(url, cancellationToken);
            var city = response?["city"]?.ToString() ?? response?["locality"]?.ToString();
            if (string.IsNullOrWhiteSpace(city)) 
            {
                city = $"Location ({Math.Round(latitude, 2)}, {Math.Round(longitude, 2)})";
            }

            var country = response?["countryName"]?.ToString() ?? "Unknown";
            var region = response?["principalSubdivision"]?.ToString() ?? "";
            
            return new LocationResponseDto
            {
                Name = city,
                Latitude = latitude,
                Longitude = longitude,
                Country = country,
                Region = region,
                Timezone = "GMT"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to perform reverse geocoding fallback.");
            return new LocationResponseDto
            {
                Name = $"Location ({Math.Round(latitude, 2)}, {Math.Round(longitude, 2)})",
                Latitude = latitude,
                Longitude = longitude,
                Country = "Unknown",
                Timezone = "GMT"
            };
        }
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
