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
            Message = context.Exception.Message,
            RequestId = context.HttpContext.TraceIdentifier,
            Details = ResponseDetailSetter(context.Exception),
        };

        var statusCode = context.Exception switch
        {
            BadRequestException _ => (int)HttpStatusCode.BadRequest,
            ArgumentException _ => (int)HttpStatusCode.BadRequest,
            InvalidOperationException _ => (int)HttpStatusCode.BadRequest,
            NotFoundException _ => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.InternalServerError
        };

        context.Result = new JsonResult(errorResponse)
        {
            StatusCode = statusCode
        };

        context.ExceptionHandled = true;
    }

    private static string ResponseDetailSetter(Exception ex)
    {
        if (ex is TaskCanceledException) return "Task was cancelled.";
        if (ex is BadRequestException exception)
        {
            if (!string.IsNullOrEmpty(exception.Details)) return exception.Details!;
        }

        return "Error occured.";
    }
}