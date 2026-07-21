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
            0 => new WeatherCondition { Name = "Clear", Description = "Clear sky" },
            1 => new WeatherCondition { Name = "MostlyClear", Description = "Mainly clear" },
            2 => new WeatherCondition { Name = "PartlyCloudy", Description = "Partly cloudy" },
            3 => new WeatherCondition { Name = "Cloudy", Description = "Overcast" },
            45 or 48 => new WeatherCondition { Name = "Fog", Description = "Fog" },
            51 or 53 or 55 or 56 or 57 => new WeatherCondition { Name = "Rain", Description = "Drizzle" },
            61 or 63 or 66 => new WeatherCondition { Name = "Rain", Description = "Rain" },
            65 or 67 => new WeatherCondition { Name = "HeavyRain", Description = "Heavy rain" },
            71 or 73 or 75 or 77 => new WeatherCondition { Name = "Snow", Description = "Snowfall" },
            80 or 81 => new WeatherCondition { Name = "Rain", Description = "Rain showers" },
            82 => new WeatherCondition { Name = "HeavyRain", Description = "Violent rain showers" },
            85 or 86 => new WeatherCondition { Name = "Snow", Description = "Snow showers" },
            95 or 96 or 99 => new WeatherCondition { Name = "Thunderstorm", Description = "Thunderstorm" },
            _ => new WeatherCondition { Name = "Unknown", Description = "Unknown condition" }
        };
    }
}
