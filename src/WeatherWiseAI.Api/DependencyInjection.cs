namespace WeatherWiseAI.Api;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WeatherWiseAI.Shared.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers(options => options.Filters.Add<RequestValidationFilter>())
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value?.Errors.Count > 0)
                        .SelectMany(kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage))
                        .ToList();

                    var response = ApiResponse<object>.ErrorResponse(errors);
                    return new BadRequestObjectResult(response);
                };
            });

        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo 
            { 
                Title = "WeatherWiseAI API", 
                Version = "v1",
                Description = "Weather API integrated with Google Gemini for insights" 
            });
            
            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }
        });

        services.AddHealthChecks();

        services.AddAutoMapper(cfg => {}, 
            typeof(WeatherWiseAI.Application.DependencyInjection).Assembly,
            typeof(WeatherWiseAI.Infrastructure.DependencyInjection).Assembly
        );

        return services;
    }
}
