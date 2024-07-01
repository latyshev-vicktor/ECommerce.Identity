using System.ComponentModel;

namespace ECommerce.Domain.Enums
{
    public enum UserType
    {
        [Description("Администратор")]
        Admin = 1,

        [Description("Продавец")]
        Salesman,

        [Description("Заказчик")]
        Customer
    }
}
