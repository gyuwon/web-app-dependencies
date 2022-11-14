using CommandModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public sealed class InvariantViolationExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is InvariantViolationException exception)
        {
            context.Result = new BadRequestObjectResult(exception.Error);
            context.ExceptionHandled = true;
        }
    }
}
