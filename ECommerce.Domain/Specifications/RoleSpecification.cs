using ECommerce.Domain.Entities;
using NSpecifications;

namespace ECommerce.Domain.Specifications
{
    public class RoleSpecification
    {
        public static Spec<Role> ByIds(Guid[] roleIds)
        {
            return new Spec<Role>(x => roleIds.Contains(x.Id));
        }

        public static Spec<Role> ByName(string name)
        {
            return new Spec<Role>(x => x.Name == name);
        }
    }
}
