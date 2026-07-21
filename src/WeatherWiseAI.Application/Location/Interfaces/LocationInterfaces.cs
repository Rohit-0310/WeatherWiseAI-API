namespace WeatherWiseAI.Application.Location.Interfaces;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherWiseAI.Application.Location.DTOs;
using WeatherWiseAI.Application.Location.Requests;

public interface ILocationProvider
{
    Task<List<LocationResponseDto>> SearchLocationsAsync(string query, int count, string language, CancellationToken cancellationToken = default);
    Task<LocationResponseDto?> ReverseGeocodeAsync(double latitude, double longitude, string language, CancellationToken cancellationToken = default);
}

public interface ILocationService
{
    Task<List<LocationResponseDto>> SearchLocationsAsync(LocationSearchRequest request, CancellationToken cancellationToken = default);
    Task<LocationResponseDto?> ReverseGeocodeAsync(LocationReverseRequest request, CancellationToken cancellationToken = default);
}
