using CSharpFunctionalExtensions;
using ECommerce.Domain.Common;
using ECommerce.Domain.Errors;

namespace ECommerce.Domain.Exceptions
{
    public class ValidateException : ApplicationException
    {
        public Result Result { get; }
        public ResultCode ResultCode { get; }
        public ValidateException(IExecutionResult executionResult)
        {
            Result = executionResult.IsSuccess ? Result.Success() : Result.Failure($"{executionResult.Error.ResultCode} - {executionResult.Error.Message}");
            ResultCode = executionResult.Error.ResultCode;
        }

        public ValidateException(Result result) : base(result.Error)
        {
            Result = result;
            ResultCode = ResultCode.ServerError;
        }

        public ValidateException(Error error) : base(error.Message)
        {
            Result = Result.Failure(error.Message);
            ResultCode = error.ResultCode;
        }
    }
}
