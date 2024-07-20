using ECommerce.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Common
{
    public interface IExecutionResult
    {
        bool IsSuccess { get; }
        bool IsFailure { get; }
        Error Error { get; }
    }

    public interface IExecutionResult<T> : IExecutionResult
    {
        T Value { get; }
    }
}
