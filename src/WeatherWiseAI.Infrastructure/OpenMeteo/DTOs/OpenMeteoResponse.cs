namespace WeatherWiseAI.Infrastructure.OpenMeteo.DTOs;

using System.Text.Json.Serialization;

public class OpenMeteoResponse
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("generationtime_ms")]
    public double GenerationTimeMs { get; set; }

    [JsonPropertyName("timezone")]
    public string Timezone { get; set; } = string.Empty;

    [JsonPropertyName("timezone_abbreviation")]
    public string TimezoneAbbreviation { get; set; } = string.Empty;

    [JsonPropertyName("elevation")]
    public double Elevation { get; set; }

    [JsonPropertyName("current_weather")]
    public CurrentWeatherDto? CurrentWeather { get; set; }

    [JsonPropertyName("current")]
    public OpenMeteoCurrentDto? Current { get; set; }

    [JsonPropertyName("daily")]
    public OpenMeteoDailyDto? Daily { get; set; }

    [JsonPropertyName("hourly")]
    public OpenMeteoHourlyDto? Hourly { get; set; }
}

public class CurrentWeatherDto
{
    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    [JsonPropertyName("windspeed")]
    public double Windspeed { get; set; }

    [JsonPropertyName("winddirection")]
    public double WindDirection { get; set; }

    [JsonPropertyName("weathercode")]
    public int WeatherCode { get; set; }

    [JsonPropertyName("time")]
    public string Time { get; set; } = string.Empty;
}

public class OpenMeteoCurrentDto
{
    [JsonPropertyName("time")]
    public string Time { get; set; } = string.Empty;

    [JsonPropertyName("temperature_2m")]
    public double Temperature { get; set; }

    [JsonPropertyName("apparent_temperature")]
    public double ApparentTemperature { get; set; }

    [JsonPropertyName("relative_humidity_2m")]
    public double RelativeHumidity { get; set; }

    [JsonPropertyName("surface_pressure")]
    public double SurfacePressure { get; set; }

    [JsonPropertyName("visibility")]
    public double Visibility { get; set; }

    [JsonPropertyName("wind_speed_10m")]
    public double WindSpeed { get; set; }

    [JsonPropertyName("wind_direction_10m")]
    public double WindDirection { get; set; }

    [JsonPropertyName("weather_code")]
    public int WeatherCode { get; set; }

    [JsonPropertyName("is_day")]
    public int IsDay { get; set; }
}
