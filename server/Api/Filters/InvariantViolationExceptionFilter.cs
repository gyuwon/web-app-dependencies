using CommandModel;
using Foundation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public sealed class InvariantViolationExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is InvariantViolationException exception)
        {
            ServiceErrorCarrier error = new(exception.Error);
            context.Result = new BadRequestObjectResult(error);
            context.ExceptionHandled = true;
        }
    }
}
