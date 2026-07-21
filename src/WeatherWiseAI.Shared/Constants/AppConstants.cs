namespace WeatherWiseAI.Shared.Constants;

public static class AppConstants
{
    public static class ErrorCodes
    {
        public const string ValidationFailed = "VALIDATION_FAILED";
        public const string InternalServerError = "INTERNAL_SERVER_ERROR";
        public const string NotFound = "NOT_FOUND";
        public const string Unauthorized = "UNAUTHORIZED";
        public const string BadGateway = "BAD_GATEWAY";
    }

    public static class Headers
    {
        public const string CorrelationId = "X-Correlation-ID";
    }
}
