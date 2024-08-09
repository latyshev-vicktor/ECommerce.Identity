using ECommerce.Domain.Common;

namespace ECommerce.Domain.Errors
{
    public static class UserErrorIds
    {
        public const string NotFoundById = "По данному идентификатору пользователь не найден";
        public const string ExistByEmail = "Пользователь с таким email уже зарегестрирован";
        public const string BlockedUser = "Пользователь заблокирован";
        public const string RolesNotFound = "Перечень ролей по переданным идентификаторам не найдены";
        public const string UserNameNotBeEmpty = "User name не может быть пустым";
        public const string PasswordNotBeEmpty = "Пароль не может быть пустым";
        public const string NotCorrectUserType = "Некорректно переданный тип пользователя";
        public const string EmailNotBeEmpty = "Email не может быть пустым";
        public const string EmailNotValid = "Введенное значение не является email";
        public const string FirstNameNotBeEmpty = "Имя пользователя не может быть пустым";
        public const string LastNameNotBeEmpty = "Фамилия пользователя не может быть пустой";
        public const string NotCorrectEmailOrPassword = "Неправильный email или пароль";
        public const string ExistUserByEmailOrUserName = "Пользователь с таким User name или email уже существует";
    }

    public static class UserErrors
    {
        public static Error NotFoundById() => new Error(ResultCode.NotFound, UserErrorIds.NotFoundById);
        public static Error ExistByEmail() => new Error(ResultCode.Conflict, UserErrorIds.ExistByEmail);
        public static Error BlockedUser() => new Error(ResultCode.Conflict, UserErrorIds.BlockedUser);
        public static Error RolesNotFound() => new Error(ResultCode.NotFound, UserErrorIds.RolesNotFound);

        public static Error UserNameNotBeEmpty() => new Error(ResultCode.BadRequest, UserErrorIds.UserNameNotBeEmpty);
        public static Error PasswordNotBeEmpty() => new Error(ResultCode.BadRequest, UserErrorIds.PasswordNotBeEmpty);
        public static Error NotCorrentUserType() => new Error(ResultCode.BadRequest, UserErrorIds.NotCorrectUserType);
        public static Error EmailNotBeEmpty() => new Error(ResultCode.BadRequest, UserErrorIds.EmailNotBeEmpty);
        public static Error EmailNotValid() => new Error(ResultCode.BadRequest, UserErrorIds.EmailNotValid);
        public static Error FirstNameNotBeEmpty() => new Error(ResultCode.BadRequest, UserErrorIds.FirstNameNotBeEmpty);
        public static Error LastNameNotBeEmpty() => new Error(ResultCode.BadRequest, UserErrorIds.LastNameNotBeEmpty);
        public static Error NotCorrentEmailOrPassword() => new Error(ResultCode.BadRequest, UserErrorIds.NotCorrectEmailOrPassword);
        public static Error ExistUserByEmailOrUserName() => new Error(ResultCode.Conflict, UserErrorIds.ExistUserByEmailOrUserName);
    }
}
