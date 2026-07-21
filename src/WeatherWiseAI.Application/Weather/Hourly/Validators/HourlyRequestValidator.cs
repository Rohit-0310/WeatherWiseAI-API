namespace WeatherWiseAI.Application.Weather.Hourly.Validators;

using FluentValidation;
using WeatherWiseAI.Application.Weather.Hourly.Requests;

public class HourlyRequestValidator : AbstractValidator<HourlyRequest>
{
    public HourlyRequestValidator()
    {
        RuleFor(x => x.Latitude).InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");
        RuleFor(x => x.Longitude).InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");
        RuleFor(x => x.Hours).InclusiveBetween(1, 48).WithMessage("Hours must be between 1 and 48.");
        RuleFor(x => x.Units).IsInEnum().WithMessage("Invalid unit system specified.");
    }
}
