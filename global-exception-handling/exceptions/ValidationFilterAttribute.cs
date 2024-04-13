using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace global_exception_handling.exceptions;

public class ValidationFilterAttribute:ActionFilterAttribute
{
    private readonly ILogger<ValidationFilterAttribute> _logger;

    public ValidationFilterAttribute(ILogger<ValidationFilterAttribute> logger)
    {
        _logger = logger;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation($"######## Action '{context.ActionDescriptor.DisplayName}' is starting.");
        if (!context.ModelState.IsValid)
        {
            //throw new BadHttpRequestException(context.ModelState);
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        throw new NotImplementedException();
    }
}