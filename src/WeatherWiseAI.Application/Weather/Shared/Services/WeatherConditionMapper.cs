namespace WeatherWiseAI.Application.Weather.Shared.Services;

using WeatherWiseAI.Application.Weather.Shared.WeatherContext;

public interface IWeatherConditionMapper
{
    WeatherCondition MapToNormalizedCondition(int providerCode);
}

public class WeatherConditionMapper : IWeatherConditionMapper
{
    public WeatherCondition MapToNormalizedCondition(int providerCode)
    {
        return providerCode switch
        {
            0 => new WeatherCondition { Name = "Clear", Description = "Clear sky", ProviderCode = providerCode },
            1 => new WeatherCondition { Name = "MostlyClear", Description = "Mainly clear", ProviderCode = providerCode },
            2 => new WeatherCondition { Name = "PartlyCloudy", Description = "Partly cloudy", ProviderCode = providerCode },
            3 => new WeatherCondition { Name = "Cloudy", Description = "Overcast", ProviderCode = providerCode },
            45 or 48 => new WeatherCondition { Name = "Fog", Description = "Fog", ProviderCode = providerCode },
            51 or 53 or 55 or 56 or 57 => new WeatherCondition { Name = "Rain", Description = "Drizzle", ProviderCode = providerCode },
            61 or 63 or 66 => new WeatherCondition { Name = "Rain", Description = "Rain", ProviderCode = providerCode },
            65 or 67 => new WeatherCondition { Name = "HeavyRain", Description = "Heavy rain", ProviderCode = providerCode },
            71 or 73 or 75 or 77 => new WeatherCondition { Name = "Snow", Description = "Snowfall", ProviderCode = providerCode },
            80 or 81 => new WeatherCondition { Name = "Rain", Description = "Rain showers", ProviderCode = providerCode },
            82 => new WeatherCondition { Name = "HeavyRain", Description = "Violent rain showers", ProviderCode = providerCode },
            85 or 86 => new WeatherCondition { Name = "Snow", Description = "Snow showers", ProviderCode = providerCode },
            95 or 96 or 99 => new WeatherCondition { Name = "Thunderstorm", Description = "Thunderstorm", ProviderCode = providerCode },
            _ => new WeatherCondition { Name = "Unknown", Description = "Unknown condition", ProviderCode = providerCode }
        };
    }
}
