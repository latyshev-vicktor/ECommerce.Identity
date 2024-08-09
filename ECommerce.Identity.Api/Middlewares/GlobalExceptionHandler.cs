using ECommerce.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails();
            problemDetails.Title = "Серверная ошибка";

            switch(exception)
            {
                case ValidateException ex:
                    problemDetails.Detail = ex.Result.Error;
                    problemDetails.Status = (int)ex.ResultCode;
                    break;
                case NotFoundException ex:
                    problemDetails.Detail = string.Join(", ", ex.DefaultMessage, ex.Message);
                    problemDetails.Status = (int)ex.ResultCode;
                    break;
                default:
                    problemDetails.Detail = exception.Message;
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    break;
            }

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
