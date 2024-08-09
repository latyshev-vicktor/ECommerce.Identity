using ECommerce.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Extensions
{
    public static class ApiExtension
    {
        public static IActionResult AsHttpResult(this IExecutionResult executionResult)
        {
            if (executionResult.IsFailure)
            {
                var result = FailureResult(executionResult);
                return result;
            }

            return new OkResult();
        }

        public static IActionResult AsHttpResult<T>(this IExecutionResult<T> executionResult)
        {
            if (executionResult.IsFailure)
            {
                var result = FailureResult(executionResult);
                return result;
            }

            return new OkObjectResult(executionResult.Value);
        }

        private static IActionResult FailureResult(IExecutionResult executionResult)
        {
            var errorMessage = executionResult.Error.Message;

            return executionResult.Error.ResultCode switch
            {
                ResultCode.NotFound => new NotFoundObjectResult(errorMessage),
                ResultCode.BadRequest => new BadRequestObjectResult(errorMessage),
                ResultCode.Conflict => new ConflictObjectResult(errorMessage),
                _ => new BadRequestObjectResult(errorMessage)
            };
        }
    }
}
