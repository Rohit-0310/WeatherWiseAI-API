# Prompt Engineering Guide

This document outlines the strategy for generating high-quality AI insights using the Google Gemini API.

## How Prompts are Built
Prompts are constructed dynamically in the `Infrastructure` layer. The application takes structured weather data (temperature, wind, conditions) and formats it into natural language instructions before sending it to Gemini.

## Prompt Builder Strategy
- **Separation of Concerns**: The logic for building prompts is decoupled from the HTTP client. 
- **Builder Pattern**: We use classes dedicated solely to constructing the prompt string based on the provided Domain models.
- **Context Injection**: Prompts must explicitly instruct the AI on its role ("You are a helpful weather assistant") and format ("Provide a concise summary").

## Prompt Templates
We utilize C# string interpolation (`$"{...}"`) to inject data into predefined templates.

*Example Template:*
> You are a witty weather forecaster. The current temperature is {Temperature}°C with {WindSpeed}km/h winds. The condition is {Condition}. Write a short, engaging 2-sentence summary for a user.

## Temperature Strategy
- **Low Temperature (0.1 - 0.3)**: Used when factual accuracy is paramount (e.g., severe weather warnings).
- **Medium Temperature (0.4 - 0.7)**: Default setting for general weather summaries to allow natural language variation while remaining accurate.
- **High Temperature (0.8+)**: Avoided, to prevent hallucinated weather data.

## Output Validation
- We instruct Gemini to return plain text or structured JSON.
- If JSON is requested, we use strong deserialization. If the structure is malformed, we catch the exception and fall back gracefully.
- The Application layer validates that the AI response is not null or empty before returning it to the user.

## Safety
- **Prompt Injection**: User input is not directly concatenated into prompts. If location names are used, they are validated against strict regex or alphanumeric rules.
- **Content Filtering**: Gemini's built-in safety settings are configured to block hate speech, harassment, and dangerous content.

## Future Prompt Improvements
- **Few-Shot Prompting**: Include examples of good weather summaries within the prompt to guide the AI's tone.
- **System Instructions**: Migrate to using explicit System Prompts if supported by the specific Gemini API endpoint utilized.
- **User Personas**: Allow prompts to be tweaked based on user preferences (e.g., "explain it for a runner" vs "explain it for a gardener").
