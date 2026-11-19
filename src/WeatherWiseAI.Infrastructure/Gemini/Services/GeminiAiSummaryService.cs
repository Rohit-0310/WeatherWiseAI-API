using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WeatherWiseAI.Application.Weather.AiSummary.Interfaces;
using WeatherWiseAI.Application.Weather.AiSummary.Requests;
using WeatherWiseAI.Application.Weather.AiSummary.Responses;
using WeatherWiseAI.Infrastructure.Configuration;
using WeatherWiseAI.Infrastructure.Gemini.DTOs;
using WeatherWiseAI.Shared.Common;

namespace WeatherWiseAI.Infrastructure.Gemini.Services;

public class GeminiAiSummaryService : IAiSummaryService
{
    private readonly HttpClient _httpClient;
    private readonly GeminiOptions _options;
    private readonly ILogger<GeminiAiSummaryService> _logger;

    public GeminiAiSummaryService(HttpClient httpClient, IOptions<GeminiOptions> options, ILogger<GeminiAiSummaryService> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<Result<AiSummaryResponse>> GenerateSummaryAsync(AiSummaryRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var prompt = $"Current Weather:\nLocation: {request.Location}\nTemperature: {request.Temperature}\nCondition: {request.Condition}\nHumidity: {request.Humidity}\nWind: {request.Wind}\nUV Index: {request.UvIndex}\n\nProvide a brief weather summary, clothing recommendation, and outdoor activity suggestion. You MUST return ONLY a raw, valid JSON object without any markdown formatting or extra text. Format exactly like this: {{\"summary\": \"...\", \"clothing\": \"...\", \"activity\": \"...\"}}";

            var geminiRequest = new GeminiRequest
            {
                Contents = new()
                {
                    new GeminiContent
                    {
                        Parts = new()
                        {
                            new GeminiPart { Text = prompt }
                        }
                    }
                },
                GenerationConfig = new GeminiGenerationConfig
                {
                    Temperature = 0.7,
                    MaxOutputTokens = 2048,
                    ResponseMimeType = "application/json"
                }
            };

            var url = $"/v1beta/models/{_options.ModelName}:generateContent";

            using var message = new HttpRequestMessage(HttpMethod.Post, url) { Content = JsonContent.Create(geminiRequest) };
            message.Headers.Add("x-goog-api-key", _options.ApiKey);
            using var response = await _httpClient.SendAsync(message, cancellationToken);
            response.EnsureSuccessStatusCode();

            var geminiResponse = await response.Content.ReadFromJsonAsync<GeminiResponse>(cancellationToken: cancellationToken);
            var textResult = geminiResponse?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;

            if (string.IsNullOrEmpty(textResult))
            {
                return Result<AiSummaryResponse>.Failure(new ApiError { Message = "Failed to generate AI summary." });
            }

            // Remove markdown code block if present
            textResult = textResult.Trim();
            if (textResult.StartsWith("```json"))
            {
                textResult = textResult.Substring(7);
            }
            if (textResult.StartsWith("```"))
            {
                textResult = textResult.Substring(3);
            }
            if (textResult.EndsWith("```"))
            {
                textResult = textResult.Substring(0, textResult.Length - 3);
            }
            textResult = textResult.Trim();

            var resultObj = JsonSerializer.Deserialize<AiSummaryResponse>(textResult, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (resultObj == null || string.IsNullOrWhiteSpace(resultObj.Summary) ||
                string.IsNullOrWhiteSpace(resultObj.Clothing) || string.IsNullOrWhiteSpace(resultObj.Activity))
            {
                return Result<AiSummaryResponse>.Failure(new ApiError { Message = "Failed to parse AI summary JSON." });
            }

            return Result<AiSummaryResponse>.Success(resultObj);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating AI summary from Gemini.");
            return Result<AiSummaryResponse>.Failure(new ApiError { Message = "An error occurred while communicating with the AI service." });
        }
    }
}
