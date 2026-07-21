namespace WeatherWiseAI.Application.Location.DTOs;

public class LocationResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Country { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Timezone { get; set; } = string.Empty;
}
