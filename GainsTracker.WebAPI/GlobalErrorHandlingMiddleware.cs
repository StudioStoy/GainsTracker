using System.Net;
using System.Text.Json;
using GainsTracker.Common.Exceptions;

namespace GainsTracker.WebAPI;

public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode status;
        string stackTrace = string.Empty;
        string message = exception.Message;

        switch (exception)
        {
            case BadRequestException:
                status = HttpStatusCode.BadRequest;
                break;
            case NotFoundException:
                status = HttpStatusCode.NotFound;
                break;
            case ConflictException:
                status = HttpStatusCode.Conflict;
                break;
            case UnauthorizedException:
            case UnauthorizedAccessException:
            case KeyNotFoundException:
                status = HttpStatusCode.Unauthorized;
                break;
            default:
                status = HttpStatusCode.InternalServerError;
                break;
        }

        string exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) status;

        return context.Response.WriteAsync(exceptionResult);
    }
}
