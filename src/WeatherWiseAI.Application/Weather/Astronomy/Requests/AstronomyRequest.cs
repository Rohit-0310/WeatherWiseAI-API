namespace WeatherWiseAI.Application.Weather.Astronomy.Requests;

using System;
using System.ComponentModel.DataAnnotations;

public class AstronomyRequest
{
    [Required]
    [Range(-90, 90)]
    public double Latitude { get; set; }

    [Required]
    [Range(-180, 180)]
    public double Longitude { get; set; }

    public DateTime? Date { get; set; }
}
