namespace WeatherWiseAI.Shared.Common;

public class ApiResponse<T>
{
    public T? Data { get; init; }
    public bool Success { get; init; }
    public IEnumerable<string> Errors { get; init; } = Array.Empty<string>();
    public object? Meta { get; init; }

    public static ApiResponse<T> SuccessResponse(T data, object? meta = null)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data,
            Meta = meta
        };
    }

    public static ApiResponse<T> ErrorResponse(IEnumerable<string> errors)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Errors = errors
        };
    }

    public static ApiResponse<T> ErrorResponse(string error)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Errors = new[] { error }
        };
    }
}
