namespace WeatherWiseAI.Shared.Exceptions;

public abstract class AppException : Exception
{
    public string? ErrorCode { get; }

    protected AppException(string message, string? errorCode = null) : base(message)
    {
        ErrorCode = errorCode;
    }

    protected AppException(string message, Exception innerException, string? errorCode = null) : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}
