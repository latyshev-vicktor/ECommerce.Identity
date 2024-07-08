using ECommerce.Domain.Entities;
using NSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Specifications
{
    public class PermissionSpecification
    {
        public static Spec<Permission> ByIds(Guid[] permissionIds)
        {
            return new Spec<Permission>(x => permissionIds.Contains(x.Id));
        }
    }
}
