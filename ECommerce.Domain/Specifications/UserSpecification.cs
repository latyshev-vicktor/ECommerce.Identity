using ECommerce.Domain.Entities;
using NSpecifications;

namespace ECommerce.Domain.Specifications
{
    public class UserSpecification
    {
        public static Spec<User> ById(Guid userId)
        {
            return new Spec<User>(x => x.Id == userId);
        }

        public static Spec<User> ByEmail(string email)
        {
            return new Spec<User>(x => x.Email.Value == email);
        }

        public static Spec<User> ByUserName(string userName)
        {
            return new Spec<User>(x => x.UserName == userName);
        }

        public static Spec<User> ByRoleId(Guid roleId)
        {
            return new Spec<User>(x => x.Roles.Any(x => x.Id == roleId));
        }
    }
}
