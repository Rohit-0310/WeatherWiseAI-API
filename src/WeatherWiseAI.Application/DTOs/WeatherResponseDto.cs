namespace WeatherWiseAI.Application.DTOs;

public class WeatherResponseDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Temperature { get; set; }
    public double WindSpeed { get; set; }
    public int WeatherCode { get; set; }
}
