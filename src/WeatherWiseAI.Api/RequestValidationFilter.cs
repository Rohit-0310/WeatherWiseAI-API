namespace WeatherWiseAI.Api;

using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WeatherWiseAI.Shared.Common;

public sealed class RequestValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var errors = new List<string>();
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument is null or CancellationToken) continue;
            var serviceType = typeof(IValidator<>).MakeGenericType(argument.GetType());
            foreach (var validator in context.HttpContext.RequestServices.GetServices(serviceType).OfType<IValidator>())
            {
                var result = await validator.ValidateAsync(new ValidationContext<object>(argument), context.HttpContext.RequestAborted);
                errors.AddRange(result.Errors.Select(error => error.ErrorMessage));
            }
        }

        if (errors.Count > 0)
        {
            context.Result = new BadRequestObjectResult(ApiResponse<object>.ErrorResponse(errors));
            return;
        }

        await next();
    }
}
