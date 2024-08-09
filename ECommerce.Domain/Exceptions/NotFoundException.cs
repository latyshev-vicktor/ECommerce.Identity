using ECommerce.Domain.Common;
using ECommerce.Domain.Errors;

namespace ECommerce.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public string DefaultMessage { get; } = "Сущность не найдена";
        public ResultCode ResultCode { get; }
        public NotFoundException(Error error) : base(error.Message)
        {
            ResultCode = ResultCode.NotFound;
        }
    }
}
