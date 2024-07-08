using ECommerce.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Constants
{
    //TODO: перенести в отдельную библиотеку и упаковать в Nuget
    public class Permissions
    {
        [Display(Name = "Заблокировать пользователя")]
        public static string BlockedUser = "BlockedUser";

        [Display(Name = "Разблокировать пользователя")]
        public static string UnBlockedUser = "UnBlockedUser";

        [Display(Name = "Создать заказ")]
        public static string CreateOrder = "CreateOrder";

        [Display(Name = "Обновить данные о заказе")]
        public static string UpdateOrder = "UpdateOrder";

        [Display(Name = "Создать продукт")]
        public static string CreateProduct = "CreateProduct";

        [Display(Name = "Обновить данные о продукте")]
        public static string UpdateProduct = "UpdateProduct";

        [Display(Name = "Удалить продукт")]
        public static string DeleteProduct = "DeleteProduct";

        [Display(Name = "Назначить роль пользователю")]
        public static string AssignRoles = "AssignRoles";
    }

    public class DefaultPermissions
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static DefaultPermissions BlockedUser = new()
        {
            Id = Guid.Parse("2f37347b-b1a1-4ada-98c4-83cec9a1c2f3"),
            Name = Permissions.BlockedUser,
            Description = Permissions.BlockedUser.GetDisplay<Permissions>()
        };

        public static DefaultPermissions UnBlockedUser = new()
        {
            Id = Guid.Parse("d836bcaa-146f-47fa-9ff0-474ed965fe3a"),
            Name = Permissions.UnBlockedUser,
            Description = Permissions.UnBlockedUser.GetDisplay<Permissions>()
        };

        public static DefaultPermissions CreateOrder = new()
        {
            Id = Guid.Parse("d79aa4c5-39df-4fba-bb13-c6ccd6b03f5b"),
            Name = Permissions.CreateOrder,
            Description = Permissions.CreateOrder.GetDisplay<Permissions>()
        };

        public static DefaultPermissions UpdateOrder = new()
        {
            Id = Guid.Parse("6e8e58cd-83fc-49bf-b10f-75b13f466b9f"),
            Name = Permissions.UpdateOrder,
            Description = Permissions.UpdateOrder.GetDisplay<Permissions>()
        };

        public static DefaultPermissions CreateProduct = new()
        {
            Id = Guid.Parse("bb70f3bf-c909-4c90-b6d5-c9bee26572c4"),
            Name = Permissions.CreateProduct,
            Description = Permissions.CreateProduct.GetDisplay<Permissions>()
        };

        public static DefaultPermissions UpdateProduct = new()
        {
            Id = Guid.Parse("2c1d68ff-79cf-41b5-a4a8-178affe94890"),
            Name = Permissions.UpdateProduct,
            Description = Permissions.UpdateProduct.GetDisplay<Permissions>()
        };

        public static DefaultPermissions AssignRoles = new()
        {
            Id = Guid.Parse("073d8184-a291-4694-9e8b-b77d79945358"),
            Name = Permissions.AssignRoles,
            Description = Permissions.AssignRoles.GetDisplay<Permissions>()
        };

        public static IReadOnlyList<DefaultPermissions> All =
        [
            BlockedUser,
            UnBlockedUser,
            CreateOrder,
            UpdateOrder,
            CreateProduct,
            UpdateProduct,
            AssignRoles
        ];
    }
}
