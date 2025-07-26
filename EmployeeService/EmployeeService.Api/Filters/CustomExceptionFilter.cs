using System.Net;
using EmployeeService.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace EmployeeService.Api.Filters;

public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        Log.Error(context.Exception, "Unhandled exception occurred: {ExceptionMessage}",
            context.Exception.Message);

        var errorResponse = new ErrorResponse
        {
            Message = "An unexpected error occurred.",
            RequestId = context.HttpContext.TraceIdentifier,
            Details = context.Exception.Message
        };

        var statusCode = context.Exception switch
        {
            BadRequestException _ => (int)HttpStatusCode.BadRequest,
            ArgumentException _ => (int)HttpStatusCode.BadRequest,
            InvalidOperationException _ => (int)HttpStatusCode.BadRequest,
            NotFoundException _ => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.InternalServerError
        };

        if (statusCode != (int)HttpStatusCode.InternalServerError)
        {
            errorResponse.Message = context.Exception.Message;
        }

        context.Result = new JsonResult(errorResponse)
        {
            StatusCode = statusCode
        };

        context.ExceptionHandled = true;
    }
}