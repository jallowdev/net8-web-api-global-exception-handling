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
        if (exception is BadHttpRequestException)
        {
            return await BadRequest(httpContext, exception, cancellationToken);
        }
        else if (exception is NotFoundException)
        {
           return await NotFound(httpContext, exception, cancellationToken);
        }
        else if (exception is Exception)
        {
           return await InternalError(httpContext, exception, cancellationToken);
        }
        else
        {
            return false;
        }
       
    }
    
    public async ValueTask<bool> BadRequest(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception:exception, message:"Exception occurred: {Message} ", exception.Message);

        var problemDetails = new ProblemDetails()
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad Request",
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
    public async ValueTask<bool> NotFound(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception:exception, message:"Exception occurred: {Message} ", exception.Message);

        var problemDetails = new ProblemDetails()
        {
            Status = StatusCodes.Status404NotFound,
            Title = "NotFound",
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
    public async ValueTask<bool> InternalError(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception:exception, message:"Exception occurred: {Message} ", exception.Message);

        var problemDetails = new ProblemDetails()
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}