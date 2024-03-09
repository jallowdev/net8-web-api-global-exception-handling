using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace global_exception_handling.exceptions;

public class GlobalExceptionHandler: IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var status=StatusCodes.Status200OK;
        var title = String.Empty;
        string message= String.Empty;
        
        if (exception is BadHttpRequestException) {
            message = exception.Message;
            status = StatusCodes.Status400BadRequest;
            title = "Bad Request";
        } else if (exception is NotFoundException || exception is KeyNotFoundException) {
            message = exception.Message;
            status = StatusCodes.Status404NotFound;
            title = "NotFound";
        } else if (exception is NotImplementedException) {
            status = StatusCodes.Status501NotImplemented;
            message = exception.Message;
            title = "Not Implemented";
        } else if (exception is UnauthorizedAccessException) {
            status = StatusCodes.Status401Unauthorized;
            message = exception.Message;
            title = "Unauthorize";
        }else {
            status = StatusCodes.Status500InternalServerError;
            message = exception.Message;
            title = "Server error";
        }
        
        _logger.LogInformation(exception:exception, message:"Exception occurred: {Message} ", exception.Message);

        var problemDetails = new ProblemDetails()
        {
            Status = status,
            Title = "Bad Request",
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;

    }
    
}