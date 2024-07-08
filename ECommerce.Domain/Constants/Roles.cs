using ECommerce.Domain.Extensions;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Constants
{
    public class Roles
    {
        [Display(Name = "Администратор")]
        public static string Admin = "Admin";

        [Display(Name = "Заказчик")]
        public static string Customer = "Customer";

        [Display(Name = "Продавец")]
        public static string Salesman = "Salesman";
    }

    public class DefaultRoles
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid[] Permissions { get; set; }


        public static DefaultRoles Admin = new()
        {
            Id = Guid.Parse("47d066c9-11a0-47dc-8dcf-efdb719b219a"),
            Name = Roles.Admin,
            Description = Roles.Admin.GetDisplay<Roles>(),
            Permissions = 
            [
                DefaultPermissions.BlockedUser.Id, 
                DefaultPermissions.UnBlockedUser.Id, 
                DefaultPermissions.AssignRoles.Id
            ]
        };

        public static DefaultRoles Customer = new()
        {

            Id = Guid.Parse("46a48ac3-cb7c-4631-a7c4-70fc32f4c2fa"),
            Name = Roles.Customer,
            Description = Roles.Customer.GetDisplay<Roles>(),
            Permissions =
            [
                DefaultPermissions.CreateOrder.Id,
                DefaultPermissions.UpdateOrder.Id
            ]
        };

        public static DefaultRoles Salesman = new()
        {

            Id = Guid.Parse("d7b9a088-a7ce-4f34-9d66-1aec7d62423e"),
            Name = Roles.Customer,
            Description = Roles.Customer.GetDisplay<Roles>(),
            Permissions =
            [
                DefaultPermissions.CreateProduct.Id,
                DefaultPermissions.UpdateProduct.Id
            ]
        };

        public static IReadOnlyList<DefaultRoles> All =
        [
            Admin,
            Customer,
            Salesman,
        ];
    }
}
