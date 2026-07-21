namespace WeatherWiseAI.Application.Location.Requests;

public class LocationSearchRequest
{
    public string Query { get; set; } = string.Empty;
    public string Language { get; set; } = "en";
    public int Count { get; set; } = 10;
}

public class LocationReverseRequest
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Language { get; set; } = "en";
}
