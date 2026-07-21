namespace WeatherWiseAI.Application.Location.Validators;

using FluentValidation;
using WeatherWiseAI.Application.Location.Requests;

public class LocationSearchRequestValidator : AbstractValidator<LocationSearchRequest>
{
    public LocationSearchRequestValidator()
    {
        RuleFor(x => x.Query).NotEmpty().MinimumLength(2).WithMessage("Search query must be at least 2 characters long.");
        RuleFor(x => x.Count).InclusiveBetween(1, 100).WithMessage("Count must be between 1 and 100.");
    }
}

public class LocationReverseRequestValidator : AbstractValidator<LocationReverseRequest>
{
    public LocationReverseRequestValidator()
    {
        RuleFor(x => x.Latitude).InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");
        RuleFor(x => x.Longitude).InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");
    }
}
