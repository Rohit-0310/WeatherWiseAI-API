namespace WeatherWiseAI.Application.DTOs;

public class WeatherResponseDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Temperature { get; set; }
    public double WindSpeed { get; set; }
    public int WeatherCode { get; set; }
    public double ApparentTemperature { get; set; }
    public double RelativeHumidity { get; set; }
    public double SurfacePressure { get; set; }
    public double Visibility { get; set; }
    public double WindDirection { get; set; }
    public int IsDay { get; set; }
    public int? Aqi { get; set; }
    public double MaxTemperature { get; set; }
    public double MinTemperature { get; set; }
    public double UvIndex { get; set; }
    public int PrecipitationProbability { get; set; }
}
