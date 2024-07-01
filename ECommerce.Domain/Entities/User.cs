using CSharpFunctionalExtensions;
using ECommerce.Domain.Enums;
using ECommerce.Domain.SeedWorks;
using ECommerce.Domain.ValueObjects;

namespace ECommerce.Domain.Entities
{
    public class User : BaseEntity, IAgreegateRoot
    {
        public string UserName { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public bool IsBlocked { get; private set; }

        public UserType UserType { get; private set; }

        public List<Role> Roles = [];

        #region Value Objects
        public Email Email { get; private set; }
        public FullName FullName { get; private set; }
        #endregion


        #region Конструкторы
        protected User() { }

        protected User(
            Guid userId, string userName, string password,
            UserType userType, Email email, FullName fullName)
        {
            Id = userId;
            UserName = userName;
            Password = password;
            UserType = userType;
            Email = email;
            FullName = fullName;
        }
        #endregion

        public static Result<User> Create(Guid userId, string userName, string password,
            UserType userType, string email, string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(userName)) return Result.Failure<User>("User name не может быть пустым");
            if (string.IsNullOrWhiteSpace(password)) return Result.Failure<User>("Пароль не может быть пустым");

            var emailResult = Email.Create(email);
            if (emailResult.IsFailure)
                return Result.Failure<User>(emailResult.Error);

            var fullNameResult = FullName.Create(firstName, lastName);
            if (fullNameResult.IsFailure)
                return Result.Failure<User>(fullNameResult.Error);

            return Result.Success(new User(userId, userName, password, userType, emailResult.Value, fullNameResult.Value));
        }

        #region DDD методы
        public void ChangeUserName(string userName) => UserName = userName;
        public void ChangePassword(string password) => Password = password;
        public void ChangeEmail(Email email) => Email = email;
        public void ChangeFullName(FullName fullName) => FullName = fullName;
        public void BlockedUser() => IsBlocked = true;
        public void UnBlockedUser() => IsBlocked = false;
        #endregion
    }
}
