# Development Guide

This guide explains how to navigate the repository and implement new features using Clean Architecture.

## Folder Structure

- `WeatherWiseAI.Api`: HTTP endpoints and configuration.
- `WeatherWiseAI.Application`: Business logic, DTOs, and Interfaces.
- `WeatherWiseAI.Domain`: Core models.
- `WeatherWiseAI.Infrastructure`: External integrations.
- `WeatherWiseAI.Shared`: Utilities.

## How to Add New Features

Always work from the inside out:
1. **Domain**: Add any necessary domain models.
2. **Application**: Add Interfaces, DTOs, Validators, and the core Service logic.
3. **Infrastructure**: Implement the interfaces (e.g., HTTP clients).
4. **API**: Create the Controller and register dependencies.

## How to Add Controllers

1. Navigate to `src/WeatherWiseAI.Api/Controllers`.
2. Create a new controller inheriting from `ControllerBase`.
3. Decorate with `[ApiController]` and `[Route("api/[controller]")]`.
4. Inject required Application services via the constructor.
5. Map endpoints using `[HttpGet]`, `[HttpPost]`, etc.

## How to Add Services

1. Define an interface in `WeatherWiseAI.Application/Interfaces`.
   ```csharp
   public interface IWeatherService { ... }
   ```
2. Implement the interface in `WeatherWiseAI.Application/Services` (for business logic) or `WeatherWiseAI.Infrastructure/Services` (for external calls).
   ```csharp
   public class WeatherService : IWeatherService { ... }
   ```

## How to Add Integrations

1. Define a Refit interface in `WeatherWiseAI.Infrastructure/HttpClients`.
   ```csharp
   public interface IOpenMeteoClient {
       [Get("/v1/forecast")]
       Task<OpenMeteoResponse> GetForecastAsync(...);
   }
   ```
2. Register the Refit client in DI.

## How to Register DI

1. Open `src/WeatherWiseAI.Api/Program.cs` (or extension methods in `Extensions/`).
2. Register services with appropriate lifetimes:
   - `builder.Services.AddScoped<IWeatherService, WeatherService>();`
3. Register Refit clients:
   - `builder.Services.AddRefitClient<IOpenMeteoClient>().ConfigureHttpClient(...)`

## How to Create DTOs

1. Navigate to `WeatherWiseAI.Application/DTOs`.
2. Create standard classes or records.
   ```csharp
   public record WeatherRequest(double Latitude, double Longitude);
   ```

## How to Create Validators

1. Navigate to `WeatherWiseAI.Application/Validators`.
2. Create a class inheriting from `AbstractValidator<T>`.
   ```csharp
   public class WeatherRequestValidator : AbstractValidator<WeatherRequest>
   {
       public WeatherRequestValidator()
       {
           RuleFor(x => x.Latitude).InclusiveBetween(-90, 90);
       }
   }
   ```
3. Ensure FluentValidation is registered in DI.
