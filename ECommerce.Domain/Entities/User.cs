using CSharpFunctionalExtensions;
using ECommerce.Domain.DomainEvents;
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
            UserType userType, Email email, 
            FullName fullName, List<Role> roles)
        {
            Id = userId;
            UserName = userName;
            Password = password;
            UserType = userType;
            Email = email;
            FullName = fullName;
            AddRoles(roles);

            AddDomainEvent(new CreatedUserEvent(userId, userName, email.Value));
        }
        #endregion

        public static Result<User> Create(Guid userId, string userName, string password,
            int userType, string email, string firstName, string lastName, List<Role> roles)
        {
            if (string.IsNullOrEmpty(userName)) return Result.Failure<User>("User name не может быть пустым");
            if (string.IsNullOrWhiteSpace(password)) return Result.Failure<User>("Пароль не может быть пустым");
            if (!Enum.TryParse(userType.ToString(), out UserType userTypeEnum)) return Result.Failure<User>("Некорректно переданный тип пользователя");

            var emailResult = Email.Create(email);
            if (emailResult.IsFailure)
                return Result.Failure<User>(emailResult.Error);

            var fullNameResult = FullName.Create(firstName, lastName);
            if (fullNameResult.IsFailure)
                return Result.Failure<User>(fullNameResult.Error);

            return Result.Success(new User(userId, userName, password, userTypeEnum, emailResult.Value, fullNameResult.Value, roles));
        }

        #region DDD методы
        public void ChangeUserName(string userName) => UserName = userName;
        public void ChangePassword(string password) => Password = password;
        public void ChangeEmail(Email email) => Email = email;
        public void ChangeFullName(FullName fullName) => FullName = fullName;
        public void BlockedUser() => IsBlocked = true;
        public void UnBlockedUser() => IsBlocked = false;

        public void AddRoles(List<Role> roles)
        {
            foreach (var role in roles)
            {
                if (!Roles.Any(x => x.Id == role.Id))
                    Roles.Add(role);
            }
        }

        public void RemoveRoles(List<Role> roles)
        {
            foreach(var role in roles)
            {
                if (Roles.Any(x => x.Id == role.Id))
                    Roles.Remove(role);
            }
        }
        #endregion
    }
}
