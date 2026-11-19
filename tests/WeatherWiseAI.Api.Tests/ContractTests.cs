using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using WeatherWiseAI.Application.DTOs;
using WeatherWiseAI.Application.Interfaces;
using WeatherWiseAI.Application.Location.DTOs;
using WeatherWiseAI.Application.Location.Interfaces;
using WeatherWiseAI.Application.Weather.AiSummary.Interfaces;
using WeatherWiseAI.Infrastructure.Gemini.Services;

public class ContractTests : IDisposable
{
    private readonly WeatherFixture fixture = new();
    private readonly WebApplicationFactory<Program> factory;
    private readonly HttpClient client;

    public ContractTests()
    {
        factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Development");
            builder.UseSetting("Gemini:ApiKey", "test-only");
            builder.UseSetting("Cors:AllowedOrigins:0", "https://weather.example");
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IWeatherProvider>(fixture);
                services.AddSingleton<ILocationProvider>(fixture);
                services.AddHttpClient<IAiSummaryService, GeminiAiSummaryService>()
                    .ConfigurePrimaryHttpMessageHandler(() => new GeminiFixture());
            });
        });
        client = factory.CreateClient();
    }

    [Theory]
    [InlineData("Weather/current")]
    [InlineData("Weather/forecast")]
    [InlineData("Weather/astronomy")]
    [InlineData("Location/search?query=London")]
    [InlineData("Location/reverse")]
    public async Task ReadsReturnEnvelope(string route)
    {
        var result = await client.GetFromJsonAsync<JsonElement>("/api/v1/" + route);
        Assert.True(result.GetProperty("success").GetBoolean());
        Assert.True(result.TryGetProperty("data", out _));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(24)]
    [InlineData(48)]
    public async Task HourlyMapsProviderData(int hours)
    {
        var result = await client.GetFromJsonAsync<JsonElement>($"/api/v1/Weather/hourly?hours={hours}");
        Assert.Equal(hours, fixture.RequestedHours);
        Assert.Equal(12, result.GetProperty("data").GetProperty("hours")[0].GetProperty("temperature").GetDouble());
    }

    [Theory]
    [InlineData("Weather/hourly?hours=0")]
    [InlineData("Weather/hourly?hours=49")]
    [InlineData("Weather/hourly?latitude=91")]
    [InlineData("Weather/hourly?latitude=invalid")]
    [InlineData("Location/search?query=a")]
    public async Task InvalidRequestsReturn400(string route)
    {
        var response = await client.GetAsync("/api/v1/" + route);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.False(result.GetProperty("success").GetBoolean());
        Assert.Null(fixture.RequestedHours);
    }

    [Theory]
    [InlineData("London", HttpStatusCode.OK)]
    [InlineData("Incomplete", HttpStatusCode.BadRequest)]
    [InlineData("Provider unavailable", HttpStatusCode.BadRequest)]
    public async Task AiUsesDocumentedResponseShape(string location, HttpStatusCode expected)
    {
        var response = await client.PostAsJsonAsync("/api/v1/AiSummary", new {
            location, temperature = "12 C", condition = "Rain", humidity = "80%", wind = "5 mph", uvIndex = "2"
        });
        Assert.Equal(expected, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.True(result.TryGetProperty(expected == HttpStatusCode.OK ? "summary" : "error", out _));
    }

    [Fact]
    public void ProviderMappingsHandlePartialSeriesAndMissingAqi()
    {
        using var scope = factory.Services.CreateScope();
        var mapper = scope.ServiceProvider.GetRequiredService<AutoMapper.IMapper>();
        var provider = new WeatherWiseAI.Infrastructure.OpenMeteo.DTOs.OpenMeteoResponse
        {
            Hourly = new() { Time = ["2026-11-19T09:00"], WeatherCode = [61], Temperature = [12] },
            Daily = new() { Time = ["2026-11-19"], TemperatureMax = [14] }
        };
        Assert.Null(mapper.Map<WeatherResponseDto>(provider).Aqi);
        var hour = Assert.Single(mapper.Map<ProviderHourlyWeatherDto>(provider).HourlyForecasts);
        Assert.Equal(61, hour.WeatherCode);
        Assert.Equal(0, hour.WindSpeed);
        Assert.Equal(14, Assert.Single(mapper.Map<WeeklyWeatherDto>(provider).DailyForecasts).MaxTemperature);
    }

    [Theory]
    [InlineData("https://weather.example", true)]
    [InlineData("https://unlisted.example", false)]
    public async Task PreflightUsesConfiguredOrigins(string origin, bool allowed)
    {
        using var request = new HttpRequestMessage(HttpMethod.Options, "/api/v1/Weather/current");
        request.Headers.Add("Origin", origin);
        request.Headers.Add("Access-Control-Request-Method", "GET");
        using var response = await client.SendAsync(request);
        Assert.Equal(allowed, response.Headers.Contains("Access-Control-Allow-Origin"));
    }

    public void Dispose() { client.Dispose(); factory.Dispose(); }
}

internal sealed class GeminiFixture : HttpMessageHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
    {
        Assert.Empty(request.RequestUri!.Query);
        Assert.True(request.Headers.Contains("x-goog-api-key"));
        var input = await request.Content!.ReadAsStringAsync(token);
        if (input.Contains("Provider unavailable")) return new(HttpStatusCode.ServiceUnavailable);
        string text = input.Contains("Incomplete") ? "{}" : JsonSerializer.Serialize(new {
            summary = "Rain", clothing = "Coat", activity = "Read"
        });
        return new(HttpStatusCode.OK) { Content = JsonContent.Create(new {
            candidates = new[] { new { content = new { parts = new[] { new { text } } } } }
        }) };
    }
}

internal sealed class WeatherFixture : IWeatherProvider, ILocationProvider
{
    public int? RequestedHours { get; private set; }
    public Task<ProviderHourlyWeatherDto> GetHourlyForecastAsync(double latitude, double longitude, int hours, string units, string? timezone, CancellationToken token)
    {
        RequestedHours = hours;
        return Task.FromResult(new ProviderHourlyWeatherDto { HourlyForecasts = [new() { Temperature = 12 }] });
    }
    public Task<WeatherResponseDto> GetCurrentWeatherAsync(double latitude, double longitude, string units, string? timezone, CancellationToken token)
        => Task.FromResult(new WeatherResponseDto { Aqi = 90 });
    public Task<WeeklyWeatherDto> GetWeeklyForecastAsync(double latitude, double longitude, string units, string? timezone, CancellationToken token)
        => Task.FromResult(new WeeklyWeatherDto { DailyForecasts = [new() { WeatherCode = 61 }] });
    public Task<ProviderAstronomyDto> GetAstronomyDataAsync(double latitude, double longitude, DateTime date, string? timezone, CancellationToken token)
        => Task.FromResult(new ProviderAstronomyDto { Sunrise = date.Date.AddHours(6), Sunset = date.Date.AddHours(18) });
    public Task<List<LocationResponseDto>> SearchLocationsAsync(string query, int count, string language, CancellationToken token)
        => Task.FromResult<List<LocationResponseDto>>([new() { Name = query }]);
    public Task<LocationResponseDto?> ReverseGeocodeAsync(double latitude, double longitude, string language, CancellationToken token)
        => Task.FromResult<LocationResponseDto?>(new() { Name = "London" });
}
