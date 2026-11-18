# Gemini prompt and output

`GeminiAiSummaryService` builds one inline prompt from the six strings accepted by [the AI endpoint](API_GUIDELINES.md). It asks for a brief summary, clothing recommendation and outdoor activity suggestion. The controller passes client-provided values. It does not fetch a verified forecast or invoke the Application weather context builder.

The typed HttpClient calls Gemini `generateContent` using the configured model. Generation settings are temperature 0.7, maximum output of 2,048 tokens and JSON response MIME type. The adapter reads the first candidate's first text part, trims optional Markdown fences and deserializes case-insensitively to the response DTO. It does not enforce a response schema or require nonempty fields. Empty output, parse exceptions and provider errors follow the [AI error path](ERROR_HANDLING.md).

Application prompt interfaces and the weather context builder exist as extension points. Extracting the inline prompt, adding input bounds and evaluation fixtures, enforcing response fields and assessing prompt injection and weather advice are proposed improvements. Treat generated text as advisory. Keep keys and private data out of prompts, documentation and tests.
