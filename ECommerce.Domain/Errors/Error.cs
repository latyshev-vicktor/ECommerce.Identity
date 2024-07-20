using ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Errors
{
    public sealed class Error
    {
        public string Message { get; }
        public ResultCode ResultCode { get; }

        public Error(ResultCode resuleCode, string message)
        {
            ResultCode = resuleCode;
            Message = message;
        }

        public static Error None() => new(ResultCode.Success, string.Empty);
    }
}
