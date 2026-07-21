namespace WeatherWiseAI.Application.Location.Services;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherWiseAI.Application.Location.DTOs;
using WeatherWiseAI.Application.Location.Interfaces;
using WeatherWiseAI.Application.Location.Requests;

public class LocationService : ILocationService
{
    private readonly ILocationProvider _locationProvider;

    public LocationService(ILocationProvider locationProvider)
    {
        _locationProvider = locationProvider;
    }

    public Task<List<LocationResponseDto>> SearchLocationsAsync(LocationSearchRequest request, CancellationToken cancellationToken = default)
    {
        return _locationProvider.SearchLocationsAsync(request.Query, request.Count, request.Language, cancellationToken);
    }

    public Task<LocationResponseDto?> ReverseGeocodeAsync(LocationReverseRequest request, CancellationToken cancellationToken = default)
    {
        return _locationProvider.ReverseGeocodeAsync(request.Latitude, request.Longitude, request.Language, cancellationToken);
    }
}
