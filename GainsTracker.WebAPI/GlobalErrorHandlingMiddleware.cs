#region

using System.Net;
using System.Text.Json;
using GainsTracker.Common.Exceptions;
using GainsTracker.Core.Friends.Exceptions;

#endregion

namespace GainsTracker.WebAPI;

public class GlobalErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode status;
        var stackTrace = string.Empty;
        var message = exception.Message;

        switch (exception)
        {
            case BadRequestException:
                status = HttpStatusCode.BadRequest;
                break;
            case NotFoundException:
                status = HttpStatusCode.NotFound;
                break;
            case ConflictException:
            case AlreadyFriendsException:
            case FriendRequestAlreadySentException:
                status = HttpStatusCode.Conflict;
                break;
            case ForbiddenException:
                status = HttpStatusCode.Forbidden;
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

        var exceptionResult = JsonSerializer.Serialize(new ErrorResult(message, stackTrace));
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) status;

        return context.Response.WriteAsync(exceptionResult);
    }
}
