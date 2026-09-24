using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WeatherWiseAI.Infrastructure.Gemini.DTOs;

public class GeminiResponse
{
    [JsonPropertyName("candidates")]
    public List<GeminiCandidate> Candidates { get; set; } = new();
}

public class GeminiCandidate
{
    [JsonPropertyName("content")]
    public GeminiContent Content { get; set; } = new();
}
