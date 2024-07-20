using ECommerce.Domain.Common;

namespace ECommerce.Domain.Errors
{
    public static class UserErrors
    {
        public static Error NotFoundById() => new Error(ResultCode.NotFound, "По данному идентификатору пользователь не найден");
        public static Error ExistByEmail() => new Error(ResultCode.Conflict, "Пользователь с таким email уже зарегестрирован");
        public static Error BlockedUser() => new Error(ResultCode.Conflict, "Пользователь заблокирован");
        public static Error RolesNotFound() => new Error(ResultCode.NotFound, "Перечень ролей по переданным идентификаторам не найдены");

        public static Error UserNameNotBeEmpty() => new Error(ResultCode.BadRequest, "User name не может быть пустым");
        public static Error PasswordNotBeEmplty() => new Error(ResultCode.BadRequest, "Пароль не может быть пустым");
        public static Error NotCorrentUserType() => new Error(ResultCode.BadRequest, "Некорректно переданный тип пользователя");
        public static Error EmailNotBeEmpty() => new Error(ResultCode.BadRequest, "Email не может быть пустым");
        public static Error EmailNotValid() => new Error(ResultCode.BadRequest, "Введенное значение не является email");
        public static Error FirstNameNotBeEmpty() => new Error(ResultCode.BadRequest, "Имя пользователя не может быть пустым");
        public static Error LastNameNotBeEmpty() => new Error(ResultCode.BadRequest, "Фамилия пользователя не может быть пустой");
    }
}
