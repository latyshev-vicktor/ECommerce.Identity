using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Common
{
    public enum ResultCode
    {
        Success = 1,
        NotFound,
        BadRequest,
        ValidationError,
        Conflict,
        ServerError
    }
}
