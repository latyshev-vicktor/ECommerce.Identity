using CSharpFunctionalExtensions;
using ECommerce.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public List<User> Users { get; private set; } = [];
        public List<Permission> Permissions { get; private set; } = [];

        protected Role() { }

        protected Role(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedDate = DateTimeOffset.UtcNow;
            MakeModify();
        }

        public static Result<Role> Create(Guid id, string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name)) return Result.Failure<Role>("Название роли не может быть пустым");
            if (string.IsNullOrWhiteSpace(description)) return Result.Failure<Role>("Описание роли не может быть пустым");

            return Result.Success(new Role(id, name, description));
        }

        public void SetName(string name) => Name = name;
        public void SetDescription(string description) => Description = description;

        public void AddPermission(Permission permission)
        {
            if (!Permissions.Any(x => x.Id == permission.Id))
                Permissions.Add(permission);
        }

        public void RemovePermission(Permission permission)
        {
            if (Permissions.Any(x => x.Id == permission.Id))
                Permissions.Remove(permission);
        }
    }
}
