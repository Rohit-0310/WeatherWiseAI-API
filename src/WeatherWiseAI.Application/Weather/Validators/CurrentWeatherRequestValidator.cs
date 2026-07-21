namespace WeatherWiseAI.Application.Weather.Validators;

using FluentValidation;
using WeatherWiseAI.Application.Weather.DTOs;

public class CurrentWeatherRequestValidator : AbstractValidator<CurrentWeatherRequest>
{
    public CurrentWeatherRequestValidator()
    {
        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");
            
        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");
            
        RuleFor(x => x.Units)
            .IsInEnum().WithMessage("Invalid unit system specified.");
    }
}
