namespace WeatherWiseAI.Application.Weather.Shared.WeatherContext;

using System.Collections.Generic;

public class WeatherContext
{
    public WeatherLocation? Location { get; set; }
    public WeatherCurrent? Current { get; set; }
    public WeatherHourly? Hourly { get; set; }
    public WeatherWeekly? Weekly { get; set; }
    public WeatherMetadata? Metadata { get; set; }
    public WeatherUnits? Units { get; set; }
    public WeatherAstronomy? Astronomy { get; set; }
    public AIHints? Hints { get; set; }
}

public class WeatherLocation
{
    public string Name { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Timezone { get; set; } = string.Empty;
}

public class WeatherMetadata
{
    public System.DateTime GeneratedAt { get; set; }
    public string Provider { get; set; } = "Open-Meteo";
}

public class WeatherUnits
{
    public string Temperature { get; set; } = string.Empty;
    public string WindSpeed { get; set; } = string.Empty;
}

public class WeatherAstronomy
{
    public System.DateTime? Sunrise { get; set; }
    public System.DateTime? Sunset { get; set; }
}

public class WeatherCurrent
{
    public double Temperature { get; set; }
    public double WindSpeed { get; set; }
    public WeatherCondition Condition { get; set; } = new();
}

public class WeatherHourly
{
    public List<HourlyWeatherSnapshot> Snapshots { get; set; } = new();
}

public class HourlyWeatherSnapshot
{
    public System.DateTime Time { get; set; }
    public double Temperature { get; set; }
    public double WindSpeed { get; set; }
    public WeatherCondition Condition { get; set; } = new();
}

public class WeatherWeekly
{
    public List<DailyWeatherSnapshot> Days { get; set; } = new();
}

public class DailyWeatherSnapshot
{
    public System.DateTime Date { get; set; }
    public double MaxTemperature { get; set; }
    public double MinTemperature { get; set; }
    public WeatherCondition Condition { get; set; } = new();
}

public class WeatherCondition
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ProviderCode { get; set; }
}

public class AIHints
{
    public bool OutdoorSuitable { get; set; }
    public bool UmbrellaRecommended { get; set; }
    public bool HeavyRainExpected { get; set; }
    public bool HotWeather { get; set; }
    public bool ColdWeather { get; set; }
    public bool HighUV { get; set; }
    public bool Windy { get; set; }
    public bool PoorVisibility { get; set; }
    public string ComfortLevel { get; set; } = string.Empty;
    public string TravelRisk { get; set; } = string.Empty;
    public string ExerciseSuitability { get; set; } = string.Empty;
}
