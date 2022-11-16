using CommandModel;
using Foundation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public sealed class EntityNotFoundExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is EntityNotFoundException exception)
        {
            ServiceErrorCarrier error = new(exception.Error);
            context.Result = new NotFoundObjectResult(error);
            context.ExceptionHandled = true;
        }
    }
}
