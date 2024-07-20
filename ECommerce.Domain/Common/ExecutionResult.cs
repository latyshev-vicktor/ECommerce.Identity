using ECommerce.Domain.Errors;

namespace ECommerce.Domain.Common
{
    public class ExecutionResult : IExecutionResult
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        protected ExecutionResult(bool isSuccess, Error error) 
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static IExecutionResult Success()
            => new ExecutionResult(true, Error.None());

        public static IExecutionResult Failure(Error error)
            => new ExecutionResult(false, error);

        public static IExecutionResult<T> Failure<T>(Error error)
            => ExecutionResult<T>.Create(default(T), false, error);

        public static IExecutionResult<T> Success<T>(T result)
            => ExecutionResult<T>.Create(result, true, Error.None());
    }

    public class ExecutionResult<T> : ExecutionResult, IExecutionResult<T>
    {
        protected ExecutionResult(T result, bool isSuccess, Error error) : base(isSuccess, error)
        {
            Value = result;
        }

        public T Value { get; }

        public static IExecutionResult<T> Create(T result, bool isSuccess, Error error)
            => new ExecutionResult<T>(result, isSuccess, error);
    }
}
