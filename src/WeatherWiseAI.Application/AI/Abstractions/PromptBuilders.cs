namespace WeatherWiseAI.Application.AI.Abstractions;

using System.Threading;
using System.Threading.Tasks;
using WeatherWiseAI.Application.Weather.Shared.WeatherContext;

public interface IPromptBuilder
{
    Task<string> BuildPromptAsync(WeatherContext context, CancellationToken cancellationToken = default);
}

public interface IWeatherSummaryPromptBuilder : IPromptBuilder { }
public interface IClothingPromptBuilder : IPromptBuilder { }
public interface IActivityPromptBuilder : IPromptBuilder { }
public interface ITravelPromptBuilder : IPromptBuilder { }
public interface IHealthPromptBuilder : IPromptBuilder { }
public interface IWeeklySummaryPromptBuilder : IPromptBuilder { }
