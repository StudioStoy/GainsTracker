using System.Net;

namespace GainsTracker.UI.Services.API;

public class ApiResult<T>
{
    public bool IsSuccess { get; set; }
    public bool IsEmptyResponse { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string? ErrorMessage { get; set; }
    public T? Value { get; private init; }

    public static ApiResult<T> Success(T? value, HttpStatusCode statusCode, bool isEmpty = false) =>
        new() { IsSuccess = true, Value = value, StatusCode = statusCode, IsEmptyResponse = isEmpty };

    public static ApiResult<T> Failure(HttpStatusCode statusCode, string? error = null) =>
        new() { IsSuccess = false, StatusCode = statusCode, ErrorMessage = error };
}

public class ApiResult
{
    public bool IsSuccess { get; set; }
    public bool IsEmptyResponse { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string? ErrorMessage { get; set; }

    public static ApiResult Success(HttpStatusCode statusCode, bool isEmpty = false) =>
        new() { IsSuccess = true, StatusCode = statusCode, IsEmptyResponse = isEmpty };

    public static ApiResult Failure(HttpStatusCode statusCode, string? error = null) =>
        new() { IsSuccess = false, StatusCode = statusCode, ErrorMessage = error };
}
