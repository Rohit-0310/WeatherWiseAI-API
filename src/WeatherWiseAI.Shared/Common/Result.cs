namespace WeatherWiseAI.Shared.Common;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public ApiError? Error { get; }

    protected Result(bool isSuccess, T? value, ApiError? error)
    {
        if (isSuccess && error != null)
            throw new InvalidOperationException("Successful result cannot have an error");
        if (!isSuccess && error == null)
            throw new InvalidOperationException("Failed result must have an error");

        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value) => new(true, value, null);
    public static Result<T> Failure(ApiError error) => new(false, default, error);
}

public class Result : Result<string>
{
    protected Result(bool isSuccess, ApiError? error) : base(isSuccess, string.Empty, error)
    {
    }

    public static Result Success() => new(true, null);
    public static new Result Failure(ApiError error) => new(false, error);
}
